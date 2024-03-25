using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Conesoft.Network_Connections;

public partial class Connections
{
    public static IEnumerable<Connection> All => GetConnections(TCP_TABLE_OWNER_PID_ALL);
    public static IEnumerable<Connection> Listening => GetConnections(TCP_TABLE_OWNER_PID_LISTENER);

    private static IEnumerable<Connection> GetConnections(int tblClass)
    {
        _ = GetExtendedTcpTable(IntPtr.Zero, out var size, tblClass: tblClass);

        using var table = new HGlobalMemory(size);

        if (0 == GetExtendedTcpTable(table.Pointer, out size, tblClass: tblClass))
        {
            for (var i = 0; i < table.Read<uint>(offset: 0); i++)
            {
                yield return new Connection(fromData: table.Read<GetExtendedTcpTableData>(offset: 1 * sizeof(uint), index: i));
            }
        }
    }

    const int AF_INET = 2; // IPv4
    const int TCP_TABLE_OWNER_PID_LISTENER = 3;
    const int TCP_TABLE_OWNER_PID_ALL = 5;

    [LibraryImport("iphlpapi.dll", SetLastError = true)]
    private static partial uint GetExtendedTcpTable(IntPtr pTcpTable, out int dwOutBufLen, [MarshalAs(UnmanagedType.Bool)] bool sort = true, int ipVersion = AF_INET, int tblClass = TCP_TABLE_OWNER_PID_LISTENER, uint reserved = 0);
}
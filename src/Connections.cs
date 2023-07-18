using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Conesoft.Network_Connections;

public class Connections
{
    public static IEnumerable<Connection> All
    {
        get
        {
            _ = GetExtendedTcpTable(IntPtr.Zero, out var size, tblClass: TCP_TABLE_OWNER_PID_ALL);

            using var table = new HGlobalMemory(size);

            if (0 == GetExtendedTcpTable(table.Pointer, out size, tblClass: TCP_TABLE_OWNER_PID_ALL))
            {
                for (var i = 0; i < table.Read<uint>(offset: 0); i++)
                {
                    yield return new Connection(fromData: table.Read<GetExtendedTcpTableData>(offset: 1 * sizeof(uint), index: i));
                }
            }
        }
    }

    public static IEnumerable<Connection> Listening
    {
        get
        {
            _ = GetExtendedTcpTable(IntPtr.Zero, out var size, tblClass: TCP_TABLE_OWNER_PID_LISTENER);

            using var table = new HGlobalMemory(size);

            if (0 == GetExtendedTcpTable(table.Pointer, out size, tblClass: TCP_TABLE_OWNER_PID_LISTENER))
            {
                for (var i = 0; i < table.Read<uint>(offset: 0); i++)
                {
                    yield return new Connection(fromData: table.Read<GetExtendedTcpTableData>(offset: 1 * sizeof(uint), index: i));
                }
            }
        }
    }

    const int AF_INET = 2; // IPv4
    const int TCP_TABLE_OWNER_PID_LISTENER = 3;
    const int TCP_TABLE_OWNER_PID_ALL = 5;

    [DllImport("iphlpapi.dll", SetLastError = true)]
    static extern uint GetExtendedTcpTable(IntPtr pTcpTable, out int dwOutBufLen, bool sort = true, int ipVersion = AF_INET, int tblClass = TCP_TABLE_OWNER_PID_LISTENER, uint reserved = 0);
}
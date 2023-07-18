using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Conesoft.Network_Connections;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
[StructLayout(LayoutKind.Sequential)]
public struct GetExtendedTcpTableData
{
    public readonly uint state;
    public readonly uint localAddress;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public readonly byte[] localPort;
    public readonly uint remoteAddress;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] public readonly byte[] remotePort;
    public readonly uint processId;
}
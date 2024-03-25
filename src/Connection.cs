using System.Buffers.Binary;
using System.Diagnostics;
using System.Net;

namespace Conesoft.Network_Connections;

public class Connection(GetExtendedTcpTableData fromData)
{
    public uint ProcessId => fromData.processId;
    public string ProcessName => Process.GetProcessById((int)ProcessId).ProcessName;

    public AddressWithPort Local => new(Address: new(fromData.localAddress), Port: BinaryPrimitives.ReadUInt16BigEndian(fromData.localPort));
    public AddressWithPort Remote => new(Address: new(fromData.remoteAddress), Port: BinaryPrimitives.ReadUInt16BigEndian(fromData.remotePort));

    public record AddressWithPort(IPAddress Address, ushort Port);
}
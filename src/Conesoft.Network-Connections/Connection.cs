using System.Buffers.Binary;
using System.Diagnostics;
using System.Net;

namespace Conesoft.Network_Connections
{
    public class Connection
    {
        public uint ProcessId { get; private set; }
        public string ProcessName => Process.GetProcessById((int)ProcessId).ProcessName;

        public record AddressWithPort(IPAddress Address, ushort Port);

        public AddressWithPort Local { get; private set; }
        public AddressWithPort Remote { get; private set; }

        public Connection(GetExtendedTcpTableData fromData)
        {
            ProcessId = fromData.processId;
            Local = new(Address: new(fromData.localAddress), Port: BinaryPrimitives.ReadUInt16BigEndian(fromData.localPort));
            Remote = new(Address: new(fromData.remoteAddress), Port: BinaryPrimitives.ReadUInt16BigEndian(fromData.remotePort));
        }

        public static IConnectionFromType Listening { get; } = new ListeningConnectionType();
    }
}

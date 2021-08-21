using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Conesoft.Network_Connections
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    class ListeningConnectionType : IConnectionFromType
    {
        public Connection? From(Process process) => Connections.Listening.FirstOrDefault(c => c.ProcessId == process.Id);
    }
}

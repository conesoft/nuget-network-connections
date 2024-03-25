using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Conesoft.Network_Connections;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ConnectionExtensionsForProcess
{
    public static IEnumerable<Connection> GetListeningPorts(this Process process) => Connections.Listening.Where(c => c.ProcessId == process.Id);
    public static IEnumerable<Connection> GetAllPorts(this Process process) => Connections.All.Where(c => c.ProcessId == process.Id);
}
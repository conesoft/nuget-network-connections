using System.ComponentModel;
using System.Diagnostics;

namespace Conesoft.Network_Connections
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConnectionFromType
    {
        public Connection? From(Process process);
    }
}

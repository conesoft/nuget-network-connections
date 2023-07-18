using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Conesoft.Network_Connections;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class HGlobalMemory : IDisposable
{
    public IntPtr Pointer { get; private set; }

    public HGlobalMemory(int size) => Pointer = Marshal.AllocHGlobal(size);

    public T? Read<T>(int offset = 0, int index = 0) => Marshal.PtrToStructure<T>(Pointer + offset + index * Marshal.SizeOf<T>());

    public void Dispose() => Marshal.FreeHGlobal(Pointer);
}

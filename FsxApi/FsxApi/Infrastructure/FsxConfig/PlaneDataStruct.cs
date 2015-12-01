namespace FsxApi.Infrastructure.FsxConfig
{
    using System.Runtime.InteropServices;

    // declaration of data structure - simconnect needs to know how to fill it/read it.
    // Sequential - position of field (in the struct) is connected to the position in memory
    // Pack - minimum size of the field (in bytes)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PlaneDataStruct
    {
        internal LocationStruct Location;
    };
}

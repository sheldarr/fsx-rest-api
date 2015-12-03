namespace FsxApi.Fsx.Config
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PlaneDataStruct
    {
        internal LocationStruct Location;
    };
}

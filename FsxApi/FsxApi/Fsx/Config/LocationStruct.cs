namespace FsxApi.Fsx.Config
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct LocationStruct
    {
        internal double Latitude;
        internal double Longitude;
    }
}

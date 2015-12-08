namespace FsxApi.Fsx
{
    enum FsxDataOffset
    {
        LatitudeHigh = 0x0564,
        LatitudeLow = 0x0560,
        LongitudeHigh = 0x056C,
        LongitudeLow = 0x0568,
        AircraftAltitudeIntegral = 0x0574,
        AircraftAltitudeFractional = 0x0570,
        TrueAirSpeed = 0x02B8,
        IndicatedAirSpeed = 0x02BC,
        VerticalSpeed = 0x02C8
    }
}

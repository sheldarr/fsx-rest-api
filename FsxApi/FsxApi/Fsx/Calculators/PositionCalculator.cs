namespace FsxApi.Fsx.Calculators
{
    using Model;

    public static class PositionCalculator
    {
        public static Position CalculatePosition(double latitudeLow, double latitudeHigh, double longitudeLow, double longitudeHigh, double altitudeIntegral, double altitudeFractional)
        {
            return new Position
            {
                Latitude = Calculate64BitNumber(latitudeLow, latitudeHigh) * 90.0 / 10001750.0,
                Longitude = Calculate64BitNumber(longitudeLow, longitudeHigh) * 360.0 / (65536.0 * 65536.0),
                Altitude = CalculateFloatingPointNumber(altitudeIntegral, altitudeFractional)
            };
        }

        private static double Calculate64BitNumber(double lowBits, double highBits)
        {
            if (lowBits != 0)
            {
                lowBits = lowBits / (65536.0 * 65536.0);
            }
            if (highBits > 0)
            {
                return highBits + lowBits;
            }

            return highBits - lowBits;
        }

        private static double CalculateFloatingPointNumber(double altitudeIntegral, double altitudeFractional)
        {
            return altitudeIntegral + altitudeFractional / (65536.0 * 65536.0);
        }
    }
}
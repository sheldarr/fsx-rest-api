namespace FsxApi.Fsx.Calculators
{
    using Model;

    public static class PositionCalculator
    {
        public static Position CalculatePosition(double latitudeLow, double latitudeHigh, double longitudeLow, double longitudeHigh)
        {
            return new Position
            {
                Latitude = CalculateCoordinate(latitudeLow, latitudeHigh),
                Longitude = CalculateCoordinate(longitudeLow, longitudeHigh)
            };
        }

        private static double CalculateCoordinate(double coordinateLow, double coordinateHigh)
        {
            if (coordinateLow != 0)
            {
                coordinateLow = coordinateLow / (65536.0 * 65536.0);
            }
            if (coordinateHigh > 0)
            {
                return coordinateHigh + coordinateLow;
            }

            return coordinateHigh - coordinateLow;
        }
    }
}
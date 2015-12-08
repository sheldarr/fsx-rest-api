namespace FsxApi.Fsx.Calculators
{
    using Model;

    public static class SpeedCalculator
    {
        public static Speed CalculateSpeed(double trueAirSpeed, double indicatedAirSpeed, double verticalSpeed)
        {
            return new Speed
            {
                TrueAirSpeed = trueAirSpeed / 128,
                IndicatedAirSpeed = indicatedAirSpeed / 128,
                VerticalSpeed = verticalSpeed / 256
            };
        }
    }
}
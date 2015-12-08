namespace FsxApi.Fsx
{
    using System;
    using Calculators;
    using FsuipcSdk;
    using Model;

    public class FsxDataRepository
    {
        private readonly Fsuipc _fsuipc;

        public FsxDataRepository(Fsuipc fsuipc)
        {
            _fsuipc = fsuipc;
        }

        public Position GetPlanePosition()
        {
            return FsxOperation(() =>
            {
                double latitudeHigh = GetFsxData(FsxDataOffset.LatitudeHigh);
                double latitudeLow = GetFsxData(FsxDataOffset.LatitudeLow);

                double longitudeHigh = GetFsxData(FsxDataOffset.LongitudeHigh); 
                double longitudeLow = GetFsxData(FsxDataOffset.LongitudeLow);

                double altitudeIntegral = GetFsxData(FsxDataOffset.AircraftAltitudeIntegral);
                double altitudeFractional = GetFsxData(FsxDataOffset.AircraftAltitudeFractional);

                return PositionCalculator.CalculatePosition(latitudeLow, latitudeHigh, longitudeLow, longitudeHigh, altitudeIntegral, altitudeFractional);
            });
        }

        public Speed GetPlaneSpeed()
        {
            return FsxOperation(() =>
            {
                var trueAirSpeed = GetFsxData(FsxDataOffset.TrueAirSpeed);
                var indicatedAirSpeed = GetFsxData(FsxDataOffset.IndicatedAirSpeed);
                var verticalSpeed = GetFsxData(FsxDataOffset.VerticalSpeed);

                return SpeedCalculator.CalculateSpeed(trueAirSpeed, indicatedAirSpeed, verticalSpeed);
            });
        }

        private TResult FsxOperation<TResult>(Func<TResult> action)
        {
            _fsuipc.FSUIPC_Initialization();

            var errorCode = 0;

            _fsuipc.FSUIPC_Open(Fsuipc.SIM_FSX, ref errorCode);

            var result = action();
             
            _fsuipc.FSUIPC_Close();

            return result;
        }

        private int GetFsxData(FsxDataOffset fsxDataOffset, int dataSizeInBytes = 4)
        {
            var token = -1;
            var result = 0;

            _fsuipc.FSUIPC_Read((int)fsxDataOffset, dataSizeInBytes, ref token, ref result);
            _fsuipc.FSUIPC_Process(ref result);
            _fsuipc.FSUIPC_Get(ref token, ref result);

            return result;
        }
    }
}

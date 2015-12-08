namespace FsxApi.Fsx
{
    using System;
    using FsuipcSdk;
    using Model;

    public class FsxDataRepository
    {
        private const int AnyFsVersion = 0;
        private const int FsSpeedMultiplier = 128;

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

                return PositionCalculator.CalculatePosition(latitudeLow, latitudeHigh, longitudeLow, longitudeHigh);
            });
        }

        public Speed GetPlaneSpeed()
        {
            return FsxOperation(() => new Speed
            {
                TrueAirSpeed = GetFsxData(FsxDataOffset.TrueAirSpeed) / FsSpeedMultiplier,
                IndicatedAirSpeed = GetFsxData(FsxDataOffset.IndicatedAirSpeed) / FsSpeedMultiplier,
                VerticalSpeed = GetFsxData(FsxDataOffset.VerticalSpeed) / FsSpeedMultiplier
            });
        }

        private TResult FsxOperation<TResult>(Func<TResult> action)
        {
            _fsuipc.FSUIPC_Initialization();

            var errorCode = 0;

            _fsuipc.FSUIPC_Open(AnyFsVersion, ref errorCode);

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

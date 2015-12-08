namespace FsxApi.Fsx
{
    using System;
    using FsuipcSdk;
    using Model;

    public class FsxDataRepository
    {
        private const int AnyFsVersion = 0;

        private readonly Fsuipc _fsuipc;

        public FsxDataRepository(Fsuipc fsuipc)
        {
            _fsuipc = fsuipc;
        }

        public Position GetPlanePosition()
        {
            return FsxOperation(() =>
            {
                double latitudeHigh = DownloadFsxData(FsxDataOffset.LatitudeHigh);
                double latitudeLow = DownloadFsxData(FsxDataOffset.LatitudeLow);

                double longitudeHigh = DownloadFsxData(FsxDataOffset.LongitudeHigh); 
                double longitudeLow = DownloadFsxData(FsxDataOffset.LongitudeLow);

                return PositionCalculator.CalculatePosition(latitudeLow, latitudeHigh, longitudeLow, longitudeHigh);
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

        private int DownloadFsxData(FsxDataOffset offset, int dataSizeInBytes = 4)
        {
            var token = -1;
            var result = 0;

            _fsuipc.FSUIPC_Read((int)offset, dataSizeInBytes, ref token, ref result);
            _fsuipc.FSUIPC_Process(ref result);
            _fsuipc.FSUIPC_Get(ref token, ref result);

            return result;
        }
    }
}

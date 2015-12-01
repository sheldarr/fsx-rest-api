namespace FsxApi
{
    using Infrastructure;
    using Interfaces;
    using Model;

    public class Fsx
    {
        private readonly FsxCommunicator _fsxCommunicator;

        public Fsx(ILogger logger)
        {
            _fsxCommunicator = new FsxCommunicator(logger);
        }

        public PlaneData GetPlaneData()
        {
            var planeData = _fsxCommunicator.GetPlaneData();

            return planeData;
        }
    }
}

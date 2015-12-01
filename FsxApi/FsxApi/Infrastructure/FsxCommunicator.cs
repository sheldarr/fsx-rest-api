namespace FsxApi.Infrastructure
{
    using System;
    using FsxConfig;
    using FsxConfig.Enums;
    using Microsoft.FlightSimulator.SimConnect;
    using Model;

    public class FsxCommunicator
    {
        // SimConnect object
        private readonly SimConnect _simConnect;
        private bool _receivedMessage;
        private PlaneData _planeData;

        public FsxCommunicator()
        {
            Console.WriteLine("FSX: Connecting");

            try
            {
                _simConnect = FsxFactory.GetSimConnectObject(this);
            }
            catch(Exception)
            {
                Console.WriteLine("FSX: Connection failure");
                throw;
            }

            Console.WriteLine("FSX: Connection estabilished");
        }

        public PlaneData GetPlaneData()
        {
            _simConnect.RequestDataOnSimObjectType(DataRequest.FromBrowser, Definition.Plane, 0, SIMCONNECT_SIMOBJECT_TYPE.USER);
            
            // ReceiveMessage must be called to trigger the events. 
            do
            {
                try
                {
                    _simConnect.ReceiveMessage();
                }
                catch (Exception)
                {
                    return null;
                }
            } while (!_receivedMessage);

            _receivedMessage = false;

            return _planeData;
        }

        internal void Fsx_ReceiveDataEventHandler(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA_BYTYPE fsxData)
        {
            switch ((DataRequest)fsxData.dwRequestID)
            {
                case DataRequest.FromBrowser:
                    var userPlaneData = (PlaneDataStruct)fsxData.dwData[0];

                    _planeData = new PlaneData
                    {
                        Location = new Location
                        {
                            Latitude = userPlaneData.Location.Latitude,
                            Longitude = userPlaneData.Location.Longitude
                        }
                    };

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _receivedMessage = true;
        }

        internal void Fsx_UserClosedFsxEventHandler(SimConnect sender, SIMCONNECT_RECV data)
        {
            _simConnect.Dispose();
            Console.WriteLine("FSX: Connection closed by user");
        }

        internal void Fsx_ExceptionEventHandler(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            Console.WriteLine("FSX: Error during connection");
        }
    }
}

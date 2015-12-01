namespace FsxApi.Infrastructure
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using FsxConfig;
    using FsxConfig.Enums;
    using Microsoft.FlightSimulator.SimConnect;

    internal static class FsxFactory
    {
        internal static SimConnect GetSimConnectObject(FsxCommunicator communicator)
        {
            try
            {
                var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;

                // the constructor
                var simconnect = new SimConnect("User Requests", mainWindowHandle, Constants.WmUserSimconnect, null, 0);

                InitializeSimConnect(simconnect, communicator);

                return simconnect;

            }
            catch (COMException)
            {
            }

            return null;
        }

        // Set up all the SimConnect data definitions and event handlers
        private static void InitializeSimConnect(SimConnect simconnect, FsxCommunicator fsxCommunicator)
        {
            try
            {
                // listen to quit (from FSX) events
                simconnect.OnRecvQuit += fsxCommunicator.Fsx_UserClosedFsxEventHandler;

                // listen to exceptions
                simconnect.OnRecvException += fsxCommunicator.Fsx_ExceptionEventHandler;

                // getting data
                // define a data structure for plane informations
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Latitude", "degrees", 
                    SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Longitude", "degrees", 
                    SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Altitude", "meters", 
                    SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                // registering plane structure in simconnect
                simconnect.RegisterDataDefineStruct<PlaneDataStruct>(Definition.Plane);

                // catch a simobject after data request
                simconnect.OnRecvSimobjectDataBytype += fsxCommunicator.Fsx_ReceiveDataEventHandler;
            }
            catch (COMException)
            {
            }
        } 
    }
}

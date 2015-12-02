namespace FsxApi.Fsx
{
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Config;
    using Config.Enums;
    using Microsoft.FlightSimulator.SimConnect;

    public static class SimConnectFactory
    {
        private const int WmUserSimconnect = 0x0402;

        public static SimConnect GetSimConnectObject(FsxConnection connection)
        {
            var mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;

            // the constructor
            var simconnect = new SimConnect("User Requests", mainWindowHandle, WmUserSimconnect, null, 0);

            InitializeSimConnect(simconnect, connection);

            return simconnect;
        }

        // Set up all the SimConnect data definitions and event handlers
        private static void InitializeSimConnect(SimConnect simconnect, FsxConnection fscConnection)
        {
            try
            {
                // listen to quit (from FSX) events
                simconnect.OnRecvQuit += fscConnection.Fsx_UserClosedFsxEventHandler;

                // listen to exceptions
                simconnect.OnRecvException += fscConnection.Fsx_ExceptionEventHandler;

                // getting data
                // define a data structure for plane informations
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Latitude", "degrees", 
                    SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(Definition.Plane, "Plane Longitude", "degrees", 
                    SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);

                // registering plane structure in simconnect
                simconnect.RegisterDataDefineStruct<PlaneDataStruct>(Definition.Plane);

                // catch a simobject after data request
                simconnect.OnRecvSimobjectDataBytype += fscConnection.Fsx_ReceiveDataEventHandler;
            }
            catch (COMException)
            {
            }
        } 
    }
}

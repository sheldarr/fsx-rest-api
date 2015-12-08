namespace FsxApi.Api
{
    using System;
    using Fsx;
    using Model;
    using Nancy;

    public class PlaneApiModule : NancyModule
    {
        private readonly FsxDataRepository _fsxDataRepository;

        public PlaneApiModule(FsxDataRepository fsxDataRepository)
        {
            _fsxDataRepository = fsxDataRepository;

            Get["/api/plane"] = parameters =>
            {
                Console.WriteLine("GET /api/plane");

                var plane = new Plane
                {
                    Position = _fsxDataRepository.GetPlanePosition(),
                    Speed = _fsxDataRepository.GetPlaneSpeed()
                };

                return Response.AsJson(plane);
            };

            Get["/api/plane/position"] = parameters =>
            {
                Console.WriteLine("GET /api/plane/position");

                var planePosition = _fsxDataRepository.GetPlanePosition();

                return Response.AsJson(planePosition);
            };

            Get["/api/plane/speed"] = parameters =>
            {
                Console.WriteLine("GET /api/plane/speed");

                var planeSpeed = _fsxDataRepository.GetPlaneSpeed();

                return Response.AsJson(planeSpeed);
            };
        }
    }
}
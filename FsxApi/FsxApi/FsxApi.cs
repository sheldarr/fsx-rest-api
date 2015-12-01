namespace FsxApi
{
    using Nancy;

    class FsxApi : NancyModule
    {
        public FsxApi() : base("/api/fsx")
        {
            Get["/"] = _ =>
            {
                var response = new
                {
                    Code = 200,
                    Message = "OK"
                };

                return Response.AsJson(response);
            };
        }
    }
}

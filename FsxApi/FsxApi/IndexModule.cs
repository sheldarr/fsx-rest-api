namespace FsxApi
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                var response = new
                {
                    Code = 200,
                    Message = "OK"
                };

                return Response.AsJson(response);
            };

            Get["/api/fsx"] = parameters =>
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
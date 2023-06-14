using System.Net;

namespace EMS.Infrastructure.ResultModels
{
    public class BaseResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}

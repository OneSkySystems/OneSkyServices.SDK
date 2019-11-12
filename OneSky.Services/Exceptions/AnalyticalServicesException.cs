using System;
using System.Net;
using Newtonsoft.Json;

namespace OneSky.Services.Exceptions
{
    public class AnalyticalServicesException: Exception
    {
        public int ErrorId { get; set; }
        public override string Message { get; }
        public HttpStatusCode Status { get; set; }

        public AnalyticalServicesException(int id, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ErrorId = id;
            Message = message;
            Status = statusCode;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

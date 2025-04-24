using System.Net;

namespace iDeviceLogAnalyzer.AIChatProvider.Exceptions
{
    public class ChatException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ChatException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
    public class AIChatTooManyRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public AIChatTooManyRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}

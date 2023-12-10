namespace GameHub.Web.Responses
{
    public class ApiResponse<T>
    {
        public T Payload { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public ApiResponse()
        {
            Payload = default;
            ErrorMessage = string.Empty;
            ErrorCode = 0;
        }

        public ApiResponse(T payload)
        {
            Payload = payload;
            ErrorMessage = string.Empty;
            ErrorCode = 0;
        }

        public ApiResponse(string errorMessage) : this(errorMessage, 601)
        {
        }

        public ApiResponse(string errorMessage, int errorCode)
        {
            Payload = default;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}

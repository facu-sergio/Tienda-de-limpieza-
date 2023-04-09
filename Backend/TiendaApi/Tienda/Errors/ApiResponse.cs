namespace Tienda.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statuscode, string mensaje = null)
        {
            StatusCode = statuscode;
            Message = mensaje ?? GetDefaultMessageForStatusCode(statuscode);

        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a bad Request",
                401 => "You are not autjorized",
                404 => "It was not resource found",
                500 => "Errors in the path",
                _=> null,
            };
        }
    }
}

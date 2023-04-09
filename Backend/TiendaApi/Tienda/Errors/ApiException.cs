namespace Tienda.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statuscode, string mensaje = null, string details = null) : base(statuscode, mensaje)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}

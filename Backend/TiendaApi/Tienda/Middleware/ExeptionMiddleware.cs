using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tienda.Errors;

namespace Tienda.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExeptionMiddleware(RequestDelegate next, ILogger<ExeptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // si no existe error la solicitud pasa a la siguiente etapa
                await _next(context);
            }
            catch (Exception ex)
            {
                //capturo la excepcion y se la paso al logger
                _logger.LogError(ex, ex.Message);
                //genero una respuesta propia para enviar al cliente
                context.Response.ContentType = "aplication/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;// configuro el codigo de esta para que sea un  error interno del server 500

                //creo una respuesta con mas detalles si estoy en desarrollo y menos si estoy en produccion
                var response = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json =  JsonSerializer.Serialize(response,options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ExemploAuth.Web.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var detalhesExcecao = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            var excecao = detalhesExcecao?.Error;
            var path = detalhesExcecao?.Path;

            if (excecao != null)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var mensagem = includeDetails ? $"Occorreu erro em [{path}]: " + excecao.Message : $"Occorreu erro em [{path}]";
                var detalhes = includeDetails ? excecao.ToString() : null;

                var erroResponse = new ValidationProblemDetails
                {
                    Title = mensagem,
                    Detail = detalhes,
                    Status = StatusCodes.Status500InternalServerError
                };

                //Serializa objeto ValidationProblemDetails na resposta do HTTP
                var stream = httpContext.Response.Body;

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(stream, erroResponse, options);
            }
        }
    }
}
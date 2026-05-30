using Inventario.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Inventario.Web.Middleware;

public static class GlobalExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionFeature == null) return;

                var exception = exceptionFeature.Error;
                
                var statusCode = exception switch
                {
                    DomainException => (int)HttpStatusCode.UnprocessableEntity,
                    FluentValidation.ValidationException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/problem+json";

                var problem = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = statusCode,
                    Title = exception switch
                    {
                        DomainException => "Regla de Negocio Violada",
                        FluentValidation.ValidationException => "Error de Validación",
                        _ => "Error Interno del Servidor"
                    },
                    Detail = exception.Message,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(problem);
            });
        });
        return app;
    }
}

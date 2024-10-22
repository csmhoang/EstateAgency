using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Net;

namespace Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            CustomizeException exception => exception.StatusCode,
                            BadRequestException => (int)HttpStatusCode.BadRequest,
                            NotFoundException => (int)HttpStatusCode.NotFound,
                            _ => (int)HttpStatusCode.InternalServerError
                        };

                        var res = new Response()
                        {
                            Success = false,
                            StatusCode = context.Response.StatusCode,
                        };

                        res.Errors.Add($"{contextFeature.Error.Message} " +
                            $"{contextFeature.Error.InnerException?.Message ?? ""}");

                        logger.LogError($"{contextFeature.Error.Message} " +
                            $"{contextFeature.Error.InnerException?.Message ?? ""}");

                        await context.Response.WriteAsync(res.ToString());
                    }
                });
            });
        }
    }
}

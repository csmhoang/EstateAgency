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
                        int statusCode = (int)HttpStatusCode.InternalServerError;

                        switch (contextFeature.Error)
                        {
                            case CustomizeException exception:
                                statusCode = exception.StatusCode;
                                break;
                            case BadRequestException exception:
                                statusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case NotFoundException exception:
                                statusCode = (int)HttpStatusCode.NotFound;
                                break;
                        }

                        context.Response.StatusCode = statusCode;
                        var res = new Response()
                        {
                            Success = false,
                            StatusCode = statusCode,
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

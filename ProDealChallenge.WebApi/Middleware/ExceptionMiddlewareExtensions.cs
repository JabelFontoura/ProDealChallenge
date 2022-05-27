using Microsoft.AspNetCore.Diagnostics;
using ProDeal.Application.Dtos;
using ProDeal.Application.Exceptions;
using System.Net;

namespace ProDealChallenge.WebApi.Middleware
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature?.Error.GetType() == typeof(BusinessException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }

                    context.Response.ContentType = "application/json";

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetailDto()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}

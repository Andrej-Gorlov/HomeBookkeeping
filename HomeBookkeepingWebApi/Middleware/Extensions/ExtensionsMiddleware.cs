using HomeBookkeepingWebApi.Middleware.CustomException;

namespace HomeBookkeepingWebApi.Middleware.Extensions
{
    public static class ExtensionsMiddleware
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}

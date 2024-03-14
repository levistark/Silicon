namespace Silicon.Helpers.Middlewares;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseUserSessionValidation(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserSessionValidationMiddleware>();
    }
}

using Stokify.Middlewares;

namespace Stokify.Extensions;

public static partial class Inject
{
    extension(WebApplication app)
    {
        public WebApplication AddApp()
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHealthChecks("/v1/health");

            app.MapControllers();

            return app;
        }
    }
}
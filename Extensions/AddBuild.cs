using Microsoft.EntityFrameworkCore;
using Stokify.Data.Context;
using Stokify.Services;

namespace Stokify.Extensions;

public static partial class Inject
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddBuild()
        {
            JwtToken.Key = builder.Configuration.GetValue<string>("JwtToken") ?? throw new NullReferenceException("");
            var conn = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conn));

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            builder.Services.AddHealthChecks();

            builder.Services.AddControllers();

            return builder;
        }
    }
}
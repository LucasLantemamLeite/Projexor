using Microsoft.EntityFrameworkCore;
using Stokify.Data.Context;

namespace Stokify.Extensions;

public static partial class Inject
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddBuild()
        {
            var conn = builder.Configuration.GetConnectionString("Default");
            
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conn));
            
            return builder;
        }
    }
}
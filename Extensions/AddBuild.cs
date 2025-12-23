using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stokify.Data.Context;
using Stokify.Services;

namespace Stokify.Extensions;

public static partial class Inject
{
    extension(WebApplicationBuilder builder)
    {
        public WebApplicationBuilder AddBuild()
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => x.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtToken.Key)),
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            });

            builder.Services.AddAuthorization();

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
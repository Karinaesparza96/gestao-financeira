using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations
{
    public static class DbContextConfiguration
    {
        public static WebApplicationBuilder AddDbContextConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
                });
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });
            }
            return builder;
        }
    }
}

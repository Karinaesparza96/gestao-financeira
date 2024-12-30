using Api.Mapper;

namespace Api.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
            return builder;
        }
    }
}

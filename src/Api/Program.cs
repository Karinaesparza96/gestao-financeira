using Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContextConfiguration()
       .AddResolveDependencie()
       .AddIdentityConfiguration()
       .AddSwaggerConfiguration()
       .AddApiConfiguration()
       .AddJwtBearerAutentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

DbMigrationHelpers.EnsureSeedData(app).Wait();

app.Run();

using CatalogApi.Models;
using CatalogApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CatalogDbSettings>(
    builder.Configuration.GetSection("CatalogDbSettings"));

builder.Services.AddSingleton<CatalogsService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
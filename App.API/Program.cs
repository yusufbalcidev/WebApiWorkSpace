using App.Repositories.Extensions;
using App.Services.Extensions;
using App.Services.Extensions.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options=>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // ModelState doğrulama hatalarını otomatik olarak 400 Bad Request olarak döndürmeyi devre dışı bırakır
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Scalar OpenAPI'yi buradan kullanıyor

builder.Services.AddRepositories(builder.Configuration)
    .AddServices(builder.Configuration);


var app = builder.Build();
app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger JSON

    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
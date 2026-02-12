using System.Reflection;
using App.Services.Categories;
using App.Services.ExceptionHandlers;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Extensions
{
        public static class ServiceExtensions
        {
            public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
            {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(System.Reflection.Assembly.GetExecutingAssembly());
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzk4MjQzMjAwIiwiaWF0IjoiMTc2Njc4NzA5OSIsImFjY291bnRfaWQiOiIwMTliNWNiNzNjMmE3NGIwODFlZGQzY2I1NmMzYTQzMCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa2RlYmZhajI1cDV3OGUxMnJtYWc0ZGFwIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.g-UExFSz7Q0Xbl6cCCDPHz4vmK_iE6XC5SK9mkNO3-LpPP5FzfytzRnZ57BiagM1Op3d11datHmUkRjW4G-hSlcvVdeDP2iPTx5xNVWJUd4Kj2k24FWs4AZpojHK3hcu10KdZWPOwzVs3pbVYGBlF9ZaeGdISzKxlkDxfNwFNF1lKgSqlyVwNaYXO9qiR7ZZ-FbMV9wV9kU6pDElmAaN_CUpr6bh89adWZISdyYO2nvnnjW1z6e40yUmw9VJBCcRvrBsmIfCnJLBX6l-mYjOg7rbaNQwuvfuFRbYlWaWdPxfDkD-3qZqeU-QqEphWU2XrbiXSH1DHkAyXcvm7w";
            });
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
            }

        }
 
}

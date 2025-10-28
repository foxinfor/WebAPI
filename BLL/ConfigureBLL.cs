using BLL.DTO;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.Services;
using BLL.Validators;
using DAL;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ConfigureBLL
    {
        public static void ConfigureBll(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthorConfigMapper>();
                cfg.AddProfile<BookConfigMapper>();
            });


            services.AddScoped<IValidator<BookDTO>, BookValidator>();
            services.AddScoped<IValidator<AuthorDTO>, AuthorValidator>();

            services.AddTransient<IAuthorService,AuthorService>();
            services.AddTransient<IBookService, BookService>();
        }
    }
}

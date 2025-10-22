using DAL.Interfaces;
using DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ConfigureDAL
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
        }
    }
}

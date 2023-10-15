using MepasTask.Abstract;
using MepasTask.Repositories;

namespace MepasTask.Utils
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddScoped<IExcelWriteRepository, ExcelWriteRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUtil, Util>();
        }
    }
}

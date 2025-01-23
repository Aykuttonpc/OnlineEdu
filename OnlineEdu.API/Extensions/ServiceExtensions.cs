using OnlineEdu.Businnes.Abstract;
using OnlineEdu.Businnes.Concrete;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Concrete;
using OnlineEdu.DataAcces.Repositories;

namespace OnlineEdu.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services) {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseCategoryService, CourseCategoryManager>();
        }
}
 }


using OnlineEdu.Businnes.Abstract;
using OnlineEdu.Businnes.Concrete;
using OnlineEdu.Businnes.Configurations;
using OnlineEdu.Businness.Abstract;
using OnlineEdu.Businness.Concrete;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Concrete;
using OnlineEdu.DataAcces.Repositories;

namespace OnlineEdu.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services,IConfiguration configuration) {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseCategoryService, CourseCategoryManager>();

            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<ICourseRegisterRepository, CourseRegisterRepository>();
            services.AddScoped<ICourseRegisterService, CourseRegisterManager>();

            services.AddScoped<IBlogCategoryRepository,BlogCategoryRepository>();
            services.AddScoped<IBlogCategoryService, BlogCategoryManager>();
            services.AddScoped<IUserService,UserService>();

            services.Configure<JwtTokenOptions>(configuration.GetSection("TokenOptions"));

            services.AddScoped<IJwtService, JwtService>();
        }
}
 }

using Microsoft.EntityFrameworkCore;
using OnlineEdu.API.Extensions;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.Businnes.Concrete;
using OnlineEdu.DataAcces.Abstract;
using OnlineEdu.DataAcces.Concrete;
using OnlineEdu.DataAcces.Context;
using OnlineEdu.DataAcces.Repositories;
using System.Reflection;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddServiceExtensions(builder.Configuration);

        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
      



        builder.Services.AddDbContext<OnlineEduContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            options.UseLazyLoadingProxies();

        });

        builder.Services.AddControllers().AddJsonOptions(options =>
             options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
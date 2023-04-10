using System.Reflection;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
//using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Domain.Wrapper;
using Domain.Interfaces;
using DataAccess;
using DataAccess.Models;

namespace BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<pharmacy199Context>(
                optionsAction: options => options.UseSqlServer(
                    connectionString: "Server = lab116-p; Database = apteka; User Id = sa; Password = 12345; TrustServerCertificate = true;"));
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name:"v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "��������-������� ���",
                    Description = "��������-������ ��� �� ������� �������� ���������",
                    Contact = new OpenApiContact
                    {
                        Name = "������ ��������",
                        Url = new Uri("https://example.cfile")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "������ ��������",
                        Url = new Uri("https://example.com/license")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

            builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            builder.Services.AddScoped<IUserService, UserService>();


            // Add services to the container.

            builder.Services.AddControllers();
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
}

//Server = lab116 - p; Database = pharmacy199; User Id = sa; Password = 12345; TrustServerCertificate = true;
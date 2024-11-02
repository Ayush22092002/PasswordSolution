using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PasswordDll.Models;
using PasswordDll.Services;
using PasswordWebApi.Services;

namespace PasswordWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Register services in Program.cs
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<UserDataServices>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // Register PasswordHasher


            builder.Services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection"));
            });

            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cors", setUp =>
                {
                    setUp.AllowAnyHeader();
                    setUp.AllowAnyMethod();
                    setUp.AllowAnyOrigin();
                });
            });
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

            app.UseCors("cors");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace BugTicketingSystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
            );

            builder.Services.AddIdentityCore<AppUser>(options =>
            {

            }).AddEntityFrameworkStores<AppDbContext>();
            

            builder.Services.AddScoped<IProjectRepository, ProjectRepoistory>();







            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            //Directory.CreateDirectory(imageFolder);
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(imageFolder),
            //    RequestPath = "staticFile"
            //});
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

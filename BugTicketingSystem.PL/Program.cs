
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using BugTicketingSystem.BLL.Services.AuthService;
using BugTicketingSystem.BLL.Services.ProjectService;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

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

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               var secretKey = builder.Configuration.GetValue<string>("JWT:secretKey");
               var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

               options.TokenValidationParameters = new()
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   IssuerSigningKey = Key,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.FromDays(30)
               };
           });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Constant.Polices.ForManager, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Manager");
                });
                options.AddPolicy(Constant.Polices.ForTester, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Tester");
                });
                options.AddPolicy(Constant.Polices.ForDeveloper, builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Developer");
                });
            });


            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<IProjectRepository, ProjectRepoistory>();
            builder.Services.AddScoped<IProjectService, ProjectService>();


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

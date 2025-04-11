
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using BugTicketingSystem.BLL.Services;
using BugTicketingSystem.BLL.Services.AuthService;
using BugTicketingSystem.BLL.Services.BugService;
using BugTicketingSystem.BLL.Services.ProjectService;
using BugTicketingSystem.DAL.Context;
using BugTicketingSystem.DAL.Entities;
using BugTicketingSystem.DAL.Repositories.AttachmentRepository;
using BugTicketingSystem.DAL.Repositories.BugRepository;
using BugTicketingSystem.DAL.Repositories.PorjectRepository;
using BugTicketingSystem.DAL.UnitOfWorks;
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

            builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();

            builder.Services.AddScoped<IBugRepository, BugRepository>();
            builder.Services.AddScoped<IBugService, BugService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "myImages");
            Directory.CreateDirectory(imageFolder);

            app.UseStaticFiles(new StaticFileOptions
            {
                // folder contain static files
                FileProvider = new PhysicalFileProvider(imageFolder),

                // route to map to static file
                RequestPath = "/api/static-files"
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

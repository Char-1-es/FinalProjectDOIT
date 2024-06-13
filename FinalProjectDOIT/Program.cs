using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;
using FinalProjectDOIT.Repos;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using FinalProjectDOIT.Data;

namespace FinalProjectDOIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<User>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddScoped<IRepository<Topic, int>, TopicRepository>();

            builder.Services.AddScoped<IRepository<Comment, int>, CommentRepository>();
            builder.Services.AddScoped<IUserInterface<User, string>, UserRepository>();

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TopicService>();
            builder.Services.AddScoped<CommentService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapIdentityApi<User>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

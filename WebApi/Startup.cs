using Domain.Entities;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add SQLite DbContext
            services.AddDbContext<BitcubeDevTaskDbContext>(options =>
                options.UseSqlite("Data Source=../BitcubeDeveloperTask.Infrastructure/Data/db.sqlite"));

            // Configure Identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true; // Adjust according to your needs
            })
            .AddEntityFrameworkStores<BitcubeDevTaskDbContext>()
            .AddDefaultTokenProviders();

            // Add CORS policy
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithOrigins("https://your-allowed-origin.com") // Specify allowed origins here
                       .AllowCredentials();
            }));

            // Add your repository interfaces and implementations for Dependency Injection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Add services to the container
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Optional: For detailed error pages in Development
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BitcubeDeveloperTask API V1"); // Update with your endpoint
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Handle exceptions gracefully in production
                app.UseHsts(); // HTTP Strict Transport Security
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // Add routing middleware

            app.UseAuthorization();

            // Map controllers to endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // This line maps attribute-routed controllers to the app's request pipeline
            });
        }
    }
}

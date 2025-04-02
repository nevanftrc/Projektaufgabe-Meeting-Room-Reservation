using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Serilog;
using System.Configuration;
using RoomRevAPI.Mapping;
using RoomRevAPI.Services;

namespace MongoDBServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // API portion

            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // Add MongoClient as a singleton
            builder.Services.AddSingleton<MongoClient>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MongoURL"); // Ensure this key exists in appsettings.json
                return new MongoClient(connectionString);
            });

            // Load configuration from appsettings.json
            var configurationport = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Use the current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Register MainContext as Scoped
            builder.Services.AddScoped<MainController>(sp =>
            {
                var client = sp.GetRequiredService<MongoClient>(); // Resolving the registered MongoClient
                var configuration = sp.GetRequiredService<IConfiguration>();
                var databaseName = configuration["DB"] ?? "RoomRevDB"; // Default to 'Skiservice' if not set
                var database = client.GetDatabase(databaseName);
                return new MainController(client, database, configuration);
            });

            // Configure Serilog logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() // Log to console
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7) // Log to file
                .Enrich.FromLogContext()
                .MinimumLevel.Information() // Adjust this level as needed
                .CreateLogger();

            // Add CORS policy configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins(configurationport["Port:Number"]) // Allow specific origin
                          .AllowAnyMethod() // Allow all HTTP methods (GET, POST, etc.)
                          .AllowAnyHeader() // Allow all headers
                          .AllowCredentials(); // Allow credentials if needed
                });
            });

            // Replace the default logger with Serilog
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();

            // Register services
            builder.Services.AddControllers();
            //builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MapProfile));
            builder.Services.AddScoped<IMeetingRoomsService, MeetingRoomsService>();
            builder.Services.AddScoped<IReservationsService, ReservationsService>();
            builder.Services.AddScoped<IReserversService, ReserversService>();

            var app = builder.Build();

            // Swagger UI for development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Add custom middleware (currently commented out)
            // app.UseMiddleware<UserAuthMiddleware>(); // Commented out as requested

            app.UseCors("AllowSpecificOrigins"); // Apply CORS policy
            app.UseAuthorization();

            app.MapControllers(); // Map controllers

            app.Run();
        }
    }
}

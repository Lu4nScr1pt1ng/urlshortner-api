using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using urlshortner.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:5173")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                                                  .AllowCredentials();
                          });
});


builder.Services.AddResponseCompression(opt =>
{
    opt.Providers.Add<GzipCompressionProvider>();
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
});



builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SECRET"));

// Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Connection with DB
// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
// builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

var connectionString2 = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPass};";
var connectionString = $"Server={dbHost},1433;User ID=sa;Password={dbPass};Trusted_Connection=False; TrustServerCertificate=True;";

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = serviceScope.ServiceProvider.GetRequiredService<DataContext>().Database;

    logger.LogInformation("Migrating database...");

    while (!db.CanConnect())
    {
        logger.LogInformation("Database not ready yet; waiting...");
        Thread.Sleep(1000);
    }

    try
    {
        serviceScope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Url Shortner v1");
    });
}

app.UseHttpsRedirection();


app.UseCors(MyAllowSpecificOrigins);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Contact_Management.Application;
using Contact_Management.Persistence;
using Contact_Management.Persistence.Extensions;
using Contact_MangeMent.API;
using Contact_MangeMent.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration));

// Add services to the container.

try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new ApplicationModule());
        // containerBuilder.RegisterModule(new InfrastructureModule());
        containerBuilder.RegisterModule(new PersistenceModule(connectionString,
            migrationAssembly));
        containerBuilder.RegisterModule(new ApiModule());
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString,
  (x) => x.MigrationsAssembly(migrationAssembly)));
    builder.Services.AddIdentity();

    builder.Services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
            };
        });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.AddSwagger();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    Log.Information("Application Starting...");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact Management");
            c.DocumentTitle = "Contact Management API";
            c.DocExpansion(DocExpansion.None);
        });
    }

    app.UseCors();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

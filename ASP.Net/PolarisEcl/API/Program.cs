using System.Text;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PolarisEcl.Application.Common.Security;
using PolarisEcl.Application.DependencyInjections;
using PolarisEcl.Infrastructure.DependencyInjections;
using PolarisEcl.Infrastructure.Errors;
using Scalar.AspNetCore;
using PolarisEcl.Filters;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Infrastructure.Data;
using FluentValidation;
using PolarisEcl.Application.Common.Validators;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    Log.Information("Starting web application.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, loggerConfiguration) => loggerConfiguration
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services));

    builder.Services.AddCors(options =>
        options.AddPolicy("AllowAll", policy =>
        {
            policy.WithOrigins("http://localhost:5173");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        })
    );

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    // configure jwt authentication middleware.
    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false; // for prod this should be true.
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!)),
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero // token to expire when it is suppose to.
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT Auth Failed: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

    builder.Services.AddAuthorizationBuilder();
    builder.Services.AddScoped<ActiveUserFilter>();
    builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
    builder.Services
    .AddAllApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ResponseWrapperFilter>();
    });


    builder.Services.AddOpenApi(options =>
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            var securitySchemeName = "Bearer";

            var bearerScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // 💡 Must be lowercase "bearer"
                BearerFormat = "JWT",
                Description = "Enter your valid JWT access token to authenticate requests."
            };

            var components = document.Components ??= new OpenApiComponents();

            components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
            components.SecuritySchemes[securitySchemeName] = bearerScheme;

            document.Security ??= new List<OpenApiSecurityRequirement>();

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference(securitySchemeName, document),
                    new List<string>()
                }
            };

            document.Security.Add(securityRequirement);

            return Task.CompletedTask;
        });

        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer", context.Document),
                    new List<string>()
                }
            };

            operation.Security.Add(securityRequirement);
            return Task.CompletedTask;
        });
    });

    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseExceptionHandler();
    app.UseCors("AllowAll");
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.WithTheme(ScalarTheme.Purple)
            .AddPreferredSecuritySchemes("Bearer");
        });

        app.MapGet("/", () => Results.Redirect("/scalar"));
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<IAppDbContext>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher>();

            await DbInitializer.SeedAdminUserAsync(context, passwordHasher);
        }
        catch (Exception err)
        {
            Log.Error(err, "An error occurred while seeding the database.");
        }
    }
    app.Run();
}
catch (HostAbortedException)
{
    // Allow EF Core CLI to stop the host safely.
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

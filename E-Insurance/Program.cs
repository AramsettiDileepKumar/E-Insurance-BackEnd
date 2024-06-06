using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IRegistrationBL,RegistrationServiceBL>();
builder.Services.AddScoped<IRegistrationRepo, RegistrationServiceRepo>();
builder.Services.AddScoped<IPolicyBL, PolicyServiceBL>();
builder.Services.AddScoped<IPolicyRepo, PolicyServiceRepo>();
builder.Services.AddScoped<IPurchaseBL,PurchaseServiceBL>();
builder.Services.AddScoped<IPurchaseRepo,PurchaseServiceRepo>();
//---JWT
// Get the secret key from the configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"]);
// Add authentication services with JWT Bearer token validation to the service collection
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // Add JWT Bearer authentication options
    .AddJwtBearer(options =>
    {
        // Configure token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Specify whether the server should validate the signing key
            ValidateIssuerSigningKey = true,

            // Set the signing key to verify the JWT signature
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Specify whether to validate the issuer of the token (usually set to false for development)
            ValidateIssuer = false,// true, // imade changes 

            // Specify whether to validate the audience of the token (usually set to false for development)
            ValidateAudience = false,// true // i made changes
        };
    });
/////////////////////////
builder.Services.AddSwaggerGen(c =>
{
    // Define Swagger document metadata (title and version)
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Insurance", Version = "v1" });

    // Configure JWT authentication for Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // Describe how to pass the token
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization", // The name of the header containing the JWT token
        In = ParameterLocation.Header, // Location of the JWT token in the request headers
        Type = SecuritySchemeType.Http, // Specifies the type of security scheme (HTTP in this case)
        Scheme = "bearer", // The authentication scheme to be used (in this case, "bearer")
        BearerFormat = "JWT" // The format of the JWT token
    });

    // Specify security requirements for Swagger endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            // Define a reference to the security scheme defined above
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // The ID of the security scheme (defined in AddSecurityDefinition)
                }
            },
            new string[] {} // Specify the required scopes (in this case, none)
        }
    });
});
/////////Nlog
var logpath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logpath);
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddSingleton<NLog.ILogger>(NLog.LogManager.GetCurrentClassLogger());


/////////////////////RabbitMQ
//builder.Services.AddMassTransit(x =>
//{
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
//    {
//        config.UseHealthCheck(provider);
//        config.Host(new Uri("rabbitmq://localhost"), h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });
//    }));
//});
//builder.Services.AddMassTransitHostedService();
//////
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

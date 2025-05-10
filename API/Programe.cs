using System;
using Infrastructure.Extensions;
using Application.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using API;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://example.com", "http://localhost:4200") // Replace with your allowed origins
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Optional: Allow credentials if needed
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

//builder.Services.AddSwaggerGen();
/*
builder.Services.AddSwaggerGen(options =>
{
    // Update names of the document
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Angular Blog App API's",
        Version = "v1",
        Description = "🔥 This is a custom description for API 🔥"
    });

    // Update the names of the API
    options.SwaggerGeneratorOptions.DocumentFilters.Add(new LowerCaseDocumentFilter());

    // Configure the JWT authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using Bearer schema. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Add JWT Bearer security requirement
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                []
            }
        });
});
*/

/*
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });
*/

builder.Services.AddWebServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Angular Blog App API");
        options.DocumentTitle = "Angular Blog App API"; 
    });
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // Get the paths
        var path = swaggerDoc.Paths.ToDictionary(
            path => path.Key.ToLowerInvariant(),
            path => swaggerDoc.Paths[path.Key]);

        // Add the paths in swagger
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var pathItem in path)
        {
            swaggerDoc.Paths.Add(pathItem.Key, pathItem.Value);
        }
    }
}

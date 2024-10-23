using BookManagementBackend.Classes;
using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Services;
using BookManagementBackend.Domain.Services.External;
using BookManagementBackend.Infraestructure.Contexts;
using BookManagementBackend.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

#region swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DEVOLUWEB Backend", Version = "v1" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "O esquema usa o Bearer token. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securitySchema);

    OpenApiSecurityRequirement securityRequirement = new()
    {
        { securitySchema, new[] { "Bearer" } }
    };
    c.AddSecurityRequirement(securityRequirement);
});
#endregion

#region AppSettings Configuration
IConfigurationSection settingsSection = builder.Configuration.GetSection("Settings");
builder.Services.Configure<AppSettings>(settingsSection);
#endregion

#region Services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IGoogleAPIService, GoogleAPIService>();
builder.Services.AddScoped<IOpenLibraryService, OpenLibraryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailService, EmailService>();
#endregion

#region Repositories
builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksReturnRepository, BooksReturnRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
#endregion

#region Database
builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("LibraryContext") ?? "");
});
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEspecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

#region Token Configuration
AppSettings? appSettings = settingsSection.Get<AppSettings>();
byte[] jwtSecret = Encoding.ASCII.GetBytes(appSettings?.JWTSecret ?? "");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<LibraryContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowEspecificOrigin");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

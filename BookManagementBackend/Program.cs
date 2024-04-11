using BookManagementBackend.Domain.Interfaces.Repositories;
using BookManagementBackend.Domain.Interfaces.Services;
using BookManagementBackend.Domain.Interfaces.Services.External;
using BookManagementBackend.Domain.Services;
using BookManagementBackend.Domain.Services.External;
using BookManagementBackend.Infraestructure.Contexts;
using BookManagementBackend.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IGoogleAPIService, GoogleAPIService>();
builder.Services.AddScoped<IOpenLibraryService, OpenLibraryService>();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IBooksReturnRepository, BooksReturnRepository>();

builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("LibraryContext") ?? "");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowEspecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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

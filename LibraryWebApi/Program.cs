using LibraryWebApi.Core.BookService;
using LibraryWebApi.Core.ExpenseService;
using LibraryWebApi.Core.UserService;
using LibraryWebApi.Services.BackupStore;
using LibraryWebApi.Services.BookStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookStoreService, BookStoreService>();
builder.Services.AddScoped<IBackupStoreService, BackupStoreService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddControllers();


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("myapp.log")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddApiVersioning(options =>
{
  options.AssumeDefaultVersionWhenUnspecified = true;
  options.DefaultApiVersion = new ApiVersion(1, 0);
  options.ReportApiVersions = true;
  options.ApiVersionReader = ApiVersionReader.Combine(
    new HeaderApiVersionReader("X-Version"),
    new QueryStringApiVersionReader("api-version"),
    new QueryStringApiVersionReader("ver")
  );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
  c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "My API" });
  c.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "My API" });
});

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


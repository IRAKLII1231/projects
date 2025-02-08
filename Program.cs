using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewsApi.Data;
using NewsApi.Interface;
using NewsApi.Repositories;
using NewsApi.Repository;
using NewsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 მონაცმეთა ბაზის კავშირი SQL Server-თან
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Repository & Service Layer-ის რეგისტრაცია
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

// 🔹 Add Controllers
builder.Services.AddControllers();

// 🔹 CORS-ის რეგისტრაცია
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// 🔹 Swagger-ის დამატება
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "News API",
        Version = "v1",
        Description = "News Management API using ASP.NET Core",
    });
});

var app = builder.Build();

// 🔹 მონაცემთა ბაზის ავტომატური მიგრაცია
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // შეცდომის წყარო აქ იყო
}

// 🔹 Development რეჟიმში Swagger-ის ჩართვა
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔹 CORS-ის გააქტიურება
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

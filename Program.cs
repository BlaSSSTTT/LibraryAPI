using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models; // Впевніться, що ваш Namespace для моделей правильний

var builder = WebApplication.CreateBuilder(args);

// Додати сервіси до контейнера.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Додати DbContext для роботи з базою даних
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Замість DefaultConnection використовуйте ваше ім'я рядка підключення

// Додати Swagger/OpenAPI

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Налаштування CORS, якщо це необхідно
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Налаштування HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Використання CORS, якщо налаштовано
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
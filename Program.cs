using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models; // ���������, �� ��� Namespace ��� ������� ����������

var builder = WebApplication.CreateBuilder(args);

// ������ ������ �� ����������.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// ������ DbContext ��� ������ � ����� �����
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // ������ DefaultConnection �������������� ���� ��'� ����� ����������

// ������ Swagger/OpenAPI

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ������������ CORS, ���� �� ���������
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

// ������������ HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ������������ CORS, ���� �����������
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
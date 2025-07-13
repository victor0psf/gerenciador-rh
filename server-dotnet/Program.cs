using Microsoft.EntityFrameworkCore;
using server_dotnet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Adicione a política CORS aqui:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // URL do seu frontend Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS antes do Authorization
app.UseCors("AllowAngularFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

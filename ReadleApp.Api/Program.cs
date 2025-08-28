using ReadleApp.Domain.Interface;
using ReadleApp.Infrastructure;
using ReadleApp.Infrastructure.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<BookApiServices>();
builder.Services.AddCors(options =>
options.AddPolicy("MyPolicy", policy =>
policy.WithOrigins("https://localhost:7097")
      .AllowAnyHeader()
      .AllowAnyMethod()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

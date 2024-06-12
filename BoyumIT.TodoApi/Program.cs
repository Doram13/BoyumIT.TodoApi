using Microsoft.EntityFrameworkCore;
using BoyumIT.TodoApi.Models;
using BoyumIT.TodoApi.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ;

builder.Services.AddDbContext<TodoListContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddScoped<ITodoItemService, TodoItemService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// TODO: As the solution grows, we should create a Startup class (with Configure & ConfigureServices methods) so the registration of Services and middlewares stays readable. 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
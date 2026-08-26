using SmartTodo.API;
using SmartTodo.Application;
using SmartTodo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);


var app = builder.Build();
//Add middleweres here
app.UseExceptionHandler();
app.AddInfrastructureMiddleware();
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

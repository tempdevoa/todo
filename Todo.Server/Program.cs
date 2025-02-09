using Microsoft.EntityFrameworkCore;
using Todo.Server.Domain.TodoItemAggregate;
using Todo.Server.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure the database is seeded with initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.TodoItems.AddRange(
        new TodoItem("projekt_anlegen", "Projekt anlegen", true),
        new TodoItem("durchstich_implementieren", "Durchstich implementieren", false)
        );
    db.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
using Microsoft.AspNetCore.Mvc;
using UserManager.Models;
using UserManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<UserService>(); // în memorie

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 👉 Aici dezactivezi redirecționarea spre HTTPS (opțional pentru localhost)
app.UseHttpsRedirection();

// =======================
//        ROUTES
// =======================

// Get all users
app.MapGet("/users", (UserService service) =>
{
    return Results.Ok(service.GetAll());
});

// Get user by ID
app.MapGet("/users/{id}", (int id, UserService service) =>
{
    var user = service.GetById(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

// Create a new user
app.MapPost("/users", ([FromBody] User user, UserService service) =>
{
    service.Create(user);
    return Results.Created($"/users/{user.Id}", user);
});

// Update user
app.MapPut("/users/{id}", (int id, [FromBody] User updatedUser, UserService service) =>
{
    var result = service.Update(id, updatedUser);
    return result ? Results.NoContent() : Results.NotFound();
});

// Delete user
app.MapDelete("/users/{id}", (int id, UserService service) =>
{
    var result = service.Delete(id);
    return result ? Results.NoContent() : Results.NotFound();
});

app.Run();

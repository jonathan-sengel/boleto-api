using BoletoAPI.Data;
using BoletoAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("/api/bancos");
    return Task.CompletedTask;
});

app.MapGet("/api/bancos", static async (ApplicationDbContext db) => await db.Bancos.ToListAsync());

app.MapGet("/api/bancos/{codigo}",
    async (string codigo, ApplicationDbContext db) =>
        {
            var banco = await db.Bancos
                .Where(b => b.CodigoBanco.Equals(codigo))
                .SingleOrDefaultAsync();
            if (banco is not null) return Results.Ok(banco);
            return Results.NotFound();
        })
    .Produces<Banco>(200)
    .Produces(404);

app.MapPost("/api/bancos/novo", async (ApplicationDbContext db,
    Banco banco) =>
        {
            db.Bancos.Add(banco);
            await db.SaveChangesAsync();
            return Results.Created($"/api/bancos/{banco.Id}", banco);
        })
    .Produces<Banco>(201);

app.UseHttpsRedirection();
app.Run();
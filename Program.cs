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

app.MapPost("/api/bancos/novo",
    async (ApplicationDbContext db, Banco banco) =>
        {
            db.Bancos.Add(banco);
            await db.SaveChangesAsync();
            return Results.Created($"/api/bancos/{banco.Id}", banco);
        })
    .Produces<Banco>(201);

app.MapPost("/api/boletos/novo",
    async (ApplicationDbContext db, Boleto boleto) =>
        {
            var banco = await db.Bancos
                .Where(b => b.Id == boleto.BancoId)
                .SingleOrDefaultAsync();
            if (banco is null) return Results.BadRequest("Banco com id informado não encontrado.");

            db.Boletos.Add(boleto);
            await db.SaveChangesAsync();
            return Results.Created($"/api/boletos/{boleto.Id}", boleto);
        })
    .Produces<Boleto>(201)
    .Produces(400);

app.MapGet("/api/boletos/{id}",
    async (Guid id, ApplicationDbContext db) =>
        {
            var boleto = await db.Boletos
                .Include(bol => bol.Banco)
                .SingleOrDefaultAsync(b => b.Id == id);
            if (boleto is null) return Results.NotFound();
            boleto.ProcessarValorAPagar();
            return Results.Ok(boleto);
        })
    .Produces<Boleto>(200)
    .Produces(404);

app.UseHttpsRedirection();
app.Run();
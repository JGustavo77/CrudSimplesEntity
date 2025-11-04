using FrutasDoSeuZe.Data;
using FrutasDoSeuZe.Exceptions;
using FrutasDoSeuZe.Models;
using Microsoft.EntityFrameworkCore;

namespace FrutasDoSeuZe.Repositories;

public class FrutaRepository : IFrutaRepository
{
    private readonly AppDbContext _db;

    public FrutaRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Fruta>> GetAllAsync()
        => await _db.Frutas.ToListAsync();

    public async Task<Fruta?> GetByNameAsync(string nome)
        => await _db.Frutas.FirstOrDefaultAsync(f => f.Nome == nome);

    public async Task AddAsync(Fruta fruta)
        => await _db.Frutas.AddAsync(fruta);

    public Task UpdateAsync(Fruta fruta)
    {
        _db.Frutas.Update(fruta);
        return Task.CompletedTask;
    }

    public async Task RemoveAsync(Fruta fruta)
    {
        if (fruta is null) throw new ArgumentNullException(nameof(fruta));

        if (await _db.ItensPedido.AnyAsync(ip => ip.FrutaId == fruta.Id))
            throw new FrutaEmUsoException("Não é possível remover a fruta, pois ela está associada a pedidos.");

        // Garante que a entidade seja anexada ao contexto antes da remoção, evitando problemas de rastreamento
        if (_db.Entry(fruta).State == EntityState.Detached)
        {
            _db.Frutas.Attach(fruta);
        }

        _db.Frutas.Remove(fruta);
    }

    public async Task SaveAsync()
        => await _db.SaveChangesAsync();
}

using FrutasDoSeuZe.Models;


public interface IFrutaRepository
{
    Task<List<Fruta>> GetAllAsync();
    Task<Fruta?> GetByNameAsync(string nome);
    Task AddAsync(Fruta fruta);
    Task UpdateAsync(Fruta fruta);
    Task RemoveAsync(Fruta fruta);
    Task SaveAsync();
}

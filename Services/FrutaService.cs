using FrutasDoSeuZe.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FrutaJaCadastradaException : ApplicationException
{
    public FrutaJaCadastradaException(string message) : base(message) { }
}

public class FrutaNaoEncontradaException : ApplicationException
{
    public FrutaNaoEncontradaException(string message) : base(message) { }
}

public class FrutaService
{
    private readonly IFrutaRepository _repo;

    public FrutaService(IFrutaRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Fruta>> ListarAsync()
    {
        var frutas = await _repo.GetAllAsync();

        return frutas;
    }

    public async Task CadastrarAsync(string nome, decimal preco, int quantidade)
    {
        var frutaExistente = await _repo.GetByNameAsync(nome);
        if (frutaExistente != null)

            throw new FrutaJaCadastradaException("Fruta já cadastrada!");

        var fruta = new Fruta
        {
            Nome = nome,
            Preco = preco,
            Quantidade = quantidade
        };

        await _repo.AddAsync(fruta);
        await _repo.SaveAsync();
    }

    public async Task AtualizarAsync(string nome, decimal novoPreco, int novaQuantidade)
    { 
        var frutaExistente = await _repo.GetByNameAsync(nome);

        if (frutaExistente is null)
            throw new FrutaNaoEncontradaException("Fruta não encontrada!");

        frutaExistente.Preco = novoPreco;
        frutaExistente.Quantidade = novaQuantidade;

        await _repo.UpdateAsync(frutaExistente);
        await _repo.SaveAsync();
    }

    public async Task RemoverAsync(string nome) 
    {
        var frutaExistente = await _repo.GetByNameAsync(nome);

        if (frutaExistente is null)
            throw new FrutaNaoEncontradaException("Fruta não encontrada!");

        await _repo.RemoveAsync(frutaExistente);
        await _repo.SaveAsync();
    }
}

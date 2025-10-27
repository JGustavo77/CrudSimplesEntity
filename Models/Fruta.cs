using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace FrutasDoSeuZe.Models;

public class Fruta
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }


public static bool VerificaPreco(decimal preco)
    {
        if (preco >= 0 && preco <= 1000)
        {
            return false;
        }
        return true;
    }

public static bool VerificaQuantidade(int quantidade)
    {
        if (quantidade < 0 || quantidade > 1000)
        {
            return true;
        }
        return false;
    }

public static bool VerificaNome(string nome)
    {
        Regex regex = new(@"^[A-Za-zÀ-ú\s]+$"); //adicionar verificaçao de uma vogal pelo menos
        if (string.IsNullOrWhiteSpace(nome))
        {
            return true;
        }

        if (regex.IsMatch(nome) && nome.Length <= 40 && nome.Length> 2)
        {
            return false;
        }
        return true;
    }
}
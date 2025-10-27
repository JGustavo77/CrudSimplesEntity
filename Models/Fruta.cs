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


    public static bool IsPrecoValido(decimal preco) => preco >= 0 && preco <= 1000;
    

public static bool IsQuantidadeValida(int quantidade)
    {
        return quantidade > 0 && quantidade < 1000;
    }

public static bool IsNomeValido(string nome)
    {
        Regex regex = new(@"^[A-Za-zÀ-ú\s]+$"); 
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
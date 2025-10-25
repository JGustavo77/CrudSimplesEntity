using FrutasDoSeuZe.Data;
using FrutasDoSeuZe.Models;
using System.Text;

using var db = new AppDbContext();

bool isRuning = true;

while (isRuning)
{
    var menu = new StringBuilder();
    menu.AppendLine("\n--Frutas do Seu Zé 🍉--");
    menu.AppendLine("1. Cadastrar fruta");
    menu.AppendLine("2. Listar frutas");
    menu.AppendLine("3. Atualizar fruta");
    menu.AppendLine("4. Deletar fruta");
    menu.AppendLine("5. Sair");
    menu.Append("Escolha uma opção: ");
    Console.Write(menu);
    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Nome da fruta: ");
            string? nome = Console.ReadLine();
            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine()!);
            Console.Write("Quantidade: ");
            int quantidade = int.Parse(Console.ReadLine()!);

            var novaFruta = new Fruta() { Nome = nome, Preco = preco, Quantidade = quantidade };
            db.Frutas.Add(novaFruta);
            db.SaveChanges();

            Console.WriteLine($"{nome} cadastrada");
            break;


    }

}

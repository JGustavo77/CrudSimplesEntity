using FrutasDoSeuZe.Data;
using FrutasDoSeuZe.Models;
using System.Text;

using var db = new AppDbContext();

bool isRuning = true;

while (isRuning)
{
    var menu = new StringBuilder();
    menu.AppendLine("\n--  Frutas do Seu Zé 🍉 --");
    menu.AppendLine("1. Cadastrar fruta");
    menu.AppendLine("2. Listar frutas");
    menu.AppendLine("3. Atualizar fruta");
    menu.AppendLine("4. Deletar fruta");
    menu.AppendLine("5. Sair");
    menu.Append("Escolha uma opção: ");
    Console.Write(menu);

    string? opcao = Console.ReadLine();

    while (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5")
    {
        Console.Clear();
        Console.WriteLine("Digite apenas uma opção válida\n");
        Console.WriteLine(menu);
        opcao = Console.ReadLine();
    }

    switch (opcao)
    {
        case "1":
            Console.Write("Nome da fruta: ");
            string? nome = Console.ReadLine();

            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine()!, out decimal preco))
            {
                Console.WriteLine("⚠️ Valor inválido!");
            }

            Console.Write("Quantidade: ");

            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("⚠️ Quantidade inválida:");
            }

            var novaFruta = new Fruta { Nome = nome, Preco = preco, Quantidade = quantidade };
            db.Frutas.Add(novaFruta);
            db.SaveChanges();

            Console.WriteLine($"✅ {nome} cadastrada com sucesso!");
            break;

        case "2":
            Console.Clear();
            var frutas = db.Frutas.ToList();

            if (frutas.Count == 0)
            {
                Console.WriteLine("📭 Nenhuma fruta cadastrada.");
            }
            else
            {
                Console.WriteLine("-- Lista de frutas --");
                foreach (var fruta in frutas)
                {
                    Console.WriteLine($" ID: {fruta.Id} | {fruta.Nome}| R${fruta.Preco} | Estoque: {fruta.Quantidade}kg");
                }
            }
            break;

        case "3":
            Console.Write("Nome da fruta a ser atualizada: ");
            string? nomeAtualizar = Console.ReadLine();
            var frutaAtualizar = db.Frutas.FirstOrDefault(f => f.Nome == nomeAtualizar);

            if (frutaAtualizar != null)
            {
                Console.Write("Novo preço: ");
                frutaAtualizar.Preco = decimal.Parse(Console.ReadLine()!);
                Console.Write("Nova quantidade: ");
                frutaAtualizar.Quantidade = int.Parse(Console.ReadLine()!);

                db.SaveChanges();
                Console.WriteLine($"Fruta '{frutaAtualizar.Nome}' atualizada!");
            }
            else
                Console.WriteLine("⚠️ Fruta não encontrada!");
            break;

        case "4":
            Console.Write("Nome da fruta a ser removida: ");
            string? nomeRemover = Console.ReadLine();
            var frutaRemover = db.Frutas.FirstOrDefault(f => f.Nome == nomeRemover);

            if (frutaRemover != null)
            {
                db.Frutas.Remove(frutaRemover);
                db.SaveChanges();
                Console.WriteLine($"❌ Fruta '{frutaRemover.Nome}' removida!");
            }
            else
                Console.WriteLine("Essa fruta não existe!/n");
            break;

        case "5":
            Console.Clear();
            Console.WriteLine("Encerrando o sistema... 🍇");
            Thread.Sleep(2000);
            isRuning = false;
            break;
    }

    if (isRuning)
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
    }

}


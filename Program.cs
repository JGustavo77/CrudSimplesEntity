using FrutasDoSeuZe.Data;
using FrutasDoSeuZe.Models;
using System.Text;

Console.OutputEncoding = System.Text.Encoding.UTF8; // utf para emojis no console

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
    menu.AppendLine("5. Registrar pedido (Venda ou Reposição)");
    menu.AppendLine("6. Resumo de pedidos");
    menu.AppendLine("7. Sair");
    menu.Append("Escolha uma opção: ");
    Console.Write(menu);

    string? opcao = Console.ReadLine();

    while (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5" && opcao != "6" && opcao != "7")
    {
        Console.Clear();
        Console.WriteLine("Digite apenas uma opção válida\n");
        Console.WriteLine(menu);
        opcao = Console.ReadLine();
    }

    switch (opcao)
    {
        case "1":
            Console.Clear();
            Console.Write("Nome da fruta: ");
            string? nome = Console.ReadLine()!.Trim();

            while (Fruta.IsNomeValido(nome))
            {
                Console.Write("\nNome inválido, certifique de não conter números");
                nome = Console.ReadLine()!.Trim();
            }

            decimal preco;
            Console.Write("\nPreço da(do) "); Console.Write(nome + ": ");
            while (!decimal.TryParse(Console.ReadLine(), out preco) || !Fruta.IsPrecoValido(preco))
            {
                Console.Write("\nPreço inválido! ... ");
            }

            int quantidade;
            Console.Write("\nQuantidade: ");
            while (!int.TryParse(Console.ReadLine(), out quantidade) || !Fruta.IsQuantidadeValida(quantidade))
            {
                Console.WriteLine("⚠️ Quantidade inválida:");
            }

            var novaFruta = new Fruta { Nome = nome, Preco = preco, Quantidade = quantidade };
            db.Frutas.Add(novaFruta);
            await db.SaveChangesAsync();

            Console.WriteLine($"✅ {nome} cadastrada com sucesso!");
            break;

        case "2":
            Console.Clear();
            var frutas = db.Frutas.ToList();

            if (frutas.Count is 0)
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
            Console.Clear();
            Console.Write("Nome da fruta a ser atualizada: ");
            string? nomeAtualizar = Console.ReadLine()!.Trim();
            var frutaAtualizar = db.Frutas.FirstOrDefault(f => f.Nome == nomeAtualizar);

            if (frutaAtualizar is null)
            {
                Console.Write("Novo preço: ");
                frutaAtualizar.Preco = decimal.Parse(Console.ReadLine()!);
                Console.Write("Nova quantidade: ");
                frutaAtualizar.Quantidade = int.Parse(Console.ReadLine()!);

                await db.SaveChangesAsync();

                Console.WriteLine($"Fruta '{frutaAtualizar.Nome}' atualizada!");
            }
            else
                Console.WriteLine("⚠️ Fruta não encontrada!");
            break;

        case "4":
            Console.Clear();
            Console.Write("Nome da fruta a ser removida: ");
            string? nomeRemover = Console.ReadLine()!.Trim();
            var frutaRemover = db.Frutas.FirstOrDefault(f => f.Nome == nomeRemover);

            if (frutaRemover is null)
            {
                db.Frutas.Remove(frutaRemover);
                await db.SaveChangesAsync();
                Console.WriteLine($"❌ Fruta '{frutaRemover.Nome}' removida!");
            }
            else
                Console.WriteLine("Essa fruta não existe!/n");
            break;

        case "5":

            try
            {
                Console.Clear();
                Console.Write("Tipo do pedido (Venda ou Reposição): ");
                string tipo = Console.ReadLine()!.ToLower();

                while (tipo != "venda" && tipo != "reposição")
                {
                    Console.WriteLine("\n⚠️ Tipo inválido! Digite 'Venda' ou 'Reposição'.");
                    Console.Write("\nTipo do pedido (Venda ou Reposição): ");
                    tipo = Console.ReadLine()!.ToLower();
                }

                Console.Write("\nDescrição (opcional): ");
                string? descricao = Console.ReadLine();

                var pedido = new Pedido()
                {
                    Data = DateTime.UtcNow,
                    Tipo = tipo,
                    Descricao = descricao
                };
                db.Pedidos.Add(pedido);
                await db.SaveChangesAsync();

                bool adicionarMais = true;

                decimal total = 0;
                while (adicionarMais)
                {
                    Console.Write("\nNome da fruta: ");
                    string? nomeFruta = Console.ReadLine()!.Trim();
                    var fruta = db.Frutas.FirstOrDefault(f => f.Nome == nomeFruta);

                    if (fruta is null)
                    {
                        Console.WriteLine("\n⚠️ Fruta não encontrada!");
                        continue;
                    }

                    Console.Write("\nQuantidade: ");
                    int quantidadeItem = int.Parse(Console.ReadLine()!);

                    while (!Fruta.IsQuantidadeValida(quantidadeItem) || (tipo == "venda" && quantidadeItem > fruta.Quantidade))
                    {
                        if (!Fruta.IsQuantidadeValida(quantidadeItem))
                            Console.Write("\nQuantidade inválida! Digite um valor entre 1 e 1.000: ");

                        else if (quantidadeItem > fruta.Quantidade)
                            Console.Write($"\n⚠️ Estoque insuficiente! Temos apenas {fruta.Quantidade}kg. Digite uma quantidade válida: ");

                        quantidadeItem = int.Parse(Console.ReadLine()!);
                    }

                    var item = new ItemPedido()
                    {
                        PedidoId = pedido.Id,
                        FrutaId = fruta.Id,
                        Quantidade = quantidadeItem
                    };

                    db.ItensPedido.Add(item);

                    // Atualiza o estoque

                    if (tipo.ToLower() == "venda")
                        fruta.Quantidade -= quantidadeItem;

                    else if (tipo.ToLower() == "reposição")
                        fruta.Quantidade += quantidadeItem;

                    total += quantidadeItem * fruta.Preco;

                    await db.SaveChangesAsync();

                    Console.Write("\nAdicionar outra fruta ao mesmo pedido? (s/n): ");
                    adicionarMais = Console.ReadLine()!.ToLower() == "s";
                }

                //pedido.ValorTotal = CalcularTotal(pedido, db);

                pedido.ValorTotal = total;
                await db.SaveChangesAsync();


                Console.WriteLine($"\n✅ Pedido {pedido.Id} registrado com sucesso!");
            }
            catch (Exception ex)
            {
                ExibirErroPedido(ex);
            }
            break;

        case "6":
            Console.Clear();
            var pedidos = db.Pedidos.OrderByDescending(p => p.Data).ToList();

            if (pedidos.Count == 0)
            {
                Console.WriteLine("\nNenhum pedido registrado ainda"); break;
            }

            Console.WriteLine("-- Histórico de pedidos --");

            foreach (var p in pedidos)
            {
                Console.WriteLine($"🧾 Pedido #{p.Id} | {p.Tipo.ToUpper()} | {p.Data:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"💰 Total: R${p.ValorTotal:F2}");
                if (!string.IsNullOrWhiteSpace(p.Descricao))
                    Console.WriteLine($"📝 {p.Descricao}");
                Console.WriteLine("----------------------------");
            }
            break;

        case "7":
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

static void ExibirErroPedido(Exception ex)
{
    Console.WriteLine("❌ ERRO AO SALVAR PEDIDO!");
    Console.WriteLine(ex.Message);
    if (ex.InnerException != null)
        Console.WriteLine($"👉 Inner: {ex.InnerException.Message}");
}

/*static decimal CalcularTotal(Pedido pedido, AppDbContext db) não performa bem
{
    return db.ItensPedido
        .Where(i => i.PedidoId == pedido.Id)
        .Join(db.Frutas,
              item => item.FrutaId,
              fruta => fruta.Id,
              (item, fruta) => item.Quantidade * fruta.Preco)
        .Sum();
}*/



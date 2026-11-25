
using System;
using System.Collections.Generic;

// situacão da tarefa
enum Situacao
{
    Pendente,
    Concluida
}

// represesentacao tarefea
class Tarefa
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public Situacao Situacao { get; set; }

    public Tarefa(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
        Situacao = Situacao.Pendente;
    }
}

class Program
{
    // lista de cadastrads
    static List<Tarefa> tarefas = new List<Tarefa>();

    // guarda as tarefas concluidas
    static Stack<int> pilha = new Stack<int>();

    static int proximoId = 1;

    static void Main()
    {
        int opcao = 0;

        while (opcao != 6)
        {
            Console.WriteLine("\nGERENCIADOR DE TAREFAS");
            Console.WriteLine("1 - Cadastrar tarefa");
            Console.WriteLine("2 - Listar tarefas");
            Console.WriteLine("3 - Buscar tarefa");
            Console.WriteLine("4 - Concluir tarefa");
            Console.WriteLine("5 - Desfazer conclusão");
            Console.WriteLine("6 - Sair");

            Console.Write("\nEscolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Digite um número válido!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;

                case 2:
                    Listar();
                    break;

                case 3:
                    Buscar();
                    break;

                case 4:
                    Concluir();
                    break;

                case 5:
                    Desfazer();
                    break;

                case 6:
                    Console.WriteLine("Programa encerrado!");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    // cadastra tarefa
    static void Cadastrar()
    {
        Console.Write("\nDigite a descrição da tarefa: ");
        string descricao = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("Descrição inválida!");
            return;
        }

        Tarefa tarefa = new Tarefa(proximoId, descricao);

        tarefas.Add(tarefa);
        proximoId++;

        Console.WriteLine("Tarefa cadastrada!");
    }

    // listar tarefa
    static void Listar()
{
    if (tarefas.Count == 0)
    {
        Console.WriteLine("\nNenhuma tarefa cadastrada.");
        return;
    }

    Console.WriteLine("\nLISTA DE TAREFAS");

    foreach (Tarefa tarefa in tarefas)
    {
        Console.WriteLine("ID: " + tarefa.Id);
        Console.WriteLine("Tarefa: " + tarefa.Descricao);
        Console.WriteLine("Situação: " + tarefa.Situacao);
    }

}

    // procurar tarefa pelo id
    static Tarefa Procurar(int id)
    {
        foreach (Tarefa tarefa in tarefas)
        {
            if (tarefa.Id == id)
            {
                return tarefa;
            }
        }

        return null;
    }

    // buscar tarefa
    static void Buscar()
    {
        Console.Write("\nDigite o ID da tarefa: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Tarefa tarefa = Procurar(id);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
        }
        else
        {
            Console.WriteLine("\nID: " + tarefa.Id);
            Console.WriteLine("Descrição: " + tarefa.Descricao);
            Console.WriteLine("Situação: " + tarefa.Situacao);
        }
    }

    // concluir tarefa
    static void Concluir()
    {
        Console.Write("\nDigite o ID da tarefa: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido!");
            return;
        }

        Tarefa tarefa = Procurar(id);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada.");
            return;
        }

        if (tarefa.Situacao == Situacao.Concluida)
        {
            Console.WriteLine("Essa tarefa já foi concluída.");
            return;
        }

        tarefa.Situacao = Situacao.Concluida;

        pilha.Push(id);

        Console.WriteLine("Tarefa concluída!");
    }

    // desfazer a conclusão ( a ultima )
    static void Desfazer()
    {
        if (pilha.Count == 0)
        {
            Console.WriteLine("\nNão existe conclusão para desfazer.");
            return;
        }

        int id = pilha.Pop();

        Tarefa tarefa = Procurar(id);

        if (tarefa != null)
        {
            tarefa.Situacao = Situacao.Pendente;

            Console.WriteLine("Conclusão desfeita!");
            Console.WriteLine("Tarefa voltou para Pendente.");
        }
    }
}

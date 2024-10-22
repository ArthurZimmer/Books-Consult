using System;
using System.IO;

class Registro
{
    public string Nome { get; set; }
    public string Autor { get; set; }
    public string Valor { get; set; }
    public string Codigo { get; set; }

    public static Registro GetRegistro()
    {
        Registro registro = new Registro();

        Console.Write("\nDigite o nome do livro: ");
        registro.Nome = Console.ReadLine();

        Console.Write("Digite o autor do livro: ");
        registro.Autor = Console.ReadLine();

        Console.Write("Digite o valor do livro: ");
        registro.Valor = Console.ReadLine();

        Console.Write("Digite o codigo do livro: ");
        registro.Codigo = Console.ReadLine();

        return registro;
    }
}

class Biblioteca
{
    public static void ConsultarPorAutor(string autor)
    {
        string caminho = @"c:\biblioteca\registro-livros.txt";
        bool encontrou = false;

        if (!File.Exists(caminho))
        {
            Console.WriteLine("Erro ao abrir o arquivo.");
            return;
        }

        foreach (string linha in File.ReadAllLines(caminho))
        {
            string[] dados = linha.Split('|');
            if (dados[1].Equals(autor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n\tCodigo: {dados[3]}\n\tTitulo: {dados[0]}\n\tAutor: {dados[1]}\n\tValor: R$ {dados[2]}");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhum livro encontrado com o autor '{autor}'.");
        }
    }

    public static void ConsultarPorNome(string nome)
    {
        string caminho = @"c:\biblioteca\registro-livros.txt";
        bool encontrou = false;

        if (!File.Exists(caminho))
        {
            Console.WriteLine("Erro ao abrir o arquivo.");
            return;
        }

        foreach (string linha in File.ReadAllLines(caminho))
        {
            string[] dados = linha.Split('|');
            if (dados[0].Equals(nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n\tCodigo: {dados[3]}\n\tTitulo: {dados[0]}\n\tAutor: {dados[1]}\n\tValor: R$ {dados[2]}");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhum livro encontrado com o nome '{nome}'.");
        }
    }

    public static void ConsultarPorValor(string valor)
    {
        string caminho = @"c:\biblioteca\registro-livros.txt";
        bool encontrou = false;

        if (!File.Exists(caminho))
        {
            Console.WriteLine("Erro ao abrir o arquivo.");
            return;
        }

        foreach (string linha in File.ReadAllLines(caminho))
        {
            string[] dados = linha.Split('|');
            if (dados[2].Equals(valor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n\tCodigo: {dados[3]}\n\tTitulo: {dados[0]}\n\tAutor: {dados[1]}\n\tValor: R$ {dados[2]}");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhum livro encontrado com o valor '{valor}'.");
        }
    }

    public static void Main()
    {
        int opcao;
        string autor, nome, valor;

        Console.WriteLine("\n1 - Inserir registro\n2 - Consultar por autor\n3 - Consultar por nome do livro\n4 - Consultar por valor\n5 - Excluir todos\n6 - Sair\n");
        Console.Write("Digite a opcao que deseja prosseguir: ");
        opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                using (StreamWriter livros = new StreamWriter(@"c:\biblioteca\registro-livros.txt", true))
                {
                    do
                    {
                        Registro registro = Registro.GetRegistro();
                        livros.WriteLine($"{registro.Nome}|{registro.Autor}|{registro.Valor}|{registro.Codigo}");
                        Console.Write("Mais um livro (s/n)? ");
                    } while (Console.ReadLine().ToLower() != "n");
                }
                break;

            case 2:
                Console.Write("Digite o autor que deseja consultar: ");
                autor = Console.ReadLine();
                ConsultarPorAutor(autor);
                break;

            case 3:
                Console.Write("Digite o nome do livro que deseja consultar: ");
                nome = Console.ReadLine();
                ConsultarPorNome(nome);
                break;

            case 4:
                Console.Write("Digite o valor que deseja consultar: ");
                valor = Console.ReadLine();
                ConsultarPorValor(valor);
                break;

            case 5:
                string caminho = @"c:\biblioteca\registro-livros.txt";
                if (File.Exists(caminho))
                {
                    File.Delete(caminho);
                    Console.WriteLine("\nRegistros excluidos com sucesso.\n");
                }
                else
                {
                    Console.WriteLine("\nErro ao excluir o arquivo.\n");
                }
                break;

            case 6:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("Opcao invalida.");
                break;
        }
    }
}


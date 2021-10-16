using System;

namespace Dio.SeriesTV
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();
            
            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListaSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizaSerie();
                        break;
                    case "4":
                        VisualizaSerie();
                        break;
                    case "5":
                        ExcluiSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ExcluiSerie()
        {
            System.Console.Write("Informe o id da série que deseja excluir: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizaSerie()
        {
            System.Console.Write("Informe o id da série desejada: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);
            System.Console.WriteLine(serie);
        }

        private static void AtualizaSerie()
        {
            Console.WriteLine("Informe o id da série que deseja alterar:");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("Informe o gênero da série entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Informe o título da série: ");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Informe o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Informe a descrição da série :");
            string entradaDescricao = Console.ReadLine();

            Serie serie = new Serie(Id: indiceSerie,
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);
            repositorio.Atualiza(indiceSerie, serie);  
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine($"{i} - {Enum.GetName(typeof(Genero), i)}");
            }

            Console.Write("Informe o gênero da série: ");
            int entradaGenero = int.Parse(Console.ReadLine());
            Console.Write("Informe o título da série: ");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Informe o ano de início da série: ");
            int entradaAno = int.Parse(Console.ReadLine());
            Console.Write("Informe a descrição da série: ");
            string entradaDescricao = Console.ReadLine();

            Serie serie = new Serie(Id: repositorio.ProximoId(),
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);
            repositorio.Insere(serie);                        
        }

        private static void ListaSeries()
        {
            System.Console.WriteLine("Listar séries:");
            var lista = repositorio.Lista();
            if (lista.Count == 0)
            {
                System.Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido() ? "***Excluido***" : "";
                System.Console.WriteLine($"#ID: {serie.retornaId()} - {serie.retornaTitulo()} - {excluido}");
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine(   );
            Console.WriteLine("DIO Séries a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar sérires");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}

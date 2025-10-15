using Muestra_de_la_api.Controllers;
using Muestra_de_la_api.Models;
using System.Text.Json;

namespace Muestra_de_la_api.Views
{
    internal class Program
    {
        private static void WriteCharacters(List<Character> results)
        {
            results.ForEach(c => Console.WriteLine($"{c.id} - {c.name} - {c.gender} - {c.species} - {c.origin.name} - {c.status}"));
        }
        static async Task Main(string[] args)
        {
            var datos = new CharacterController();

            var characters = await datos.GetCharactersFirstPage();

            var results = characters.results;
            var info = characters.info;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Personajes de Rick and Morty:\n");
                Console.WriteLine($"Hay un total de {info.count} personajes.\n");
                Console.WriteLine($"Desde el: #{results.Select(x=> x.id).First()} hasta el #{results.Select(x=> x.id).Last()}\n\n");
                WriteCharacters(results);

                Console.WriteLine("\n1:Siguiente pagina.");
                Console.WriteLine("2:Pagina anterior.");
                var select = Console.ReadLine();

                if (!int.TryParse(select,out int option))
                {
                    Console.Clear();
                    Console.WriteLine("Opcion no valida. Solo se permiten los numeros 1 y 2.");
                    Console.ReadKey();
                    continue;
                }

                if(option != 1 && option != 2)
                {
                    Console.Clear();
                    Console.WriteLine("Elige entre uno o dos.");
                    Console.ReadKey();
                    continue;
                }

                if(option == 1 && info.next == null)
                {
                    Console.Clear();
                    Console.WriteLine("No hay mas paginas.");
                    Console.ReadKey();
                    continue;
                }

                if(option == 2 && info.prev == null)
                {
                    Console.Clear();
                    Console.WriteLine("No hay paginas anteriores.");
                    Console.ReadKey();
                    continue;
                }

                if(option == 1)
                {
                    characters = await datos.GetCharactersByUrl(info.next);
                    results = characters.results;
                    info = characters.info;
                }
               
                if(option == 2)
                {
                    characters = await datos.GetCharactersByUrl(info.prev);
                    results = characters.results;
                    info = characters.info;
                }
            }
        }
    }
}

using Muestra_de_la_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Muestra_de_la_api.Controllers
{
    public  class CharacterController
    {
        HttpClient client;
        public CharacterController()
        {
            client = new HttpClient();
        }

        public async Task<Characters> GetCharactersFirstPage()
        {
            var response = await client.GetAsync("https://rickandmortyapi.com/api/character/");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Characters>(json);
                return result;
            }
            return null;
        }

        public async Task<Characters> GetCharactersByUrl(string url)
        {
            var response = await client.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Characters>(json);
                return result;
            }
            return null;
        }
    }
}

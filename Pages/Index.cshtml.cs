using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string PokemonName { get; private set; } // Updated property name
    public string PokemonID { get; private set;}


    public async Task OnGetAsync()
{
    try
    {
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=9"); // Limit to 10 Pokemon for example
    
        if (response.IsSuccessStatusCode)
        {            
            var content = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(content);
            var results = data["results"];
        
            PokemonNames = results.Select(p => p["name"].ToString()).ToList();
            
            PokemonSrc = new List<string>();
            PokemonSrcBack = new List<string>();
            PokemonTypes = new List<string>();
                foreach(string pokeName in PokemonNames)
            {
                Console.Write($"name: \t {pokeName} \n");

                    var singlePokeResponse = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokeName}");
                if (singlePokeResponse.IsSuccessStatusCode)
                {
                    var content2 = await singlePokeResponse.Content.ReadAsStringAsync();
                    var data2 = JObject.Parse(content2);
                    // Console.WriteLine(data2);
                    var results2 = data["types"];
                    // Access the properties of the single Pokemon from data2
                    
                    var name = data2["name"].ToString();
                    var id = data2["id"].ToString();
                    // data2.sprites.frontDefault

                    var frontDefault = data2["sprites"]["front_default"].ToString();
                    string backDefault = data2["sprites"]["back_default"].ToString();                    
                    string pokemonType = data2["types"]?[0]?["type"]?["name"]?.ToString();                    
                    PokemonSrc.Add(frontDefault);
                    PokemonSrcBack.Add(backDefault);
                    PokemonTypes.Add(pokemonType);                    
                }
            }

        }
        else
        {
            PokemonNames = new List<string> { "Not Found" };
        }
    }
    catch (Exception ex)
    {
        PokemonNames = new List<string> { "Error" };
    }
}

public List<string> PokemonNames { get; private set; }
public List<string> PokeIDs { get; private set; }
public List<string> PokemonSrc { get; private set; }
public List<string> PokemonSrcBack { get; private set; }
public List<string> PokemonTypes { get; private set; }

}




    // public async Task<IActionResult> OnGetAsync()
    // {
    //     try
    //     {
    //         var httpClient = _httpClientFactory.CreateClient();
    //         var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/108");

    //         if (response.IsSuccessStatusCode)
    //         {
    //             var content = await response.Content.ReadAsStringAsync();
    //             var data = JObject.Parse(content);
    //             PokemonName = data["name"].ToString(); // Update the property here
    //             PokemonID = data["id"].ToString();

    //             return Page(); // Return the Razor Page
    //         }
    //         else
    //         {
    //             return new JsonResult(new { name = "Not Found" });
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return new JsonResult(new { name = "Error" });
    //     }
    // }

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

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/108");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JObject.Parse(content);
                PokemonName = data["name"].ToString(); // Update the property here

                return Page(); // Return the Razor Page
            }
            else
            {
                return new JsonResult(new { name = "Not Found" });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new { name = "Error" });
        }
    }
}

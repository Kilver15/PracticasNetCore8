using ApiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ApiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        [HttpGet("TodosPokemon")]
        public async Task<ActionResult<ApiResponse>> GetAllPokemon()
        {
            try
            {
                using(var client = new HttpClient())
                {
                    string url = "https://pokeapi.co/api/v2/pokemon";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        ApiResponse pokemon = JsonConvert.DeserializeObject<ApiResponse>(result);
                        return Ok(pokemon);
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pokemon/{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemon(int id)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    string url = $"https://pokeapi.co/api/v2/pokemon/{id}";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(result);
                        return Ok(pokemon);
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

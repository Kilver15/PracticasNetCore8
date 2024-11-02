namespace ApiAPI.Models
{
    public class ApiResponse
    {
        public List<Pokedex> results { get; set; }
    }
    public class Pokedex
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<Types> Types { get; set; }
        public List<Stats> Stats { get; set; }
    }

    public class Types
    {
        public int slot { get; set; }
        public Pokedex Type { get; set; }
    }

    public class Stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public Pokedex Stat { get; set; }
    }
}

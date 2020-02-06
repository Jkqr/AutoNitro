using Newtonsoft.Json;

namespace AutoNitro
{
    public class Config
    {
        [JsonProperty("auth_token")]
        public string Token { get; set; }
        
        [JsonProperty("giveaway_reaction_percentage_chance")]
        public int GiveAwayReactChance { get; set; }
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApp.Services
{
    public class FactService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FactService> _logger;

        public FactService(HttpClient httpClient, ILogger<FactService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetFactAsync()
        {
            try
            {
                var resp = await _httpClient.GetStringAsync("https://catfact.ninja/fact");
                _logger.LogInformation("Response: {Response}", resp);
                var fact = JsonSerializer.Deserialize<CatFact>(resp);
                _logger.LogInformation("Deserialized: {@Fact}", fact);

                if (fact != null && !string.IsNullOrEmpty(fact.Fact))
                {
                    return fact.Fact;
                }
                else
                {
                    return "No fact received.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error fetching fact");
                return "error fetching fact";
            }
        }
    }

    public class CatFact
    {
        [JsonPropertyName("fact")]
        public string Fact { get; set; }
        
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}

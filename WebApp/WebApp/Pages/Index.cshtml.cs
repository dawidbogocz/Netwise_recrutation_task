using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FactService _factService;

        public List<string> Facts { get; private set; } = new List<string>();

        //public string Fact {  get; private set; }

        public IndexModel(ILogger<IndexModel> logger, FactService factService)
        {
            _logger = logger;
            _factService = factService;
        }

        public async Task OnGetAsync(int count = 1)
        {
            try{
                for (int i = 0; i < count; i++)
                {
                    var fact = await _factService.GetFactAsync();
                    Facts.Add(fact);
                    await System.IO.File.AppendAllTextAsync("catfacts.txt", fact + Environment.NewLine);
                }

                /*Fact = await _factService.GetFactAsync();
                _logger.LogInformation("Retrieved Fact: {Response}", Fact);
                await System.IO.File.AppendAllTextAsync("facts.txt {@Fact}", Fact + Environment.NewLine);
                _logger.LogInformation($"{Fact}");*/
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error in OnGetAsync");
            }
        }
    }
}

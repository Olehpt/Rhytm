using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rhytm.Models;
namespace Rhytm.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly RhytmContext _context;
        public LoginModel(IHttpClientFactory factory, RhytmContext context)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44370/api/Users/");
            _context = context;
        }
        public void OnGet() { }
        [BindProperty]
        public User user { get; set; } = new User();
        public string message { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            user.Name = "randomname";
            var response = await _httpClient.PostAsJsonAsync("login", user);
            if (response.IsSuccessStatusCode)
            {
                message = "Succes.";
                return Page();
            }
            message = "Error: " + await response.Content.ReadAsStringAsync();
            return Page();
        }
    }
}

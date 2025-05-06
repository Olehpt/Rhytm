using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rhytm.Models;
namespace Rhytm.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public RegisterModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44370/api/Users/");
        }
        public void OnGet() { }
        [BindProperty]
        public User user { get; set; } = new User();
        public string message { get; set; } 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var response = await _httpClient.PostAsJsonAsync("register", user);
            if (response.IsSuccessStatusCode)
            {
                message = "Succes. New user in database";
                return Page();
            }
            message = "Error: " + await response.Content.ReadAsStringAsync();
            return Page();
        }
    }
}

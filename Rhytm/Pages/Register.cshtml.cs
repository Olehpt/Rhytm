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
        [BindProperty]
        public string ConfirmPasswordField { get; set; }
        public string ConfirmPasswordMessage { get; set; } 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (user.Password != ConfirmPasswordField)
            {
                ConfirmPasswordMessage = "Passwords do not match";
                return Page();
            }
            var response = await _httpClient.PostAsJsonAsync("register", user);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            ConfirmPasswordMessage = "Error: " + await response.Content.ReadAsStringAsync();
            return Page();
        }
    }
}

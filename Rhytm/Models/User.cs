using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace Rhytm.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
        public string? ProfilePicturePath { get; set; }
        public DateTime SignUpDate { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role? Role { get; set; } 
    }
}

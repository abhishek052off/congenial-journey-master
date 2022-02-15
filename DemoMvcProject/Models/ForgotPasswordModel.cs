using System.ComponentModel.DataAnnotations;

namespace DemoMvcProject.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
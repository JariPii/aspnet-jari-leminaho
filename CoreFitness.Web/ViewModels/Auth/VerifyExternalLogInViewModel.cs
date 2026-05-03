using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.ViewModels.Auth
{
    public class VerifyExternalLogInViewModel
    {
        public string Email { get; set; } = null!;
        public string? ReturnUrl { get; set; }

        [Required(ErrorMessage = "Please provide verification code")]        
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Varification code is 6 characters")]
        public string Code { get; set; } = null!;
    }
}

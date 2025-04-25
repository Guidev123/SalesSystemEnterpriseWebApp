using SalesSystem.UI.ViewModels.Validators;
using System.ComponentModel.DataAnnotations;

namespace SalesSystem.UI.ViewModels
{
    public record SignUpViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Document is required.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Document must be 11 characters.")]
        public string Document { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Birth Date is required.")]
        [AgeValidator(16)]
        public DateTime? BirthDate { get; set; }
    }
}

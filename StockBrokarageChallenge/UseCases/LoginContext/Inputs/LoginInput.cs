using System.ComponentModel.DataAnnotations;

namespace StockBrokarageChallenge.Application.UseCases.LoginContext.Inputs
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Cpf is required")]
        [StringLength(11, ErrorMessage = "Cpf length must be 11 characters")]
        public string CustomerCpf { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(6)]
        [Compare("Password", ErrorMessage = "Password doesnt match")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SSE.Identidade.API.Model
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [EmailAddress(ErrorMessage ="O campo {0} está em um formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres",  MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [Compare("Password", ErrorMessage ="As senhas não coferem")]
        public string ConfirmPassword { get; set; }
    }
}

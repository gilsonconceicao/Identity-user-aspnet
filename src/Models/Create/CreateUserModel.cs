using System.ComponentModel.DataAnnotations;

namespace UsuariosIdentity.src.Models.Create; 

public class CreateUserModel
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }
    [Required]
    public string Email { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace UsuariosIdentity.src.Models.Identity;
public class UserSignInModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}


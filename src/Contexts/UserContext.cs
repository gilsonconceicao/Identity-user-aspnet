using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosIdentity.src.Models.User;

namespace UsuariosIdentity.src.Contexts; 

public class UserContext : IdentityDbContext<User>
{
    public UserContext(DbContextOptions options) : base(options) { }
}

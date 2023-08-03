using AutoMapper;
using UsuariosIdentity.src.Models.Create;
using UsuariosIdentity.src.Models.User;

namespace UsuariosIdentity.src.Profiles; 
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, User>(); 
    }
}


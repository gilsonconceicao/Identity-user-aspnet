using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosIdentity.src.Models.Create;
using UsuariosIdentity.src.Models.Identity;
using UsuariosIdentity.src.Models.User;

namespace UsuariosIdentity.src.Services;

public class ServicesUser
{
    private IMapper _mapper; 
    private UserManager<User> _userManager;
    private SignInManager<User> _siginManager;
    private TokenServices _tokenServices; 

    public ServicesUser(
        UserManager<User> manageUser, 
        IMapper mapper, 
        SignInManager<User> signInManager, 
        TokenServices tokenServices
    )
    {
        _userManager = manageUser; 
        _siginManager = signInManager;
        _tokenServices = tokenServices;
        _mapper = mapper;
    }

    public async Task CreateUser(CreateUserModel user)
    {
        var newUser = _mapper.Map<User>(user); 
        var createUser = await _userManager.CreateAsync(newUser, user.Password); 

        if (!createUser.Succeeded) 
        {
            throw new ApplicationException("Erro ao cadastrar usuário");
        }
    }

    public async Task<string> SignIn(UserSignInModel user)
    {
        SignInResult result = await _siginManager.PasswordSignInAsync(
            user.UserName, 
            user.Password, 
            false, 
            false
        );
         
        if (!result.Succeeded) 
        {
            throw new ApplicationException("Erro ao cadastrar usuário");
        }
        var userSign = _siginManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.NormalizedUserName == user.UserName.ToUpper());

        var token = _tokenServices.GenerateToken(userSign); 

        return token; 
    }
}

using KennelPro.Interfaces.Authentication;
using KennelPro.Models.Authentication;

namespace KennelPro.Services.Authentication;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordService _passwordService;


    public AuthenticationService(
        IUserRepository userRepository,
        PasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }


    public async Task<User?> LoginAsync(
        string email,
        string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);


        if (user == null)
            return null;


        bool correctPassword =
            _passwordService.VerifyPassword(
                password,
                user.PasswordHash);


        if (!correctPassword)
            return null;


        return user;
    }
}
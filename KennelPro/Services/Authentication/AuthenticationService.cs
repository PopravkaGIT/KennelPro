using KennelPro.Helpers;
using KennelPro.Interfaces.Authentication;
using KennelPro.Interfaces.Kennels;
using KennelPro.Models.Authentication;
using KennelPro.Models.Kennels;

namespace KennelPro.Services.Authentication;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IKennelRepository _kennelRepository;
    private readonly PasswordService _passwordService;

    public AuthenticationService(
        IUserRepository userRepository,
        IKennelRepository kennelRepository,
        PasswordService passwordService)
    {
        _userRepository = userRepository;
        _kennelRepository = kennelRepository;
        _passwordService = passwordService;
    }

    /// <summary>
    /// Registers a new user with validation, trimming, and creates a kennel.
    /// </summary>
    public async Task<User?> RegisterAsync(
        string name,
        string email,
        string password,
        string kennelName)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;
        if (string.IsNullOrWhiteSpace(email))
            return null;
        if (string.IsNullOrWhiteSpace(password))
            return null;
        if (string.IsNullOrWhiteSpace(kennelName))
            return null;

        name = name.Trim();
        email = email.Trim().ToLowerInvariant();
        kennelName = kennelName.Trim();

        // Email already exists
        if (await _userRepository.GetByEmailAsync(email) != null)
            return null;

        // Create kennel
        Kennel kennel = new()
        {
            Id = Guid.NewGuid(),
            Name = kennelName,
            CreatedAt = DateTime.UtcNow
        };

        await _kennelRepository.AddAsync(kennel);

        // Create user
        User user = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            PasswordHash = _passwordService.HashPassword(password),
            KennelId = kennel.Id,
            EmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        // Save session
        SessionManager.SaveUser(user.Id);

        return user;
    }

    /// <summary>
    /// User login with trimming and lowercasing email.
    /// </summary>
    public async Task<User?> LoginAsync(
        string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;
        if (string.IsNullOrWhiteSpace(password))
            return null;

        email = email.Trim().ToLowerInvariant();

        User? user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return null;

        bool passwordCorrect = _passwordService.VerifyPassword(
            password,
            user.PasswordHash);

        if (!passwordCorrect)
            return null;

        SessionManager.SaveUser(user.Id);

        return user;
    }

    /// <summary>
    /// Logout current user.
    /// </summary>
    public void Logout()
    {
        SessionManager.Logout();
    }

    /// <summary>
    /// Returns true if user is logged in.
    /// </summary>
    public bool IsLoggedIn()
    {
        return SessionManager.IsLoggedIn();
    }

    /// <summary>
    /// Returns current user id.
    /// </summary>
    public Guid? GetCurrentUserId()
    {
        return SessionManager.GetCurrentUserId();
    }

    /// <summary>
    /// Returns current logged user.
    /// </summary>
    public async Task<User?> GetCurrentUserAsync()
    {
        Guid? userId = SessionManager.GetCurrentUserId();

        if (userId == null)
            return null;

        return await _userRepository.GetByIdAsync(userId.Value);
    }
}
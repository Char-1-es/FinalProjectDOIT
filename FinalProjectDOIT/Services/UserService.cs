using FinalProjectDOIT.Repos;
using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;

public class UserService
{
    private readonly IUserInterface<User, string> _userRepository;

    public UserService(IUserInterface<User, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetOneAsync(email);
    }

    public async Task LockOutUserAsync(string email)
    {
        await _userRepository.LockoutUser(email);
    }
}

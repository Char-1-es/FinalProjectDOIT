using FinalProjectDOIT.Data;
using FinalProjectDOIT.Entities;
using FinalProjectDOIT.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectDOIT.Repos
{
    public class UserRepository : IUserInterface<User, string>
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetOneAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task LockoutUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}

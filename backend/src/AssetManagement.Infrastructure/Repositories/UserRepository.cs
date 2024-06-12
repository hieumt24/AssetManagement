using AssetManagement.Application.Interfaces;
using AssetManagement.Domain.Entites;
using AssetManagement.Infrastructure.Common;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Infrastructure.Repositories
{
    public class UserRepository : BaseRepositoryAsync<User>, IUserRepositoriesAsync
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserRoles> AddUserRolesAysnc(UserRoles userRoles)
        {
            await _dbContext.UserRoles.AddAsync(userRoles);
            await _dbContext.SaveChangesAsync();
            return userRoles;
        }

        public string GeneratePassword(string userName, DateTime dateOfBirth)
        {
            string dob = dateOfBirth.ToString("ddMMyyyy");
            return $"{userName}@{dob}";
        }

        public async Task<string> GenerateUsername(string firstName, string lastName)
        {
            // Normalize names to lower case
            firstName = firstName.ToLower().Replace(" ", "");
            lastName = string.Join(" ", lastName.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));

            // Get the first letter of each part of the last name
            var lastNameParts = lastName.Split(' ');
            var lastNameInitials = string.Join("", lastNameParts.Select(part => part[0]));

            // Combine first name and initials of last names
            string baseUserName = firstName + lastNameInitials;

            string username = baseUserName;
            int count = 1;

            while (await IsUsernameExist(username))
            {
                username = baseUserName + count;
                count++;
            }

            return username;
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }
    }
}
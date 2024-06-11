﻿using AssetManagement.Application.Interfaces;
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

        public string GeneratePassword(string userName, DateTime dateOfBirth)
        {
            string dob = dateOfBirth.ToString("ddMMyyyy");
            return $"{userName}@{dob}";
        }

        public string GenerateStaffCode()
        {
            var nextValue = _dbContext.Database.ExecuteSqlRaw("select next value for UserIdSequence");
            return $"SD{nextValue:D4}";
        }

        public string GenerateUsername(string firstName, string lastName)
        {
            // Normalize names to lower case
            firstName = firstName.ToLower();
            lastName = lastName.ToLower();

            // Get the first letter of each part of the last name
            var lastNameParts = lastName.Split(' ');
            var lastNameInitials = string.Join("", lastNameParts.Select(part => part[0]));

            // Combine first name and initials of last names
            string username = firstName + lastNameInitials;

            return username;
        }
    }
}
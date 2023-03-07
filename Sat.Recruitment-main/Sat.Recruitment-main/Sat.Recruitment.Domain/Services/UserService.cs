using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers() 
        {
            return await _userRepository.GetAllUsers();
        }
       
        public async Task<User> AddUser(User user)
        {
            var allUsers = await GetAllUsers();

            user.NormalizeEmail();

            var errors = user.Validate(allUsers);

            if (errors != null && errors != "")
                throw new Exception(errors);

            user.CalculateMoney();

            return await _userRepository.AddUser(user);
        }

    }
}

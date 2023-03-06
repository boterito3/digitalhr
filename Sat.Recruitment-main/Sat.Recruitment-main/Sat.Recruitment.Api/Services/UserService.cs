using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
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

            CalculateMoney(user);

            return await _userRepository.AddUser(user);
        }

        public void CalculateMoney(User user) 
        {
            decimal percentage = 0;

            switch (user.UserType)
            {
                case "Normal":
                    if (user.Money > 100)
                        percentage = Convert.ToDecimal(0.12);
                    else if (user.Money < 100 && user.Money > 10)
                        percentage = Convert.ToDecimal(0.8);
                    break;
                case "SuperUser":
                    if (user.Money > 100)
                        percentage = Convert.ToDecimal(0.20);
                    break;
                case "Premium":
                    if (user.Money > 100)
                        percentage = Convert.ToDecimal(2);
                    break;
            }

            user.Money = user.Money + (user.Money * percentage);
        }


    }
}

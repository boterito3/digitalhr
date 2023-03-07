using System;
using System.Dynamic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class ExtensionMethodsTests
    {
        [Fact]
        public void CalculateMoney_Normal_Success()
        {
            var user = new User() { UserType = "Normal", Money = 50};

            user.CalculateMoney();

            Assert.Equal(90, user.Money);
        }

        [Fact]
        public void CalculateMoney_Normal_Fail()
        {
            var user = new User() { UserType = "Normal", Money = 70 };

            user.CalculateMoney();

            Assert.NotEqual(100, user.Money);
        }

        [Fact]
        public void CalculateMoney_SuperUser_Success()
        {
            var user = new User() { UserType = "SuperUser", Money = 200 };

            user.CalculateMoney();

            Assert.Equal(240, user.Money);
        }

        [Fact]
        public void CalculateMoney_SuperUser_Fail()
        {
            var user = new User() { UserType = "SuperUser", Money = 70 };

            user.CalculateMoney();

            Assert.NotEqual(100, user.Money);
        }

        [Fact]
        public void CalculateMoney_Premium_Success()
        {
            var user = new User() { UserType = "Premium", Money = 200 };

            user.CalculateMoney();

            Assert.Equal(400, user.Money);
        }

        [Fact]
        public void CalculateMoney_Premium_Fail()
        {
            var user = new User() { UserType = "Premium", Money = 70 };

            user.CalculateMoney();

            Assert.NotEqual(100, user.Money);
        }

        [Fact]
        public void ConvertToTextLine_Success()
        {
            var user = new User() 
            { 
                Name = "Franco", 
                Email = "Franco.Perez@gmail.com", 
                Phone = "+534645213542", 
                Address = "Alvear y Colombres", 
                UserType = "Premium", 
                Money = 112234 
            };

            var line = user.ConvertToTextLine();

            Assert.Equal("Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234", line);
        }

    }
}

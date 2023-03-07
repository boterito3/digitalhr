using System;
using System.Dynamic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Domain.Models;
using Xunit;
using Sat.Recruitment.DataAccess;

namespace Sat.Recruitment.Test
{

    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserRepositoryTests
    {
        [Fact]
        public void GetFileReader_Success()
        {
            var userRepository = new UserRepository();

            var result = userRepository.GetFileReader();

            Assert.IsType<StreamReader>(result);

            result.Close();
        }

        [Fact]
        public void GetFileWriter_Success()
        {
            var userRepository = new UserRepository();

            var result = userRepository.GetFileWriter();

            Assert.IsType<StreamWriter>(result);

            result.Close();
        }

        [Fact]
        public void TransformLineIntoUser_Success()
        {
            var userRepository = new UserRepository();
            var line = "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium,112234";

            var result = userRepository.TransformLineIntoUser(line);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TransformLineIntoUser_Exception()
        {
            var userRepository = new UserRepository();
            var line = "Franco,Franco.Perez@gmail.com,+534645213542,Alvear y Colombres,Premium";

            Assert.Throws<IndexOutOfRangeException>(() => userRepository.TransformLineIntoUser(line));
        }
    }
}

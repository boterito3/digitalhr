using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.DataAccess
{
    public class UserRepository: IUserRepository
    {
        private const string _filePath = "/Files/Users.txt";
        public async Task<List<User>> GetAllUsers()
        {
            List<User> allUsers = new List<User>();
            var reader = GetFileReader();
            try
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    allUsers.Add(user);
                }
                reader.Close();
            }
            catch (System.Exception)
            {
                reader.Close();
                throw;
            }
            
            return allUsers;
        }

        public async Task<User> AddUser(User user)
        {
            var writer = GetFileWriter();
            var newLine = $"\n{user.ConverToTextLine()}";
            await writer.WriteLineAsync(newLine);
            writer.Close();
            return user;
        }

        private StreamReader GetFileReader()
        {
            var path = Directory.GetCurrentDirectory() + _filePath;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        private StreamWriter GetFileWriter()
        {
            var path = Directory.GetCurrentDirectory() + _filePath;
            StreamWriter reader = new StreamWriter(path, append: true);
            return reader;
        }
    }
}

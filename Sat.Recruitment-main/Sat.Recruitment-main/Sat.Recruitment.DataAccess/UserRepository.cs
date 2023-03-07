using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.DataAccess
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
                    var user = TransformLineIntoUser(line);
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

        public User TransformLineIntoUser(string line) 
        {
            var user = new User
            {
                Name = line.Split(',')[0].ToString(),
                Email = line.Split(',')[1].ToString(),
                Phone = line.Split(',')[2].ToString(),
                Address = line.Split(',')[3].ToString(),
                UserType = line.Split(',')[4].ToString(),
                Money = decimal.Parse(line.Split(',')[5].ToString()),
            };
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            var writer = GetFileWriter();
            var newLine = $"\n{user.ConvertToTextLine()}";
            await writer.WriteLineAsync(newLine);
            writer.Close();
            return user;
        }

        public StreamReader GetFileReader()
        {
            var path = Directory.GetCurrentDirectory() + _filePath;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public StreamWriter GetFileWriter()
        {
            var path = Directory.GetCurrentDirectory() + _filePath;
            StreamWriter reader = new StreamWriter(path, append: true);
            return reader;
        }
    }
}

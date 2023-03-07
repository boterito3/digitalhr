using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Domain.Models
{
    public static class ExtensionMethods
    {
        public static string Validate(this User user, List<User> allUsers) 
        {
            var validationErrors = new StringBuilder();
            if (string.IsNullOrEmpty(user.Name))
                validationErrors.Append("The name is required");
            if (string.IsNullOrEmpty(user.Email))
                validationErrors.Append("The email is required");
            if (string.IsNullOrEmpty(user.Address))
                validationErrors.Append("The address is required");
            if (string.IsNullOrEmpty(user.Phone))
                validationErrors.Append("The phone is required");
            if (allUsers.Any(x => x.Email == user.Email) || 
                allUsers.Any(x => x.Phone == user.Phone) ||
                (allUsers.Any(x => x.Name == user.Name) && allUsers.Any(x => x.Address == user.Address)))
                validationErrors.Append("User duplicated");

            return validationErrors.ToString();
        }

        public static void NormalizeEmail(this User user)
        {
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }

        public static string ConvertToTextLine(this User user)
        {
            return $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
        }

        public static void CalculateMoney(this User user)
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
                        percentage = Convert.ToDecimal(1);
                    break;
            }

            user.Money = user.Money + (user.Money * percentage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Api.Models
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

        public static string ConverToTextLine(this User user)
        {
            return $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashPasswords;

namespace PR5Modyli.Models
{
    internal class UserService
    {
        public bool RegisterUser(string login, string password)
        {
            using (var db = new InsuranceMedicalCompanyEntities())
            {
                if (db.Users.Any(u => u.Login == login))
                {
                    Console.WriteLine("Пользователь с таким логином уже существует.");
                    return false;
                }

                var user = new Users
                {
                    Login = login,
                    PasswordHash = Hash.GetPasswordHash(password)
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }
    }
}

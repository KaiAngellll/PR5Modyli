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
        public bool RegisterUser(string login, string password, int roleId)
        {
            using (var db = new InsuranceMedicalCompanyEntities())
            {
                if (db.Users.Any(u => u.Login == login))
                {
                    Console.WriteLine("пользователь с таким логином уже существует");
                    return false;
                }

                var role = db.Roles.FirstOrDefault(r => r.ID_Roles == roleId);

                if (role == null)
                {
                    Console.WriteLine("такой роли не существует");
                    return false;
                }

                var user = new Users
                {
                    Login = login,
                    PasswordHash = Hash.GetPasswordHash(password),
                    ID_Roles = roleId
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public Users LoginUser(string login, string password)
        {
            using (var db = new InsuranceMedicalCompanyEntities())
            {
                string passwordHash = Hash.GetPasswordHash(password);

                var user = db.Users.FirstOrDefault(u =>
                    u.Login == login &&
                    u.PasswordHash == passwordHash);

                return user;
            }
        }

        public void ShowUsers()
        {
            using (var db = new InsuranceMedicalCompanyEntities())
            {
                var users = db.Users
                    .Join(
                        db.Roles,
                        user => user.ID_Roles,
                        role => role.ID_Roles,
                        (user, role) => new
                        {
                            user.UserId,
                            user.Login,
                            role.RoleName
                        })
                    .ToList();

                Console.WriteLine();
                Console.WriteLine("СПИСОК ПОЛЬЗОВАТЕЛЕЙ");

                foreach (var user in users)
                {
                    Console.WriteLine(
                        $"ID: {user.UserId} | Логин: {user.Login} | Роль: {user.RoleName}");
                }
            }
        }

        public bool ChangeUserRole(int userId, int newRoleId)
        {
            using (var db = new InsuranceMedicalCompanyEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == userId);

                if (user == null)
                {
                    Console.WriteLine("пользователь с таким ID не найден");
                    return false;
                }

                var role = db.Roles.FirstOrDefault(r => r.ID_Roles == newRoleId);

                if (role == null)
                {
                    Console.WriteLine("такой роли не существует");
                    return false;
                }

                user.ID_Roles = newRoleId;

                db.SaveChanges();

                return true;
            }
        }
    }
}
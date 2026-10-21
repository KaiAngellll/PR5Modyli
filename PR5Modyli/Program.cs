using HashPasswords;
using PR5Modyli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR5Modyli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("создание новой учетной записи пользователя");
            Console.WriteLine();

            Console.Write("введите логин пользователя: ");
            string login = Console.ReadLine();

            Console.Write("введите пароль пользователя: ");
            string password = Console.ReadLine();

            UserService service = new UserService();

            try
            {
                bool result = service.RegisterUser(login, password);

                if (result)
                {
                    Console.WriteLine("учетная запись добавлена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ошибка при регистрации: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

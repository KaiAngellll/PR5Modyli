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
            Console.WriteLine("СИСТЕМА СТРАХОВОЙ МЕДИЦИНСКОЙ КОМПАНИИ");
            Console.WriteLine();
            Console.WriteLine("Вход в систему");
            Console.WriteLine();

            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            UserService service = new UserService();

            try
            {
                Users user = service.LoginUser(login, password);

                if (user == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Неверный логин или пароль!!!");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Вход выполнен успешно!");
                    Console.WriteLine("Логин: " + user.Login);

                    switch (user.ID_Roles)
                    {
                        case 1:
                            Console.WriteLine("Роль: Клиент");
                            ClientMenu();
                            break;

                        case 2:
                            Console.WriteLine("Роль: Администратор");
                            AdminMenu();
                            break;

                        case 3:
                            Console.WriteLine("Роль: Инспектор");
                            InspectorMenu();
                            break;

                        default:
                            Console.WriteLine("Неизвестная роль пользователя");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Ошибка при входе: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void ClientMenu()
        {
            Console.WriteLine();
            Console.WriteLine("МЕНЮ КЛИЕНТА");
            Console.WriteLine("пока ничего нет");
        }

        static void AdminMenu()
        {
            UserService service = new UserService();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("МЕНЮ АДМИНИСТРАТОРА");
                Console.WriteLine("1. Добавить пользователя");
                Console.WriteLine("2. Посмотреть пользователей");
                Console.WriteLine("3. Изменить роль пользователя");
                Console.WriteLine("4. Выйти");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser(service);
                        break;

                    case "2":
                        service.ShowUsers();
                        break;

                    case "3":
                        ChangeUserRole(service);
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("неверный пункт меню");
                        break;
                }
            }
        }

        static void AddUser(UserService service)
        {
            Console.WriteLine();
            Console.WriteLine("ДОБАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯ");

            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Выберите роль:");
            Console.WriteLine("1. Клиент");
            Console.WriteLine("2. Администратор");
            Console.WriteLine("3. Инспектор");

            Console.Write("Роль: ");
            string roleInput = Console.ReadLine();

            if (!int.TryParse(roleInput, out int roleId) ||
                roleId < 1 ||
                roleId > 3)
            {
                Console.WriteLine("неверная роль!");
                return;
            }

            bool result = service.RegisterUser(login, password, roleId);

            if (result)
            {
                Console.WriteLine("пользователь успешно добавлен");
            }
        }

        static void ChangeUserRole(UserService service)
        {
            Console.WriteLine();
            Console.WriteLine("ИЗМЕНЕНИЕ РОЛИ ПОЛЬЗОВАТЕЛЯ");

            service.ShowUsers();

            Console.WriteLine();

            Console.Write("Введите ID пользователя: ");
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int userId))
            {
                Console.WriteLine("некорректный ID");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Выберите новую роль:");
            Console.WriteLine("1. Клиент");
            Console.WriteLine("2. Администратор");
            Console.WriteLine("3. Инспектор");

            Console.Write("новая роль: ");
            string roleInput = Console.ReadLine();

            if (!int.TryParse(roleInput, out int roleId) ||
                roleId < 1 ||
                roleId > 3)
            {
                Console.WriteLine("неверная роль");
                return;
            }

            bool result = service.ChangeUserRole(userId, roleId);

            if (result)
            {
                Console.WriteLine("роль пользователя успешно изменена");
            }
        }

        static void InspectorMenu()
        {
            Console.WriteLine();
            Console.WriteLine("МЕНЮ ИНСПЕКТОРА");
            Console.WriteLine("пока пусто");
        }
    }
}
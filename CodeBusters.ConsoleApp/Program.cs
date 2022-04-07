using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CodeBusters.ConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string codeBustersUrl = "https://localhost:5001/api/User";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program program = new Program();
            program.UIMenu();
        }

        private void UIMenu()
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Who ya gonna call? Code Busters! \n\n" +
                "Enter a number associated with the menu item below:\n" +
                "1. Register new user\n" +
                "2. View list of users\n" +
                "3. Find user by Id\n" +
                "4. Edit existing user\n" +
                "5. Delete an existing user\n" +
                "6. Close application\n");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        RegisterUser().Wait();
                        break;
                    case "2":
                        GetAllUsers().Wait();
                        break;
                    case "3":
                        GetUserById().Wait();
                        break;
                    case "4":
                        EditUserById().Wait();
                        break;
                    case "5":
                        DeleteUser().Wait();
                        break;
                    case "6":
                        Console.WriteLine("Thank you for being a part of the CodeBusters community!\n");
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task RegisterUser()
        {
            Console.Clear();
            Console.Write("New User Registration\n\n" +
            "Enter your email address: ");
            string email = Console.ReadLine();
            Console.Write("Enter your name: ");
            string fullName = Console.ReadLine();
            Console.Write("Create a password (min 8 characters): ");
            string password = Console.ReadLine();
            Console.Write("Confirm your password: ");
            string confirmPassword = Console.ReadLine();

            var user = new User()
            {
                Email = email,
                FullName = fullName,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            // var json = JsonSerializer.Serialize<User>(user);
            var response = await client.PostAsJsonAsync<User>($"{codeBustersUrl}/Register", user);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("New user successfully registered.");
            }
            else Console.WriteLine("There was a problem with registration. Please try again.");
        }

        static async Task EditUserById()
        {
            Console.Clear();
            Console.Write("Edit an existing user\n\nEnter the Id of the user you wish to edit: ");
            int userId = int.Parse(Console.ReadLine()!);

            var response = await client.GetAsync($"{codeBustersUrl}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                User user = await client.GetFromJsonAsync<User>($"{codeBustersUrl}/{userId}");
                Console.Write($"User {userId} was found.\n" +
                "Enter the new email address: ");
                string email = Console.ReadLine();
                Console.Write("Enter the new name: ");
                string fullName = Console.ReadLine();
                Console.Write("Create a new password (min 8 characters): ");
                string password = Console.ReadLine();
                Console.Write("Confirm your new password: ");
                string confirmPassword = Console.ReadLine();

                user.Id = userId;
                user.Email = email;
                user.FullName = fullName;
                user.Password = password;
                user.ConfirmPassword = confirmPassword;

                var saved = await client.PutAsJsonAsync<User>($"{codeBustersUrl}", user);
                if (saved.IsSuccessStatusCode)
                {
                    Console.WriteLine($"User {userId} successfully updated.");
                }
                else Console.WriteLine($"There was a problem updating User {userId}. Please try again.");
            }
            else Console.WriteLine($"User {userId} was not found.");
        }

        static async Task GetUserById()
        {
            Console.Clear();
            Console.Write("Find user by Id\n\nEnter the Id of the user you are looking for: ");
            int userId = int.Parse(Console.ReadLine()!);

            var response = await client.GetAsync($"{codeBustersUrl}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                User user = await client.GetFromJsonAsync<User>($"{codeBustersUrl}/{userId}");
                Console.WriteLine($" \nHere is the user info you requested: \n \n" +
                $"Id: {user.Id} \n" +
                $"Name: {user.FullName} \n" +
                $"Email: {user.Email} \n" +
                $"Is Admin?: {user.isAdmin} \n");
            }
            else Console.WriteLine($"User Id {userId} was not found.");
        }

        static async Task GetAllUsers()
        {
            Console.Clear();
            Console.WriteLine("List of all users:\n");
            List<User> users = await client.GetFromJsonAsync<List<User>>($"{codeBustersUrl}");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id} \n" +
            $"Name: {user.FullName} \n");
            }

        }

        static async Task DeleteUser()
        {
            Console.Clear();
            Console.Write("Delete a User\n\nEnter the Id of the user you wish to delete: ");
            int userId = int.Parse(Console.ReadLine()!);

            var response = await client.GetAsync($"{codeBustersUrl}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                await client.DeleteAsync($"{codeBustersUrl}/{userId}");
                Console.WriteLine($"User {userId} was successfully deleted.");
            }
            else Console.WriteLine($"User Id {userId} was not found.");
        }

        [Serializable]
        public class User
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("fullName")]
            public string FullName { get; set; }
            [JsonPropertyName("email")]
            public string Email { get; set; }
            [JsonPropertyName("isAdmin")]
            public bool isAdmin { get; set; }
            [JsonPropertyName("password")]
            public string Password { get; set; }
            [JsonPropertyName("confirmPassword")]
            public string ConfirmPassword { get; set; }
        }
    }
}


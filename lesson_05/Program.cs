using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace college_DB
{
    class Program
    {
        static string conn_str = Connect();
        static bool is_running = true;
        static void Main()
        {
      
            ShowAllStudents(conn_str);
            Console.WriteLine("Введите номер задачи!");
            while (is_running)
            {
                Console.WriteLine("-----Меню-----");
                Console.WriteLine("1. Список студентов");
                Console.WriteLine("0. Выход");
                string choise = Console.ReadLine();
                switch(choise)
                {
                    case "1":
                        ShowAllStudents(conn_str);
                        break;
                    case "0":
                        is_running = false;
                        break;
                    default:
                        Console.WriteLine("такой команды нет");
                        break;
                }
                if (is_running)
                {
                    Console.WriteLine("Нажмите любую кнопку для продолжения");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }
        static string Connect()
        {
            string connectionString =
            "Data Source=COMP11A1\\SQLEXPRESS;" +
            "Initial Catalog=College_DB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

            return connectionString;
        }

        static void AddGroup(string conn_str, string name)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql = "INSERT INTO dbo.Groups(GroupName) VALUES(@name)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void AddStudets(string conn_str, string first_name, string last_name, string age, string group_id)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql = "INSERT INTO dbo.Students(FirstName, LastName, Age, GroupId) " +
                "VALUES(@firstName, @lastName, @age, @groupId)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@firstName", first_name);
            command.Parameters.AddWithValue("@lastName", last_name);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@groupId", group_id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void ShowAllStudents(string conn_str)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            
            Console.WriteLine("Connected!");
            string sql = "SELECT * FROM dbo.Students";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                // первый способ обратиться к Reader чтобы он вывел данные, т.к. он сам по себе не может их писать
                int id = reader.GetInt32(0);
                string first_name = reader.GetString(1);
                string last_name = reader.GetString(2);
                int age = reader.GetInt32(3);
                int group_id = reader.GetInt32(4);

                Console.WriteLine($"{id}, {first_name}, {last_name}, {age}, {group_id}");


                // второй способ напрямую обратиться к reader
                // Console.WriteLine($"{reader["StudentId"]}, {reader["FirstName"]}, {reader["LastName"]}, {reader["Age"]}, {reader["GroupId"]}");
            }
            connection.Close();

        }
    }
}

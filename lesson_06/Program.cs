using System;
using college_DB.Data;
using System.Data.SqlClient;

namespace college_DB
{
    class Program
    {
        static string conn_str = "Data Source=COMP11A1\\SQLEXPRESS;" +
            "Initial Catalog=College_DB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";
        static bool is_running = true;
        static StudentRepository student_repo = new StudentRepository(conn_str);
        static GroupRepository group_repo = new GroupRepository(conn_str);

        static void Main()
        {

            ShowAllStudents();
            Console.WriteLine("Введите номер задачи!");
            while (is_running)
            {
                Console.WriteLine("-----Меню-----");
                Console.WriteLine("1. Список студентов");
                Console.WriteLine("2. Список групп");
                Console.WriteLine("0. Выход");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        ShowAllStudents();
                        break;
                    case "2":
                        ShowAllGroup();
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
        static void ShowAllStudents()
        {
            var students = student_repo.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
            
        }
        static void ShowAllGroup()
        {
            var groups = group_repo.GetAllGroups();
            foreach (var group in groups)
            {
                Console.WriteLine(group.ToString());
            }
        }
        static void ShowStudentFromGroup(string conn_str, string id)
        {
            

        }
    }
}

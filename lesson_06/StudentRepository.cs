using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using college_DB.Models;
using Microsoft.Data.SqlClient;

namespace college_DB.Data
{
    class StudentRepository
    {
        private readonly string _conn_str;
        public StudentRepository(string connection_string)
        {
            _conn_str = connection_string;
        }
        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            var connection = new SqlConnection(_conn_str);
            connection.Open();
            string sql = "SELECT StudentId, FirstName, LastName, Age FROM Students";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                students.Add(
                   new Student {
                        StudentId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Age = reader.GetInt32(3),

                   }
                );
            }
            return students;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using college_DB.Models;
using Microsoft.Data.SqlClient;
using Dapper;

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
           using (var connection = new SqlConnection(_conn_str))
            {
                return connection.Query<Student>("SELECT StudentId, FirstName, LastName, Age FROM Students").ToList();
            }
        }
        public Student GetStudentById(string Id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                return connection.QueryFirstOrDefault<Student>("SELECT StudentId, FirstName, LastName, Age FROM Students",
                    new { @id = Id });
            }
        }
        public void CreateStudent(string first_name, string last_name, string age, string group_id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
               var sql ="Insert INTO Students(FirstName, LastName, Age, GroupId)" +
                "VALUES(@firstName, @lastName, @age, @groupId SELECT" +
                "CAST(SCOPE_IDENTITY() AS INT);";

                connection.QuerySingle(sql);
            }
        }
    }
}

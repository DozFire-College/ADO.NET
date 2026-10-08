using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using college_DB.Models;
using Microsoft.Data.SqlClient;

namespace college_DB.Data
{
    class GroupRepository
    {
        private readonly string _conn_str;
        public GroupRepository(string connection_string)
        {
            _conn_str = connection_string;
        }
        public List<Group> GetAllGroups()
        {
            var groups = new List<Group>();
            var connection = new SqlConnection(_conn_str);
            connection.Open();
            string sql = "SELECT GroupId, GroupName FROM Groups";
            var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                groups.Add(
                   new Group
                   {
                       GroupId = reader.GetInt32(0),
                       GroupName = reader.GetString(1),   
                   }
                );
            }
            return groups;
        }
    }
}

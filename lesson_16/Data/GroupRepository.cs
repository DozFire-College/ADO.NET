using System;
using System.Collections.Generic;
using ASP_College.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Linq;

namespace College_DB.Data
{
    class GroupRepository
    {
        private readonly string _conn_str;
        public GroupRepository(string connection_string)
        {
            _conn_str = connection_string;
        }
        public List<Group> GetAllGroups2()
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
            connection.Close();
            return groups;
        }

        public List<Group> GetAllGroups()
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                return connection
                    .Query<Group>("SELECT GroupId, GroupName FROM Groups")
                    .ToList();
            }
        }

        public Group GetGroupById(string id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                return connection
                    .QueryFirstOrDefault<Group>("SELECT g.GroupId, g.GroupName, s.FirstName, s.LastName FROM Groups AS g, Students AS s WHERE g.GroupId = @id",
                    new { @id = id });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Repositories
{
    class GroupRawSqlRepository : IGroupRepository
    {
        private readonly string _connectionString;
        public GroupRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Group> GetAllGroups()
        {
            var result = new List<Group>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [Id], [Name] from [Groups]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Group
                            {
                                IdOfGroup = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"])
                            });

                        }
                    }
                }
            }

            return result;
        }

        public void AddGroup(Group group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [Groups]
                        values
                            (@name)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = group.Name;

                    group.IdOfGroup = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public List<Group> GetGroupByName(string name)
        {
            var result = new List<Group>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select [Id], [Name] from [Groups]
                                            where [Name] = name";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Group
                            {
                                IdOfGroup = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"])
                            });

                        }
                    }
                }
            }

            return result;
        }
    }
}

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
    class StudentInGroupRawSqlRepository : IStudentInGroupRepository
    {
        private readonly string _connectionString;
        public StudentInGroupRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<StudentInGroup> GetStudentsInGroups()
        {
            var result = new List<StudentInGroup>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [IdOfGroup], [IdOfStudent] from [StudentsInGroups]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentInGroup
                            {
                                IdOfGroup = Convert.ToInt32(reader["IdOfGroup"]),
                                IdOfStudent = Convert.ToInt32(reader["IdOfStudent"]),
                            });

                        }
                    }
                }
            }

            return result;
        }

        public List<StudentInGroup> GetStudentsInGroup(int id)
        {
            var result = new List<StudentInGroup>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    StudentInGroup studentInGroups = new StudentInGroup();
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    command.CommandText =
                        @"select [IdOfStudent] from [StudentsInGroups] 
                        where [IdOfGroup] = @id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentInGroup
                            {
                                IdOfGroup = id,
                                IdOfStudent = Convert.ToInt32(reader["IdOfStudent"]),
                            });

                        }
                    }
                }
            }
            return result;
        }

        public void AddStudentInGroup(int idOfGroup, int idOfStudent)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [StudentsInGroups]
                        values
                            (@idOfGroup, @idOfStudent)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@idOfGroup", SqlDbType.NVarChar).Value = idOfGroup;
                    command.Parameters.Add("@idOfStudent", SqlDbType.NVarChar).Value = idOfStudent;

                    idOfGroup = Convert.ToInt32(command.ExecuteScalar());
                    idOfStudent = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public List<StudentInGroup> GetStudentInGroup(int idOfStudent, int idOfGroup)
        {
            var result = new List<StudentInGroup>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    StudentInGroup studentInGroups = new StudentInGroup();
                    command.Parameters.Add("@idOfStudent", SqlDbType.NVarChar).Value = idOfStudent;
                    command.Parameters.Add("@idOfGroup", SqlDbType.NVarChar).Value = idOfGroup;
                    command.CommandText =
                        @"select [IdOfStudent], [idOfGroup] from [StudentsInGroups] 
                        where [idOfStudent] = @idOfStudent and [IdOfGroup] = @idOfGroup";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentInGroup
                            {
                                IdOfGroup = Convert.ToInt32(reader["IdOfGroup"]),
                                IdOfStudent = Convert.ToInt32(reader["IdOfStudent"]),
                            });

                        }
                    }
                }
            }
            return result;
        }
    }
}

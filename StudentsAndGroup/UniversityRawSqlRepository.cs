using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace University
{
    class UniversityRawSqlRepository : IUniversityRepository
    {
        private readonly string _connectionString;

        public UniversityRawSqlRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public List<Students> GetAllStudents()
        {
            var result = new List<Students>();

            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText = "select [Id], [Name], [Age] from [Students]";

                    using ( var reader = command.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            result.Add( new Students
                            {
                                Id = Convert.ToInt32( reader["Id"] ),
                                Name = Convert.ToString( reader["Name"] ),
                                Age = Convert.ToInt32( reader["Age"])
                            } );
                        }
                    }
                }
            }

            return result;
        }

        public List<Students> GetStudentById(int id)
        {
            var result = new List<Students>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select [Id], [Name], [Age] from [Students]
                                          where [Id] = @id";

                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Students
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Age = Convert.ToInt32(reader["Age"])
                            });
                        }
                    }
                }
            }
            return result;
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

        public List<StudentsInGroups> GetStudentsInGroups()
        {
            var result = new List<StudentsInGroups>();

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
                            result.Add(new StudentsInGroups
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

        public List<StudentsInGroups> GetStudentsInGroup(int id)
        {
            var result = new List<StudentsInGroups>();
            var result2 = new List<StudentsInGroups>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    StudentsInGroups studentsInGroups = new StudentsInGroups();
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    command.CommandText =
                        @"select [IdOfStudent] from [StudentsInGroups] 
                        where [IdOfGroup] = @id";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new StudentsInGroups
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

        public void AddStudents( Students student )
        {
            using ( var connection = new SqlConnection( _connectionString ) )
            {
                connection.Open();
                using ( SqlCommand command = connection.CreateCommand() )
                {
                    command.CommandText =
                        @"insert into [Students]
                        values
                            (@name, @age)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = student.Name;
                    command.Parameters.Add("@age", SqlDbType.NVarChar).Value = student.Age;

                    student.Id = Convert.ToInt32( command.ExecuteScalar() );
                }
            }
        }

        public void AddGroups(Group group)
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

        public void AddStudentInGroup(StudentsInGroups studentsInGroups)
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

                    command.Parameters.Add("@idOfGroup", SqlDbType.NVarChar).Value = studentsInGroups.IdOfGroup;
                    command.Parameters.Add("@idOfStudent", SqlDbType.NVarChar).Value = studentsInGroups.IdOfStudent;

                    studentsInGroups.IdOfGroup = Convert.ToInt32(command.ExecuteScalar());
                    studentsInGroups.IdOfStudent = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
    }
}

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
    public class StudentRawSqlRepository : IStudentRepository
    {
        private readonly string _connectionString;
        public StudentRawSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            var result = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select [Id], [Name], [Age] from [Students]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Student
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

        public void AddStudents(Student student)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        @"insert into [Students]
                        values
                            (@name, @age)
                        select SCOPE_IDENTITY()";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = student.Name;
                    command.Parameters.Add("@age", SqlDbType.NVarChar).Value = student.Age;

                    student.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public List<Student> GetStudentById(int id)
        {
            var result = new List<Student>();

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
                            result.Add(new Student
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
        public List<Student> GetStudentByNameAndAge(string name, int age)
        {
            var result = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select [Id], [Name], [Age] from [Students]
                                          where [Name] = @name and [Id] = @id";

                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@id", SqlDbType.NVarChar).Value = age;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Student
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
    }
}

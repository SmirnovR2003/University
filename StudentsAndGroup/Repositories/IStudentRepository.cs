using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Repositories
{
    interface IStudentRepository
    {
        List<Student> GetAllStudents();
        void AddStudents(Student student);
        List<Student> GetStudentById(int id);
        List<Student> GetStudentByNameAndAge(string name, int age);
    }
}

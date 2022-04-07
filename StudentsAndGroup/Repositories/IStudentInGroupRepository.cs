using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Repositories
{
    interface IStudentInGroupRepository
    {
        void AddStudentInGroup(int idOfStudent, int idOfGroup);
        List<StudentInGroup> GetStudentsInGroup(int id);
        List<StudentInGroup> GetStudentsInGroups();
        List<StudentInGroup> GetStudentInGroup(int idOfStudent, int idOfGroup);
    }
}

using System.Collections.Generic;

namespace University
{
    interface IUniversityRepository
    {
        void AddStudents( Students student );
        List<Students> GetAllStudents();
        List<Students> GetStudentById( int id );
        List<Group> GetAllGroups();
        void AddGroups(Group group);
        void AddStudentInGroup(StudentsInGroups studentsInGroups);
        List<StudentsInGroups> GetStudentsInGroup(int id);
        List<StudentsInGroups> GetStudentsInGroups();
    }
}
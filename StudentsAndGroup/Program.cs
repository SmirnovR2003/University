using System;
using System.Collections.Generic;

namespace University
{
    class Program
    {
        private static string _connectionString = @"Data Source=LAPTOP-Q9O8IFD3\SQLEXPRESS;Initial Catalog=GroopsAndStudents;Pooling=true;Integrated Security=SSPI";

        static void Main(string[] args)
        {
            IUniversityRepository studentRepository = new UniversityRawSqlRepository(_connectionString);
            IUniversityRepository groupRepository = new UniversityRawSqlRepository(_connectionString);

            Console.WriteLine("Доступные команды:");
            Console.WriteLine("get-all-students - показать список студентов");
            Console.WriteLine("get-all-groups - показать список групп");
            Console.WriteLine("get-students-in-group - показать список студентов в группе");
            Console.WriteLine("add-student - добавить студента");
            Console.WriteLine("add-group - добавить группу");
            Console.WriteLine("add-student-in-group - добавить студента в группу");
            Console.WriteLine("exit - выйти из приложения");

            while (true)
            {
                string command = Console.ReadLine();
                bool isHave = true;

                if (command == "get-all-students")
                {
                    List<Students> students = studentRepository.GetAllStudents();
                    if (students != null)
                    {
                        foreach (Students student in students)
                        {
                            Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Список студентов пуст");
                    }
                    
                }
                else if (command == "get-all-groups")
                {
                    List<Group> groups = groupRepository.GetAllGroups();
                    if (groups != null)
                    {
                        foreach (Group group in groups)
                        {
                            Console.WriteLine($"Id: {group.IdOfGroup}, Name: {group.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Список групп пуст");
                    }
                }
                else if (command == "get-students-in-group")
                {
                    Console.WriteLine("Введите id группы");
                    int id = Convert.ToInt32(Console.ReadLine());
                    List<StudentsInGroups> StudentsInGroup = studentRepository.GetStudentsInGroup(id);
                    if (StudentsInGroup != null)
                    {
                        foreach (StudentsInGroups studentsInGroup in StudentsInGroup)
                        {
                            List<Students> students = studentRepository.GetStudentById(studentsInGroup.IdOfStudent);
                            foreach (Students student in students)
                            {
                                Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("В группе нет студентов");
                    }
                }
                else if (command == "add-student")
                {
                    Console.WriteLine("Введите имя студента");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите возраст студента");
                    int age = Convert.ToInt32(Console.ReadLine());

                    List<Students> students = studentRepository.GetAllStudents();
                    foreach (Students student in students)
                    {
                        if ((student.Name == name) && (student.Age == age))
                        {
                            isHave = false;
                        }
                    }
                    if (isHave)
                    {
                        studentRepository.AddStudents(new Students
                        {
                            Name = name,
                            Age = age
                        });
                        Console.WriteLine("Успешно добавлено");
                    }
                    else
                    {
                        Console.WriteLine("Такой студент уже существует");
                    }
                }
                else if (command == "add-group")
                {
                    Console.WriteLine("Введите название группы");
                    string name = Console.ReadLine();
                    List<Group> groups = groupRepository.GetAllGroups();
                    foreach (Group group in groups)
                    {
                        if (group.Name == name)
                        {
                            isHave = false;
                        }
                    }
                    if (isHave)
                    {
                        groupRepository.AddGroups(new Group
                        {
                            Name = name
                        });
                        Console.WriteLine("Успешно добавлено");
                    }
                    else
                    {
                        Console.WriteLine("Такая группа уже существует");
                    }
                }
                else if (command == "add-student-in-group")
                {
                    Console.WriteLine("Введите id группы");
                    int idOfGroup = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите id студента");
                    int idOfStudent = Convert.ToInt32(Console.ReadLine());
                    List<StudentsInGroups> studentsInGroups = groupRepository.GetStudentsInGroups();
                    foreach (StudentsInGroups studentsInGroup in studentsInGroups)
                    {
                        if ((studentsInGroup.IdOfGroup == idOfGroup) && (studentsInGroup.IdOfStudent == idOfStudent))
                        {
                            isHave = false;
                        }
                    }
                    if (isHave)
                    {
                        studentRepository.AddStudentInGroup(new StudentsInGroups
                        {
                            IdOfGroup = idOfGroup,
                            IdOfStudent = idOfStudent,
                        });
                        Console.WriteLine("Успешно добавлено");
                    }
                    else
                    {
                        Console.WriteLine("Такой студент в группе уже есть");
                    }
                }
                else if (command == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Команда не найдена");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using University.Models;
using University.Repositories;

namespace University
{
    class Program
    {
        private static string _connectionString = @"Data Source=LAPTOP-Q9O8IFD3\SQLEXPRESS;Initial Catalog=GroopsAndStudents;Pooling=true;Integrated Security=SSPI";

        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRawSqlRepository(_connectionString);
            IGroupRepository groupRepository = new GroupRawSqlRepository(_connectionString);
            IStudentInGroupRepository studentInGroupRepository = new StudentInGroupRawSqlRepository(_connectionString);


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
                bool isNotHave = true;

                if (command == "get-all-students")
                {
                    List<Student> students = studentRepository.GetAllStudents();
                    if (!(students.Count == 0))
                    {
                        foreach (Student student in students)
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
                    if (!(groups.Count == 0))
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
                    if (!Int32.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Вводите id цифрами");
                        Console.WriteLine();
                        continue;
                    }
                    List<StudentInGroup> studentInGroups = studentInGroupRepository.GetStudentsInGroup(id);
                    if (!(studentInGroups.Count == 0))
                    {
                        foreach (StudentInGroup studentInGroup in studentInGroups)
                        {
                            List<Student> students = studentRepository.GetStudentById(studentInGroup.IdOfStudent);
                            foreach (Student student in students)
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
                    if (String.IsNullOrEmpty(name) || name.Contains(' '))
                    {
                        Console.WriteLine("Имя студента не должно быть пустым или иметь пробелы");
                        Console.WriteLine();
                        continue;
                    }
                    Console.WriteLine("Введите возраст студента");
                    if (!Int32.TryParse(Console.ReadLine(), out int age))
                    {
                        Console.WriteLine("Вводите возраст цифрами");
                        Console.WriteLine();
                        continue;
                    }

                    List<Student> student = studentRepository.GetStudentByNameAndAge(name, age);
                   
                    if (student.Count == 0)
                    {
                        isNotHave = false;
                    }
                    
                    if (isNotHave)
                    {
                        studentRepository.AddStudents(new Student
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
                    if (String.IsNullOrEmpty(name) || name.Contains(' '))
                    {
                        Console.WriteLine("Название группы не должно быть пустым или иметь");
                        Console.WriteLine();
                        continue;
                    }
                    List<Group> groups = groupRepository.GetGroupByName(name);
                    foreach (Group group in groups)
                    {
                        if (group.Name == name)
                        {
                            isNotHave = false;
                        }
                    }
                    if (isNotHave)
                    {
                        groupRepository.AddGroup(new Group
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
                    if (!Int32.TryParse(Console.ReadLine(), out int idOfGroup))
                    {
                        Console.WriteLine("Вводите id группы цифрами");
                        Console.WriteLine();
                        continue;
                    }
                    Console.WriteLine("Введите id студента");
                    if (!Int32.TryParse(Console.ReadLine(), out int idOfStudent))
                    {
                        Console.WriteLine("Вводите id студента цифрами");
                        Console.WriteLine();
                        continue;
                    }
                    List<StudentInGroup> studentInGroup = studentInGroupRepository.GetStudentInGroup(idOfStudent, idOfGroup);
                    
                    if (!(studentInGroup.Count == 0))
                    {
                        isNotHave = false;
                    }
                    
                    if (isNotHave)
                    {
                        studentInGroupRepository.AddStudentInGroup(idOfGroup, idOfStudent);
                        Console.WriteLine("Успешно добавлено");
                    }
                    else
                    {
                        Console.WriteLine("Такой студент в группе уже состоит в этой группе");
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
                Console.WriteLine();
            }
        }
    }
}

namespace University
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Group
    {
        public int IdOfGroup { get; set; }
        public string Name { get; set; }
    }
    public class StudentsInGroups
    {
        public int IdOfStudent { get; set; }
        public int IdOfGroup { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

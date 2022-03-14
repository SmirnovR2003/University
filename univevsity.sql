use GroopsAndStudents

create table Groups
(
    IdOfGroup int identity(1,1) constraint PK_Groups primary key,
    Name nvarchar(100)
)

create table Students
(
    Id int identity(1,1) constraint PK_Students primary key,
    Name nvarchar(100),
    Age int 
)

create table StudentsInGroups
(
	IdOfStudent int constraint FK_Post_Student references Students(Id),
	IdOfGroups int constraint FK_Post_Groups references Groups(Id)
)

select*from Students

select*from Groups

select*from StudentsInGroups


USE Master
GO
IF DB_ID('SundayTimeTable') IS NOT NULL
  DROP DATABASE SundayTimeTable;
 GO

 CREATE DATABASE SundayTimeTable collate Ukrainian_CI_AI
 GO
 USE SundayTimeTable;
 GO
 

 --����� ��� �������� � ��������
 CREATE TABLE SGroup
  (
   SGroupId int identity primary key,
   SGroup_Name nvarchar(64) not null,
   Course_Name nvarchar(64)  null,
   Specialization nvarchar(64)  null,
  )
  SET IDENTITY_INSERT SGroup ON
 INSERT INTO SGroup(SGroupId,SGroup_Name, Course_Name, Specialization)  VALUES
 (1, '�����1', '1� ����', '�������������'),
 (2, '�����2', '2� ����', '�������������'),
 (3, '�����3', '3� ����', '������'); 
 SET IDENTITY_INSERT SGroup OFF



  CREATE TABLE Student
 (
   StudentId int identity primary key,
   LastName nvarchar(64) not null,
   FirstName nvarchar(64) not null,
   SGroupId int foreign key REFERENCES SGroup(SGroupId) not null,
 )

 SET IDENTITY_INSERT Student ON
 INSERT INTO Student(StudentId,LastName, FirstName, SGroupId)  VALUES
 (1, '�������', '������', 1),
 (2, '��������', '���������', 1),
 (3, '�����', '���������', 2),
 (4, '���������', '����', 2), 
 (5, '����������', '������', 2),
 (6, '��������', '���ò�', 2), 
 (7, '���������', '²����', 2), 
 (8, '���������', '�����˲�', 3); 
 SET IDENTITY_INSERT Student OFF
 
  --������, �������� � ������� �� manyToMany
  CREATE TABLE Teacher
 (
   TeacherId int identity primary key,
   LastName nvarchar(64) not null,
   FirstName nvarchar(64) not null,  
 )  
  SET IDENTITY_INSERT Teacher ON
 INSERT INTO Teacher(TeacherId,LastName, FirstName )  VALUES
 (1, '����', '�������'),
 (2, 'ѳ�������', '�����'),
 (3, '������', '���в�'),
 (4, '��������', '�в�');  
 SET IDENTITY_INSERT Teacher OFF
     

      CREATE TABLE SSubject
 (
   SSubjectId int identity primary key,
   SSubject_Name nvarchar(64) not null    
 )
  SET IDENTITY_INSERT SSubject ON
 INSERT INTO SSubject(SSubjectId,SSubject_Name)  VALUES
 (1, '�������������' ),
 (2, 'ѳ�������' ),
 (3, '������' ),
 (4, '�������' ),
 (5, 'ASP.NET' ),
 (6, 'ASP.NET Core' ) ;  
  SET IDENTITY_INSERT SSubject OFF

 CREATE TABLE TeacherInSubject
 (
	TeacherInSubjectId int identity primary key,
	TeacherId int foreign key REFERENCES Teacher(TeacherId) not null ,
	SSubjectId int foreign key REFERENCES SSubject(SSubjectId) not null,
 )

  SET IDENTITY_INSERT TeacherInSubject ON
 INSERT INTO TeacherInSubject(TeacherInSubjectId, TeacherId,SSubjectId)  VALUES
 (1, 1, 1 ),
 (2, 2, 2 ),
 (3, 3, 3 ),
 (4, 3, 5 ),
 (5, 3, 6 ),
 (6, 4, 4 ) ;  
 SET IDENTITY_INSERT TeacherInSubject OFF



-- �����
   CREATE TABLE Lesson
 (
   LessonId int identity primary key,
   Lesson_Datetime  Datetime not null,
   Cabinet nvarchar(64) not null,
   SSubjectId int foreign key REFERENCES SSubject(SSubjectId) not null,
   TeacherId int foreign key REFERENCES Teacher(TeacherId) not null,
   SGroupId int foreign key REFERENCES SGroup(SGroupId) not null,      
 )


	

	--Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB; 	Database=SundayTimeTable;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir /Models

	
--Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=TimeTable_Data;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

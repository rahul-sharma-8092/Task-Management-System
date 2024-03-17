--CREATE TABLE RahulTMG_Users
--(   UserId BIGINT identity(1001, 2) PRIMARY KEY,
--    FullName NVARCHAR(200),
--    Email NVARCHAR(200) NOT NULL,
--    Password NVARCHAR(MAX) NOT NULL,
--    Mobile NVARCHAR(15),
--    DateOfJoining DATE,
--    RoleId BIGINT,
--    Image NVARCHAR(MAX),
--	IsDeleted BIT DEFAULT 0,
--	CreatedAt DATE DEFAULT GETDATE(),
--	UpdatedAt DATE DEFAULT GETDATE(),
--	FOREIGN KEY (RoleId) REFERENCES RahulTMG_Roles(RoleId)
--);

--Create Table RahulTMG_Roles(
--	RoleId BIGINT IDENTITY(1,1) PRIMARY KEY,
--	Role NVARCHAR(15)
--)

--INSERT INTO RahulTMG_Users(FullName, Email, Password, Mobile, DateOfJoining, RoleId) VALUES('Sangeeta Sharma', 'sangeeta@gmail.com', '$2b$10$Y7QBuPXGQd6LN2wsoNgISuV/aXGeQ1WZQxyNZAqGG4gc7qCF8opem', '9523741122', GETDATE(), 1)


--Create Table RahulTMG_Employee(
--	Id BIGINT NOT NULL Identity(1, 1) Primary Key,
--	EmpId BIGINT NOT NULL UNIQUE,
--	EmpEmail NVARCHAR(200) NOT NULL,
--	IsDeleted BIT DEFAULT 0,
--	Foreign Key (EmpId) REferences RahulTMG_Users(UserId)
--)

--Create Table RahulTMG_Project (
--	ProjectId BIGINT NOT NULL Identity(5000, 5) PRIMARY KEY,
--	ProjectName NVARCHAR(255) NOT NULL,
--	ProjectDescription NVARCHAR(MAX),
--	[CreatedBy] BIGINT NOT NULL,
--	StartDate Date NOT NULL DEFAULT GETDATE(),
--	[ModifiedBy] BIGINT NOT NULL,
--	ModifiedDate Date,
--	EndDate Date,
--	Status NVARCHAR(20) DEFAULT 'Open',
--	IsClosed BIT NOT NULL DEFAULT 0,
--	IsDeleted BIT NOT NULL DEFAULT 0,
--	FOREIGN KEY ([CreatedBy]) REFERENCES RahulTMG_Employee(EmpId),
--	FOREIGN KEY ([ModifiedBy]) REFERENCES RahulTMG_Employee(EmpId)
--)

--CREATE TABLE RahulTMG_Employee_Project(
--	Id BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--	EmpId BIGINT NOT NULL,
--	ProjectId BIGINT NOT NULL,
--	AssignedDate Date NOT NULL DEFAULT GETDATE(),
--	IsEmployeeAccess BIT NOT NULL DEFAULT 1
--)

select * from RahulTMG_Users

select * from RahulTMG_Employee

Select * from RahulTMG_Project

Select * from RahulTMG_Employee_Project

select * from RahulTMG_Roles

--Insert Into RahulTMG_Employee(EmpId, EmpEmail) Values(1003, 'sangeeta@gmail.com')

--Update RahulTMG_Employee set IsDeleted = 0 where Email='nabin@concept.co.in'

--Update RahulTMG_Employee_Project SET IsEmployeeAccess = 1

--Update RahulTMG_Project SET IsDeleted = 0

--Users
--1001	Rahul Sharma	rahulrohanroshan@gmail.com	$2b$10$LNgTPM6vcIIXnwrf2mEGJOqFeMoWdBo1acL9tnhnFsQk.gysYjY.u	8092726691	2024-03-15	1	/Docs/Images/IMG1831338263.png	0	2024-03-15	2024-03-17
--1003	Sangeeta Sharma	sangeeta@gmail.com	$2b$10$Y7QBuPXGQd6LN2wsoNgISuV/aXGeQ1WZQxyNZAqGG4gc7qCF8opem	9523741122	2024-03-15	1	/Docs/Images/IMG754673850.png	0	2024-03-15	2024-03-17
--1005	Jarvis 	jarvis@gmail.com	$2b$10$uDl4VV6KvKWpMDAwQjfWjeeuZDDWnlff16H.pINblYmgxIYB9GxCa	9876543321	2024-03-16	2	/Docs/Images/IMG1439068693.jpg	0	2024-03-16	2024-03-17
--1007	Admin	admin@gmail.com	$2b$10$hM5IcqHQ6nEEtH8n7IahWOA2YMra5CtVtm0fqs4DFk1qJAlVgMese	8989898989	2023-01-01	1	/Docs/Images/IMG2129072453.png	0	2024-03-16	2024-03-16
--1009	Client AI	client@gmail.com	$2b$10$U7iH//qJPjjp4Lson8Ihz.Db/LwjreLSq5yIQzhsn48zsssXYRjMe	7899877890	2023-12-15	3	/Docs/Images/IMG1272304515.png	0	2024-03-16	2024-03-16
--1011	Nabin Mohanty	nabin@concept.co.in	$2b$10$chFMMmqaxTd.RW.U7PGoKu..9j4noThzgzu/6sK1asPLjwY90BjbK	9090909090	2024-01-01	3	/Docs/Images/IMG1167111887.png	0	2024-03-16	2024-03-17


--Employee
--1	1001	rahulrohanroshan@gmail.com	0
--2	1003	sangeeta@gmail.com	0
--3	1005	jarvis@gmail.com	0
--4	1007	admin@gmail.com	0
--5	1011	nabin@concept.co.in	0


--Project
--5005	Learning Management System	A Learning Management System (LMS) is a software application or platform used to deliver, manage, and track educational courses or training programs. It provides a centralized system for creating, organizing, delivering, and tracking educational content, as well as managing student enrollment, progress, and assessments. Here's an overview of key features and components typically found in a Learning Management System.	1003	2024-03-17	1003	2024-03-17	2024-03-31	Open	0	0
--5010	Train Ticket Booking System	A Train Ticket Booking System is a software application or platform designed to facilitate the reservation and management of train tickets for passengers. It provides an online platform where users can search for train routes, check seat availability, book tickets, make payments, and manage their bookings. Here's an overview of key features and components typically found in a Train Ticket Booking System	1003	2024-03-20	1003	2024-03-17	2024-03-30	Open	0	0
--5015	Smart Drone System	A Smart Drone System refers to an integrated system of drones (Unmanned Aerial Vehicles or UAVs), ground control stations, software, and sensors designed to perform various tasks autonomously or semi-autonomously. These systems leverage advanced technologies such as artificial intelligence, computer vision, machine learning, and IoT (Internet of Things) to enable intelligent decision-making, navigation, and data collection. Here are the key components and features of a Smart Drone System	1001	2024-03-30	1003	2024-03-17	2024-07-31	Open	0	0
--5020	Flipkart Clone	Creating a clone of Flipkart's cart functionality involves designing and implementing a web application with similar features for browsing products, adding them to a cart, and managing the cart contents. Below are the key components and features you would typically include in a Flipkart Cart Clone	1001	2024-03-01	1001	2024-03-17	2025-11-20	Open	0	0

--Employee Project
--2	1003	5005	2024-03-17	1
--3	1003	5010	2024-03-17	1
--4	1001	5015	2024-03-17	1
--5	1001	5020	2024-03-17	1

--Roles
--1	Admin
--2	Employee
--3	Client
create database EmployeeDB

create table dbo.Department(
DepartmentId int identity(1,1),
DepartmentName varchar(500)
)

create table dbo.Employee(
EmployeeId int identity(1,1),
EmployeeName varchar(500),
Department varchar(500),
DateOfJoining date,
PhotoFileName varchar(500)
)

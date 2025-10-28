# JobApplication

This project simulates a **job application management system** with role-based access.  
It demonstrates how **applicants, HR staff, and administrators** interact through APIs:

- **Applicants** can register, log in, and submit job applications.  
- **HR users** can review, approve, or reject applications.  
- **Admins** can manage users, roles, and system data.  

## Highlights
- **Layered architecture:** Separate Controllers, Services, Repositories, and Data layers following SOLID principles  
- **Entity Framework Core:** Integrated with multiple connection strings for authentication and jobs data  
- **JWT Authentication:** Token-based security with configurable issuer and audience  
- **Database Migrations:** Pre-configured EF migrations for easy setup  
- **Dependency Injection:** Cleanly implemented for maintainability and testability  
- **RESTful API Design:** Organized endpoints with proper routing conventions  

## Tech Stack
- **.NET 8 / ASP.NET Core**
- **Entity Framework Core**
- **JWT Authentication**
- **SQL Server (LocalDB)**

##  Setup
1. Clone the repository  
   git clone https://github.com/Rati5832/JobApplication.git

2. Update appsettings.json with your local database connection strings.

Run using Visual Studio

or

dotnet run in terminal

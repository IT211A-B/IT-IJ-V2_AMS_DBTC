DonBosco Attendance Management System
An ASP.NET Core MVC web application with server-rendered Razor views built for Don Bosco Technical College.

Tech Stack & Status
Framework: .NET 9.0 (ASP.NET Core MVC + Razor Views)

Database: PostgreSQL (EF Core 9 + Npgsql)

Authentication: Cookie-based ASP.NET Identity

Frontend: Bootstrap, jQuery, CSS, and JS

Project Structure
Backend/ – Server logic, database configurations, and controllers.

Frontend/ – UI layouts, static assets (wwwroot), and Razor views.

Tests/ – Automated testing project.

Quick Start (Local Setup)
1. Prerequisites
.NET SDK 9.0+

PostgreSQL installed and running locally

Git

2. Build the App
Run this command in your terminal to restore and build the project:

Bash
dotnet build
3. Set Database Connection
Set your local PostgreSQL password using the environment variable below.

PowerShell (Windows):

PowerShell
$env:ConnectionStrings__Default='Host=localhost;Port=5432;Database=attendance_db;Username=postgres;Password=YOUR_PASSWORD'
Bash (Mac/Linux):

Bash
export ConnectionStrings__Default="Host=localhost;Port=5432;Database=attendance_db;Username=postgres;Password=YOUR_PASSWORD"
4. Run the App
Bash
dotnet run --project Backend/ --launch-profile manual-qa
Login: http://localhost:5003/login

Signup: http://localhost:5003/signup

Health Check: http://localhost:5003/health

Default Test Accounts
These accounts are automatically created only when the database is completely fresh and empty.

Admin: admin@dbtc-cebu.edu.ph

Teacher: it.faculty@dbtc-cebu.edu.ph

Student: student01@dbtc-cebu.edu.ph

Note: These credentials are for local testing only. Change them before deploying to a live server.

Testing & QA
Run Automated Tests:

Bash
dotnet test -v minimal
QR Code Testing Pages:

Student scanner page: /attendance/scan

Teacher QR generation page: /attendance/qr

Workflow
Create and work on a separate feature branch.

Push your changes to GitHub.

Open a Pull Request (PR) to merge into the main branch. Direct pushes to main are restricted.

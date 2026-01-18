# Gym Management System

_Full-stack web application built with ASP.NET Core MVC and Bootstrap._

> 🎨 **Design inspiration:** Project’s UI is inspired by a Figma Community design by **Moch. Rizki Eko Waluyo**  
> https://www.figma.com/community/file/1268413829309445731  
> All visual credit goes to the original creator.

> ⚠️ **Status:** Project in planning & development phase.
> Features listed below are planned and will be added gradually.

_The project simulates a real gym management platform:_

- user registration and login
- browsing classes and trainers
- booking training sessions
- managing memberships (passes)
- dmin panel for managing content

_Built as a learning + portfolio project._

## Tech stack

- ASP.NET Core MVC
- Entity Framework Core
- MS SQL
- Bootstrap 5
- Razor Views
- Dependency Injection
- Design patterns (Factory, Abstract Factory, Mediator, Singleton etc.)

## Main features (MVP)

_User_

- Create an account & log in
- View available classes
- Book classes
- See active membership

_Admin_

- Manage users
- Manage trainers
- Manage gym classes
- Manage memberships
- View bookings

_Trainer (planned)_

- Manage own classes
- View attendees
- Mark attendance
- Update class info

## Roadmap

- Bootstrap created responsive UI
- Authentication & roles
- Classes booking
- Membershippasses
- Admin panel
- Filters / search (Interpreter pattern)
- Notifications (Factory Method)
- UI polishing

## Why this project?

_I built this project to:_

- learn ASP.NET Core full-stack development
- understand clean architecture and patterns
- build a real-world app for portfolio


*18.01.2026*

- Introduced custom ApplicationUser and updated Identity configuration
- Implemented profile view with sidebar navigation and section switching
- Added EditProfileViewModel and profile data editing (name, email, phone, document)
- Synced email change with Identity login (UserName)
- Added password change UI with separate ChangePasswordViewModel and partial view
- Refactored profile views into clean partial-based structure
- Fixed Identity DI configuration and cookie-related issues

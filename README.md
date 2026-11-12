# PromotionsCRM

PromotionsCRM is an ASP.NET Core MVC application for managing promotional campaigns. Client companies run promotions for their products in different countries, end customers register for these promotions after a purchase, and staff members process the submissions and track their status.

## Main Features

- **Authentication and authorization**
  - User registration, login and logout with ASP.NET Core Identity
  - All pages require a logged-in user; anonymous visitors are redirected to the login page and returned to the requested page after logging in
  - Custom-styled login and registration pages
- **Home page**
  - Landing page with a short description of the application
- **Clients**
  - List of all clients with search by name
  - Client details page with contact information and all promotions of the client
  - Search within the promotions of a specific client
  - Edit client details, with the country selected from a dropdown list
- **Promotions**
  - List of all promotions with search by name, sorted by client, start date and name
  - Promotion details: client, countries, products, number of submissions and a breakdown of submissions by status
  - Create a promotion directly from the client's details page (the client is preselected)
  - Edit a promotion
  - Delete a promotion with confirmation; promotions that already have submissions cannot be deleted
  - Automatic promotion status (Upcoming / Ongoing / Completed) based on the start and end dates, displayed as a colored badge
- **Products**
  - Add products to a promotion directly from the promotion's details page (the promotion is preselected)
- **Validation**
  - Server-side validation with data annotations and business rules (e.g. the start date must be before the end date, a new promotion cannot start in the past)
  - Duplicate prevention: client names must be unique, a client cannot have two promotions with the same name, and a promotion cannot have two products with the same name (the comparison ignores letter case and spaces)
  - Client-side validation in all forms
- **User feedback**
  - Success and error messages after create, edit and delete operations

## Planned Features (ASP.NET Advanced)

- Editing and deleting products, and managing the countries of each promotion
- Customer registrations (submissions) for promotions
- Submission processing by staff members
- User roles (e.g. administrators and staff members) with different permissions

## Architecture

The solution follows a layered architecture:

| Project | Responsibility |
|---|---|
| `PromotionsCRM.Common` | Validation constants and shared enums |
| `PromotionsCRM.Data.Models` | Entity classes |
| `PromotionsCRM.Data` | `DbContext`, entity configurations, seed data and migrations |
| `PromotionsCRM.ViewModels` | View models used by the controllers and views |
| `PromotionsCRM.Services` | Service interfaces and implementations containing the business logic and database access |
| `PromotionsCRM.Web` | ASP.NET Core MVC project: controllers, views, Identity pages and configuration |

Controllers do not access the database directly. They depend on service interfaces, which are registered in the dependency injection container in `Program.cs`. All database operations are asynchronous.

## Data Model

- **Client** – companies running promotions
- **Promotion** – promotional campaigns of a client
- **Product** – products included in a promotion
- **Country** – countries
- **PromotionCountry** – many-to-many relation between promotions and countries
- **Customer** – end customers
- **Submission** – a customer's registration for a promotion
- **User** – staff members processing submissions

## Technologies

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- ASP.NET Core Identity
- SQL Server / LocalDB
- Razor views with layout, partial views and sections
- Bootstrap 5

## Setup Instructions

### Requirements

- .NET 8 SDK
- SQL Server LocalDB (installed with the "ASP.NET and web development" workload in Visual Studio) or any other SQL Server instance

### Steps

1. Clone the repository:

   ```
   git clone https://github.com/mNenkovGit/PromotionsCRM.git
   ```

2. Connection string

   The default connection string is located in `PromotionsCRM.Web/appsettings.json` and uses LocalDB:

   ```
   Server=(localdb)\MSSQLLocalDB;Database=PromotionsCrmDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
   ```

   No changes are needed if LocalDB is installed. To use a different SQL Server instance, replace the value of `DefaultConnection` in `appsettings.json`, for example:

   ```
   Server=.;Database=PromotionsCrmDb;Trusted_Connection=True;TrustServerCertificate=True
   ```

   In appsettings.json the backslash must be escaped: (localdb)\\MSSQLLocalDB
   
   No other credentials or environment variables are required. User accounts are created through the registration page (see step 5).

4. Apply the migrations

   **Visual Studio (Package Manager Console):**

   - Set `PromotionsCRM.Web` as the startup project
   - Select `PromotionsCRM.Data` as the default project in the Package Manager Console
   - Run:

     ```
     Update-Database
     ```

   **.NET CLI:**

   ```
   dotnet tool install --global dotnet-ef --version 8.0.*
   dotnet ef database update --project PromotionsCRM.Data --startup-project PromotionsCRM.Web
   ```

5. Run the application with F5 in Visual Studio or with:

   ```
   dotnet run --project PromotionsCRM.Web
   ```

6. Create a user account

   The application requires a logged-in user. On first launch you will be redirected to the login page. Click **Register as a new user** and create an account. No email confirmation is required.

   Password requirements:
   - at least 6 characters
   - at least 2 different characters (e.g. `aaaaaa` is not accepted)

   Example of a valid password: `test12`

The database is seeded with sample data (countries, clients, promotions, products and submissions), so all pages display data immediately after the migrations are applied.

## Project Status

In development. Created as part of the ASP.NET Fundamentals course at SoftUni and will be extended during the ASP.NET Advanced course.

# PromotionsCRM

PromotionsCRM is an ASP.NET Core MVC application for managing promotional campaigns. Client companies run promotions for their products in different countries, end customers register for these promotions after a purchase, and staff members process the submissions and track their status.

The application has two parts:

- a **public part**, where visitors browse the current offers and register for them without an account;
- a **staff part**, available after login, where staff members manage clients, promotions and products.

## Main Features

### Public part (no login required)

- **Current offers**
  - List of all currently active promotions (between their start and end date), with search by name
  - Each offer shows the client, the promotion type and period, the days left, the participating products and countries
  - Offer details page
- **Registration for an offer**
  - Registration form with personal details (name, email, phone, country) and purchase details (product, purchase date)
  - The product and country dropdowns contain only the products and countries of the selected promotion
  - The purchase date picker is limited to the promotion period
  - Returning customers are recognized by their email address; their existing data is not changed
  - Confirmation page after a successful registration (Post/Redirect/Get, so refreshing the page does not submit the form again)

### Staff part (login required)

- **Authentication**
  - User registration, login and logout with ASP.NET Core Identity
  - Custom-styled login and registration pages
  - Anonymous visitors who open a staff page are redirected to the login page and returned to the requested page after logging in
- **Clients**
  - List of all clients with search by name
  - Client details with contact information and all promotions of the client, with search within these promotions
  - Edit client details, with the country selected from a dropdown list
- **Promotions**
  - List of all promotions with search by name, sorted by client, start date and name
  - Promotion details: client, countries, products, number of submissions and a breakdown of submissions by status
  - Create a promotion from the client's details page (the client is preselected), with selection of one or more countries
  - Edit a promotion
  - Delete a promotion with confirmation; promotions that already have submissions cannot be deleted
  - Automatic promotion status (Upcoming / Ongoing / Completed) based on the dates, displayed as a colored badge
- **Products**
  - Add products to a promotion from the promotion's details page (the promotion is preselected)

## Validation and Business Rules

All rules are checked on the server. Client-side validation is enabled in all forms for a better user experience.

- **Promotions:** the start date must be before the end date; a new promotion cannot start or end in the past; at least one country must be selected and all selected countries must exist
- **Registrations:**
  - the promotion must be active
  - the product must belong to the promotion
  - the country must be one of the promotion's countries
  - the purchase date must be within the promotion period and cannot be in the future
  - the same customer (email) cannot register the same product for the same promotion twice
- **Duplicate prevention:** client names must be unique, a client cannot have two promotions with the same name, and a promotion cannot have two products with the same name (the comparison ignores letter case and spaces)
- **Database level:** required fields, maximum lengths and relationships are enforced by the entity configurations

## Security

| Vulnerability | Protection |
|---|---|
| SQL Injection | All queries go through Entity Framework Core, which generates parameterized SQL. No raw SQL is used. |
| XSS | Razor HTML-encodes all output. `Html.Raw` is not used. |
| CSRF | All forms include an antiforgery token (form tag helpers), and `[AutoValidateAntiforgeryToken]` on the base controller validates it for every POST request. |
| Parameter tampering | Every value that comes from the browser is validated on the server: hidden fields (client and promotion IDs), dropdown values, selected countries, dates and the promotion status. The ID of an edited entity is taken from the route, not from a hidden field. |
| Unauthorized access | All staff pages require authentication through a global authorization filter and `[Authorize]` on the base controller. Only the public pages are marked with `[AllowAnonymous]`. |

## Architecture

The solution follows a layered architecture:

| Project | Responsibility |
|---|---|
| `PromotionsCRM.Common` | Validation constants and shared enums |
| `PromotionsCRM.Data.Models` | Entity classes |
| `PromotionsCRM.Data` | `DbContext`, entity configurations, seed data, migrations and repositories |
| `PromotionsCRM.Services.DTOs` | Request and response DTOs used between the controllers and the services |
| `PromotionsCRM.Services` | Service interfaces and implementations containing the business logic |
| `PromotionsCRM.ViewModels` | View models used by the controllers and views, including validation attributes |
| `PromotionsCRM.Web` | ASP.NET Core MVC project: controllers, views, Identity pages and configuration |

### Design decisions

- **Repository pattern.** A generic `IRepository<T>` / `BaseRepository<T>` provides the common data access operations. Each entity has its own repository interface and implementation, which are registered automatically in the DI container through reflection.
- **DTOs.** Services receive request DTOs and return response DTOs. They do not depend on view models or on ASP.NET Core MVC types. Controllers map DTOs to view models and back.
- **Thin controllers.** Controllers validate the input, call the services, map the results and choose the response. All database access and business logic is in the services.
- **Base controller.** All controllers inherit `BaseController`, which applies authorization and antiforgery validation and provides helpers for the current user.
- **Separate public and staff logic.** The public offers are served by `CampaignsController` and `CampaignService`, separately from the staff `PromotionsController` and `PromotionService`, so that internal data is never exposed to anonymous visitors.
- **Asynchronous processing.** All database operations are asynchronous.

### Services

| Service | Responsibility |
|---|---|
| `ClientService` | Client list and search, client details with their promotions, client editing, country list, duplicate client name check |
| `PromotionService` | Promotion list and search, details, create (with countries), edit and delete, business rule checks (duplicate names, existing countries, existing submissions) |
| `ProductService` | Adding products to a promotion, duplicate product name check |
| `CampaignService` | Public part: active offers and their details, registration form data, duplicate registration check, and registration of a customer for an offer |

### Repositories

Each entity used by the services has its own repository (`ClientRepository`, `CountryRepository`, `CustomerRepository`, `ProductRepository`, `PromotionRepository`, `SubmissionRepository`), built on the generic `BaseRepository<T>`. They provide queries with and without change tracking, finding an entity by ID, adding, updating and removing entities, and saving changes.

### DTOs

DTOs are grouped by service (`Client`, `Product`, `Promotion`, `Campaign`), and each group is split into:

- **Request DTOs** – data sent from a controller to a service (e.g. `CreatePromotionRequestDto`, `CampaignRegistrationRequestDto`)
- **Response DTOs** – data returned from a service to a controller (e.g. `PromotionDetailsResponseDto`, `ActiveCampaignResponseDto`)

### Registration flow

When a visitor registers for an offer, `CampaignService` looks for an existing customer by email. If none is found, a new customer is created; otherwise the existing customer is used without changing their data. A new submission is then created with the status *Being processed*. The customer and the submission are saved in a single transaction.

## Data Model

- **Client** – companies running promotions
- **Promotion** – promotional campaigns of a client
- **Product** – products included in a promotion
- **Country** – countries
- **PromotionCountry** – many-to-many relation between promotions and countries
- **Customer** – end customers who register for promotions
- **Submission** – a customer's registration for a promotion
- **User** – staff members processing submissions

## Seeding

The database is seeded with sample countries, clients, promotions, products, customers and submissions, so all pages display data immediately after the migrations are applied.

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

   In `appsettings.json` the backslash must be escaped: `(localdb)\\MSSQLLocalDB`.

   No other credentials or environment variables are required. User accounts are created through the registration page (see step 5).

3. Apply the migrations

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

4. Run the application with F5 in Visual Studio or with:

   ```
   dotnet run --project PromotionsCRM.Web
   ```

   On first launch you will see the current offers. They are available without an account.

5. Create a staff account

   To access the staff part, click **Register** in the navigation bar and create an account. No email confirmation is required.

   Password requirements:
   - at least 6 characters
   - at least 2 different characters (e.g. `aaaaaa` is not accepted)

   Example of a valid password: `test12`

## Planned Features

- Processing of submissions by staff members: list with filtering and pagination, details and status changes
- User roles (User and Administrator) and an administration area
- Custom 404 and 500 error pages
- Unit tests for the services

## Project Status

In development. Created during the ASP.NET Fundamentals course at SoftUni and extended during the ASP.NET Advanced course.

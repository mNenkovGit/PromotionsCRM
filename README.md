# PromotionsCRM

PromotionsCRM is an ASP.NET Core MVC application for managing promotional campaigns. Client companies run promotions for their products in different countries, end customers register for these promotions after a purchase, and staff members process the submissions and track their status.

The application has two parts:

- a **public part**, where visitors browse the current offers and register for them without an account;
- a **staff part**, available after login, where staff members process the submissions and administrators manage clients, promotions and products.

## Main Features

### Public part (no login required)

- **Current offers**
  - List of all currently active promotions (between their start and end date), with search by name
  - Each offer is shown as a coupon-style card with the client, the promotion type and period, the participating products and countries, and a "days left" label that turns red when the offer ends within three days
  - Offer details page
- **Registration for an offer**
  - Registration form with personal details (name, email, phone, country) and purchase details (product, purchase date)
  - The product and country dropdowns contain only the products and countries of the selected promotion
  - The purchase date picker is limited to the promotion period
  - Returning customers are recognized by their email address; their existing data is not changed
  - Confirmation page after a successful registration

### Staff part (login required)

- **Authentication and roles**
  - Registration, login and logout with ASP.NET Core Identity, with custom-styled login and registration pages
  - Two roles: **Administrator** (manages clients, promotions and products) and **User** (staff member processing submissions)
  - Roles and two test accounts are seeded automatically on application start; newly registered accounts receive the **User** role
  - Management actions are protected on the server with role-based authorization; the corresponding buttons are hidden for users without the Administrator role
- **Submissions processing** (Administrator and User)
  - List of all submissions, with submissions waiting for processing shown first (oldest first)
  - Filtering by status, by promotion and by customer name or email; the filters can be combined and are kept in the URL
  - Pagination (10 submissions per page); the filters are preserved when moving between pages
  - Submission details: customer, promotion and client, product, purchase date (with a check whether it is within the promotion period), submission date, status, processing date and the staff member who processed it
  - Status change with allowed transitions only; the staff member and the date are recorded automatically
- **Clients**
  - List of all clients with search by name
  - Client details with contact information and all promotions of the client, with search within these promotions
  - Edit client details, with the country selected from a dropdown list (Administrator)
- **Promotions**
  - List of all promotions with search by name
  - Promotion details: client, period, type, countries, products and a breakdown of submissions by status; each status links to the filtered list of submissions
  - Create a promotion from the client's details page, with selection of one or more countries (Administrator)
  - Edit and delete a promotion; promotions that already have submissions cannot be deleted (Administrator)
  - Automatic promotion status (Upcoming / Ongoing / Completed) based on the dates
- **Products**
  - Add products to a promotion from the promotion's details page (Administrator)

## Validation and Business Rules

All rules are checked on the server. Client-side validation is enabled in all forms for a better user experience.

- **Promotions:** the start date must be before the end date; a new promotion cannot start or end in the past; at least one country must be selected and all selected countries must exist
- **Registrations:**
  - the promotion must be active
  - the product must belong to the promotion
  - the country must be one of the promotion's countries
  - the purchase date must be within the promotion period and cannot be in the future
  - the same customer (email) cannot register the same product for the same promotion twice
- **Submission status transitions:**

  | From | Allowed next statuses |
  |---|---|
  | Being processed | Missing info, Valid, Invalid, Cancelled |
  | Missing info | Valid, Invalid, Cancelled |
  | Valid, Invalid, Cancelled | none (final statuses) |

- **Duplicate prevention:** client names must be unique, a client cannot have two promotions with the same name, and a promotion cannot have two products with the same name (the comparison ignores letter case and spaces)
- **Database level:** required fields, maximum lengths and relationships are enforced by the entity configurations

## Security

| Vulnerability | Protection |
|---|---|
| SQL Injection | All queries go through Entity Framework Core, which generates parameterized SQL. No raw SQL is used. |
| XSS | Razor HTML-encodes all output. `Html.Raw` is not used. |
| CSRF | All forms include an antiforgery token (form tag helpers), and `[AutoValidateAntiforgeryToken]` on the base controller validates it for every POST request. |
| Parameter tampering | Every value that comes from the browser is validated on the server: hidden fields (client and promotion IDs), dropdown values, selected countries, dates, the promotion status and the submission status transition. The ID of an edited entity is taken from the route, not from a hidden field. The staff member who changes a submission status is taken from the authenticated user, never from the form. |
| Over-posting | Forms are bound to view models and services receive DTOs, so properties such as the submission status or the processing user cannot be set from the browser. |
| Unauthorized access | All staff pages require authentication (global authorization filter and `[Authorize]` on the base controller). Management actions additionally require the Administrator role. Only the public pages are marked with `[AllowAnonymous]`. |

## Architecture

The solution follows a layered architecture:

| Project | Responsibility |
|---|---|
| `PromotionsCRM.Common` | Validation constants, role names, page size and shared enums |
| `PromotionsCRM.Data.Models` | Entity classes |
| `PromotionsCRM.Data` | `DbContext`, entity configurations, seed data, migrations and repositories |
| `PromotionsCRM.Services.DTOs` | Request and response DTOs used between the controllers and the services |
| `PromotionsCRM.Services` | Service interfaces and implementations containing the business logic |
| `PromotionsCRM.ViewModels` | View models used by the controllers and views, including validation attributes |
| `PromotionsCRM.Web` | ASP.NET Core MVC project: controllers, views, Identity pages, seeding of roles and users, and configuration |

### Design decisions

- **Repository pattern.** A generic `IRepository<T>` / `BaseRepository<T>` provides the common data access operations. Each entity has its own repository interface and implementation, which are registered automatically in the DI container through reflection.
- **DTOs.** Services receive request DTOs and return response DTOs. They do not depend on view models or on ASP.NET Core MVC types. Controllers map DTOs to view models and back.
- **Thin controllers.** Controllers validate the input, call the services, map the results and choose the response. All database access and business logic is in the services.
- **Base controller.** All controllers inherit `BaseController`, which applies authorization and antiforgery validation and provides helpers for the current user.
- **Separate public and staff logic.** The public offers are served by `CampaignsController` and `CampaignService`, separately from the staff `PromotionsController` and `PromotionService`, so that internal data is never exposed to anonymous visitors.
- **Staff members are Identity users.** A submission references the ASP.NET Core Identity user who processed it (`ProcessedById`). An earlier custom `User` entity with its own password column was removed, so all staff data and passwords are managed by Identity.
- **Business rules in one place.** The allowed status transitions are defined once in `SubmissionService` and used both to show the available options and to reject invalid changes.
- **Post/Redirect/Get.** Every successful POST request is followed by a redirect, so refreshing a page never submits a form twice.
- **Asynchronous processing.** All database operations are asynchronous.

### Services

| Service | Responsibility |
|---|---|
| `ClientService` | Client list and search, client details with their promotions, client editing, country list, duplicate client name check |
| `PromotionService` | Promotion list and search, details, create (with countries), edit and delete, business rule checks (duplicate names, existing countries, existing submissions) |
| `ProductService` | Adding products to a promotion, duplicate product name check |
| `CampaignService` | Public part: active offers and their details, registration form data, duplicate registration check, and registration of a customer for an offer |
| `SubmissionService` | Staff part: filtered and paginated list of submissions, submission details, allowed status transitions and status change |

### Repositories

Each entity used by the services has its own repository (`ClientRepository`, `CountryRepository`, `CustomerRepository`, `ProductRepository`, `PromotionRepository`, `SubmissionRepository`), built on the generic `BaseRepository<T>`. They provide queries with and without change tracking, finding an entity by ID, adding, updating and removing entities, and saving changes.

### DTOs

DTOs are grouped by service (`Client`, `Product`, `Promotion`, `Campaign`, `Submission`), and each group is split into:

- **Request DTOs** – data sent from a controller to a service (e.g. `CreatePromotionRequestDto`, `SubmissionFilterRequestDto`)
- **Response DTOs** – data returned from a service to a controller (e.g. `PromotionDetailsResponseDto`, `SubmissionDetailsResponseDto`)

A generic `PagedResponseDto<T>` returns one page of results together with the total count.

### Registration flow

When a visitor registers for an offer, `CampaignService` looks for an existing customer by email. If none is found, a new customer is created; otherwise the existing customer is used without changing their data. A new submission is then created with the status *Being processed*. The customer and the submission are saved in a single transaction.

### Processing flow

A staff member opens the submissions list, filters it and opens a submission. The status can be changed only to one of the allowed next statuses. When the status changes, the current user is stored as the processing staff member and the current date as the processing date.

## Data Model

- **Client** – companies running promotions
- **Promotion** – promotional campaigns of a client
- **Product** – products included in a promotion
- **Country** – countries
- **PromotionCountry** – many-to-many relation between promotions and countries
- **Customer** – end customers who register for promotions
- **Submission** – a customer's registration for a promotion, linked to the Identity user who processed it

User accounts and roles are stored in the standard ASP.NET Core Identity tables.

## Seeding

- **Data:** the database is seeded with sample countries, clients, promotions, products, customers and submissions, so all pages display data immediately after the migrations are applied.
- **Identity:** the roles and two test accounts are created on application start (see step 5 of the setup).

## User Interface

- Custom theme on top of Bootstrap 5, with a consistent color palette and the Archivo font
- Coupon-style cards for the public offers
- Colored status badges for promotions and submissions (partial views)
- Responsive layout for desktop, tablet and mobile
- Small JavaScript helpers: highlighting the current section in the navigation, automatically closing success messages and marking offers that end soon

## Technologies

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8
- ASP.NET Core Identity (with roles)
- SQL Server / LocalDB
- Razor views with layout, partial views and sections
- Bootstrap 5 with a custom theme
- JavaScript

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

   No other credentials or environment variables are required. Test accounts are created automatically (see step 5).

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

5. Test accounts

   Two accounts are created automatically on application start. Their credentials are in `PromotionsCRM.Web/appsettings.json` (section `UserSeed`):

   | Role | Email | Password |
   |---|---|---|
   | Administrator | admin@promotionscrm.com | admin123 |
   | User | user@promotionscrm.com | user123 |

   New accounts can also be created through the **Register** page. They receive the **User** role automatically.

   Password requirements:
   - at least 6 characters
   - at least 2 different characters (e.g. `aaaaaa` is not accepted)

## Planned Features

- Administration area (user and role management)
- Custom 404 and 500 error pages
- Unit tests for the services

## Project Status

In development. Created during the ASP.NET Fundamentals course at SoftUni and extended during the ASP.NET Advanced course.

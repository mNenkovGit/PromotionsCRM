# PromotionsCRM

ASP.NET Core CRM for managing client promotions and customer submissions.

## Description

PromotionsCRM is a web application for managing promotional campaigns. Clients (companies) create promotions with products across different countries. End customers register for promotions after making a purchase, while staff members process the submissions and track their status.

## Main Features

- Manage clients (companies) and their promotions
- Promotions with multiple products and countries
- Customer registrations for promotions
- Track submission status (being processed, valid, invalid, etc.)
- Submission processing by staff members

## Data Model

- **Client** — companies running promotions
- **Promotion** — promotional campaigns
- **Product** — products included in promotions
- **Country** — countries
- **Customer** — end customers
- **Submission** — customer registrations
- **User** — staff members processing submissions

## Technologies

- ASP.NET Core (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap

## Setup and Installation

### Requirements

- .NET 8 SDK
- SQL Server (or SQL Server running in a Docker container)

### Steps

1. Clone the repository:

       git clone https://github.com/mnenkov83/PromotionsCRM.git

2. Configure the connection string in

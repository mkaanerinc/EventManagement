# EventManagement - Event Planning and Ticket Sales System

Welcome to **EventManagement**, an event planning and ticket sales system designed to streamline the creation, management, and tracking of events, tickets, and attendees.

EventManagement allows organizers to create events, manage ticket sales, track attendees, and generate reports—all without user authentication for simplicity in this initial phase. It serves as a practical case study to demonstrate skills in CRUD operations, relational database design, and reporting.

## Project Overview

EventManagement is a system where:
- Organizers can create and manage events.
- Tickets can be added and sold for those events.
- Attendees who purchase tickets are tracked.
- Event-specific reports (e.g., total revenue, tickets sold) are generated.

This project is built with scalability and extensibility in mind, leveraging modern technologies and architectural patterns.

## Features (Planned)

### Event Managemen
- Create, update, and delete events.
- Retrieve events by ID, date, or date range.
- List upcoming events and check event capacity status.

### Ticket Sales
- Add ticket types (e.g., VIP, General) to events.
- Track ticket availability and sales.
- Calculate ticket sales summaries (e.g., revenue per ticket type).

### Attendee Tracking
- Record attendees with details like name and email upon ticket purchase.
- Mark attendees as checked-in during the event.
- List attendees by event or ticket type.

### Reporting
- Generate event-based reports (e.g., total revenue, number of tickets sold).
- Process reports asynchronously using a queue-based system (RabbitMQ).

## Technology Stack

- **Backend**: .NET (ASP.NET Web API)
- **Frontend**: To be determined (options: Blazor, ASP.NET MVC or Angular)
- **Database**: SQL Server
- **Message Queue**: RabbitMQ (for asynchronous reporting)
- **Real-time Updates**: SignalR (planned for UI integration)
- **Validation**: FluentValidation
- **ORM**: Entity Framework Core

## Project Structure

The project follows a clean architecture approach with separation of concerns. Here’s the planned structure:

```bash
EventManagement/
|── src/
|   |── EventManagement.Api/               # Web API layer
|   |── EventManagement.Domain/            # Entities, DTOs, enums
|   |── EventManagement.Application/       # Business logic (commands, queries, handlers)
|   |── EventManagement.Infrastructure/    # Data access, repositories, RabbitMQ integration
|   |── EventManagement.Worker/            # Background service for report generation
|   
|── tests/
|   |── EventManagement.UnitTests/         # Birim tests
|   |── EventManagement.IntegrationTests/  # Entegrasyon tests
|
|── EventManagement.sln                    # Solution file
```

## Database Schema

The system uses a relational database with the following core tables:

1. **Events**: Stores event details (e.g., title, date, location, organizer).
2. **Tickets**: Manages ticket types and sales data for each event.
3. **Attendees**: Tracks individuals who purchase tickets.
4. **Reports**: Stores generated report data (e.g., ticket sales summaries).

Entity relationships:
- `Events` (1) → (*) `Tickets`
- `Tickets` (1) → (*) `Attendees`
- `Events` (1) → (*) `Reports`

## Development Roadmap

The development of this project will progress through the following steps, reflecting my current plan as a developer:

- [X] **Initial Setup**: Set up the initial project structure and solution file to kick things off.
- [ ] **Adding Entities**: Implement the core entities like Events, Tickets, and Attendees.
- [ ] **Database Operations**: Establish and configure the database connection to get persistence up and running.
- [ ] **CRUD Operations**: Build create, read, update, and delete (CRUD) functionality for Events and Tickets to handle core data management.
- [ ] **Attendee Management**: Add attendee tracking and check-in features to manage participants during events.
- [ ] **Validation**: Integrate FluentValidation to enforce rules and ensure data integrity across entities.
- [ ] **Asynchronous Reporting**: Set up RabbitMQ integration for an async reporting system to handle heavy computations off the main thread.
- [ ] **Reporting Features**: Develop ticket sales and attendee reports using a Worker Service for scalable analytics.
- [ ] **Testing**: Write unit and integration tests to verify functionality and increase test coverage as the codebase grows.
- [ ] **Frontend Development**: Choose a frontend tech (Blazor, Angular, or React) and build the user interface to bring the app to life.

### Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/mkaanerinc/EventManagement.git
   cd EventManagement
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore EventManagement.sln
   ```

3. **Set up the database**:
   - Update the connection string in appsettings.json (under EventManagement.Api or EventManagement.Infrastructure).
   - Run migrations to create the database:
   <br><br>
   ```bash
   dotnet ef migrations add InitialCreate --project EventManagement.Infrastructure
   dotnet ef database update --project EventManagement.Infrastructure
   ```
   
4. **Run the API**:
   ```bash
   dotnet run --project EventManagement.Api
   ```
   
5. **Run the Worker Service (for reporting)**:
   ```bash
   dotnet run --project EventManagement.Worker
   ```
### Configuration
- Configure RabbitMQ settings in appsettings.json (host, queue name, etc.).
- Adjust database provider (SQL Server) in the ApplicationDbContext.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

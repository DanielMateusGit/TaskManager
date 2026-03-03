# Task Manager API

REST API per gestione task, sviluppata con TDD rigoroso.

## What I Learned

- **Clean Architecture** - Separazione Domain, Application, Infrastructure, Api
- **TDD** - Red → Green → Refactor workflow
- **Entity Pattern** - Oggetti con identità, stato mutabile, comportamento
- **Smart Enum** - Value Objects con comportamento (Priority)
- *(in progress...)*

## Tech Stack

- .NET 8
- xUnit + FluentAssertions
- Redis (caching) - *coming soon*
- SQL Server - *coming soon*

## Project Structure

```
TaskManager/
├── src/
│   ├── TaskManager.Domain/         # Entities, Value Objects, Events
│   ├── TaskManager.Application/    # Commands, Queries, Handlers
│   ├── TaskManager.Infrastructure/ # EF Core, Redis
│   └── TaskManager.Api/            # Controllers, Program.cs
└── tests/
    ├── TaskManager.Domain.Tests/
    ├── TaskManager.Application.Tests/
    └── TaskManager.Infrastructure.Tests/
```

## How to Run

```bash
# Build
dotnet build

# Run API
dotnet run --project src/TaskManager.Api

# Run tests
dotnet test

# Watch mode (TDD)
dotnet watch test --project tests/TaskManager.Domain.Tests
```

## Tests

| Layer | Tests | Coverage |
|-------|-------|----------|
| Domain | 3 | *TBD* |
| Application | 0 | *TBD* |
| Infrastructure | 0 | *TBD* |

## Progress

- [x] Project setup (Clean Architecture)
- [x] TaskItem entity (TDD)
- [ ] Priority Smart Enum
- [ ] Complete() behavior
- [ ] Repository pattern
- [ ] Redis caching
- [ ] API endpoints

---

*Part of the Senior Engineer learning path*

# Web API for managing job applications in companies

This project reworks the Spring Boot backend from [this repository](https://github.com/vuk1011/job-application-system), in ASP.NET Core.
The same Vue.js frontend can be used as in the linked repository.

## Running the Web API

Go to terminal and run:
```bash
dotnet run --project JobApplicationAPI
```
The API will be exposed at `https://localhost:5001`.

## Migrations

To create a new migration, go to terminal and run:
```bash
dotnet ef migrations add <MigrationName> --project Infrastructure --startup-project JobApplicationAPI
```

To apply migrations to the database, run:
```bash
dotnet ef database update --project JobApplicationAPI
```

## Swagger Docs

Swagger API docs are available at `https://localhost:5001/swagger/index.html`
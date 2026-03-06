Here's the improved `README.md` file incorporating the new content while maintaining the existing structure and coherence:

# Project Title

## Description

[Provide a brief description of the project, its purpose, and key features.]

## Prerequisites

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0) must be installed on your machine.
- Install the EF Core CLI tool if you don't have it:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

- Ensure the project has the EF Core packages (Pomelo) and the design-time dependencies.

## Generate SQL from DbContext

This project targets .NET 8 and uses the Pomelo MySQL provider. Use the EF Core CLI to generate SQL scripts from the DbContext or from migrations.

### Generate SQL to create the current model (create schema SQL)

Run from the project folder (the folder that contains the .csproj for the DbContext):

dotnet ef dbcontext script --context JobSearchDbContex --output create.sql

This produces `create.sql` that contains the SQL statements to create the database schema from the current model.

### Generate SQL for migrations

If you use migrations, you can generate a SQL script that applies migrations:

- Generate a script for all migrations (from empty database to latest):

    ```bash
    dotnet ef migrations script --idempotent --output migrations.sql
    ```

- Generate a script for a specific migration range (from `InitialCreate` to `AddNewTable`):

    ```bash
    dotnet ef migrations script InitialCreate AddNewTable --output range.sql
    ```

### Override connection string (optional)

If you need to override the connection string used at design time, pass it with `--connection`:

dotnet ef dbcontext script --context JobSearchDbContex --connection "Server=192.168.50.100;Port=3306;Database=job_search;User=job_search;Password=job_search;" --output create.sql

### Notes

- Replace `JobSearchDbContex` with the actual DbContext class name if it differs.
- Run the commands from the project directory containing the DbContext's .csproj unless you specify `--project` and `--startup-project` explicitly.
- Use `--no-build` to skip building if you are certain the project is already built.

### Example with explicit project and startup project:

dotnet ef dbcontext script --project JobAnalyzer\JobAnalyzer.csproj --startup-project JobAnalyzer --context JobSearchDbContex --output create.sql

## Usage

[Provide instructions on how to use the project, including examples and any necessary commands.]

## Contributing

[Explain how others can contribute to the project, including guidelines for submitting issues and pull requests.]

## License

[Specify the license under which the project is distributed.]

## Acknowledgments

[Give credit to any resources, libraries, or individuals that contributed to the project.]

This revised README maintains the original structure while integrating the new content seamlessly, ensuring clarity and coherence throughout the document.
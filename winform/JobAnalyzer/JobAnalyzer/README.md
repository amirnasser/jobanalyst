### Entity Framework Scaffold

```
dotnet ef dbcontext scaffold "Server=192.168.50.100;Port=3306;Database=job_search;User=job_search;Password=job_search;" Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --no-onconfiguring  --context JobSearchDbContex --force
```
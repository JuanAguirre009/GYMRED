# Capa de infraestructura - GymRed

## Responsabilidad

La capa de infraestructura implementa el acceso a datos y la persistencia. Conecta la aplicacion con la base de datos relacional SQL Server mediante Entity Framework Core.

## Proyecto

`GymRed.Infrastructure`

## Estructura

```
GymRed.Infrastructure/
├── Data/
│   ├── GymRedDbContext.cs
│   └── Configurations/
│       ├── AsistenciaConfiguration.cs
│       ├── ClaseConfiguration.cs
│       ├── ClienteConfiguration.cs
│       ├── InstructorConfiguration.cs
│       ├── MembresiaConfiguration.cs
│       ├── PagoConfiguration.cs
│       ├── PlanConfiguration.cs
│       ├── ReservaConfiguration.cs
│       ├── SedeConfiguration.cs
│       └── SesionClaseConfiguration.cs
└── Repositories/
    ├── AsistenciaRepository.cs
    ├── ClaseRepository.cs
    ├── ClienteRepository.cs
    ├── InstructorRepository.cs
    ├── MembresiaRepository.cs
    ├── PagoRepository.cs
    ├── PlanRepository.cs
    ├── ReservaRepository.cs
    ├── SedeRepository.cs
    ├── SesionClaseRepository.cs
    └── UnitOfWork.cs
```

## DbContext

`GymRedDbContext` es la clase principal de EF Core. Define los `DbSet<T>` para cada entidad y aplica las configuraciones de entidad mediante `ApplyConfigurationsFromAssembly`.

## Configuraciones de entidades

Cada entidad tiene su propia configuracion en `Data/Configurations/`:

- Nombres de tabla y columnas.
- Longitudes maximas.
- Relaciones entre tablas.
- Indices unicos.
- Conversion de enums a string.
- Configuracion de value objects `Dinero` como owned entities.

### Reglas de negocio reflejadas en la base de datos

| Regla | Configuracion |
|---|---|
| RN1: historial de membresias/pagos | Relaciones 1-N con `Restrict` en eliminacion. |
| RN2: membresia con fechas coherentes | Columnas `date` para fechas de inicio y fin. |
| RN5: no reserva duplicada | Indice unico filtrado `(SesionClaseId, ClienteId) WHERE Estado = 'Activa'`. |
| RN7: pagos inmutables | Solo insercion y anulacion controlada. |

## Repositorios concretos

Implementan las interfaces definidas en `GymRed.Application.Interfaces.Repositories`. Cada repositorio:

- Recibe `GymRedDbContext` por constructor.
- Usa `AsNoTracking()` en consultas de solo lectura.
- Incluye navegaciones necesarias para los mapeos.
- Opera de forma asincrona.

## UnitOfWork

`UnitOfWork` implementa `IUnitOfWork` y delega en `DbContext.SaveChangesAsync()`. Permite que los servicios confirmen transacciones de forma coordinada.

## Seed de datos

El seed de datos no se ejecuta automaticamente desde codigo. Los datos de prueba deben crearse directamente en SQL Server Management Studio (SSMS) mediante el script `docs/seed-ssms.sql`.

Esto permite controlar manualmente los datos iniciales y evita que la aplicacion modifique la base de datos en cada inicio.

## Registro de dependencias

En `GymRed.Web/Program.cs` se registran:

- `GymRedDbContext` con SQL Server.
- Todos los repositorios como `Scoped`.
- `IUnitOfWork`.

Al iniciar, la aplicacion aplica automaticamente las migraciones pendientes (`MigrateAsync`), pero no inserta datos de prueba.

## Migraciones

La migracion inicial se genero con:

```bash
dotnet ef migrations add InitialCreate --project GymRed.Infrastructure --startup-project GymRed.Web --output-dir Migrations
```

Y se aplico a la base de datos con:

```bash
dotnet ef database update --project GymRed.Infrastructure --startup-project GymRed.Web
```

## Cadena de conexion

Configurada en `appsettings.json` y `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GymRedDb;Integrated Security=True;TrustServerCertificate=True"
}
```

## Versiones y dependencias

| Proyecto | Framework / Paquete | Version |
|---|---|---|
| Todos | .NET | 10.0 |
| GymRed.Infrastructure | Microsoft.EntityFrameworkCore | 10.0.10 |
| GymRed.Infrastructure | Microsoft.EntityFrameworkCore.SqlServer | 10.0.10 |
| GymRed.Infrastructure | Microsoft.EntityFrameworkCore.Tools | 10.0.10 |
| GymRed.Web | Microsoft.EntityFrameworkCore.Design | 10.0.10 |
| GymRed.Infrastructure/Web/Tests | System.Security.Cryptography.Pkcs | 10.0.13 |

### Parche de seguridad

El paquete transitorio `System.Security.Cryptography.Pkcs` traído por `Microsoft.Data.SqlClient` contenía las vulnerabilidades [GHSA-2fmv-rv2c-59x7](https://github.com/advisories/GHSA-2fmv-rv2c-59x7) y [GHSA-846f-f764-76mx](https://github.com/advisories/GHSA-846f-f764-76mx). Se agregó una referencia directa a la versión parcheada **10.0.13** en `GymRed.Infrastructure`, `GymRed.Web` y `GymRed.Tests`.

La herramienta global `dotnet-ef` también se actualizó a la versión **10.0.10** para coincidir con el runtime de EF Core.

## Principios SOLID / Clean Code aplicados

- **DIP:** los repositorios dependen de abstracciones definidas en Application.
- **SRP:** cada repositorio accede a un solo agregado.
- **LSP:** las implementaciones de repositorios pueden sustituir a las interfaces sin afectar servicios.
- **Separacion de responsabilidades:** configuraciones, repositorios y seed en carpetas separadas.
- **Tipos explicitos:** no se usa `var`.

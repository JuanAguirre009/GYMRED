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
│   ├── Configurations/
│   │   ├── AsistenciaConfiguration.cs
│   │   ├── ClaseConfiguration.cs
│   │   ├── ClienteConfiguration.cs
│   │   ├── InstructorConfiguration.cs
│   │   ├── MembresiaConfiguration.cs
│   │   ├── PagoConfiguration.cs
│   │   ├── PlanConfiguration.cs
│   │   ├── ReservaConfiguration.cs
│   │   ├── SedeConfiguration.cs
│   │   └── SesionClaseConfiguration.cs
│   └── Seed/
│       └── DbInitializer.cs
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

`DbInitializer` inserta datos iniciales si las tablas estan vacias:

- 1 sede: "Sede Principal".
- 2 instructores: "Carlos Gomez" y "Ana Rodriguez".
- 2 planes: "Mensual" y "Trimestral".
- 1 cliente de prueba: "Laura Martinez".
- 1 clase: "CrossFit".

## Registro de dependencias

En `GymRed.Web/Program.cs` se registran:

- `GymRedDbContext` con SQL Server.
- Todos los repositorios como `Scoped`.
- `IUnitOfWork`.
- Seed automatico al iniciar la aplicacion.

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
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=GymRedDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

## Principios SOLID / Clean Code aplicados

- **DIP:** los repositorios dependen de abstracciones definidas en Application.
- **SRP:** cada repositorio accede a un solo agregado.
- **LSP:** las implementaciones de repositorios pueden sustituir a las interfaces sin afectar servicios.
- **Separacion de responsabilidades:** configuraciones, repositorios y seed en carpetas separadas.
- **Tipos explicitos:** no se usa `var`.

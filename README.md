# GymRed - Sistema de Gestion de Gimnasio

Proyecto academico para administrar las operaciones de una cadena de gimnasios aplicando arquitectura en capas y patron MVC.

## Arquitectura en capas

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| Presentacion | `GymRed.Web` | Controladores, vistas, modelos de vista, recepcion de solicitudes HTTP. |
| Aplicacion / Negocio | `GymRed.Application` | Casos de uso, servicios, validaciones y coordinacion de operaciones. |
| Dominio | `GymRed.Domain` | Entidades, enums, value objects e interfaces del nucleo del negocio. |
| Acceso a datos | `GymRed.Infrastructure` | Repositorios, DbContext, persistencia y comunicacion con la base de datos. |
| Pruebas | `GymRed.Tests` | Tests unitarios y de integracion por capa. |

## Dependencias entre capas

```
GymRed.Web
    -> GymRed.Application
        -> GymRed.Domain

GymRed.Infrastructure
    -> GymRed.Domain
    -> GymRed.Application

GymRed.Tests
    -> GymRed.Web
    -> GymRed.Application
    -> GymRed.Domain
    -> GymRed.Infrastructure
```

## Flujo de una solicitud

1. El usuario interactua con una **Vista**.
2. La vista envia la solicitud a un **Controlador**.
3. El controlador delega en un **Servicio** de la capa de aplicacion.
4. El servicio aplica las reglas de negocio y usa el **Dominio**.
5. El servicio consulta/persiste mediante un **Repositorio** de infraestructura.
6. El repositorio accede a la **Base de datos**.
7. La respuesta fluye de regreso hacia el controlador y la vista.

## Estructura de ramas propuesta

- `main`: proyecto base estable.
- `desarrollo`: integracion de las fases aprobadas.
- `feature/implementacion_inicial`: proyecto base + capa de dominio.
- `feature/fase-2-aplicacion`: servicios, DTOs e interfaces de repositorio.
- `feature/fase-3-infraestructura`: DbContext, repositorios y migraciones.
- `feature/fase-4-presentacion`: controladores, vistas y modelos de vista.
- `feature/fase-5-reportes-seguridad`: reportes, filtros y autenticacion.
- `feature/fase-6-pruebas`: pruebas unitarias e integracion.
- `docs/entregables`: documentacion final.

## Fases completadas

- **Fase 1 (Dominio):** entidades, enums, value objects, excepciones de dominio y pruebas unitarias iniciales. Ver `docs/modelo-dominio.md`.
- **Fase 2 (Aplicacion):** DTOs, interfaces de repositorio, servicios de casos de uso, mapeos y reglas de negocio. Ver `docs/capa-aplicacion.md`.
- **Fase 3 (Infraestructura):** DbContext, configuraciones EF Core, repositorios concretos, UnitOfWork, migraciones SQL Server y seed de datos. Ver `docs/capa-infraestructura.md`.

## Requerimientos funcionales

- RF01: Gestion de clientes
- RF02: Gestion de membresias
- RF03: Gestion de pagos
- RF04: Gestion de clases grupales
- RF05: Reservas y asistencia

## Tecnologias

- .NET 10
- ASP.NET Core MVC
- Entity Framework Core 10
- xUnit (pruebas)
- SQL Server

# GymRed - Sistema de Gestión de Gimnasio

Proyecto académico para administrar las operaciones de una cadena de gimnasios aplicando arquitectura en capas y patrón MVC.

## Arquitectura en capas

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| Presentación | `GymRed.Web` | Controladores, vistas, modelos de vista, recepción de solicitudes HTTP. |
| Aplicación / Negocio | `GymRed.Application` | Casos de uso, servicios, validaciones y coordinación de operaciones. |
| Dominio | `GymRed.Domain` | Entidades, enums, value objects e interfaces del núcleo del negocio. |
| Acceso a datos | `GymRed.Infrastructure` | Repositorios, DbContext, persistencia y comunicación con la base de datos. |
| Pruebas | `GymRed.Tests` | Tests unitarios y de integración por capa. |

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

1. El usuario interactúa con una **Vista**.
2. La vista envía la solicitud a un **Controlador**.
3. El controlador delega en un **Servicio** de la capa de aplicación.
4. El servicio aplica las reglas de negocio y usa el **Dominio**.
5. El servicio consulta/persiste mediante un **Repositorio** de infraestructura.
6. El repositorio accede a la **Base de datos**.
7. La respuesta fluye de regreso hacia el controlador y la vista.

## Estructura de ramas propuesta

- `main`: proyecto base estable.
- `capa-dominio`: entidades, enums, value objects e interfaces.
- `capa-aplicacion`: servicios, DTOs y casos de uso.
- `capa-infraestructura`: DbContext, repositorios y migraciones.
- `capa-presentacion`: controladores, vistas y modelos de vista.
- `tests`: pruebas unitarias e integración.

## Requerimientos funcionales

- RF01: Gestión de clientes
- RF02: Gestión de membresías
- RF03: Gestión de pagos
- RF04: Gestión de clases grupales
- RF05: Reservas y asistencia

## Tecnologías

- .NET 10
- ASP.NET Core MVC
- Entity Framework Core (a configurar en la rama de infraestructura)
- xUnit (pruebas)
- SQL Server / PostgreSQL (a definir)

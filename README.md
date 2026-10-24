> **Nota sobre codificaci�n:** los archivos fuente usan UTF-8. Visual Studio y .NET los manejan correctamente.

# GymRed - Sistema de Gesti�n de Gimnasio

Proyecto ac�demico para administrar las operaciones de una cadena de gimnasios aplicando arquitectura en capas y patr�n MVC.

## Arquitectura en capas

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| Presentaci�n | `GymRed.Web` | Controladores, vistas, modelos de vista, recepci�n de solicitudes HTTP. |
| Aplicaci�n / Negocio | `GymRed.Application` | Casos de uso, servicios, validaciones y coordinaci�n de operaciones. |
| Dominio | `GymRed.Domain` | Entidades, enums, value objects e interfaces del n�cleo del negocio. |
| Acceso a datos | `GymRed.Infrastructure` | Repositorios, DbContext, persistencia y comunicaci�n con la base de datos. |
| Pruebas | `GymRed.Tests` | Tests unitarios y de integraci�n por capa. |

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

1. El usuario interact�a con una **Vista**.
2. La vista env�a la solicitud a un **Controlador**.
3. El controlador delega en un **Servicio** de la capa de aplicaci�n.
4. El servicio aplica las reglas de negocio y usa el **Dominio**.
5. El servicio consulta/persiste mediante un **Repositorio** de infraestructura.
6. El repositorio accede a la **Base de datos**.
7. La respuesta fluye de regreso hacia el controlador y la vista.

## Fases completadas

- **Fase 1 (Dominio):** entidades, enums, value objects, excepciones de dominio y pruebas unitarias iniciales. Ver `docs/modelo-dominio.md`.
- **Fase 2 (Aplicaci�n):** DTOs, interfaces de repositorio, servicios de casos de uso, mapeos y reglas de negocio. Ver `docs/capa-aplicacion.md`.

## Requerimientos funcionales

- RF01: Gesti�n de clientes
- RF02: Gesti�n de membres�as
- RF03: Gesti�n de pagos
- RF04: Gesti�n de clases grupales
- RF05: Reservas y asistencia

## Tecnolog�as

- .NET 10
- ASP.NET Core MVC
- Entity Framework Core (a configurar en la rama de infraestructura)
- xUnit (pruebas)
- SQL Server / PostgreSQL (a definir)

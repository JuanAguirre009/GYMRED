# Capa de aplicacion - GymRed

## Responsabilidad

La capa de aplicacion coordina los casos de uso del sistema. Recibe DTOs desde la capa de presentacion, aplica las reglas de negocio, orquesta llamadas a repositorios y devuelve DTOs de respuesta.

## Proyecto

`GymRed.Application`

## Estructura

```
GymRed.Application/
├── DTOs/
│   └── Reportes/
├── Exceptions/
├── Interfaces/
│   ├── Repositories/
│   └── Services/
├── Mappings/
└── Services/
```

## DTOs (Data Transfer Objects)

Separan las entidades de dominio de los datos que entran y salen del sistema:

- DTOs de entrada: `CrearClienteDto`, `ActualizarClienteDto`, `CrearSedeDto`, `CrearInstructorDto`, `CrearPlanDto`, `AsignarMembresiaDto`, `RegistrarPagoDto`, `CrearClaseDto`, `ProgramarSesionDto`, `CrearReservaDto`, `RegistrarAsistenciaDto`.
- DTOs de salida: `ClienteDto`, `SedeDto`, `InstructorDto`, `PlanDto`, `MembresiaDto`, `PagoDto`, `ClaseDto`, `SesionClaseDto`, `ReservaDto`, `AsistenciaDto`.
- DTOs de reporte: `ReporteIngresosDto`, `ReporteMembresiasDto`, `ReporteAsistenciaDto`.

## Interfaces de repositorio (DIP)

Se definen en `GymRed.Application.Interfaces.Repositories` para que la capa de aplicacion dependa de abstracciones, no de EF Core:

- `IClienteRepository`
- `ISedeRepository`
- `IInstructorRepository`
- `IPlanRepository`
- `IMembresiaRepository`
- `IPagoRepository`
- `IClaseRepository`
- `ISesionClaseRepository`
- `IReservaRepository`
- `IAsistenciaRepository`
- `IUnitOfWork`

Las implementaciones concretas iran en `GymRed.Infrastructure`.

## Servicios / casos de uso

| Servicio | Responsabilidad |
|---|---|
| `ClienteService` | Registrar, consultar, actualizar y cambiar estado de clientes (RF01). |
| `SedeService` | Crear y administrar sedes. |
| `InstructorService` | Crear y administrar instructores. |
| `PlanService` | Crear y administrar planes de membresia (RF02). |
| `MembresiaService` | Asignar planes, consultar vigencias y vencimientos (RF02). |
| `PagoService` | Registrar pagos, historial y verificar si cliente esta al dia (RF03). |
| `ClaseService` | Crear clases y programar sesiones con instructores y cupos (RF04). |
| `ReservaService` | Reservar y cancelar cupos, validando acceso, cupos y duplicados (RF05). |
| `AsistenciaService` | Registrar asistencia solo en sesiones programadas (RF05). |
| `ReporteService` | Generar reportes de ingresos, membresias activas y asistencia. |

## Reglas de negocio implementadas

| Regla | Servicio | Detalle |
|---|---|---|
| RN1 | `MembresiaService`, `PagoService` | Consulta de historial por cliente. |
| RN2 | `MembresiaService` | Asignacion con plan, fechas y estado. |
| RN3 | `ReservaService` | Cliente activo y con membresia vigente. |
| RN4 | `ReservaService` | Validacion de cupos disponibles. |
| RN5 | `ReservaService` | Rechazo de reserva duplicada activa. |
| RN6 | `AsistenciaService` | Asistencia solo en sesion programada. |
| RN7 | `PagoService` | Anulacion controlada, sin edicion arbitraria. |

## Mapeo

Se usa mapeo manual mediante metodos de extension en `GymRed.Application.Mappings`. Esto evita dependencias externas como AutoMapper y mantiene el control sobre como se transforman los datos.

## Excepciones

- `AplicacionException`: errores tecnicos o de validacion en la capa de aplicacion.
- `ReglaNegocioException` (de dominio): violaciones de reglas de negocio detectadas durante la ejecucion de un caso de uso.

## Principios SOLID / Clean Code aplicados

- **DIP:** servicios dependen de interfaces de repositorio, no de implementaciones.
- **SRP:** cada servicio gestiona un solo modulo funcional.
- **ISP:** interfaces pequenas y especificas.
- **DTOs:** separacion entre modelos de dominio y contratos de entrada/salida.
- **Metodos pequenos:** cada metodo realiza una sola operacion clara.
- **Async/await:** todos los metodos de I/O son asincronos.
- **Tipos explicitos:** no se usa `var`.

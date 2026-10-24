# Capa de aplicaci�n - GymRed

## Responsabilidad

La capa de aplicaci�n coordina los casos de uso del sistema. Recibe DTOs desde la capa de presentaci�n, aplica las reglas de negocio, orquesta llamadas a repositorios y devuelve DTOs de respuesta.

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

- DTOs de entrada: `CrearClienteDto`, `ActualizarClienteDto`, `AsignarMembresiaDto`, `RegistrarPagoDto`, `CrearClaseDto`, `ProgramarSesionDto`, `CrearReservaDto`, `RegistrarAsistenciaDto`.
- DTOs de salida: `ClienteDto`, `PlanDto`, `MembresiaDto`, `PagoDto`, `ClaseDto`, `SesionClaseDto`, `ReservaDto`, `AsistenciaDto`.
- DTOs de reporte: `ReporteIngresosDto`, `ReporteMembresiasDto`, `ReporteAsistenciaDto`.

## Interfaces de repositorio (DIP)

Se definen en `GymRed.Application.Interfaces.Repositories` para que la capa de aplicaci�n dependa de abstracciones, no de EF Core:

- `IClienteRepository`
- `IPlanRepository`
- `IMembresiaRepository`
- `IPagoRepository`
- `IClaseRepository`
- `ISesionClaseRepository`
- `IReservaRepository`
- `IAsistenciaRepository`
- `IUnitOfWork`

Las implementaciones concretas ir�n en `GymRed.Infrastructure`.

## Servicios / casos de uso

| Servicio | Responsabilidad |
|---|---|
| `ClienteService` | Registrar, consultar, actualizar y cambiar estado de clientes (RF01). |
| `PlanService` | Crear y administrar planes de membres�a (RF02). |
| `MembresiaService` | Asignar planes, consultar vigencias y vencimientos (RF02). |
| `PagoService` | Registrar pagos, historial y verificar si cliente est� al d�a (RF03). |
| `ClaseService` | Crear clases y programar sesiones con instructores y cupos (RF04). |
| `ReservaService` | Reservar y cancelar cupos, validando acceso, cupos y duplicados (RF05). |
| `AsistenciaService` | Registrar asistencia solo en sesiones programadas (RF05). |
| `ReporteService` | Generar reportes de ingresos, membres�as activas y asistencia. |

## Reglas de negocio implementadas

| Regla | Servicio | Detalle |
|---|---|---|
| RN1 | `MembresiaService`, `PagoService` | Consulta de historial por cliente. |
| RN2 | `MembresiaService` | Asignaci�n con plan, fechas y estado. |
| RN3 | `ReservaService` | Cliente activo y con membres�a vigente. |
| RN4 | `ReservaService` | Validaci�n de cupos disponibles. |
| RN5 | `ReservaService` | Rechazo de reserva duplicada activa. |
| RN6 | `AsistenciaService` | Asistencia solo en sesi�n programada. |
| RN7 | `PagoService` | Anulaci�n controlada, sin edici�n arbitraria. |

## Mapeo

Se usa mapeo manual mediante m�todos de extensi�n en `GymRed.Application.Mappings`. Esto evita dependencias externas como AutoMapper y mantiene el control sobre c�mo se transforman los datos.

## Excepciones

- `AplicacionException`: errores t�cnicos o de validaci�n en la capa de aplicaci�n.
- `ReglaNegocioException` (de dominio): violaciones de reglas de negocio detectadas durante la ejecuci�n de un caso de uso.

## Principios SOLID / Clean Code aplicados

- **DIP:** servicios dependen de interfaces de repositorio, no de implementaciones.
- **SRP:** cada servicio gestiona un solo m�dulo funcional.
- **ISP:** interfaces peque�as y espec�ficas.
- **DTOs:** separaci�n entre modelos de dominio y contratos de entrada/salida.
- **M�todos peque�os:** cada m�todo realiza una sola operaci�n clara.
- **Async/await:** todos los m�todos de I/O son as�ncronos.
- **Tipos expl�citos:** no se usa `var`.

# Modelo de dominio - GymRed

## Entidades principales

| Entidad | Descripcion |
|---------|-------------|
| `Sede` | Ubicacion fisica de la cadena de gimnasios. |
| `Cliente` | Usuario del gimnasio con datos personales, membresias, reservas y asistencias. |
| `Plan` | Configuracion de duracion, precio y beneficios de una membresia. |
| `Membresia` | Asignacion de un plan a un cliente con periodo de vigencia. |
| `Pago` | Transaccion economica asociada a una membresia. |
| `Instructor` | Persona encargada de dictar clases grupales. |
| `Clase` | Plantilla que define nombre, instructor, sede y capacidad maxima. |
| `SesionClase` | Instancia programada de una clase con fecha, hora y estado. |
| `Reserva` | Cupo reservado por un cliente en una sesion de clase. |
| `Asistencia` | Registro de ingreso de un cliente a una sesion. |

## Relaciones

- Una `Sede` tiene muchos `Cliente` y muchas `Clase`.
- Un `Cliente` tiene muchas `Membresia`, muchas `Reserva` y muchas `Asistencia`.
- Un `Plan` puede estar en muchas `Membresia`.
- Una `Membresia` tiene muchos `Pago`.
- Un `Instructor` dicta muchas `Clase`.
- Una `Clase` tiene muchas `SesionClase`.
- Una `SesionClase` tiene muchas `Reserva` y muchas `Asistencia`.

## Value objects

| Value object | Descripcion |
|--------------|-------------|
| `Dinero` | Representa un monto con moneda, inmutable y comparable. |
| `Periodo` | Representa un rango de fechas con validacion de coherencia. |

## Enums

- `EstadoCliente`: Activo, Inactivo, Suspendido.
- `EstadoMembresia`: Activa, Vencida, Cancelada.
- `EstadoPago`: Registrado, Anulado.
- `EstadoSesionClase`: Programada, Cancelada, Finalizada.
- `EstadoReserva`: Activa, Cancelada, Cumplida.
- `MetodoPago`: Efectivo, TarjetaCredito, TarjetaDebito, Transferencia.

## Reglas de negocio cubiertas en dominio

| Regla | Implementacion |
|-------|----------------|
| RN2: membresia con plan, fechas y estado | Constructor de `Membresia`. |
| RN4: clase no supera capacidad maxima | `SesionClase.HayCuposDisponibles(int reservasActivas)`. |
| RN6: asistencia solo en sesion programada | `SesionClase.EstaProgramada()`. |
| RN7: pagos inmutables | `Pago.Anular(string motivo)` con control de estado. |
| Fechas coherentes | Validaciones en `Periodo`, `Membresia` y `SesionClase`. |
| Plan inactivo no asignable | `Membresia` valida `plan.Activo`. |
| Cliente inactivo no puede recibir membresia | `Cliente.AsignarMembresia` valida estado. |
| Clase inactiva no puede programar sesiones | `Clase.ProgramarSesion` valida estado. |

## Principios aplicados

- **SRP:** cada entidad representa un solo concepto del negocio.
- **OCP:** los value objects son inmutables y extensibles.
- **DIP:** las entidades dependen de abstracciones (`IEntity`, `IAggregateRoot`).
- **Encapsulamiento:** setters privados, constructores con validacion.
- **Inmutabilidad parcial:** value objects y pagos protegidos contra cambios arbitrarios.

> **Nota sobre codificaci�n:** los archivos pueden contener caracteres con acentos. Visual Studio y .NET manejan UTF-8 correctamente.

# Modelo de dominio - GymRed

## Entidades principales

| Entidad | Descripci�n |
|---------|-------------|
| `Sede` | Ubicaci�n f�sica de la cadena de gimnasios. |
| `Cliente` | Usuario del gimnasio con datos personales, membres�as, reservas y asistencias. |
| `Plan` | Configuraci�n de duraci�n, precio y beneficios de una membres�a. |
| `Membresia` | Asignaci�n de un plan a un cliente con per�odo de vigencia. |
| `Pago` | Transacci�n econ�mica asociada a una membres�a. |
| `Instructor` | Persona encargada de dictar clases grupales. |
| `Clase` | Plantilla que define nombre, instructor, sede y capacidad m�xima. |
| `SesionClase` | Instancia programada de una clase con fecha, hora y estado. |
| `Reserva` | Cupo reservado por un cliente en una sesi�n de clase. |
| `Asistencia` | Registro de ingreso de un cliente a una sesi�n. |

## Relaciones

- Una `Sede` tiene muchos `Cliente` y muchas `Clase`.
- Un `Cliente` tiene muchas `Membresia`, muchas `Reserva` y muchas `Asistencia`.
- Un `Plan` puede estar en muchas `Membresia`.
- Una `Membresia` tiene muchos `Pago`.
- Un `Instructor` dicta muchas `Clase`.
- Una `Clase` tiene muchas `SesionClase`.
- Una `SesionClase` tiene muchas `Reserva` y muchas `Asistencia`.

## Value objects

| Value object | Descripci�n |
|--------------|-------------|
| `Dinero` | Representa un monto con moneda, inmutable y comparable. |
| `Periodo` | Representa un rango de fechas con validaci�n de coherencia. |

## Enums

- `EstadoCliente`: Activo, Inactivo, Suspendido.
- `EstadoMembresia`: Activa, Vencida, Cancelada.
- `EstadoPago`: Registrado, Anulado.
- `EstadoSesionClase`: Programada, Cancelada, Finalizada.
- `EstadoReserva`: Activa, Cancelada, Cumplida.
- `MetodoPago`: Efectivo, TarjetaCredito, TarjetaDebito, Transferencia.

## Reglas de negocio cubiertas en dominio

| Regla | Implementaci�n |
|-------|----------------|
| RN2: membres�a con plan, fechas y estado | Constructor de `Membresia`. |
| RN4: clase no supera capacidad m�xima | `SesionClase.HayCuposDisponibles(int reservasActivas)`. |
| RN6: asistencia solo en sesi�n programada | `SesionClase.EstaProgramada()`. |
| RN7: pagos inmutables | `Pago.Anular(string motivo)` con control de estado. |
| Fechas coherentes | Validaciones en `Periodo`, `Membresia` y `SesionClase`. |
| Plan inactivo no asignable | `Membresia` valida `plan.Activo`. |

## Principios aplicados

- **SRP:** cada entidad representa un solo concepto del negocio.
- **OCP:** los value objects son inmutables y extensibles.
- **DIP:** las entidades dependen de abstracciones (`IEntity`, `IAggregateRoot`).
- **Encapsulamiento:** setters privados, constructores con validaci�n.
- **Inmutabilidad parcial:** value objects y pagos protegidos contra cambios arbitrarios.

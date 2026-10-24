-- Script de datos de prueba para GymRed
-- Ejecutar en SQL Server Management Studio contra la base de datos GymRedDb
-- despues de aplicar las migraciones de Entity Framework Core.

USE GymRedDb;
GO

-- Sedes
INSERT INTO Sedes (Nombre, Direccion, Telefono, Email, FechaApertura, Activa)
VALUES ('Sede Principal', 'Calle 123 # 45-67', '6011234567', 'principal@gymred.com', '2020-01-15', 1);
GO

-- Instructores
INSERT INTO Instructores (Nombres, Apellidos, Email, Telefono, Especialidad, Activo)
VALUES
('Carlos', 'Gomez', 'carlos.gomez@gymred.com', '3001112233', 'Entrenamiento funcional', 1),
('Ana', 'Rodriguez', 'ana.rodriguez@gymred.com', '3004445566', 'Yoga y pilates', 1);
GO

-- Planes
INSERT INTO Planes (Nombre, Descripcion, DuracionDias, Precio, Moneda, Beneficios, Activo)
VALUES
('Mensual', 'Acceso ilimitado por 30 dias', 30, 120000, 'COP', 'Acceso a todas las sedes, clases grupales y zona de pesas', 1),
('Trimestral', 'Acceso ilimitado por 90 dias', 90, 300000, 'COP', 'Acceso a todas las sedes, clases grupales, zona de pesas y 1 sesion de nutricion', 1);
GO

-- Clientes
DECLARE @SedeId INT = (SELECT TOP 1 Id FROM Sedes);

INSERT INTO Clientes (Nombres, Apellidos, NumeroIdentificacion, Email, Telefono, FechaNacimiento, FechaRegistro, Estado, SedeId)
VALUES ('Laura', 'Martinez', '1000123456', 'laura.martinez@correo.com', '3109876543', '1995-05-20', GETUTCDATE(), 0, @SedeId);
GO

-- Clases
DECLARE @InstructorId INT = (SELECT TOP 1 Id FROM Instructores ORDER BY Id);
DECLARE @SedeId INT = (SELECT TOP 1 Id FROM Sedes);

INSERT INTO Clases (Nombre, Descripcion, InstructorId, SedeId, CapacidadMaxima, Activa)
VALUES ('CrossFit', 'Entrenamiento de alta intensidad', @InstructorId, @SedeId, 20, 1);
GO

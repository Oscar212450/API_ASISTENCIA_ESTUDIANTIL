CREATE DATABASE Asistencia_Estudiantes
GO

USE Asistencia_Estudiantes
GO

CREATE TABLE Cls_Estados
(
Id_Estado INT PRIMARY KEY IDENTITY (1,1),
Estado VARCHAR(80) NOT NULL,
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Activo BIT NOT NULL
)

CREATE TABLE Cls_Tipo_Catalogos
(
Id_Tipo_Catalogo INT PRIMARY KEY IDENTITY (1,1),
Tipo_Catalogo VARCHAR(80) NOT NULL,
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Activo BIT NOT NULL
)

CREATE TABLE Cls_Catalogos
(
Id_Catalogo INT PRIMARY KEY IDENTITY (1,1),
Id_Tipo_Catalogo INT REFERENCES Cls_Tipo_Catalogos (Id_Tipo_Catalogo),
Catalogo VARCHAR(80) NOT NULL,
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Activo BIT NOT NULL
)


CREATE TABLE Tbl_Datos_Personales
(
Id_Persona INT PRIMARY KEY IDENTITY (1,1),
Primer_Nombre VARCHAR(80) NOT NULL,
Segundo_Nombre VARCHAR(80) NULL,
Primer_Apellido VARCHAR(80) NOT NULL,
Segundo_Apellido VARCHAR(80) NULL,
Edad CHAR(2) NOT NULL,
Tipo_Cargo INT REFERENCES Cls_Catalogos (Id_Catalogo),
Tipo_DNI INT REFERENCES Cls_Catalogos (Id_Catalogo),
DNI VARCHAR(20) NOT NULL,
Genero INT REFERENCES Cls_Catalogos (Id_Catalogo),
Nacionalidad INT REFERENCES Cls_Catalogos (Id_Catalogo),
Departamento INT REFERENCES Cls_Catalogos (Id_Catalogo),
Estado_Civil INT REFERENCES Cls_Catalogos (Id_Catalogo),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Tbl_Contactos
(
Id_Contacto INT PRIMARY KEY IDENTITY (1,1),
Id_Persona INT REFERENCES Tbl_Datos_Personales (Id_Persona),
Tipo_Contacto INT REFERENCES Cls_Catalogos (Id_Catalogo),
Contacto VARCHAR(200) NOT NULL,
Codigo_Postal VARCHAR(10) NULL,
Pais INT REFERENCES Cls_Catalogos (Id_Catalogo),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Tbl_Usuarios
(
Id_Usuario INT PRIMARY KEY IDENTITY (1,1),
Id_Persona INT REFERENCES Tbl_Datos_Personales (Id_Persona),
Usuario VARCHAR(50) NOT NULL,
Contrasena VARCHAR(255) NOT NULL,
Ultima_Sesion DATETIME NULL,
Ultima_Cambio_Credenciales DATETIME NULL,
Intentos_Sesion INT NULL,
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Cls_Horario
(
Id_Horario INT PRIMARY KEY IDENTITY(1,1),
Id_Turno INT REFERENCES Cls_Catalogos (Id_Catalogo),
Horario VARCHAR(20),    --Primer hora, Segunda hora
Hora_Inicio TIME NOT NULL, --Inicio Hora
Hora_Fin TIME NOT NULL,    --Fin Hora
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Cls_Asignaturas
(
Id_Asignatura INT PRIMARY KEY IDENTITY(1,1),
Nombre_Asignatura VARCHAR(50),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Cls_Grupos
(
Id_Grupo INT PRIMARY KEY IDENTITY(1,1),
Codigo_Grupo varchar(20),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados (Id_Estado)
)

CREATE TABLE Cls_Grupo_Asignaturas
(
Id_Grupo_Asignatura INT PRIMARY KEY IDENTITY(1,1),
Id_Grupo INT REFERENCES Cls_Grupos(Id_Grupo),
Id_Asignatura INT REFERENCES Cls_Asignaturas(Id_Asignatura),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado),  
)

CREATE TABLE Cls_Grupo_Turno
(
Id_Grupo_Turno INT PRIMARY KEY IDENTITY(1,1),
Id_Grupo INT REFERENCES Cls_Grupos(Id_Grupo),
Id_Horario INT REFERENCES Cls_Horario(Id_Horario),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado),  
)

CREATE TABLE Tbl_Docentes
(
Id_Docente INT PRIMARY KEY IDENTITY(1,1),
Id_Persona INT REFERENCES Tbl_Datos_Personales(Id_Persona),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado)
)

CREATE TABLE Cls_Docente_Grupo
(
Id_Docente_Grupo INT PRIMARY KEY IDENTITY(1,1),
Id_Docente INT REFERENCES Tbl_Docentes(Id_Docente),
Id_Grupo INT REFERENCES Cls_Grupos(Id_Grupo),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado),
)

CREATE TABLE Tbl_Estudiantes
(
Id_Estudiante INT PRIMARY KEY IDENTITY(1,1),
Id_Persona	INT REFERENCES Tbl_Datos_Personales(Id_Persona),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado)
)

CREATE TABLE Cls_Estudiante_Grupo
(
Id_Estudiante_Grupo INT PRIMARY KEY IDENTITY(1,1),
Id_Estudiante INT REFERENCES Tbl_Estudiantes(Id_Estudiante),
Id_Grupo INT REFERENCES Cls_Grupos(Id_Grupo),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado)
)

CREATE TABLE Tbl_Asistencias
(
Id_Asistencia INT PRIMARY KEY IDENTITY(1,1),
Id_Estudiante_Grupo INT REFERENCES Cls_Estudiante_Grupo(Id_Estudiante_Grupo),
Id_Grupo_Asignatura INT REFERENCES Cls_Grupo_Asignaturas(Id_Grupo_Asignatura),
Id_Docente_Grupo INT REFERENCES Cls_Docente_Grupo(Id_Docente_Grupo),
Fecha DATE NOT NULL,  -- Fecha de la asistencia
Asistio VARCHAR(20) NOT NULL,  -- 1: Asistió, 0: No asistió
Observacion VARCHAR(100),
Fecha_Creacion DATETIME NOT NULL,
Fecha_Modificacion DATETIME NOT NULL,
Id_Creador INT,
Id_Modificador INT,
Id_Estado INT REFERENCES Cls_Estados(Id_Estado)
)

--Tbl_Datos_Personales
ALTER TABLE Tbl_Datos_Personales ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario)
ALTER TABLE Tbl_Datos_Personales ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario)

-- Cls_Estados
ALTER TABLE Cls_Estados ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Estados ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Tipo_Catalogos
ALTER TABLE Cls_Tipo_Catalogos ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Tipo_Catalogos ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Catalogos
ALTER TABLE Cls_Catalogos ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Catalogos ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Tbl_Contactos
ALTER TABLE Tbl_Contactos ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Tbl_Contactos ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Tbl_Usuarios
ALTER TABLE Tbl_Usuarios ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Tbl_Usuarios ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Horario
ALTER TABLE Cls_Horario ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Horario ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Asignaturas
ALTER TABLE Cls_Asignaturas ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Asignaturas ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Grupos
ALTER TABLE Cls_Grupos ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Grupos ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Grupo_Asignaturas
ALTER TABLE Cls_Grupo_Asignaturas ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Grupo_Asignaturas ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Grupo_Turno
ALTER TABLE Cls_Grupo_Turno ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Grupo_Turno ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Tbl_Docentes
ALTER TABLE Tbl_Docentes ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Tbl_Docentes ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Docente_Grupo
ALTER TABLE Cls_Docente_Grupo ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Docente_Grupo ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Tbl_Estudiantes
ALTER TABLE Tbl_Estudiantes ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Tbl_Estudiantes ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Cls_Estudiante_Grupo
ALTER TABLE Cls_Estudiante_Grupo ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Cls_Estudiante_Grupo ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

-- Tbl_Asistencias
ALTER TABLE Tbl_Asistencias ADD FOREIGN KEY (Id_Creador) REFERENCES Tbl_Usuarios(Id_Usuario);
ALTER TABLE Tbl_Asistencias ADD FOREIGN KEY (Id_Modificador) REFERENCES Tbl_Usuarios(Id_Usuario);

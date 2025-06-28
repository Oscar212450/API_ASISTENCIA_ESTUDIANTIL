-- Crear la base de datos
CREATE DATABASE BooKMarkets;
GO

USE BooKMarkets;
GO

-- Tabla Estados
CREATE TABLE Estados (
    Id_estado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla Autores
CREATE TABLE Autores (
    Id_autor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Biografia TEXT,
    Sitio_web VARCHAR(255),
    Fecha_creacion DATE DEFAULT GETDATE()
);

-- Tabla Categorías
CREATE TABLE Categorias (
    Id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre_Categoria VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla Libros
CREATE TABLE Libros (
    Id_Libro INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(255) NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,
    Autor_id INT NOT NULL FOREIGN KEY REFERENCES Autores(Id_autor) ON DELETE CASCADE,
    Categoria_id INT FOREIGN KEY REFERENCES Categorias(Id_categoria) ON DELETE SET NULL,
    Precio NUMERIC(8,2) NOT NULL CHECK (Precio >= 0),
    Estado_id INT FOREIGN KEY REFERENCES Estados(Id_estado),
    Fecha_publicacion DATE,
    Fecha_creacion DATETIME DEFAULT GETDATE()
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    Id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Correo_electronico VARCHAR(255) NOT NULL UNIQUE,
    Hash_contrasena VARCHAR(255) NOT NULL,
    Estado_id INT FOREIGN KEY REFERENCES Estados(Id_estado),
    Telefono VARCHAR(20),
    Fecha_creado DATETIME DEFAULT GETDATE()
);

-- Tabla Pedidos
CREATE TABLE Pedidos (
    Id_pedido INT IDENTITY(1,1) PRIMARY KEY,
    Usuario_id INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id_usuario),
    Monto_total NUMERIC(10,2) NOT NULL CHECK (Monto_total >= 0),
    Estado_id INT FOREIGN KEY REFERENCES Estados(Id_estado),
    Fecha_pedido DATE DEFAULT GETDATE()
);

-- Tabla Detalle_pedido
CREATE TABLE Detalle_pedido (
    Id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    Pedido_id INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id_pedido) ON DELETE CASCADE,
    Libro_id INT NOT NULL FOREIGN KEY REFERENCES Libros(Id_Libro),
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    Precio_unitario NUMERIC(8,2) NOT NULL CHECK (Precio_unitario >= 0)
);

-- Tabla Pagos
CREATE TABLE Pagos (
    Id_pago INT IDENTITY(1,1) PRIMARY KEY,
    Pedido_id INT NOT NULL FOREIGN KEY REFERENCES Pedidos(Id_pedido),
    Fecha_pago DATE DEFAULT GETDATE(),
    Monto NUMERIC(10,2) NOT NULL CHECK (Monto > 0),
    Metodo_pago VARCHAR(50) NOT NULL,
    Transaccion_id VARCHAR(255) UNIQUE,
    Estado_id INT FOREIGN KEY REFERENCES Estados(Id_estado)
);

select * from Autores;
select * from Categorias;
select * from Detalle_pedido
select * from Estados;
select * from Libros;
select * from Pagos;
select * from Pedidos;
select * from Usuarios;

select Biografia from Autores where Biografia = 'string' or Biografia like 'E%';

Alter Table Autores 
Alter Column Biografia varchar(MAX);
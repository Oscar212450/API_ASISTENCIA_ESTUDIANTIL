USE BooKMarkets;
GO

-- Insertar Estados
INSERT INTO Estados (Nombre) VALUES
('Activo'),
('Inactivo'),
('Pendiente'),
('Cancelado');

-- Insertar Autores
INSERT INTO Autores (Nombre, Biografia, Sitio_web) VALUES
('Gabriel Garc�a M�rquez', 'Escritor colombiano, ganador del Nobel de Literatura.', 'http://gabrielgarciamarquez.com'),
('Isabel Allende', 'Escritora chilena conocida por sus novelas hist�ricas.', 'http://isabelallende.com'),
('J.K. Rowling', 'Autora brit�nica de la serie Harry Potter.', 'http://jkrowling.com');

-- Insertar Categorias
INSERT INTO Categorias (Nombre_Categoria) VALUES
('Ficci�n'),
('No Ficci�n'),
('Ciencia Ficci�n'),
('Historia');

-- Insertar Libros
INSERT INTO Libros (Titulo, Descripcion, Autor_id, Categoria_id, Precio, Estado_id, Fecha_publicacion) VALUES
('Cien a�os de soledad', 'Novela emblem�tica del realismo m�gico.', 1, 1, 350.00, 1, '1967-05-30'),
('La casa de los esp�ritus', 'Novela sobre una familia chilena a lo largo de varias generaciones.', 2, 1, 280.50, 1, '1982-01-15'),
('Harry Potter y la piedra filosofal', 'Primera novela de la serie Harry Potter.', 3, 3, 450.75, 1, '1997-06-26');

-- Insertar Usuarios
INSERT INTO Usuarios (Nombre, Apellido, Correo_electronico, Hash_contrasena, Estado_id, Telefono) VALUES
('Carlos', 'P�rez', 'carlos.perez@mail.com', 'hashed_password1', 1, '555-1234'),
('Ana', 'Gonz�lez', 'ana.gonzalez@mail.com', 'hashed_password2', 1, '555-5678');

-- Insertar Pedidos
INSERT INTO Pedidos (Usuario_id, Monto_total, Estado_id) VALUES
(1, 350.00, 1),
(2, 731.25, 1);

-- Insertar Detalle_pedido
INSERT INTO Detalle_pedido (Pedido_id, Libro_id, Cantidad, Precio_unitario) VALUES
(1, 1, 1, 350.00),
(2, 2, 1, 280.50),
(2, 3, 1, 450.75);

-- Insertar Pagos
INSERT INTO Pagos (Pedido_id, Monto, Metodo_pago, Transaccion_id, Estado_id) VALUES
(1, 350.00, 'Tarjeta Cr�dito', 'TX123456789', 1),
(2, 731.25, 'PayPal', 'TX987654321', 1);

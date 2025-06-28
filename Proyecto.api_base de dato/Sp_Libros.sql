CREATE PROC SP_LIBROS
(
    @Id_Libro INT = NULL,
    @Titulo VARCHAR(255) = NULL,
    @Descripcion VARCHAR(255) = NULL,
    @Autor_id INT = NULL,
    @Categoria_id INT = NULL,
    @Precio NUMERIC(8,2) = NULL,
    @Estado_id INT = NULL,
    @Fecha_publicacion DATE = NULL,
    @Fecha_creacion DATETIME = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF @Fecha_creacion IS NULL SET @Fecha_creacion = GETDATE()

    IF @Accion = 'INS'
    BEGIN
        IF ISNULL(@Titulo, '') = '' OR ISNULL(@Descripcion, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'Título y Descripción son obligatorios'
        END
        ELSE
        BEGIN
            BEGIN TRY
                INSERT INTO Libros (Titulo, Descripcion, Autor_id, Categoria_id, Precio, Estado_id, Fecha_publicacion, Fecha_creacion)
                VALUES (@Titulo, @Descripcion, @Autor_id, @Categoria_id, @Precio, @Estado_id, @Fecha_publicacion, @Fecha_creacion)

                SET @O_Numero = 0
                SET @O_Msg = 'Libro insertado correctamente'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = ERROR_MESSAGE()
            END CATCH
        END
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Libros WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

CREATE PROC SP_AUTORES
(
    @Id_autor INT = NULL,
    @Nombre VARCHAR(150) = NULL,
    @Biografia VARCHAR(MAX) = NULL, 
    @Sitio_web VARCHAR(255) = NULL,
    @Fecha_creacion DATE = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Asignar fecha de creaci�n si no viene
    IF @Fecha_creacion IS NULL
    BEGIN
        SET @Fecha_creacion = GETDATE()
    END

    -- Acci�n INSERTAR
    IF (@Accion = 'INS')
    BEGIN
        IF ISNULL(@Nombre, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'El par�metro NOMBRE no puede ser nulo'
        END
        ELSE
        BEGIN
            BEGIN TRAN TRX_INSERTAR_AUTOR
            BEGIN TRY
                INSERT INTO Autores (Nombre, Biografia, Sitio_web, Fecha_creacion)
                VALUES (@Nombre, @Biografia, @Sitio_web, @Fecha_creacion)

                COMMIT TRAN TRX_INSERTAR_AUTOR
                SET @O_Numero = 0
                SET @O_Msg = 'Autor insertado correctamente'
            END TRY
            BEGIN CATCH
                ROLLBACK TRAN TRX_INSERTAR_AUTOR
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), ' - ', ERROR_MESSAGE())
            END CATCH
        END
    END

    -- Acci�n ACTUALIZAR
    ELSE IF (@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_autor, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'El par�metro ID_AUTOR no puede ser nulo'
        END
        ELSE
        BEGIN
            BEGIN TRAN TRX_ACTUALIZAR_AUTOR
            BEGIN TRY
                UPDATE Autores
                SET 
                    Nombre = COALESCE(@Nombre, Nombre),
                    Biografia = COALESCE(@Biografia, Biografia),
                    Sitio_web = COALESCE(@Sitio_web, Sitio_web),
                    Fecha_creacion = COALESCE(@Fecha_creacion, Fecha_creacion)
                WHERE Id_autor = @Id_autor

                COMMIT TRAN TRX_ACTUALIZAR_AUTOR
                SET @O_Numero = 0
                SET @O_Msg = 'Autor actualizado correctamente'
            END TRY
            BEGIN CATCH
                ROLLBACK TRAN TRX_ACTUALIZAR_AUTOR
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), ' - ', ERROR_MESSAGE())
            END CATCH
        END
    END

    -- Acci�n LISTAR TODOS
    ELSE IF (@Accion = 'LIS')
    BEGIN
        SELECT Id_autor, Nombre, Biografia, Sitio_web, Fecha_creacion
        FROM Autores WITH (NOLOCK)
        ORDER BY Id_autor DESC

        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END

    -- Acci�n FILTRAR POR NOMBRE
    ELSE IF (@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Nombre, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'El par�metro NOMBRE no puede ser nulo'
        END
        ELSE
        BEGIN
            SELECT Id_autor, Nombre, Biografia, Sitio_web, Fecha_creacion
            FROM Autores WITH (NOLOCK)
            WHERE Nombre LIKE '%' + @Nombre + '%'
            ORDER BY Id_autor DESC

            SET @O_Numero = 0
            SET @O_Msg = 'Correcto'
        END
    END

    -- Acci�n OBTENER POR ID
    ELSE IF (@Accion = 'GET')
    BEGIN
        IF ISNULL(@Id_autor, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'El par�metro ID_AUTOR no puede ser nulo'
        END
        ELSE
        BEGIN
            SELECT Id_autor, Nombre, Biografia, Sitio_web, Fecha_creacion
            FROM Autores WITH (NOLOCK)
            WHERE Id_autor = @Id_autor

            SET @O_Numero = 0
            SET @O_Msg = 'Correcto'
        END
    END

    -- Acci�n no v�lida
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acci�n no v�lida. Usa INS, UPD, LIS, FIL o GET.'
    END
END

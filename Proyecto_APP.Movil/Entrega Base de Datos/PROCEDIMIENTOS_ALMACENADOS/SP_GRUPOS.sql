CREATE OR ALTER PROC SP_GRUPOS
(
    @Id_Grupo INT = NULL,
    @Codigo_Grupo VARCHAR(20) = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Id_Estado INT = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    IF ISNULL(@Fecha_Creacion, '') = ''
    BEGIN
        SET @Fecha_Creacion = GETDATE();
    END

    IF ISNULL(@Fecha_Modificacion, '') = ''
    BEGIN
        SET @Fecha_Modificacion = GETDATE();
    END

    IF @Accion = 'INS'
    BEGIN
        IF ISNULL(@Codigo_Grupo, '') = ''
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El Codigo de grupo no puede ser nulo.';
        END
		IF EXISTS (
    SELECT 1 FROM Cls_Grupos
    WHERE Id_Grupo= @Id_Grupo AND Codigo_Grupo = @Codigo_Grupo
)
BEGIN
    SET @O_Numero = -1
    SET @O_Msg = 'YA EXISTE UNA RELACIÓN ENTRE ESTE GRUPO Y TURNO'
    RETURN
END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_GRUPOS
            BEGIN TRY
                INSERT INTO Cls_Grupos (
                   
                    Codigo_Grupo,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    
                    @Codigo_Grupo,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                );

                COMMIT TRANSACTION TRX_INSERTAR_GRUPOS
                SET @O_Numero = 0;
                SET @O_Msg = 'Grupo insertado correctamente.';
            END TRY
            BEGIN CATCH
                ROLLBACK TRANSACTION;
                SET @O_Numero = ERROR_NUMBER();
                SET @O_Msg = ERROR_MESSAGE();
            END CATCH
        END
    END
    ELSE IF @Accion = 'UPD'
    BEGIN
        IF ISNULL(@Id_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de grupo no puede ser nulo.';
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            BEGIN TRY
                UPDATE Cls_Grupos
                SET 
                    Codigo_Grupo = COALESCE(@Codigo_Grupo, Codigo_Grupo),
                    Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Grupo = @Id_Grupo;

                COMMIT TRANSACTION;
                SET @O_Numero = 0;
                SET @O_Msg = 'Grupo actualizado correctamente.';
            END TRY
            BEGIN CATCH
                ROLLBACK TRANSACTION;
                SET @O_Numero = ERROR_NUMBER();
                SET @O_Msg = ERROR_MESSAGE();
            END CATCH
        END
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Cls_Grupos
		ORDER BY Codigo_Grupo DESC

        SET @O_Numero = 0;
        SET @O_Msg = 'Listado completado correctamente.';
    END
    ELSE IF @Accion = 'FIL'
    BEGIN
        IF ISNULL(@Codigo_Grupo, '') = ''
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El CODIGO_GRUPO no puede ser nulo para la búsqueda.';
        END
        ELSE
        BEGIN
            SELECT * FROM Cls_Grupos
            WHERE Codigo_Grupo LIKE '%' + @Codigo_Grupo  + '%'
            SET @O_Numero = 0;
            SET @O_Msg = 'Filtrado completado correctamente.';
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Grupo, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_GRUPO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Grupos (NOLOCK)
        WHERE Id_Grupo = @Id_Grupo

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
END
    ELSE
    BEGIN
        SET @O_Numero = -1;
        SET @O_Msg = 'Acción no reconocida.';
    END
END;




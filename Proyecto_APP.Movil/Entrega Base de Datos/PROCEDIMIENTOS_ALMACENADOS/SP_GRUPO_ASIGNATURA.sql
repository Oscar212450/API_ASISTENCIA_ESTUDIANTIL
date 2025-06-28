CREATE OR ALTER PROC SP_GRUPO_ASIGNATURA
(
    @Id_Grupo_Asignatura INT = NULL,
    @Id_Grupo INT = NULL,
    @Id_Asignatura INT = NULL,
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
        IF ISNULL(@Id_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de grupo no puede ser nulo.';
        END
        ELSE IF ISNULL(@Id_Asignatura, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de Asignatura no puede ser nulo.';
        END
		IF EXISTS (
    SELECT 1 FROM Cls_Grupo_Asignaturas
    WHERE Id_Asignatura = @Id_Asignatura AND Id_Grupo= @Id_Grupo
)
BEGIN
    SET @O_Numero = -1
    SET @O_Msg = 'YA EXISTE UNA RELACIÓN ENTRE ESTE ESTUDIANTE Y GRUPO'
    RETURN
END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_GRUPO_ASIGNATURA;
            BEGIN TRY
                INSERT INTO Cls_Grupo_Asignaturas(
                    Id_Grupo,
                    Id_Asignatura,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Id_Grupo,
                    @Id_Asignatura,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                );

                COMMIT TRANSACTION TRX_INSERTAR_GRUPO_ASIGNATURA;
                SET @O_Numero = 0;
                SET @O_Msg = 'Grupo-Asignatura insertado correctamente.';
            END TRY
            BEGIN CATCH
                ROLLBACK TRANSACTION TRX_INSERTAR_GRUPO_ASIGNATURA;
                SET @O_Numero = ERROR_NUMBER();
                SET @O_Msg = ERROR_MESSAGE();
            END CATCH
        END
    END
    ELSE IF @Accion = 'UPD'
    BEGIN
        IF ISNULL(@Id_Grupo_Asignatura, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de GRUPO_ASIGNATURA no puede ser nulo.';
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            BEGIN TRY
                UPDATE Cls_Grupo_Asignaturas
                SET 
                    Id_Grupo = COALESCE(@Id_Grupo, Id_Grupo),
                    Id_Asignatura = COALESCE(@Id_Asignatura, Id_Asignatura),
                    Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Grupo_Asignatura = @Id_Grupo_Asignatura;

                COMMIT TRANSACTION;
                SET @O_Numero = 0;
                SET @O_Msg = 'GRUPO_ASIGNATURA actualizado correctamente.';
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
        SELECT * FROM Cls_Grupo_Asignaturas
		ORDER BY Id_Grupo_Asignatura DESC

        SET @O_Numero = 0;
        SET @O_Msg = 'Listado completado correctamente.';
    END
    ELSE IF @Accion = 'FIL'
    BEGIN
        IF ISNULL(@Id_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID_GRUPO no puede ser nulo para la búsqueda.';
        END
        ELSE
        BEGIN
            SELECT * FROM Cls_Grupo_Asignaturas
            WHERE Id_Grupo = @Id_Grupo 
            SET @O_Numero = 0;
            SET @O_Msg = 'Filtrado completado correctamente.';
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Grupo_Asignatura, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_GRUPO_ASIGNATURA NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Grupo_Asignaturas (NOLOCK)
        WHERE Id_Grupo_Asignatura = @Id_Grupo_Asignatura

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




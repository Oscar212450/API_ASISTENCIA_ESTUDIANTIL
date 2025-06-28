CREATE OR ALTER PROC SP_GRUPO_TURNO
(
    @Id_Grupo_Turno INT = NULL,
    @Id_Grupo INT = NULL,
    @Id_Horario INT = NULL,
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
        ELSE IF ISNULL(@Id_Horario, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de horario no puede ser nulo.';
        END
		IF EXISTS (
    SELECT 1 FROM Cls_Grupo_Turno
    WHERE Id_Horario = @Id_Horario AND Id_Grupo= @Id_Grupo
)
BEGIN
    SET @O_Numero = -1
    SET @O_Msg = 'YA EXISTE UNA RELACIÓN ENTRE ESTE GRUPO Y TURNO'
    RETURN
END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            BEGIN TRY
                INSERT INTO Cls_Grupo_Turno (
                    Id_Grupo,
                    Id_Horario,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Id_Grupo,
                    @Id_Horario,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                );

                COMMIT TRANSACTION;
                SET @O_Numero = 0;
                SET @O_Msg = 'Grupo-Turno insertado correctamente.';
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
        IF ISNULL(@Id_Grupo_Turno, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de grupo-turno no puede ser nulo.';
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION;
            BEGIN TRY
                UPDATE Cls_Grupo_Turno
                SET 
                    Id_Grupo = COALESCE(@Id_Grupo, Id_Grupo),
                    Id_Horario = COALESCE(@Id_Horario, Id_Horario),
                    Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Grupo_Turno = @Id_Grupo_Turno;

                COMMIT TRANSACTION;
                SET @O_Numero = 0;
                SET @O_Msg = 'Grupo-Turno actualizado correctamente.';
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
        SELECT * FROM Cls_Grupo_Turno;
        SET @O_Numero = 0;
        SET @O_Msg = 'Listado completado correctamente.';
    END
    ELSE IF @Accion = 'FIL'
    BEGIN
        IF ISNULL(@Id_Horario, 0) = 0
        BEGIN
            SET @O_Numero = -1;
            SET @O_Msg = 'El ID de Horario no puede ser nulo para la búsqueda.';
        END
        ELSE
        BEGIN
            SELECT * FROM Cls_Grupo_Turno
            WHERE Id_Horario = @Id_Horario;
            SET @O_Numero = 0;
      SET @O_Msg = 'Filtrado completado correctamente.';
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Grupo_Turno, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_GRUPO_TURNO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Grupo_Turno (NOLOCK)

        WHERE Id_Grupo_Turno = @Id_Grupo_Turno

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




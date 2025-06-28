CREATE OR ALTER PROC SP_HORARIOS(

		@Id_Horario INT = NULL,
		@Id_Turno INT = NULL,
		@Horario VARCHAR(20) = NULL,    
		@Hora_Inicio TIME = NULL,
		@Hora_Fin TIME = NULL, 
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
        SET @Fecha_Creacion = GETDATE()
    END

    IF ISNULL(@Fecha_Modificacion, '') = ''
    BEGIN
        SET @Fecha_Modificacion = GETDATE()
    END

    IF(@Accion = 'INS')
    BEGIN
        IF ISNULL(@Id_Turno, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_TURNO NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Horario, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO HORARIO NO PUEDE SER NULO'
        END
		 ELSE IF ISNULL(@Hora_Inicio, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO HORA_INICIO NO PUEDE SER NULO'
        END
		 ELSE IF ISNULL(@Hora_Fin, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO HORA_FIN NO PUEDE SER NULO'
        END
		IF EXISTS (
    SELECT 1 FROM Cls_Horario
    WHERE Id_Horario = @Id_Horario AND Horario = @Horario
)
BEGIN
    SET @O_Numero = -1
    SET @O_Msg = 'YA EXISTE UNA RELACIÓN ENTRE ESTE GRUPO Y TURNO'
    RETURN
END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_HORARIO
            BEGIN TRY
                INSERT INTO Cls_Horario(
                    
					Id_Turno,
					Horario,
					Hora_Inicio,
					Hora_Fin, 
					Fecha_Creacion,
					Fecha_Modificacion,
					Id_Creador,
					Id_Modificador,
					Id_Estado 
					
                )
                VALUES (
                    @Id_Turno,
					@Horario,
					@Hora_Inicio,
					@Hora_Fin, 
					@Fecha_Creacion,
					@Fecha_Modificacion,
					@Id_Creador,
					@Id_Modificador,
					@Id_Estado 
					

                )

                COMMIT TRAN TRX_INSERTAR_HORARIO
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_HORARIO
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Horario, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_HORARIO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_HORARIO
            BEGIN TRY
                UPDATE Cls_Horario
                SET
                   Id_Turno = COALESCE (@Id_Turno, Id_Turno),
					Horario = COALESCE (@Horario, Horario),
					Hora_Inicio = COALESCE (@Hora_Inicio, Hora_Inicio),
					Hora_Fin = COALESCE (@Hora_Fin, Hora_Fin),
					Fecha_Modificacion = COALESCE (@Fecha_Modificacion, Fecha_Modificacion),
					Id_Modificador = COALESCE (@Id_Modificador, Id_Modificador),
					Id_Estado = COALESCE (@Id_Estado, Id_Estado)
                WHERE Id_Horario = @Id_Horario

               COMMIT TRAN TRX_ACTUALIZAR_HORARIO
                SET @O_Numero = 0
                SET @O_Msg = 'HORARIO ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_HORARIO
            END CATCH
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Cls_Horario (NOLOCK)
        ORDER BY Id_Horario DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Horario, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO HORARIO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            SELECT *
            FROM Cls_Horario (NOLOCK)
            WHERE Horario Like '%'+@Horario+'%'
            ORDER BY Id_Horario DESC

            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Horario, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_HORARIO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Horario (NOLOCK)
        WHERE Id_Horario = @Id_Horario

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'OPCION NO DISPONIBLE'
    END
END


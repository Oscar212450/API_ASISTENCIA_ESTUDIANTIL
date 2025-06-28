---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	CREATE OR ALTER PROC SP_ASIGNATURAS
(
    @Id_Asignatura INT = NULL,
    @Nombre_Asignatura VARCHAR(50) = NULL,
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
        IF ISNULL(@Nombre_Asignatura, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO NOMBRE_ASIGNATURA NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_ASIGNATURAS
            BEGIN TRY
                INSERT INTO Cls_Asignaturas(
                    Nombre_Asignatura,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Nombre_Asignatura,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                )

                COMMIT TRAN TRX_INSERTAR_ASIGNATURAS
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_ASIGNATURAS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Asignatura, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ASIGNATURA NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_ASIGNATURAS
            BEGIN TRY
                UPDATE Cls_Asignaturas
                SET
                    Nombre_Asignatura = COALESCE(@Nombre_Asignatura, Nombre_Asignatura),
                    Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Asignatura = @Id_Asignatura

                COMMIT TRAN TRX_ACTUALIZAR_ASIGNATURAS
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_ASIGNATURAS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Cls_Asignaturas (NOLOCK)
        ORDER BY Id_Asignatura DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Nombre_Asignatura, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO NOMBRE_ASIGNATURA NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            SELECT *
            FROM Cls_Asignaturas (NOLOCK)
            WHERE Nombre_Asignatura LIKE '%'+@Nombre_Asignatura+'%' OR
			Id_Asignatura = @Id_Asignatura
            ORDER BY Id_Asignatura DESC

            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Asignatura, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_ASIGNATURA NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Asignaturas (NOLOCK)
  
      WHERE Id_Asignatura = @Id_Asignatura

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




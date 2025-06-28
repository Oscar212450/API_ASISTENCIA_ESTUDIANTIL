CREATE OR ALTER PROC SP_ESTUDIANTES
(
    @Id_Estudiante INT = NULL,
    @Id_Persona INT = NULL,
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

    IF (@Accion = 'INS')
    BEGIN
        IF ISNULL(@Id_Persona, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_PERSONA NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_DOCENTE
            BEGIN TRY
                INSERT INTO Tbl_Estudiantes(
                    Id_Persona,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Id_Persona,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                )
                
                COMMIT TRAN TRX_INSERTAR_ESTUDIANTE
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_ESTUDIANTE
            END CATCH
        END
    END
    ELSE IF (@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Estudiante, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_ESTUDIANTE
            BEGIN TRY
                UPDATE Tbl_Estudiantes
                SET
                    Id_Persona = COALESCE(@Id_Persona, Id_Persona),
                    Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Estudiante = @Id_Estudiante
                
                COMMIT TRAN TRX_ACTUALIZAR_ESTUDIANTE
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_ESTUDIANTE
            END CATCH
        END
    END
    ELSE IF (@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Tbl_Estudiantes (NOLOCK)
        ORDER BY Id_Estudiante DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF (@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Id_Estudiante, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            SELECT *
            FROM Tbl_Estudiantes (NOLOCK)
            WHERE Id_Estudiante = @Id_Estudiante
            
            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Estudiante, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Tbl_Estudiantes (NOLOCK)
  
      WHERE Id_Estudiante = @Id_Estudiante

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




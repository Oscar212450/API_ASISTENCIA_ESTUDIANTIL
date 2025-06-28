CREATE OR ALTER PROC SP_ESTUDIANTE_GRUPO
(
    @Id_Estudiante_Grupo INT = NULL,
	@Id_Estudiante INT = NULL,
	@Id_Grupo INT = NULL,
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
        IF ISNULL(@Id_Estudiante, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE NO PUEDE SER NULO'
        END
		IF EXISTS (
    SELECT 1 FROM Cls_Estudiante_Grupo
    WHERE Id_Estudiante = @Id_Estudiante AND Id_Grupo = @Id_Grupo
)
BEGIN
    SET @O_Numero = -1
    SET @O_Msg = 'YA EXISTE UNA RELACIÓN ENTRE ESTE ESTUDIANTE Y GRUPO'
    RETURN
END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_ESUTDIANTE
            BEGIN TRY
                INSERT INTO Cls_Estudiante_Grupo(
                    Id_Estudiante,
					Id_Grupo,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Id_Estudiante,
					@Id_Grupo,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                )
                
                COMMIT TRAN TRX_INSERTAR_ESTUDIANTE_GRUPO
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_ESTUDIANTE_GRUPO
            END CATCH
        END
    END
    ELSE IF (@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Estudiante_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE_GRUPO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_ESTUDIANTE_GRUPO
            BEGIN TRY
                UPDATE Cls_Estudiante_Grupo
                SET
                    Id_Estudiante = COALESCE(@Id_Estudiante, Id_Estudiante),
					Id_Grupo = COALESCE(@Id_Grupo, Id_Grupo),
                    Fecha_Modificacion =COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
                    Id_Modificador =COALESCE(@Id_Modificador, Id_Modificador),
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Estudiante_Grupo = @Id_Estudiante_Grupo
                
                COMMIT TRAN TRX_ACTUALIZAR_ESTUDIANTE_GRUPO
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_ESTUDIANTE_GRUPO
            END CATCH
        END
    END
    ELSE IF (@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Cls_Estudiante_Grupo (NOLOCK)
        ORDER BY Id_Estudiante_Grupo DESC

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
            FROM Cls_Estudiante_Grupo (NOLOCK)
            WHERE Id_Estudiante = @Id_Estudiante
            
            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Estudiante_Grupo, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE_GRUPO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Estudiante_Grupo (NOLOCK)
        WHERE Id_Estudiante_Grupo = @Id_Estudiante_Grupo

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




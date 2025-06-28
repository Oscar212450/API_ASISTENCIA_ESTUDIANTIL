CREATE OR ALTER PROC SP_ESTADOS
(
    @Id_Estado INT = NULL,
    @Estado VARCHAR(80) = NULL,
    @Fecha_Creacion DATETIME = NULL,
    @Fecha_Modificacion DATETIME = NULL,
    @Id_Creador INT = NULL,
    @Id_Modificador INT = NULL,
    @Activo BIT = NULL,
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
        IF ISNULL(@Estado, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ESTADO NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Activo, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ACTIVO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_ESTADOS
            BEGIN TRY
                INSERT INTO Cls_Estados (
                    Estado,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Activo
                )
                VALUES (
                    @Estado,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Activo
                )
                
                COMMIT TRAN TRX_INSERTAR_ESTADOS
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_ESTADOS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Estado, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID ESTADO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_ESTADOS
            BEGIN TRY
                UPDATE Cls_Estados
                SET
                    Estado = COALESCE(@Estado, Estado),
                    Fecha_Modificacion = @Fecha_Modificacion,
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Activo = COALESCE(@Activo, Activo)
                WHERE Id_Estado = @Id_Estado

                COMMIT TRAN TRX_ACTUALIZAR_ESTADOS
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_ESTADOS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT
		*
		FROM Cls_Estados (NOLOCK)
        --where Id_Estado = 10
		ORDER BY Id_Estado DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
        
		IF ISNULL(@Estado, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ESTADO NO PUEDE SER NULO O VACIO'
        END
        ELSE
        BEGIN
            SELECT * FROM Cls_Estados (NOLOCK)
            WHERE Estado LIKE @Estado + '%' 
            ORDER BY Id_Estado DESC

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




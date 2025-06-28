CREATE OR ALTER PROC SP_TIPO_CATALOGOS
(
    @Id_Tipo_Catalogo INT = NULL,
    @Tipo_Catalogo VARCHAR(80) = NULL,
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
        IF ISNULL(@Tipo_Catalogo, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO TIPO CATALOGO NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Activo, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ACTIVO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_TIPO_CATALOGOS
            BEGIN TRY
                INSERT INTO Cls_Tipo_Catalogos (
                    Tipo_Catalogo,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Activo
                )
                VALUES (
                    @Tipo_Catalogo,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Activo
                )
                
                COMMIT TRAN TRX_INSERTAR_TIPO_CATALOGOS
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_TIPO_CATALOGOS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Tipo_Catalogo, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID TIPO CATALOGO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_TIPO_CATALOGOS
            BEGIN TRY
                UPDATE Cls_Tipo_Catalogos
                SET
                    Tipo_Catalogo = COALESCE(@Tipo_Catalogo, Tipo_Catalogo),
                    Fecha_Modificacion = @Fecha_Modificacion,
                    Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
                    Activo = COALESCE(@Activo, Activo)
                WHERE Id_Tipo_Catalogo = @Id_Tipo_Catalogo

                COMMIT TRAN TRX_ACTUALIZAR_TIPO_CATALOGOS
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_TIPO_CATALOGOS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT * FROM Cls_Tipo_Catalogos (NOLOCK)
        ORDER BY Id_Tipo_Catalogo DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
		IF ISNULL(@Tipo_Catalogo, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO TIPO CATALOGO NO PUEDE SER NULO O VACIO'
        END
        ELSE
        BEGIN
            SELECT * FROM Cls_Tipo_Catalogos (NOLOCK)
            WHERE Tipo_Catalogo LIKE '%' + @Tipo_Catalogo + '%'
            ORDER BY Id_Tipo_Catalogo DESC

            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Tipo_Catalogo, 0) = 0
   BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_TIPO_CATALOGO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Cls_Tipo_Catalogos (NOLOCK)
        WHERE Id_Tipo_Catalogo = @Id_Tipo_Catalogo

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



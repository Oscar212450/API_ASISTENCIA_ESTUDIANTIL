CREATE OR ALTER PROC SP_CONTACTOS
(

		@Id_Contacto INT = NULL,
		@Id_Persona INT = NULL,
		@Tipo_Contacto INT = NULL,
		@Contacto VARCHAR(200) = NULL,
		@Codigo_Postal VARCHAR(10) = NULL,
		@Pais INT = NULL,
		@Id_Creador INT = NULL,
		@Id_Modificador INT = NULL,
		@Fecha_Creacion DATETIME = NULL,
		@Fecha_Modificacion DATETIME = NULL,
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
        IF ISNULL(@Id_Persona, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_CONTACTO NO PUEDE SER NULO '
        END
		 ELSE IF ISNULL(@Tipo_Contacto, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO TIPO_CONTACTO NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Contacto, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO CONTACTO NO PUEDE SER NULO'
        END
		ELSE IF ISNULL(@Id_Estado, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTADO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_CONTACTO
            BEGIN TRY
                INSERT INTO Tbl_Contactos(
                    Id_Persona,
                    Tipo_Contacto,
                    Contacto,
					Codigo_Postal,
					Pais,
					Id_Creador,
                    Id_Modificador,
					Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Estado
                )
                VALUES (
                    @Id_Persona,
                    @Tipo_Contacto,
                    @Contacto,
					@Codigo_Postal,
					@Pais,
					@Id_Creador,
                    @Id_Modificador,
					@Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Estado
                )

                COMMIT TRAN TRX_INSERTAR_CONTACTO
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_CONTACTO
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Contacto, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_CONTACTO NO PUEDE SER NULO O IGUAL A CERO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_CONTACTO
            BEGIN TRY
                UPDATE Tbl_Contactos
                SET
                    Id_Persona = COALESCE(@Id_Persona, Id_Persona),
                    Tipo_Contacto = COALESCE(@Tipo_Contacto, Tipo_Contacto),
                    Contacto = COALESCE(@Contacto, Contacto),
                    Codigo_Postal = COALESCE(@Codigo_Postal, Codigo_Postal),
                    Pais = COALESCE(@Pais, Pais),
					Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
					Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
					Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Contacto = @Id_Contacto

				IF @@ROWCOUNT = 0
				BEGIN
				ROLLBACK TRAN TRX_ACTUALIZAR_CONTACTO
				SET @O_Numero = -1
				SET @O_Msg = 'NO SE ENCONTRO EL CONTACTO PARA ACTUALIZAR'
			RETURN
			END

               COMMIT TRAN TRX_ACTUALIZAR_CONTACTO
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_CONTACTO
            END CATCH
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Tbl_Contactos (NOLOCK)
        ORDER BY Id_Contacto DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Contacto, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO CONTACTO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            SELECT *
            FROM Tbl_Contactos (NOLOCK)
            WHERE Contacto LIKE '%' + @Contacto + '%' 
            ORDER BY Id_Contacto DESC

            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Contacto, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_CONTACTO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Tbl_Contactos (NOLOCK)
        
WHERE Id_Contacto = @Id_Contacto

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




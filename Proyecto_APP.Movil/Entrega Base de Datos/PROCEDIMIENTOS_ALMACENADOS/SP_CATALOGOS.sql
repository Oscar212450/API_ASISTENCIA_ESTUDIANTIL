CREATE OR ALTER PROC SP_CATALOGOS
(
@Id_Catalogo INT = NULL,
@Id_Tipo_Catalogo INT = NULL,
@Catalogo VARCHAR(80) = NULL,
@Fecha_Creacion	DATETIME = NULL,
@Fecha_Modificacion	DATETIME = NULL,
@Id_Creador INT = NULL,
@Id_Modificador INT = NULL,
@Activo	BIT = NULL,
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
		IF ISNULL(@Id_Tipo_Catalogo, 0) = 0
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO TIPO CATALOGO NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@Catalogo, '') = ''
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO CATALOGO NO PUEDE SER NULO O VACIO'
			END
		ELSE IF ISNULL(@Activo, -1) = -1
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO ACTIVO NO PUEDE SER NULO'
			END
		ELSE
			BEGIN
				BEGIN TRANSACTION TRX_INSERTAR_CATALOGO
				BEGIN TRY
					INSERT INTO Cls_Catalogos (Id_Tipo_Catalogo, Catalogo, Fecha_Creacion, Fecha_Modificacion, Id_Creador,
								 Id_Modificador, Activo)
					VALUES (@Id_Tipo_Catalogo, @Catalogo, @Fecha_Creacion, @Fecha_Modificacion, @Id_Creador, @Id_Modificador, @Activo)
					
					COMMIT TRAN TRX_INSERTAR_CATALOGO
					SET @O_Numero = 0
					SET @O_Msg = 'INSERTADO'
				END TRY
				BEGIN CATCH
					SET @O_Numero = ERROR_NUMBER()
					SET @O_Msg = CONCAT(ERROR_PROCEDURE(),'-',ERROR_MESSAGE())
					ROLLBACK TRAN TRX_INSERTAR_CATALOGO
				END CATCH
			END
	END
	ELSE IF(@Accion = 'UPD')
		BEGIN
			IF ISNULL(@Id_Catalogo, 0) = 0
				BEGIN
					SET @O_Numero = -1
					SET @O_Msg = 'PARAMETRO ID_CATALOGO NO PUEDE SER NULO'
				END
			ELSE
				BEGIN
					BEGIN TRANSACTION TRX_ACTUALIZAR_CATALOGO
					BEGIN TRY
				
						UPDATE Cls_Catalogos
						SET
							Id_Tipo_Catalogo = COALESCE(@Id_Tipo_Catalogo, Id_Tipo_Catalogo),
							Catalogo = COALESCE(@Catalogo, Catalogo),
							Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
							Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
							Activo = COALESCE(@Activo, Activo)
						WHERE Id_Catalogo = @Id_Catalogo
					
						COMMIT TRAN TRX_ACTUALIZAR_CATALOGO
						SET @O_Numero = 0
						SET @O_Msg = 'ACTUALIZADO'
					END TRY
					BEGIN CATCH
						SET @O_Numero = ERROR_NUMBER()
						SET @O_Msg = CONCAT(ERROR_PROCEDURE(),'-',ERROR_MESSAGE())
						ROLLBACK TRAN TRX_ACTUALIZAR_CATALOGO
					END CATCH
				END
		END
	ELSE IF(@Accion = 'LIS')
				BEGIN
					SELECT *
					FROM Cls_Catalogos (NOLOCK)
					ORDER BY Id_Tipo_Catalogo DESC

					SET @O_Numero = 0
					SET @O_Msg = 'CORRECTO'
				END
	ELSE IF(@Accion = 'FIL')
		BEGIN
			IF ISNULL(@Catalogo, '') = ''
				BEGIN
					SET @O_Numero = -1
					SET @O_Msg = 'PARAMETRO CATALOGO NO PUEDE SER NULO O VACIO'
				END
			ELSE
				BEGIN
					SELECT *
					FROM Cls_Catalogos (NOLOCK)
					WHERE Catalogo LIKE '%'+@Catalogo+'%'
					ORDER BY Id_Tipo_Catalogo DESC

					SET @O_Numero = 0
					SET @O_Msg = 'CORRECTO'
				END
		END
		ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Catalogo, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_CATALOGO NO PUEDE SER NULO'
    END
	ELSE
    BEGIN
        SELECT *
        FROM Cls_Catalogos (NOLOCK)
        WHERE Id_Catalogo = @Id_Catalogo

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




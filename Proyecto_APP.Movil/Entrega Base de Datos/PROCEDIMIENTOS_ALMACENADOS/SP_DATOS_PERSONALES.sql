CREATE OR ALTER PROC SP_DATOS_PERSONALES
(
@Id_Persona INT = NULL,
@Primer_Nombre VARCHAR(80) = NULL,
@Segundo_Nombre VARCHAR(80) = NULL,
@Primer_Apellido VARCHAR(80) = NULL,
@Segundo_Apellido VARCHAR(80) = NULL,
@Edad CHAR(2) = NULL,
@Tipo_Cargo INT = NULL,
@Tipo_DNI INT = NULL,
@DNI VARCHAR(20) = NULL,
@Genero INT = NULL,
@Nacionalidad INT = NULL,
@Departamento INT = NULL,
@Estado_Civil INT = NULL,
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
		IF ISNULL(@Primer_Nombre, '') = ''
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO PRIMER NOMBRE NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@Primer_Apellido, '') = ''
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO PRIMER APELLIDO NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@Edad, 0) = 0
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO EDAD NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@Tipo_Cargo, 0) = 0
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO TIPO_CARGO NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@Tipo_DNI, 0) = 0
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO DNI NO PUEDE SER NULO'
			END
		ELSE IF ISNULL(@DNI, '') = ''
			BEGIN
				SET @O_Numero = -1
				SET @O_Msg = 'PARAMETRO DNI NO PUEDE SER NULO'
			END
			ELSE
				BEGIN
					BEGIN TRANSACTION TRX_INSERTAR_DATOS_PERSONALES
					BEGIN TRY
						INSERT INTO Tbl_Datos_Personales(
						Primer_Nombre,
						Segundo_Nombre,
						Primer_Apellido,
						Segundo_Apellido,
						Edad,
						Tipo_Cargo,
						Tipo_DNI,
						DNI,
						Genero,
						Nacionalidad,
						Departamento,
						Estado_Civil,
						Fecha_Creacion,
						Fecha_Modificacion,
						Id_Creador,
						Id_Modificador,
						Id_Estado
						)
						VALUES (
							@Primer_Nombre,
							@Segundo_Nombre, 
							@Primer_Apellido, 
							@Segundo_Apellido, 
							@Edad ,
							@Tipo_Cargo,
							@Tipo_DNI, 
							@DNI ,
							@Genero, 
							@Nacionalidad, 
							@Departamento,
							@Estado_Civil ,
							@Fecha_Creacion, 
							@Fecha_Modificacion, 
							@Id_Creador ,
							@Id_Modificador, 
							@Id_Estado
						)
					
						COMMIT TRAN TRX_INSERTAR_DATOS_PERSONALES
						SET @O_Numero = 0
						SET @O_Msg = 'INSERTADO'
					END TRY
					BEGIN CATCH
						SET @O_Numero = ERROR_NUMBER()
						SET @O_Msg = CONCAT(ERROR_PROCEDURE(),'-',ERROR_MESSAGE())
						ROLLBACK TRAN TRX_INSERTAR_DATOS_PERSONALES
					END CATCH
				END
	END
	ELSE IF(@Accion = 'UPD')
		BEGIN
			IF ISNULL(@Id_Persona, 0) = 0
				BEGIN
					SET @O_Numero = -1
					SET @O_Msg = 'PARAMETRO PERSONA NO PUEDE SER NULO'
				END
			ELSE
				BEGIN
					BEGIN TRANSACTION TRX_ACTUALIZAR_DATOS_PERSONALES
					BEGIN TRY
				
						UPDATE Tbl_Datos_Personales
						SET
						
							Primer_Nombre = COALESCE(@Primer_Nombre, Primer_Nombre),
							Segundo_Nombre =  COALESCE(@Segundo_Nombre, Segundo_Nombre),
							Primer_Apellido =  COALESCE(@Primer_Apellido, Primer_Apellido),
							Segundo_Apellido = COALESCE(@Segundo_Apellido, Segundo_Apellido),
							Edad = COALESCE(@Edad, Edad),
							Tipo_Cargo = COALESCE(@Tipo_Cargo, Tipo_Cargo),
							Tipo_DNI = COALESCE(@Tipo_DNI, Tipo_DNI),
							DNI = COALESCE(@DNI, DNI),
							Genero = COALESCE(@Genero, Genero),
							Nacionalidad =  COALESCE(@Nacionalidad, Nacionalidad),
							Departamento = COALESCE(@Departamento, Departamento),
							Estado_Civil  = COALESCE(@Estado_Civil, Estado_Civil),
							Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
							Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
							Id_Estado = COALESCE(@Id_Estado, Id_Estado)

						WHERE Id_Persona = @Id_Persona
						IF @@ROWCOUNT = 0
				BEGIN
				ROLLBACK TRAN TRX_ACTUALIZAR_CONTACTO
				SET @O_Numero = -1
				SET @O_Msg = 'NO SE ENCONTRO EL CONTACTO PARA ACTUALIZAR'
				RETURN
				END
						COMMIT TRAN TRX_ACTUALIZAR_DATOS_PERSONALES
						SET @O_Numero = 0
						SET @O_Msg = 'ACTUALIZADO'
					END TRY
					BEGIN CATCH
						SET @O_Numero = ERROR_NUMBER()
						SET @O_Msg = CONCAT(ERROR_PROCEDURE(),'-',ERROR_MESSAGE())
						ROLLBACK TRAN TRX_ACTUALIZAR_DATOS_PERSONALES
					END CATCH
				END
		END
	ELSE IF(@Accion = 'LIS')
		BEGIN

			SELECT *
			FROM Tbl_Datos_Personales (NOLOCK)
			ORDER BY Id_Persona DESC

			SET @O_Numero = 0
			SET @O_Msg = 'CORRECTO'
		END
	ELSE IF(@Accion = 'FIL')
		BEGIN
			IF ISNULL(@DNI, '') = ''
				BEGIN
					SET @O_Numero = -1
					SET @O_Msg = 'PARAMETRO DNI NO PUEDE SER NULO O VACIO'
				END
				
			ELSE
				BEGIN
					SELECT *
					FROM Tbl_Datos_Personales (NOLOCK)
					WHERE DNI LIKE '%'+@DNI+'%' OR
					Primer_Apellido lIKE '%'+@Primer_Apellido+'%' 
					ORDER BY Id_Persona DESC

					SET @O_Numero = 0
					SET @O_Msg = 'CORRECTO'
				END
		END
		ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Persona, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_PERSONA NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Tbl_Datos_Personales (NOLOCK)
 
       WHERE Id_Persona = @Id_Persona

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




CREATE OR ALTER PROC SP_USUARIOS
(
    @Id_Usuario INT = NULL,
    @Id_Persona INT = NULL,
    @Usuario VARCHAR(50) = NULL,
    @Contrasena VARCHAR(255) = NULL,
    @Ultima_Sesion DATETIME = NULL,
    @Ultima_Cambio_Credenciales DATETIME = NULL,
    @Intentos_Sesion INT = NULL,
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
        IF ISNULL(@Id_Persona, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID PERSONA NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Usuario, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO USUARIO NO PUEDE SER NULO'
        END
        ELSE IF ISNULL(@Contrasena, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO CONTRASENA NO PUEDE SER NULO'
        END
		ELSE IF (NOT EXISTS (SELECT*FROM Tbl_Usuarios WHERE Usuario = @Usuario))
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_USUARIOS
            BEGIN TRY
                INSERT INTO Tbl_Usuarios(
                    Id_Persona,
                    Usuario,
                    Contrasena,
                    Ultima_Sesion,
                    Ultima_Cambio_Credenciales,
                    Intentos_Sesion,
                    Fecha_Creacion,
                    Fecha_Modificacion,
                    Id_Creador,
                    Id_Modificador,
                    Id_Estado
                )
                VALUES (
                    @Id_Persona,
                    @Usuario,
                    @Contrasena,
                    @Ultima_Sesion,
                    @Ultima_Cambio_Credenciales,
                    @Intentos_Sesion,
                    @Fecha_Creacion,
                    @Fecha_Modificacion,
                    @Id_Creador,
                    @Id_Modificador,
                    @Id_Estado
                )
                
                COMMIT TRAN TRX_INSERTAR_USUARIOS
                SET @O_Numero = 0
                SET @O_Msg = 'USUARIO INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_USUARIOS
            END CATCH
        END
    END
    ELSE IF(@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Usuario, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID USUARIO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_USUARIOS
            BEGIN TRY
                UPDATE Tbl_Usuarios
                SET
                    Id_Persona = COALESCE(@Id_Persona, Id_Persona),
                    Usuario = COALESCE(@Usuario, Usuario),
                    Contrasena = COALESCE(@Contrasena, Contrasena),
                    Ultima_Sesion = COALESCE(@Ultima_Sesion, Ultima_Sesion),
                    Ultima_Cambio_Credenciales = COALESCE(@Ultima_Cambio_Credenciales, Ultima_Cambio_Credenciales),
                    Intentos_Sesion = COALESCE(@Intentos_Sesion, Intentos_Sesion),
                    Fecha_Modificacion = @Fecha_Modificacion,
                    Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Usuario = @Id_Usuario

                COMMIT TRAN TRX_ACTUALIZAR_USUARIOS
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_USUARIOS
            END CATCH
        END
    END
	 ELSE IF(@Accion = 'LOG')
    BEGIN
        IF ISNULL(@Usuario, '') = '' OR ISNULL(@Contrasena, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'USUARIO Y CONTRASEÑA SON OBLIGATORIOS'
        END
        ELSE
        BEGIN

            SELECT 
                Id_Usuario,
                Usuario,
                Id_Persona,
                Ultima_Sesion,
                Id_Estado
            FROM Tbl_Usuarios (NOLOCK)
            WHERE Usuario = @Usuario AND Contrasena = @Contrasena

            IF @@ROWCOUNT = 0
            BEGIN
                SET @O_Numero = -1
                SET @O_Msg = 'CREDENCIALES INCORRECTAS'
            END
            ELSE
            BEGIN
                
                UPDATE Tbl_Usuarios
   
             SET Ultima_Sesion = GETDATE()
                WHERE Usuario = @Usuario AND Contrasena = @Contrasena

                SET @O_Numero = 0
                SET @O_Msg = 'LOGIN EXITOSO'
            END
        END
    END
    ELSE IF(@Accion = 'LIS')
    BEGIN
        SELECT * FROM Tbl_Usuarios (NOLOCK)
        ORDER BY Id_Usuario DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF(@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Usuario, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO USUARIO NO PUEDE SER NULO O VACIO'
        END
        ELSE
        BEGIN
            SELECT * FROM Tbl_Usuarios (NOLOCK)
            WHERE Usuario LIKE '%' + @Usuario + '%'
            ORDER BY Id_Usuario DESC

            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Usuario, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_USUARIO NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Tbl_Usuarios (NOLOCK)
        WHERE Id_Usuario = @Id_Usuario

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


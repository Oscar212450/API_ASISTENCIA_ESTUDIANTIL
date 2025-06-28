CREATE PROC SP_USUARIOS
(
    @Id_usuario INT = NULL,
    @Nombre VARCHAR(100) = NULL,
    @Apellido VARCHAR(100) = NULL,
    @Correo_electronico VARCHAR(255) = NULL,
    @Hash_contrasena VARCHAR(255) = NULL,
    @Estado_id INT = NULL,
    @Telefono VARCHAR(20) = NULL,
    @Fecha_creado DATETIME = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF @Fecha_creado IS NULL SET @Fecha_creado = GETDATE()

    IF @Accion = 'INS'
    BEGIN
        IF ISNULL(@Correo_electronico, '') = '' OR ISNULL(@Hash_contrasena, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'Correo electrónico y contraseña son obligatorios'
        END
        ELSE
        BEGIN
            BEGIN TRY
                INSERT INTO Usuarios (Nombre, Apellido, Correo_electronico, Hash_contrasena, Estado_id, Telefono, Fecha_creado)
                VALUES (@Nombre, @Apellido, @Correo_electronico, @Hash_contrasena, @Estado_id, @Telefono, @Fecha_creado)

                SET @O_Numero = 0
                SET @O_Msg = 'Usuario insertado correctamente'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = ERROR_MESSAGE()
            END CATCH
        END
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Usuarios WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

CREATE PROC SP_CATEGORIAS
(
    @Id_categoria INT = NULL,
    @Nombre_Categoria VARCHAR(100) = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'INS'
    BEGIN
        IF ISNULL(@Nombre_Categoria, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'El campo Nombre_Categoria no puede estar vacío'
        END
        ELSE
        BEGIN
            BEGIN TRY
                INSERT INTO Categorias (Nombre_Categoria)
                VALUES (@Nombre_Categoria)

                SET @O_Numero = 0
                SET @O_Msg = 'Categoría insertada correctamente'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = ERROR_MESSAGE()
            END CATCH
        END
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Categorias WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

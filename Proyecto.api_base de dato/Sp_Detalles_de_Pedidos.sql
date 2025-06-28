CREATE PROC SP_DETALLE_PEDIDO
(
    @Id_detalle INT = NULL,
    @Pedido_id INT = NULL,
    @Libro_id INT = NULL,
    @Cantidad INT = NULL,
    @Precio_unitario NUMERIC(8,2) = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Accion = 'INS'
    BEGIN
        BEGIN TRY
            INSERT INTO Detalle_pedido (Pedido_id, Libro_id, Cantidad, Precio_unitario)
            VALUES (@Pedido_id, @Libro_id, @Cantidad, @Precio_unitario)

            SET @O_Numero = 0
            SET @O_Msg = 'Detalle insertado correctamente'
        END TRY
        BEGIN CATCH
            SET @O_Numero = ERROR_NUMBER()
            SET @O_Msg = ERROR_MESSAGE()
        END CATCH
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Detalle_pedido WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

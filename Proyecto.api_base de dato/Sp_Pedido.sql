CREATE PROC SP_PEDIDOS
(
    @Id_pedido INT = NULL,
    @Usuario_id INT = NULL,
    @Monto_total NUMERIC(10,2) = NULL,
    @Estado_id INT = NULL,
    @Fecha_pedido DATE = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF @Fecha_pedido IS NULL SET @Fecha_pedido = GETDATE()

    IF @Accion = 'INS'
    BEGIN
        BEGIN TRY
            INSERT INTO Pedidos (Usuario_id, Monto_total, Estado_id, Fecha_pedido)
            VALUES (@Usuario_id, @Monto_total, @Estado_id, @Fecha_pedido)

            SET @O_Numero = 0
            SET @O_Msg = 'Pedido insertado correctamente'
        END TRY
        BEGIN CATCH
            SET @O_Numero = ERROR_NUMBER()
            SET @O_Msg = ERROR_MESSAGE()
        END CATCH
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Pedidos WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

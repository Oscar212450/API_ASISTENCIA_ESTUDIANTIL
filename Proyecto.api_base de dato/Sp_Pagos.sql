CREATE PROC SP_PAGOS
(
    @Id_pago INT = NULL,
    @Pedido_id INT = NULL,
    @Fecha_pago DATE = NULL,
    @Monto NUMERIC(10,2) = NULL,
    @Metodo_pago VARCHAR(50) = NULL,
    @Transaccion_id VARCHAR(255) = NULL,
    @Estado_id INT = NULL,
    @Accion CHAR(3),
    @O_Numero INT = NULL OUTPUT,
    @O_Msg VARCHAR(255) = NULL OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    IF @Fecha_pago IS NULL SET @Fecha_pago = GETDATE()

    IF @Accion = 'INS'
    BEGIN
        BEGIN TRY
            INSERT INTO Pagos (Pedido_id, Fecha_pago, Monto, Metodo_pago, Transaccion_id, Estado_id)
            VALUES (@Pedido_id, @Fecha_pago, @Monto, @Metodo_pago, @Transaccion_id, @Estado_id)

            SET @O_Numero = 0
            SET @O_Msg = 'Pago insertado correctamente'
        END TRY
        BEGIN CATCH
            SET @O_Numero = ERROR_NUMBER()
            SET @O_Msg = ERROR_MESSAGE()
        END CATCH
    END
    ELSE IF @Accion = 'LIS'
    BEGIN
        SELECT * FROM Pagos WITH(NOLOCK)
        SET @O_Numero = 0
        SET @O_Msg = 'Correcto'
    END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'Acción no válida'
    END
END

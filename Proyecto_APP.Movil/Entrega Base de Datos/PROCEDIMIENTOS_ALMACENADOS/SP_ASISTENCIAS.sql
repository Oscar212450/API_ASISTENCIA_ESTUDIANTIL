CREATE OR ALTER PROC SP_ASISTENCIAS
(
    @Id_Asistencia INT = NULL,
	@Id_Estudiante_Grupo INT = NULL,
	@Id_Grupo_Asignatura INT = NULL,
	@Id_Docente_Grupo INT = NULL,
	@Fecha DATE = NULL,
	@Asistio VARCHAR(20) = NULL,
	@Observacion VARCHAR(100) = NULL,
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

	 IF ISNULL(@Fecha, '') = ''
    BEGIN
        SET @Fecha = GETDATE()
    END

    IF (@Accion = 'INS')
    BEGIN
        IF ISNULL(@Id_Estudiante_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE_GRUPO NO PUEDE SER NULO'
        END
		ELSE IF ISNULL(@Id_Grupo_Asignatura, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_GRUPO_ASIGNATURA NO PUEDE SER NULO'
        END
		ELSE IF ISNULL(@Id_Docente_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_DOCENTE_GRUPO NO PUEDE SER NULO'
        END
		ELSE IF ISNULL(@Fecha, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO FECHA NO PUEDE SER NULO'
        END
		ELSE IF ISNULL(@Asistio, '') = ''
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ASISTIO NO PUEDE SER NULO'
        END
		ELSE IF EXISTS(SELECT 1 FROM Tbl_Asistencias
						WHERE Id_Estudiante_Grupo = @Id_Estudiante_Grupo
						AND Id_Grupo_Asignatura = @Id_Grupo_Asignatura
						AND Fecha = @Fecha)
		BEGIN
			SET @O_Numero = 1
			SET @O_Msg = 'YA EXISTE UNA ASISTENCIA PARA ESTE ESTUDIANTE, GRUPO Y FECHA'
		END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_INSERTAR_ASISTENCIA
            BEGIN TRY
                INSERT INTO Tbl_Asistencias(
                    Id_Estudiante_Grupo,
	                Id_Grupo_Asignatura,
	                Id_Docente_Grupo,
	                Fecha,
	                Asistio,
	                Observacion,
	                Fecha_Creacion,
	                Fecha_Modificacion,
	                Id_Creador,
	                Id_Modificador,
	                Id_Estado
                )
                VALUES (
                    @Id_Estudiante_Grupo,
	                @Id_Grupo_Asignatura,
	                @Id_Docente_Grupo,
	                @Fecha,
	                @Asistio,
	                @Observacion,
	                @Fecha_Creacion,
	                @Fecha_Modificacion,
	                @Id_Creador,
	                @Id_Modificador,
	                @Id_Estado
                )
                
                COMMIT TRAN TRX_INSERTAR_ASISTENCIA
                SET @O_Numero = 0
                SET @O_Msg = 'INSERTADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_INSERTAR_ASISTENCIA
            END CATCH
        END
    END
    ELSE IF (@Accion = 'UPD')
    BEGIN
        IF ISNULL(@Id_Asistencia, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ASISTENCIA NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            BEGIN TRANSACTION TRX_ACTUALIZAR_ASISTENCIA
            BEGIN TRY
                UPDATE Tbl_Asistencias
                SET
                    Id_Estudiante_Grupo = COALESCE(@Id_Estudiante_Grupo, Id_Estudiante_Grupo),
	                Id_Grupo_Asignatura = COALESCE(@Id_Grupo_Asignatura, Id_Grupo_Asignatura),
	                Id_Docente_Grupo = COALESCE(@Id_Docente_Grupo, Id_Docente_Grupo),
	                Fecha = COALESCE(@Fecha, Fecha),
	                Asistio = COALESCE(@Asistio, Asistio),
	                Observacion = COALESCE(@Observacion, Observacion),
	                Fecha_Modificacion = COALESCE(@Fecha_Modificacion, Fecha_Modificacion),
	                Id_Modificador = COALESCE(@Id_Modificador, Id_Modificador),
	                Id_Estado = COALESCE(@Id_Estado, Id_Estado)
                WHERE Id_Asistencia = @Id_Asistencia
                
                COMMIT TRAN TRX_ACTUALIZAR_ASISTENCIA
                SET @O_Numero = 0
                SET @O_Msg = 'ACTUALIZADO'
            END TRY
            BEGIN CATCH
                SET @O_Numero = ERROR_NUMBER()
                SET @O_Msg = CONCAT(ERROR_PROCEDURE(), '-', ERROR_MESSAGE())
                ROLLBACK TRAN TRX_ACTUALIZAR_ASISTENCIA
            END CATCH
        END
    END
    ELSE IF (@Accion = 'LIS')
    BEGIN
        SELECT *
        FROM Tbl_Asistencias (NOLOCK)
        ORDER BY Id_Asistencia DESC

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
    ELSE IF (@Accion = 'FIL')
    BEGIN
        IF ISNULL(@Id_Estudiante_Grupo, 0) = 0
        BEGIN
            SET @O_Numero = -1
            SET @O_Msg = 'PARAMETRO ID_ESTUDIANTE_GRUPO NO PUEDE SER NULO'
        END
        ELSE
        BEGIN
            SELECT *
            FROM Tbl_Asistencias (NOLOCK)
            WHERE Id_Estudiante_Grupo = @Id_Estudiante_Grupo OR
			Id_Grupo_Asignatura = @Id_Grupo_Asignatura OR
			Fecha = @Fecha OR Asistio LIKE '%' + @Asistio + '%'
            
            SET @O_Numero = 0
            SET @O_Msg = 'CORRECTO'
        END
    END
	ELSE IF(@Accion = 'GET')
BEGIN
    IF ISNULL(@Id_Asistencia, 0) = 0
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'PARAMETRO ID_ASISTENCIA NO PUEDE SER NULO'
    END
    ELSE
    BEGIN
        SELECT *
        FROM Tbl_Asistencias (NOLOCK)
  
      WHERE Id_Asistencia = @Id_Asistencia

        SET @O_Numero = 0
        SET @O_Msg = 'CORRECTO'
    END
END
    ELSE
    BEGIN
        SET @O_Numero = -1
        SET @O_Msg = 'OPCION NO DISPONIBLE'
    END
END;




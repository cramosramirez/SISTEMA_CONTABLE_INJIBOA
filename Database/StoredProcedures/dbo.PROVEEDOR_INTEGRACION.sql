USE [INJIBOA]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[PROVEEDOR_INTEGRACION]
    @ACCION VARCHAR(20),
    @FILTRO VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @ACCION = UPPER(LTRIM(RTRIM(@ACCION)));
    SET @FILTRO = NULLIF(LTRIM(RTRIM(@FILTRO)), '');

    IF @ACCION = 'PROVEEDOR'
    BEGIN
        SELECT
            T.CODIGO,
            T.NOMBRE
        FROM
        (
            SELECT
                P.CODIPROVEEDOR AS CODIGO,
                RTRIM(P.NOMBRES) + ' ' + RTRIM(P.APELLIDOS) AS NOMBRE
            FROM [dbo].[PROVEEDOR] AS P
        ) AS T
        WHERE LEN(ISNULL(T.NOMBRE, '')) > 0
          AND
          (
              @FILTRO IS NULL
              OR CONVERT(VARCHAR(100), T.CODIGO) LIKE '%' + @FILTRO + '%'
              OR T.NOMBRE LIKE '%' + @FILTRO + '%'
          )
        ORDER BY T.NOMBRE;

        RETURN;
    END;

    THROW 50000, 'La acción indicada no es válida para PROVEEDOR_INTEGRACION.', 1;
END;
GO

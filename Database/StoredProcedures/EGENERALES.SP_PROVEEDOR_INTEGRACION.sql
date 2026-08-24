USE [PH2];
GO

CREATE OR ALTER PROCEDURE [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
    @ACTION VARCHAR(20),
    @FILTRO VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @ACTION = UPPER(LTRIM(RTRIM(ISNULL(@ACTION, ''))));
    SET @FILTRO = NULLIF(LTRIM(RTRIM(@FILTRO)), '');

    IF @ACTION = 'PRODUCTOR'
    BEGIN
        SELECT
            T.CODIGO,
            RTRIM(T.NOMBRE) AS NOMBRE,
            T.NIT
        FROM
        (
            SELECT
                P.CODIPROVEEDOR AS CODIGO,
                LTRIM(RTRIM(
                    ISNULL(RTRIM(P.NOMBRES), '') + ' ' +
                    ISNULL(RTRIM(P.APELLIDOS), '')
                )) AS NOMBRE,
                P.NIT
            FROM [INJIBOA].dbo.PROVEEDOR AS P
            WHERE [INJIBOA].dbo.QuitarFormatoCODIPROVEEDOR(
                      P.CODIPROVEEDOR
                  ) <> '159'

            UNION ALL

            SELECT
                P.CODIPROVEEDOR AS CODIGO,
                LTRIM(RTRIM(
                    ISNULL(RTRIM(P.NOMBRES), '') + ' ' +
                    ISNULL(RTRIM(P.APELLIDOS), '')
                )) AS NOMBRE,
                P.NIT
            FROM [INJIBOA].dbo.PROVEEDOR AS P
            WHERE P.CODIPROVEEDOR = '0001590000'
        ) AS T
        WHERE LEN(RTRIM(T.NOMBRE)) > 0
          AND
          (
              @FILTRO IS NULL
              OR T.CODIGO LIKE '%' + @FILTRO + '%'
              OR T.NOMBRE LIKE '%' + @FILTRO + '%'
          )
        ORDER BY RTRIM(T.NOMBRE);

        RETURN;
    END;

    IF @ACTION = 'TRASPORTISTA'
    BEGIN
        SELECT
            T.CODIGO,
            RTRIM(T.NOMBRE) AS NOMBRE,
            T.NIT
        FROM
        (
            SELECT
                TT.CODTRANSPORT AS CODIGO,
                LTRIM(RTRIM(
                    ISNULL(RTRIM(TT.NOMBRES), '') + ' ' +
                    ISNULL(RTRIM(TT.APELLIDOS), '')
                )) AS NOMBRE,
                TT.NIT
            FROM [INJIBOA].dbo.TRANSPORTISTA AS TT
        ) AS T
        WHERE LEN(RTRIM(T.NOMBRE)) > 0
          AND
          (
              @FILTRO IS NULL
              OR CONVERT(VARCHAR(100), T.CODIGO) LIKE '%' + @FILTRO + '%'
              OR T.NOMBRE LIKE '%' + @FILTRO + '%'
          )
        ORDER BY RTRIM(T.NOMBRE);

        RETURN;
    END;

    IF @ACTION = 'CARGADORA'
    BEGIN
        SELECT
            PC.CODIGO AS CODIGO,
            LTRIM(RTRIM(PC.NOMBRE_PROVEEDOR_CARGA)) AS NOMBRE,
            PC.NIT
        FROM [INJIBOA].dbo.PROVEEDOR_CARGA AS PC
        WHERE LEN(LTRIM(RTRIM(ISNULL(PC.NOMBRE_PROVEEDOR_CARGA, '')))) > 0
          AND
          (
              @FILTRO IS NULL
              OR CONVERT(VARCHAR(100), PC.CODIGO) LIKE '%' + @FILTRO + '%'
              OR PC.NOMBRE_PROVEEDOR_CARGA LIKE '%' + @FILTRO + '%'
          )
        ORDER BY LTRIM(RTRIM(PC.NOMBRE_PROVEEDOR_CARGA));

        RETURN;
    END;

    IF @ACTION = 'ROZA'
    BEGIN
        SELECT
            PR.ID_PROVEEDOR_ROZA AS CODIGO,
            LTRIM(RTRIM(PR.NOMBRE_PROVEEDOR_ROZA)) AS NOMBRE,
            PR.NIT
        FROM [dbo].[PROVEEDOR_ROZA] AS PR
        WHERE PR.ACTIVO = 1
          AND
          (
              @FILTRO IS NULL
              OR CONVERT(VARCHAR(100), PR.ID_PROVEEDOR_ROZA) LIKE '%' + @FILTRO + '%'
              OR PR.NOMBRE_PROVEEDOR_ROZA LIKE '%' + @FILTRO + '%'
          )
        ORDER BY LTRIM(RTRIM(PR.NOMBRE_PROVEEDOR_ROZA));

        RETURN;
    END;

    IF @ACTION = 'QUERQUEO'
    BEGIN
        SELECT
            T.CODIGO,
            RTRIM(T.NOMBRE) AS NOMBRE,
            T.NIT
        FROM
        (
            SELECT
                Q.ID_PROVEE_QQ AS CODIGO,
                LTRIM(RTRIM(
                    ISNULL(RTRIM(Q.NOMBRES), '') + ' ' +
                    ISNULL(RTRIM(Q.APELLIDOS), '')
                )) AS NOMBRE,
                Q.NIT
            FROM [dbo].[PROVEEDOR_QUERQUEO] AS Q
        ) AS T
        WHERE LEN(RTRIM(T.NOMBRE)) > 0
          AND
          (
              @FILTRO IS NULL
              OR CONVERT(VARCHAR(100), T.CODIGO) LIKE '%' + @FILTRO + '%'
              OR T.NOMBRE LIKE '%' + @FILTRO + '%'
          )
        ORDER BY RTRIM(T.NOMBRE);

        RETURN;
    END;

    THROW 50001,
          'La acción indicada no es válida para SP_PROVEEDOR_INTEGRACION.',
          1;
END;
GO

-- Pruebas de lectura:
-- EXEC [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
--     @ACTION = 'TRASPORTISTA';
--
-- EXEC [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
--     @ACTION = 'TRASPORTISTA',
--     @FILTRO = 'JUAN';
--
-- EXEC [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
--     @ACTION = 'CARGADORA',
--     @FILTRO = NULL;
--
-- EXEC [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
--     @ACTION = 'ROZA',
--     @FILTRO = NULL;
--
-- EXEC [EGENERALES].[SP_PROVEEDOR_INTEGRACION]
--     @ACTION = 'QUERQUEO',
--     @FILTRO = NULL;

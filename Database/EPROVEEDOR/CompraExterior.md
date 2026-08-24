# Compra Exterior - esquema EPROVEEDOR

## Formularios

- `frmConsultaCompraExterior`: clon independiente de `frmConsultaQuedan`.
- `frmDocumentoCompraExterior`: clon independiente de `frmDocumentoCompra`.
- Los formularios nacionales conservan sus llamadas actuales a `dbo`.
- Los formularios de Compra Exterior llaman los procedimientos con el nombre calificado `[EPROVEEDOR].[procedimiento]`.

## Procedimientos versionados en EPROVEEDOR

### Consulta y documento

- `SP_COMPRA_EXTERIOR`
- `SP_COMPRA_CLASIFICACION`
- `SP_COMPRA_SECTOR`
- `SP_COMPRA_TIPO_COSTO`
- `SP_COMPRA_TIPO_OPERACION`
- `SP_COMPRA_TIPO_SERVICIO`
- `SP_ENTIDAD`
- `SP_SUCURSAL`
- `SP_TIPO_DTE`
- `SP_TIPO_RENTA`

`SP_COMPRA_EXTERIOR` recibe `@ID_QUEDAN_CEX` para identificar o generar el Quedan exclusivo del flujo exterior. Los resultados conservan `ID_QUEDAN`, `NUM_QUEDAN`, `ID_QUEDAN_GENERADO` y `NUM_QUEDAN_GENERADO`, que corresponden a las columnas físicas y al contrato de respuesta.

El formulario exterior carga `Tipo Importación` mediante `[ECOMPRAS].[SP_TIPO_IMPORTACION]` con `@ACCION = 'BUSCAR'`. El valor seleccionado se persiste en `COMPRA_EXTERIOR.ID_TIPO_IMPORTACION` a través del parámetro homónimo de `SP_COMPRA_EXTERIOR`. Esta columna es independiente de `ID_TIPO_SERVI`, que se conserva por compatibilidad.

### Provisión y reportes usados por los formularios

- `SP_CATALOGO_CUENTA`
- `SP_CREDITO_FISCAL_PROVISION`
- `SP_QUEDAN_RPT`
- `SP_COMPROBANTE_RETENCION_RPT`

## Tablas y tipos utilizados

La tabla canónica del módulo es `[EPROVEEDOR].[COMPRA_EXTERIOR]` y su llave es `[ID_COMPRA_EXTERIOR]`. La migración renombra la tabla y la columna con `sp_rename`, conservando los datos, el `object_id` y las relaciones existentes.

Para no romper los módulos nacionales que todavía usan `[dbo].[CREDITO_FISCAL_COMPRA]` e `ID_CCF_COMPRA`, se sustituye el sinónimo anterior por una vista actualizable con esos nombres. La vista apunta a la misma tabla y publica `ID_COMPRA_EXTERIOR AS ID_CCF_COMPRA`; no crea una copia ni duplica registros.

### PH2.EPROVEEDOR

- `COMPRA_EXTERIOR`

### PH2.ECOMPRAS

- `TIPO_IMPORTACION`
- `SP_TIPO_IMPORTACION`

### PH2.dbo

- `ACTIVIDAD_ECONOMICA`
- `AMBIENTE_DESTINO`
- `CATALOGO_CUENTA`
- `CHEQUE`
- `COMPRA_CLASIFICACION`
- `COMPRA_SECTOR`
- `COMPRA_TIPO_COSTO`
- `COMPRA_TIPO_OPERACION`
- `COMPRA_TIPO_SERVICIO`
- `COMPRA_TPCOSTGAST`
- `COMPROBANTE_RETENCION`
- `CREDITO_FISCAL_PROVISION`
- `DEPARTAMENTO`
- `DOCUMENTO_NUMERACION`
- `ENTIDAD`
- `ENTIDAD_ROL`
- `ENTIDAD_TIPO_CLIENTE`
- `MODELO_FACTURACION`
- `MUNICIPIO`
- `QUEDAN`
- `SUCURSAL`
- `TIPO_CONTRIBUYENTE`
- `TIPO_CUENTA`
- `TIPO_DTE`
- `TIPO_OPERACION`
- `TIPO_PERSONA`
- `TIPO_RENTA`
- `USUARIO`

### PH2.CONTA

- `PARAMETROS_CUENTA`

### Tipo de tabla

- `[dbo].[typeCREDITO_FISCAL_PROVISION]`

## Procedimientos compartidos llamados internamente

Estos objetos mantienen su esquema canónico porque pertenecen a otros módulos:

- `[dbo].[SP_COMPROBANTE_RETENCION]`
- `[EIVA].[SP_LBCOMPRAS_INS]`
- `[EIVA].[SP_LBCOMPRAS_RET_INS]`

## Despliegue

### Base PH2 existente

1. Respaldar la base de datos.
2. Si la tabla todavía está en `dbo`, ejecutar primero `Database/Migrations/20260823_Mover_CREDITO_FISCAL_COMPRA_a_EPROVEEDOR.sql`.
3. Ejecutar `Database/Migrations/20260823_Renombrar_CREDITO_FISCAL_COMPRA_a_COMPRA_EXTERIOR.sql`.
4. Ejecutar `Database/Migrations/20260823_Agregar_ID_TIPO_IMPORTACION_COMPRA_EXTERIOR.sql`.
5. Ejecutar `Database/StoredProcedures/EPROVEEDOR.SP_COMPRA_EXTERIOR.sql` y los procedimientos EPROVEEDOR complementarios que se vayan a publicar.
6. Publicar la aplicación.

El script de renombrado es transaccional, elimina el sinónimo anterior, crea la vista de compatibilidad y verifica que se conserven la cantidad de registros y al menos las 13 claves foráneas originales. La migración de tipo de importación agrega una relación adicional hacia `ECOMPRAS.TIPO_IMPORTACION`.

### Instalación nueva

Usar `Database/Tables/EPROVEEDOR.COMPRA_EXTERIOR.sql` cuando se trate de una instalación nueva. El script reproduce las 47 columnas físicas, la PK sobre `ID_COMPRA_EXTERIOR`, las 13 claves foráneas salientes y la vista nacional de compatibilidad con sus 46 columnas históricas.

El traslado inicial a `EPROVEEDOR` ya se encontraba aplicado en PH2. El renombrado a `COMPRA_EXTERIOR` debe validarse junto con el procedimiento principal antes de confirmar la transacción de despliegue.

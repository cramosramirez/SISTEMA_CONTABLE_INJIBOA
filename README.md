# Sistema Contable Integrado — INJIBOA

> **EN:** Integrated accounting system for a sugar mill (accounts payable, suppliers, check issuance), built with C#, WinForms, Dapper, and SQL Server. Replaces legacy manual/FoxPro-based processes with an auditable, centralized solution.

Sistema desarrollado para automatizar y centralizar la gestión contable del Ingenio Central Azucarero Jiboa (INJIBOA), específicamente los procesos de **cuentas por pagar, proveedores, quedanes (CxP) y emisión de cheques**. Reemplaza flujos manuales heredados de un sistema anterior en FoxPro, preservando los comportamientos de usuario que el personal ya conocía (navegación por teclado, búsquedas rápidas) mientras moderniza la arquitectura por debajo.

## 🧱 Stack técnico

| Capa | Tecnología |
|---|---|
| Lenguaje | C# |
| UI | Windows Forms + DevExpress 20.1 |
| Acceso a datos | Dapper 2.0.123 |
| Base de datos | SQL Server |
| Actualizaciones | Actualizador propio (auto-update silencioso) |

## 🏗️ Arquitectura

Arquitectura por capas clásica (DAL / BLL / UI), separando el acceso a datos de la lógica de negocio y de la interfaz:

│
├── SistemaContable.DAL\ Acceso a datos
│ ├── DALBase.cs 4 métodos Dapper reutilizables
│ ├── Configuracion.cs Cadena de conexión y sesión
│ ├── CatalogosDAL.cs Todos los catálogos
│ ├── EntidadDAL.cs Proveedores / Clientes
│ ├── QuedanDAL.cs Quedan y CCF
│ └── ChequeDAL.cs Cheques y partida
│
├── SistemaContable.BLL\ Lógica de negocio
│ └── QuedanBLL.cs Validaciones de Quedan
│
├── SistemaContable.UI\ Interfaz Windows Forms
│ ├── Program.cs Skin + Updater + Login
│ ├── App.config Configuración local
│ ├── Forms
│ │ ├── FrmBase.cs Base de todos los formularios
│ │ ├── FrmBusqueda.cs Ventana genérica * + Enter
│ │ ├── FrmLogin.cs Inicio de sesión
│ │ ├── FrmMain.cs Ribbon principal completo
│ │ ├── Catalogos
│ │ │ └── FrmCatalogo.cs Formulario genérico de catálogos
│ │ └── CxP
│ │ ├── FrmQuedan.cs Emisión de Quedan
│ │ └── FrmCCF.cs Cheques (en desarrollo)
│ └── Helpers
│ ├── FoxProBehavior.cs Enter=Tab, *+Enter, Grid editable
│ └── Actualizador.cs Auto-actualización silenciosa
│
└── SistemaContable.Updater\ Actualizador independiente
└── Program.cs Reemplaza archivos y reinicia


## ⌨️ Comportamientos FoxPro implementados

Uno de los retos del proyecto fue migrar usuarios de un sistema FoxPro legado sin perder su flujo de trabajo. Se replicaron intencionalmente estos comportamientos:

| Comportamiento | Descripción |
|---|---|
| **Enter = Tab** | En todos los campos del formulario |
| **\* + Enter** | Abre `FrmBusqueda` en campos lookup y celdas de grid |
| **Grid editable** | Enter avanza columna; al final pasa a la siguiente fila |
| **Búsqueda incremental** | Delay de 300ms, TOP controlado en el stored procedure |
| **Auto-actualización** | Silenciosa al iniciar, sin que el usuario lo note |

### Teclas rápidas en formularios modales

| Tecla | Acción |
|---|---|
| F2 | Nuevo |
| F3 | Guardar |
| F4 | Eliminar |
| F5 | Anular |
| F6 | Imprimir |
| Esc | Cerrar |

## 🚀 Guía de instalación

### 1. Abrir la solución
Doble clic en: SistemaContable.sln
### 2. Restaurar paquete NuGet (Dapper)
Clic derecho en la solución → Restaurar paquetes NuGet
Esto instala automáticamente **Dapper 2.0.123** en el proyecto DAL.

### 3. Agregar referencias de DevExpress
En el proyecto **SistemaContable.UI**, agregar referencias a los siguientes DLLs de DevExpress 20.1.17.0:
DevExpress.Data.v20.1.dll
DevExpress.Utils.v20.1.dll
DevExpress.XtraEditors.v20.1.dll
DevExpress.XtraGrid.v20.1.dll
DevExpress.XtraBars.v20.1.dll
DevExpress.XtraLayout.v20.1.dll
DevExpress.Skins.v20.1.dll
DevExpress.UserSkins.v20.1.dll
DevExpress.XtraPrinting.v20.1.dll

Ruta típica: `C:\Program Files (x86)\DevExpress 20.1\Components\Bin\Framework4.0\`

### 4. Configurar App.config
Editar `SistemaContable.UI\App.config`:

```xml
<connectionStrings>
  <add name="SistemaContable"
       connectionString="Server=TU_SERVIDOR;Database=PH2;
                        User Id=TU_USUARIO;Password=TU_CLAVE;"
       providerName="System.Data.SqlClient"/>
</connectionStrings>
<appSettings>
  <add key="NombreEmpresa" value="INJIBOA"/>
  <add key="RutaServidor"  value="\\TU_SERVIDOR\SistemaContable\"/>
</appSettings>
```

### 5. Ejecutar el script de base de datos
En SQL Server Management Studio:
SCRIPT_PH2.sql ← crea las tablas
SP_PH2.sql ← crea los stored procedures

### 6. Compilar y ejecutar

## 🔄 Servidor de actualizaciones

Estructura de carpetas en el servidor:
\SERVIDOR\SistemaContable
├── version.txt ← solo el número: 1.0.0
├── Updater.exe ← copia del ejecutable Updater
└── Releases
└── SistemaContable_1.0.0.zip

### Publicar nueva versión
Compilar en modo Release
Comprimir bin\Release\ → SistemaContable_1.0.1.zip
Copiar ZIP a \SERVIDOR\SistemaContable\Releases\
Actualizar version.txt → escribir: 1.0.1
Listo — próximo inicio se actualiza solo

---
*Desarrollado y mantenido por [Christiam Ramos](https://github.com/cramosramirez).*
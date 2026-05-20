# Sistema Contable Integrado — INJIBOA
## Guía de Configuración del Proyecto

---

## PASOS PARA ABRIR EN VISUAL STUDIO

### 1. Abrir la solución
```
Doble clic en: SistemaContable.sln
```

### 2. Restaurar paquete NuGet (Dapper)
```
Clic derecho en la solución → Restaurar paquetes NuGet
```
Esto instala automáticamente **Dapper 2.0.123** en el proyecto DAL.

### 3. Agregar referencias de DevExpress

En el proyecto **SistemaContable.UI**, agregar referencias a los siguientes DLLs de DevExpress 20.1.17.0.

La ruta típica es:
```
C:\Program Files (x86)\DevExpress 20.1\Components\Bin\Framework4.0\
```

DLLs requeridos:
```
DevExpress.Data.v20.1.dll
DevExpress.Utils.v20.1.dll
DevExpress.XtraEditors.v20.1.dll
DevExpress.XtraGrid.v20.1.dll
DevExpress.XtraBars.v20.1.dll
DevExpress.XtraLayout.v20.1.dll
DevExpress.Skins.v20.1.dll
DevExpress.UserSkins.v20.1.dll
DevExpress.XtraPrinting.v20.1.dll
```

Pasos:
```
1. Clic derecho en SistemaContable.UI → Agregar → Referencia
2. Examinar → navegar a la carpeta de DevExpress
3. Seleccionar los DLLs de la lista anterior
4. Aceptar
```

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

Ejecutar en SQL Server Management Studio:
```
1. SCRIPT_PH2.sql      ← crea las tablas
2. SP_PH2.sql          ← crea los stored procedures
```

### 6. Compilar y ejecutar
```
F5 o Ctrl+F5
```

---

## ESTRUCTURA DEL PROYECTO

```
SistemaContable.sln
│
├── SistemaContable.DAL\              Acceso a datos
│   ├── DALBase.cs                    4 métodos Dapper reutilizables
│   ├── Configuracion.cs              Cadena de conexión y sesión
│   ├── CatalogosDAL.cs               Todos los catálogos
│   ├── EntidadDAL.cs                 Proveedores / Clientes
│   ├── QuedanDAL.cs                  Quedan y CCF
│   └── ChequeDAL.cs                  Cheques y partida
│
├── SistemaContable.BLL\              Lógica de negocio
│   └── QuedanBLL.cs                  Validaciones de Quedan
│
├── SistemaContable.UI\               Interfaz Windows Forms
│   ├── Program.cs                    Skin + Updater + Login
│   ├── App.config                    Configuración local
│   ├── Forms\
│   │   ├── FrmBase.cs                Base de todos los formularios
│   │   ├── FrmBusqueda.cs            Ventana genérica * + Enter
│   │   ├── FrmLogin.cs               Inicio de sesión
│   │   ├── FrmMain.cs                Ribbon principal completo
│   │   ├── Catalogos\
│   │   │   └── FrmCatalogo.cs        Formulario genérico de catálogos
│   │   └── CxP\
│   │       ├── FrmQuedan.cs          Emisión de Quedan
│   │       └── FrmCCF.cs             Cheques (en desarrollo)
│   └── Helpers\
│       ├── FoxProBehavior.cs         Enter=Tab, *+Enter, Grid editable
│       └── Actualizador.cs           Auto-actualización silenciosa
│
└── SistemaContable.Updater\          Actualizador independiente
    └── Program.cs                    Reemplaza archivos y reinicia
```

---

## SERVIDOR DE ACTUALIZACIONES

Estructura de carpetas en el servidor:
```
\\SERVIDOR\SistemaContable\
  ├── version.txt                 ← solo el número: 1.0.0
  ├── Updater.exe                 ← copia del ejecutable Updater
  └── Releases\
        └── SistemaContable_1.0.0.zip
```

### Publicar nueva versión:
```
1. Compilar en modo Release
2. Comprimir bin\Release\ → SistemaContable_1.0.1.zip
3. Copiar ZIP a \\SERVIDOR\SistemaContable\Releases\
4. Actualizar version.txt → escribir: 1.0.1
5. Listo — próximo inicio se actualiza solo
```

---

## COMPORTAMIENTOS FOXPRO IMPLEMENTADOS

| Comportamiento | Descripción |
|---------------|-------------|
| **Enter = Tab** | En todos los campos del formulario |
| **\* + Enter** | Abre FrmBusqueda en campos lookup y celdas de grid |
| **Grid editable** | Enter avanza columna; al final pasa a siguiente fila |
| **Búsqueda incremental** | Delay 300ms, TOP controlado en SP |
| **Auto-actualización** | Silenciosa al iniciar, sin que el usuario note nada |

---

## TECLAS RÁPIDAS EN FORMULARIOS MODALES

| Tecla | Acción |
|-------|--------|
| F2    | Nuevo  |
| F3    | Guardar |
| F4    | Eliminar |
| F5    | Anular |
| F6    | Imprimir |
| Esc   | Cerrar |

using DevExpress.XtraBars;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars.Ribbon;
using SistemaContable.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SistemaContable.UI.Forms.Proveedores;
using System.Reflection;

namespace SistemaContable.UI.Forms
{
    public partial class frmPrincipalRibbon : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPrincipalRibbon()
        {
            InitializeComponent();
        }
        private void frmPrincipalRibbon_Load(object sender, EventArgs e)
        {
            ribbon.Pages.Clear();
            using (var login = new frmLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {                                     
                    CargarMenuPorRol(Configuracion.IdRolActual);
                    ribbonStatusBar.ItemLinks.Add(barStaticItem1);
                    barStaticItem1.Caption = $"Usuario: {Configuracion.UsuarioActual} " +
                         $"| Nombre: {Configuracion.NombreUsuarioActual} " +
                         $"| Rol: {Configuracion.NombreRolActual}";
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void CargarMenuPorRol(int idRol)
        {
            var dal = new DALBase();
            var dt = dal.EjecutarConsulta("SP_OPCION", new
            {
                ACCION = "MENU_POR_ROL",
                ID_ROL = idRol
            });
            var pages = new Dictionary<int, RibbonPage>();
            var groups = new Dictionary<int, RibbonPageGroup>();                
            foreach (DataRow row in dt.Rows)
            {
                int idOpcion = Convert.ToInt32(row["ID_OPCION"]);
                string nombre = row["NOMBRE_OPCION"].ToString();
                int nivel = Convert.ToInt32(row["NIVEL"]);
                int idPadre = row["ID_OPCION_PADRE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID_OPCION_PADRE"]);

                switch (nivel)
                {
                    case 1: // RibbonPage
                        var page = new RibbonPage(nombre);
                        ribbon.Pages.Add(page);
                        pages[idOpcion] = page;
                        break;

                    case 2: // RibbonPageGroup
                        if (pages.ContainsKey(idPadre))
                        {
                            var group = new RibbonPageGroup(nombre);                            
                            pages[idPadre].Groups.Add(group);
                            groups[idOpcion] = group;
                        }
                        break;

                    case 3: // BarButtonItem
                        if (groups.ContainsKey(idPadre))
                        {
                            var btn = new BarButtonItem(this.ribbon.Manager, nombre);
                            btn.RibbonStyle = RibbonItemStyles.Large;
                            string imagenSvg = row["IMAGEN_SVG"].ToString();
                            if (!String.IsNullOrEmpty(imagenSvg))
                            {
                                object recurso = Properties.Resources.ResourceManager.GetObject(imagenSvg);
                                if (recurso is SvgImage svg)
                                {
                                    btn.ImageOptions.SvgImage = svg;
                                }
                                else if (recurso is Bitmap bmp)
                                {
                                    btn.ImageOptions.Image = bmp;
                                }
                            }                            
                            Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
                            btn.ItemAppearance.Normal.Font = boldFont;
                            btn.ItemAppearance.Normal.Options.UseFont = true;
                            btn.ItemAppearance.Hovered.Font = boldFont;
                            btn.ItemAppearance.Hovered.Options.UseFont = true;
                            btn.ItemAppearance.Pressed.Font = boldFont;
                            btn.ItemAppearance.Pressed.Options.UseFont = true;                            
                            btn.Tag = row["FORMULARIO_WIN"].ToString();                            
                            //btn.ItemClick += ribbon_ItemClick;
                            groups[idPadre].ItemLinks.Add(btn);
                        }
                        break;
                }
            }
        }
                

        // Cache para mejorar rendimiento (opcional pero recomendado)
        private static readonly Dictionary<string, Type> _formCache = new Dictionary<string, Type>();

        // ==================== MÉTODO AUXILIAR ====================
        private Type ObtenerTipoFormulario(string nombreFormulario)
        {
            if (string.IsNullOrWhiteSpace(nombreFormulario))
                return null;

            // Buscar en caché
            if (_formCache.TryGetValue(nombreFormulario, out var tipoCache))
                return tipoCache;

            // Buscar el tipo
            string nombreCompleto = $"SistemaContable.UI.Forms.{nombreFormulario}";
            var assembly = Assembly.GetExecutingAssembly();

            Type tipo = assembly.GetType(nombreCompleto, false, true); // false = no throw, true = ignoreCase

            // Guardar en caché (aunque sea null, para no buscar de nuevo)
            _formCache[nombreFormulario] = tipo;

            return tipo;
        }

        // ==================== MÉTODO PRINCIPAL ====================
        private void AbrirFormulario(string nombreFormulario)
        {
            if (string.IsNullOrWhiteSpace(nombreFormulario))
            {
                XtraMessageBox.Show("El nombre del formulario no puede estar vacío.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Type tipo = ObtenerTipoFormulario(nombreFormulario);

                if (tipo == null)
                {
                    XtraMessageBox.Show($"Formulario '{nombreFormulario}' no encontrado.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!typeof(Form).IsAssignableFrom(tipo))
                {
                    XtraMessageBox.Show($"El tipo '{nombreFormulario}' no es un formulario válido.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (Form frm = Activator.CreateInstance(tipo) as Form)
                {
                    if (frm == null) return;
                    
                    if (frm is frmConsultaQuedan quedanForm)
                    {                        
                        quedanForm.Width = this.ClientRectangle.Width;
                        quedanForm.Height = this.ClientRectangle.Height - this.Ribbon.Height - this.StatusBar.Height;
                        quedanForm.Top = this.Ribbon.Height;           // Justo debajo del ribbon
                        quedanForm.Left = 0;
                        quedanForm.StartPosition = FormStartPosition.Manual;
                    }
                    else
                        frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error al abrir el formulario '{nombreFormulario}':\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ribbon_ItemClick(object sender, ItemClickEventArgs e)
        {
            string formulario = e.Item.Tag?.ToString();
            if (string.IsNullOrEmpty(formulario)) return;
            AbrirFormulario(formulario);
        }
    }
}
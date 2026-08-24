using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Dapper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmMercados : DevExpress.XtraEditors.XtraForm
    {
        // ------------------------------------------------------------------
        // Item auxiliar para el combo: guarda el codigo real (ACT/INA)
        // pero se muestra con ToString() -> la descripcion legible.
        // ------------------------------------------------------------------
        private readonly DALBase _dal = new DALBase();
        private class EstadoItem
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public override string ToString() => Descripcion;
        }

        public frmMercados()
        {
            InitializeComponent();
            LimpiarControles();
            CargarComboEstado();
            CargarMercado();

            // Coloreado + carga de fila seleccionada
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            gridView1.RowCellStyle += gridView1_RowCellStyle;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
        }

        // ------------------------------------------------------------------
        // Combo de Estado: valores reales ACT / INA, texto Activo / Inactivo
        // ------------------------------------------------------------------
        private void CargarComboEstado()
        {
            cboEstado.Properties.Items.Clear();
            cboEstado.Properties.Items.Add(new EstadoItem { Codigo = "ACT", Descripcion = "Activo" });
            cboEstado.Properties.Items.Add(new EstadoItem { Codigo = "INA", Descripcion = "Inactivo" });
            cboEstado.SelectedIndex = 0;
        }

        private string ObtenerCodigoEstadoSeleccionado()
        {
            return (cboEstado.SelectedItem as EstadoItem)?.Codigo;
        }

        private void EstablecerEstadoPorCodigo(string codigo)
        {
            foreach (var obj in cboEstado.Properties.Items)
            {
                if (obj is EstadoItem item && item.Codigo == codigo)
                {
                    cboEstado.SelectedItem = item;
                    return;
                }
            }
            cboEstado.SelectedItem = null;
        }

        // ------------------------------------------------------------------
        // Grid: texto legible en la columna ESTADO
        // ------------------------------------------------------------------
        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "ESTADO") return;

            switch (e.Value?.ToString())
            {
                case "ACT": e.DisplayText = "Activo"; break;
                case "INA": e.DisplayText = "Inactivo"; break;
            }
        }

        // ------------------------------------------------------------------
        // Grid: color de la celda ESTADO (verde ACT / rojo INA)
        // ------------------------------------------------------------------
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "ESTADO") return;

            var view = (GridView)sender;
            string estado = view.GetRowCellValue(e.RowHandle, "ESTADO")?.ToString();

            if (estado == "ACT")
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 140, 0);
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            else if (estado == "INA")
            {
                e.Appearance.ForeColor = Color.FromArgb(200, 0, 0);
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
        }

        // ------------------------------------------------------------------
        // Grid: al seleccionar/hacer click en una fila, cargar los
        // controles del formulario para poder Editar o Eliminar.
        // ------------------------------------------------------------------
        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                LimpiarControles();
                return;
            }

            var view = gridView1;

            txtCodMerc.Text = view.GetRowCellValue(e.FocusedRowHandle, "CODMDO")?.ToString();
            txtDescripcion.Text = view.GetRowCellValue(e.FocusedRowHandle, "DESCRIPCION")?.ToString();

            string estado = view.GetRowCellValue(e.FocusedRowHandle, "ESTADO")?.ToString();
            EstablecerEstadoPorCodigo(estado);

            // NUMVAP es autogenerado por el SP de insercion; nunca se edita a mano.
            txtCodMerc.Properties.ReadOnly = true;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void LimpiarControles()
        {
            txtCodMerc.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
           
            
            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        // ------------------------------------------------------------------
        // Carga / refresco del grid
        // ------------------------------------------------------------------
        private void CargarMercado()
        {
            var dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_MERCADOS_CONSULTAR]");
            gridControl1.DataSource = dt;
        }

        // ------------------------------------------------------------------
        // Botones
        // ------------------------------------------------------------------
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
            LimpiarControles();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CODMDO", txtCodMerc.Text.Trim());
                parametros.Add("@DESCRIPCION", txtDescripcion.Text.Trim());
                parametros.Add("@ESTADO", ObtenerCodigoEstadoSeleccionado());
                parametros.Add("@INGRESADO_POR", Configuracion.UsuarioActual);               
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_MERCADOS_INSERTAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarMercado();
                    LimpiarControles();
                }
                else
                {
                    Alertas.Advertencia(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodMerc.Text))
            {
                Alertas.Advertencia("Seleccione un Mercado en la lista.");
                return;
            }
            if (!ValidarCampos()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CODMDO", txtCodMerc.Text.Trim());
                parametros.Add("@DESCRIPCION", txtDescripcion.Text.Trim());
                parametros.Add("@ESTADO", ObtenerCodigoEstadoSeleccionado());
                parametros.Add("@MODIFICADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_MERCADOS_ACTUALIZAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarMercado();
                    LimpiarControles();
                }
                else
                {
                    Alertas.Advertencia(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodMerc.Text))
            {
                Alertas.Advertencia("Seleccione un Mercado en la lista.");
                return;
            }

            if (!Alertas.Confirmar($"¿Eliminar el Mercado \"{txtDescripcion.Text}\"?"))
                return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@CODMDO", txtCodMerc.Text.Trim());
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_MERCADOS_ELIMINAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarMercado();
                    LimpiarControles();
                }
                else
                {
                    Alertas.Advertencia(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();
                Alertas.Error(mensaje);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            gridView1.ClearSelection();
            LimpiarControles();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodMerc.Text))
            {
                Alertas.Advertencia("Ingrese la codigo del Mercado.");
                txtCodMerc.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                Alertas.Advertencia("Ingrese la descripcion del Mercado.");
                txtDescripcion.Focus();
                return false;
            }
            if (ObtenerCodigoEstadoSeleccionado() == null)
            {
                Alertas.Advertencia("Seleccione el estado del Mercado.");
                cboEstado.Focus();
                return false;
            }
            return true;
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
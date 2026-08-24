using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dapper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;

namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmDestinos : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();
        private class EstadoItem
        {
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public override string ToString() => Descripcion;
        }
        public frmDestinos()
        {
            InitializeComponent();
            LimpiarControles();
            CargarComboEstado();
            CargarComboEstadoPuerto();
            CargarDestinos();
            CargarPais();
            LimpiarControlesPuerto();
            // Coloreado + carga de fila seleccionada
            gridView1.CustomColumnDisplayText += gridView1_CustomColumnDisplayText;
            gridView1.RowCellStyle += gridView1_RowCellStyle;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

            gridView2.CustomColumnDisplayText += gridView2_CustomColumnDisplayText;
            gridView2.RowCellStyle += gridView2_RowCellStyle;
            gridView2.FocusedRowChanged += gridView2_FocusedRowChanged;
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
        public static string QuitarAcentos(string texto)
        {
            string normalizado = texto.Normalize(
                System.Text.NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (char c in normalizado)
            {
                var uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);

                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(
                System.Text.NormalizationForm.FormC);
        }
        private void CargarPais()
        {
            DataTable dt = _dal.EjecutarConsulta("[EMH].[SP_PAIS]",
                new
                {
                    ACCION = "LISTAR"
                });

            cboCodPaisMH.Properties.DataSource = dt;
            cboCodPaisMH.Properties.ValueMember = "CODI_MH";
            cboCodPaisMH.Properties.DisplayMember  = "VALORES";
        }

        private string ObtenerCodigoEstadoSeleccionado()
        {
            return (cboEstado.SelectedItem as EstadoItem)?.Codigo;
        }
        private string ObtenerCodigoEstadoPuertoSeleccionado()
        {
            return (cboEstadoPuerto.SelectedItem as EstadoItem)?.Codigo;
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
        private void EstablecerEstadoPuertoPorCodigo(string codigo)
        {
            foreach (var obj in cboEstadoPuerto.Properties.Items)
            {
                if (obj is EstadoItem item && item.Codigo == codigo)
                {
                    cboEstadoPuerto.SelectedItem = item;
                    return;
                }
            }
            cboEstadoPuerto.SelectedItem = null;
        }
        // puerto 
        private void CargarComboEstadoPuerto()
        {
            cboEstadoPuerto.Properties.Items.Clear();
            cboEstadoPuerto.Properties.Items.Add(new EstadoItem { Codigo = "ACT", Descripcion = "Activo" });
            cboEstadoPuerto.Properties.Items.Add(new EstadoItem { Codigo = "INA", Descripcion = "Inactivo" });
            cboEstadoPuerto.SelectedIndex = 0;
        }


        // ------------------------------------------------------------------
        // Grid: texto legible en la columna ESTADO
        // ------------------------------------------------------------------
        private void gridView1_CustomColumnDisplayText( object sender,  DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ESTADO")
            {
                switch (e.Value?.ToString())
                {
                    case "ACT":
                        e.DisplayText = "Activo";
                        break;

                    case "INA":
                        e.DisplayText = "Inactivo";
                        break;
                }
            }
             if (e.Column.FieldName == "AREA_CA")
            {
                switch (e.Value?.ToString())
                {
                    case "S":
                        e.DisplayText = "Sí";
                        break;

                    case "N":
                        e.DisplayText = "No";
                        break;
                }
            }
        }
      
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

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                LimpiarControles();
                return;
            }

            var view = gridView1;

            txtNumDes.Text = view.GetRowCellValue(e.FocusedRowHandle, "NUMDES")?.ToString();
            txtDescripcion.Text = view.GetRowCellValue(e.FocusedRowHandle, "DESCRIPCION")?.ToString();

            // AREA_CA (S/N)
            string areaCA = Convert.ToString(
                view.GetRowCellValue(e.FocusedRowHandle, "AREA_CA")
            )?.Trim();

            cboAreaCA.Checked = string.Equals(
                areaCA, "S", StringComparison.OrdinalIgnoreCase);

            // CODPAIS_MH (puede ser nulo)
            string codPais = Convert.ToString(
                view.GetRowCellValue(e.FocusedRowHandle, "CODPAIS_MH")
            );

            cboCodPaisMH.EditValue =
                string.IsNullOrWhiteSpace(codPais)
                    ? null
                    : codPais;

            string estado = view.GetRowCellValue(e.FocusedRowHandle, "ESTADO")?.ToString();
            EstablecerEstadoPorCodigo(estado);

            // NUMVAP es autogenerado por el SP de insercion; nunca se edita a mano.
            txtNumDes.Properties.ReadOnly = true;
            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            LimpiarControlesPuerto();
            CargarPuertoDestinos( Convert.ToInt32(txtNumDes.Text));
            btnGuardarPuerto.Enabled = true;
        }

        private void LimpiarControles()
        {
            txtNumDes.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            cboAreaCA.Checked = false;
            cboCodPaisMH.ItemIndex = 0;


            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void LimpiarControlesPuerto()
        {
            txtNumPto.Text = string.Empty;
            txtDescripcionPuerto.Text = string.Empty;
           

            btnNuevoPuerto.Enabled = false;
            btnGuardarPuerto.Enabled = false;
            btnEditarPuerto.Enabled = false;
            btnEliminarPuerto.Enabled = false;
        }

        // ------------------------------------------------------------------
        // Carga / refresco del grid
        // ------------------------------------------------------------------
        private void CargarDestinos()
        {
            var dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_DESTINOS_CONSULTAR]");
            gridControl1.DataSource = dt;
        }
        private void CargarPuertoDestinos(int IdDestino)
        {
            var dt = _dal.EjecutarConsulta("[EEXPORTACION].[SP_EXP_PUERTOS_DESTINOS_CONSULTAR]",
                new
                {
                    NUMDES = IdDestino
                });
            gridControl2.DataSource = dt;
        }
        private bool ValidarCampos()
        {
            
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                Alertas.Advertencia("Ingrese la descripcion del Destino.");
                txtDescripcion.Focus();
                return false;
            }
            if (ObtenerCodigoEstadoSeleccionado() == null)
            {
                Alertas.Advertencia("Seleccione el estado del Destino.");
                cboEstado.Focus();
                return false;
            }
            return true;
        }
        private bool ValidarCamposPuerto()
        {

            if (string.IsNullOrWhiteSpace(txtNumDes.Text))
            {
                Alertas.Advertencia("Destino Requerido. crear o seleccionar");               
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcionPuerto.Text))
            {
                Alertas.Advertencia("Ingrese la descripcion del Puerto.");
                txtDescripcionPuerto.Focus();
                return false;
            }
            if (ObtenerCodigoEstadoPuertoSeleccionado() == null)
            {
                Alertas.Advertencia("Seleccione el estado del Puerto.");
                cboEstadoPuerto.Focus();
                return false;
            }
            return true;
        }

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
            
                parametros.Add("@DESCRIPCION", txtDescripcion.Text.Trim());
                parametros.Add("@AREA_CA", Convert.ToBoolean(cboAreaCA.EditValue) ? "S" : "N");
                parametros.Add("@CODPAIS_MH", cboCodPaisMH.EditValue);
                parametros.Add("@ESTADO", ObtenerCodigoEstadoSeleccionado());
                parametros.Add("@INGRESADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@NUMDES", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_DESTINOS_INSERTAR]", parametros);

                int? _NumDes = parametros.Get<int?>("@NUMDES");
               
                int? resultado = parametros.Get<int?>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    txtNumDes.Text = Convert.ToString(_NumDes);
                    Alertas.Exito(mensaje);
                    CargarDestinos();
                    LimpiarControlesPuerto();
                    CargarPuertoDestinos(Convert.ToInt32(txtNumDes.Text));
                    btnGuardar.Enabled = false;
                    btnGuardarPuerto.Enabled = true;
                    txtDescripcionPuerto.Focus();
                    //LimpiarControles();
                }
                else
                {
                    LimpiarControlesPuerto();
                    btnGuardar.Enabled = true;
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
            if (string.IsNullOrEmpty(txtNumDes.Text))
            {
                Alertas.Advertencia("Seleccione un Destino en la lista.");
                return;
            }
            if (!ValidarCampos()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NUMDES", txtNumDes.Text.Trim());
                parametros.Add("@DESCRIPCION", txtDescripcion.Text.Trim());
                parametros.Add("@AREA_CA", Convert.ToBoolean(cboAreaCA.EditValue) ? "S" : "N");
                parametros.Add("@CODPAIS_MH", cboCodPaisMH.EditValue);
                parametros.Add("@ESTADO", ObtenerCodigoEstadoSeleccionado());
                parametros.Add("@MODIFICADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_DESTINOS_ACTUALIZAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarDestinos();
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
            if (string.IsNullOrEmpty(txtNumDes.Text))
            {
                Alertas.Advertencia("Seleccione un Destino en la lista.");
                return;
            }

            if (!Alertas.Confirmar($"¿Eliminar el Destino \"{txtDescripcion.Text}\"?"))
                return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NUMDES", txtNumDes.Text.Trim());
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_DESTINOS_ELIMINAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarDestinos();
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
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCodPaisMH_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            string texto = QuitarAcentos(cboCodPaisMH.Text).ToUpper();
        }

        private void btnNuevoPuerto_Click(object sender, EventArgs e)
        {
            LimpiarControlesPuerto();
            btnGuardarPuerto.Enabled = true;
            txtDescripcionPuerto.Focus();
        }

        private void btnGuardarPuerto_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposPuerto()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NUMDES", Convert.ToInt32(txtNumDes.Text));
                parametros.Add("@DESCRIPCION", txtDescripcionPuerto.Text.Trim());
                parametros.Add("@ESTADO", ObtenerCodigoEstadoPuertoSeleccionado());
                parametros.Add("@INGRESADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@NUMPTO", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_PUERTOS_DESTINOS_INSERTAR]", parametros);

                int? numpto = parametros.Get<int?>("@NUMPTO");              
                int? resultado = parametros.Get<int?>("@Resultado");              
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    txtNumPto.Text = Convert.ToString(numpto);
                    Alertas.Exito(mensaje);

                    LimpiarControlesPuerto();
                    CargarPuertoDestinos(Convert.ToInt32(txtNumDes.Text));
                    btnNuevoPuerto.Enabled = true;
                    btnGuardarPuerto.Enabled = false;
                    
                }
                else
                {
                    btnNuevoPuerto.Enabled = false;
                    btnGuardarPuerto.Enabled = true;
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

        private void btnEditarPuerto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumDes.Text))
            {
                Alertas.Advertencia("Seleccione un Destino en la lista.");
                return;
            }
            if (string.IsNullOrEmpty(txtNumPto.Text))
            {
                Alertas.Advertencia("Seleccione un Puerto en la lista.");
                return;
            }
            if (!ValidarCamposPuerto()) return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NUMDES",Convert.ToInt32(txtNumDes.Text));
                parametros.Add("@NUMPTO", Convert.ToInt32(txtNumPto.Text));
                parametros.Add("@DESCRIPCION", txtDescripcionPuerto.Text.Trim());
                parametros.Add("@ESTADO", ObtenerCodigoEstadoPuertoSeleccionado());
                parametros.Add("@MODIFICADO_POR", Configuracion.UsuarioActual);
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_PUERTOS_DESTINOS_ACTUALIZAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarPuertoDestinos(Convert.ToInt32(txtNumDes.Text));
                    LimpiarControlesPuerto();
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

        private void btnEliminarPuerto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumDes.Text))
            {
                Alertas.Advertencia("Seleccione un Destino en la lista.");
                return;
            }
            if (string.IsNullOrEmpty(txtNumPto.Text))
            {
                Alertas.Advertencia("Seleccione un Puerto en la lista.");
                return;
            }

            if (!Alertas.Confirmar($"¿Eliminar el Puerto \"{txtDescripcionPuerto.Text}\"?"))
                return;

            try
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NUMDES", Convert.ToInt32(txtNumDes.Text));
                parametros.Add("@NUMPTO", Convert.ToInt32(txtNumPto.Text));
                parametros.Add("@Resultado", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parametros.Add("@Mensaje", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

                _dal.EjecutarConSalida("[EEXPORTACION].[SP_EXP_PUERTOS_DESTINOS_ELIMINAR]", parametros);

                int resultado = parametros.Get<int>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    CargarPuertoDestinos(Convert.ToInt32(txtNumDes.Text));
                    LimpiarControlesPuerto();
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

        private void btnCancelarPuerto_Click(object sender, EventArgs e)
        {
            gridView2.ClearSelection();
            LimpiarControlesPuerto();
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ESTADO")
            {
                switch (e.Value?.ToString())
                {
                    case "ACT":
                        e.DisplayText = "Activo";
                        break;

                    case "INA":
                        e.DisplayText = "Inactivo";
                        break;
                }
            }
            
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void gridView2_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                LimpiarControlesPuerto();
                return;
            }
            LimpiarControlesPuerto();
            var view2 = gridView2;

            txtNumPto.Text = view2.GetRowCellValue(e.FocusedRowHandle, "NUMPTO")?.ToString();
            txtDescripcionPuerto.Text = view2.GetRowCellValue(e.FocusedRowHandle, "DESCRIPCION")?.ToString();


            string estado = view2.GetRowCellValue(e.FocusedRowHandle, "ESTADO")?.ToString();
            EstablecerEstadoPuertoPorCodigo(estado);

            //// NUMVAP es autogenerado por el SP de insercion; nunca se edita a mano.
            txtNumPto.Properties.ReadOnly = true;
            btnGuardarPuerto.Enabled = false;
            btnEditarPuerto.Enabled = true;
            btnEliminarPuerto.Enabled = true;



        }
    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;


namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmFechaEmbarque : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();

        private SqlDataSource _sqlDataSource1;

        private StoredProcQuery _selectQuery;

        public int IdContrato { get; set; }
        public string Client { get; set; }
        public string NumContrato { get; set; }
        public string TonelContrato { get; set; }

        // TODO: ajusta el nombre de la connection string a la de tu App.config / Web.config
        private const string CONNECTION = "SistemaContable"; 

        // TODO: reemplaza por tu clase real de sesión de usuario (ej. Sesion.UsuarioActual)
        private string UsuarioActual => Environment.UserName;

        public frmFechaEmbarque()
        {
            InitializeComponent();
        }

        private void frmFechaEmbarque_Load(object sender, EventArgs e)
        {
            lbIdContrato.Text = IdContrato.ToString();
            txtCliente.Text = Client;
            txtContrato.Text = NumContrato;
            txtTonelContrato.Text = TonelContrato;
          
            CargarFechasEmbarque();
            btnNuevo.Focus();
        }

        // ------------------------------------------------------------------
        // LECTURA (SqlDataSource + procedimiento SP_EXP_FECHAS_EMBARQUE_SEL)
        // ------------------------------------------------------------------


        private void CargarFechasEmbarque()
        {
            using (var conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
            using (var cmd = new SqlCommand(
                "EEXPORTACION.SP_EXP_FECHAS_EMBARQUE_CONSULTAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCONTEXP", IdContrato);

                DataTable dt = new DataTable();

                conn.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                gridControl1.DataSource = dt;
            }
        }
        // ------------------------------------------------------------------
        // NUEVO -> agrega la fila nueva en la parte superior del grid
        // ------------------------------------------------------------------
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gridView1.SetRowCellValue(e.RowHandle, colIdContrato, lbIdContrato.Text);
            gridView1.SetRowCellValue(e.RowHandle, colFechaInicio, DateTime.Today);
            gridView1.SetRowCellValue(e.RowHandle, colActivo, true);
            gridView1.SetRowCellValue(e.RowHandle, colEstado, "ACT");
        }

        // ------------------------------------------------------------------
        // GUARDAR -> recorre las filas del grid y aplica INSERT/UPDATE según
        // corresponda (fila nueva = IDCNTEXD vacío/0; fila existente = UPDATE)
        // ------------------------------------------------------------------
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            try
            {
                for (int handle = 0; handle < gridView1.RowCount; handle++)
                {
                    GuardarFila(handle);
                }

                XtraMessageBox.Show("Cambios guardados con éxito.", "Información",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                CargarFechasEmbarque();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudieron guardar los cambios: {ex.Message}",
                    "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        // Se dispara al confirmar la edición de una fila (Enter / cambiar de fila).
        // Persiste inmediatamente esa fila para no perder cambios si cierran el form.
        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GuardarFila(e.RowHandle);
        }

        private void GuardarFila(int rowHandle)
        {
            if (rowHandle < 0)
            {
                return; // fila de nuevo item vacía o handle inválido
            }

            object idContratoVal = gridView1.GetRowCellValue(rowHandle, colIdContrato);
            object idDetalleVal = gridView1.GetRowCellValue(rowHandle, colCodigo);
            object fechaInicioVal = gridView1.GetRowCellValue(rowHandle, colFechaInicio);
            object fechaFinalVal = gridView1.GetRowCellValue(rowHandle, colFechaFinal);
            object toneladasVal = gridView1.GetRowCellValue(rowHandle, colToneladas);
            object observacionesVal = gridView1.GetRowCellValue(rowHandle, colObservaciones);
            object activoVal = gridView1.GetRowCellValue(rowHandle, colActivo);

            if (idContratoVal == null || idContratoVal == DBNull.Value)
            {
                return; // fila del "new item row" todavía sin datos
            }

            string estado = (activoVal != null && (bool)activoVal) ? "ACT" : "INA";
            bool esNuevo = idDetalleVal == null || idDetalleVal == DBNull.Value || (int)idDetalleVal == 0;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
            {
                conn.Open();

                if (esNuevo)
                {
                    using (var cmd = new SqlCommand("[EEXPORTACION].[SP_EXP_FECHAS_EMBARQUE_INSERTAR]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCONTEXP", idContratoVal);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", (object)fechaInicioVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FECHA_FINAL", (object)fechaFinalVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TONELADAS", (object)toneladasVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OBSERVACIONES", (object)observacionesVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ESTADO", estado);
                        cmd.Parameters.AddWithValue("@INGRESADO_POR", UsuarioActual);

                        var outParam = new SqlParameter("@IDCNTEXD", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(outParam);

                        cmd.ExecuteNonQuery();

                        int nuevoId = (int)outParam.Value;
                        gridView1.SetRowCellValue(rowHandle, colCodigo, nuevoId);
                        gridView1.SetRowCellValue(rowHandle, colEstado, estado);
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand("[EEXPORTACION].[SP_EXP_FECHAS_EMBARQUE_ACTUALIZAR]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCONTEXP", idContratoVal);
                        cmd.Parameters.AddWithValue("@IDCNTEXD", idDetalleVal);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", (object)fechaInicioVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FECHA_FINAL", (object)fechaFinalVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TONELADAS", (object)toneladasVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OBSERVACIONES", (object)observacionesVal ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ESTADO", estado);
                        cmd.Parameters.AddWithValue("@MODIFICADO_POR", UsuarioActual);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // ------------------------------------------------------------------
        // ELIMINAR
        // ------------------------------------------------------------------
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int handle = gridView1.FocusedRowHandle;
            if (handle < 0)
            {
                XtraMessageBox.Show("Selecciona una fila para eliminar.", "Aviso",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }

            object idDetalleVal = gridView1.GetRowCellValue(handle, colCodigo);
            if (idDetalleVal == null || idDetalleVal == DBNull.Value)
            {
                // Fila nueva aún no guardada: solo se quita del grid.
                gridView1.DeleteRow(handle);
                return;
            }

            if (XtraMessageBox.Show("¿Deseas eliminar la fecha de embarque seleccionada?", "Confirmar",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question)
                != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
                using (var cmd = new SqlCommand("[EEXPORTACION].[SP_EXP_FECHAS_EMBARQUE_ELIMINAR]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCONTEXP", IdContrato);
                    cmd.Parameters.AddWithValue("@IDCNTEXD", idDetalleVal);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gridView1.DeleteRow(handle);

                XtraMessageBox.Show("Registro eliminado con éxito.", "Información",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo eliminar el registro: {ex.Message}",
                    "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------------
        // EXPORTAR A EXCEL
        // ------------------------------------------------------------------
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            using (var sfd = new System.Windows.Forms.SaveFileDialog())
            {
                sfd.Filter = "Archivo de Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"FechasEmbarque_{txtContrato.Text}.xlsx";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        gridControl1.ExportToXlsx(sfd.FileName);
                        XtraMessageBox.Show("Exportación realizada con éxito.", "Información",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"No se pudo exportar el archivo: {ex.Message}",
                            "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ------------------------------------------------------------------
        // BUSCADOR
        // ------------------------------------------------------------------
        private void txtBuscar_EditValueChanged(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text?.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                gridView1.ActiveFilterString = string.Empty;
                return;
            }

            gridView1.ApplyFindFilter(texto);
        }

        

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

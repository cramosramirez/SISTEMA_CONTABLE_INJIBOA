using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;

namespace SistemaContable.UI.Forms.Exportacion
{
    public partial class frmAddendum : DevExpress.XtraEditors.XtraForm
    {
        private readonly DALBase _dal = new DALBase();

      

        public int IdContratoAddendum { get; set; }
        public string ClientAddendum { get; set; }
        public string NumContratoAddendum { get; set; }
        public string TonelContratoAddendum { get; set; }

        // TODO: ajusta el nombre de la connection string a la de tu App.config / Web.config
        private const string CONNECTION = "SistemaContable";

        // TODO: reemplaza por tu clase real de sesión de usuario (ej. Sesion.UsuarioActual)
        private string UsuarioActual => Environment.UserName;

        public frmAddendum()
        {
            InitializeComponent();
        }

        private void frmAddendum_Load(object sender, EventArgs e)
        {
            lbIdContrato.Text = IdContratoAddendum.ToString();
            txtCliente.Text = ClientAddendum;
            txtContrato.Text = NumContratoAddendum;
            txtTonelContrato.Text = TonelContratoAddendum;

            //CargarAddendums();
            btnNuevo.Focus();
        }

        // ------------------------------------------------------------------
        // LECTURA (SqlDataSource + procedimiento SP_EXP_FECHAS_EMBARQUE_SEL)
        // ------------------------------------------------------------------


        private void CargarAddendums()
        {
            using (var conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
            using (var cmd = new SqlCommand(
                "EEXPORTACION.SP_EXP_ADDENDUM_CONSULTAR", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDCONTEXP", IdContratoAddendum);

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
            gridView1.SetRowCellValue(e.RowHandle, colFechaAddendum, DateTime.Today);
            gridView1.SetRowCellValue(e.RowHandle, colEstado, "ACT");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            try
            {
                for (int handle = 0; handle < gridView1.RowCount; handle++)
                    GuardarFila(handle);

                XtraMessageBox.Show("Cambios guardados con éxito.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarAddendums(); // Ajusta al nombre real de tu método de carga.
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudieron guardar los cambios: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_RowUpdated(object sender,
            DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            GuardarFila(e.RowHandle);
        }

        private static object ValorDb(object valor)
        {
            return valor == null || valor == DBNull.Value ? DBNull.Value : valor;
        }

        private void GuardarFila(int rowHandle)
        {
            if (rowHandle < 0)
                return;

            object idContratoVal = gridView1.GetRowCellValue(rowHandle, colIdContrato);
            object idAddendumVal = gridView1.GetRowCellValue(rowHandle, colIdAddendum);
            object numeroVal = gridView1.GetRowCellValue(rowHandle, colNumeroAddendum);
            object fechaVal = gridView1.GetRowCellValue(rowHandle, colFechaAddendum);
            object tipoVal = gridView1.GetRowCellValue(rowHandle, colTipoAddendum);
            object toneladasVal = gridView1.GetRowCellValue(rowHandle, colToneladasAddendum);
            object precioVal = gridView1.GetRowCellValue(rowHandle, colPrecioAddendum);
            object observacionesVal = gridView1.GetRowCellValue(rowHandle, colObservaciones);
            object estadoVal = gridView1.GetRowCellValue(rowHandle, colEstado);

            if (idContratoVal == null || idContratoVal == DBNull.Value)
                return;

            string estado = Convert.ToString(estadoVal);
            if (string.IsNullOrWhiteSpace(estado))
                estado = "ACT";

            bool esNuevo = idAddendumVal == null ||
                           idAddendumVal == DBNull.Value ||
                           Convert.ToInt32(idAddendumVal) == 0;

            using (var conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
            {
                conn.Open();

                if (esNuevo)
                {
                    using (var cmd = new SqlCommand(
                        "[EEXPORTACION].[SP_EXP_ADDENDUM_INSERTAR]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDCONTEXP", idContratoVal);
                        cmd.Parameters.AddWithValue("@NUMERO_ADDENDUM", ValorDb(numeroVal));
                        cmd.Parameters.AddWithValue("@FECHA_ADDENDUM", ValorDb(fechaVal));
                        cmd.Parameters.AddWithValue("@TIPO_ADDENDUM", ValorDb(tipoVal));
                        cmd.Parameters.AddWithValue("@TONELADAS_ADDENDUM", ValorDb(toneladasVal));
                        cmd.Parameters.AddWithValue("@PRECIO_ADDENDUM", ValorDb(precioVal));
                        cmd.Parameters.AddWithValue("@OBSERVACIONES", ValorDb(observacionesVal));
                        cmd.Parameters.AddWithValue("@ESTADO", estado);
                        cmd.Parameters.AddWithValue("@INGRESADO_POR", UsuarioActual);

                        var salida = new SqlParameter("@IDADDEN", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(salida);

                        cmd.ExecuteNonQuery();

                        gridView1.SetRowCellValue(rowHandle, colIdAddendum,
                            Convert.ToInt32(salida.Value));
                        gridView1.SetRowCellValue(rowHandle, colEstado, estado);
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand(
                        "[EEXPORTACION].[SP_EXP_ADDENDUM_ACTUALIZAR]", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDCONTEXP", idContratoVal);
                        cmd.Parameters.AddWithValue("@IDADDEN", idAddendumVal);
                        cmd.Parameters.AddWithValue("@NUMERO_ADDENDUM", ValorDb(numeroVal));
                        cmd.Parameters.AddWithValue("@FECHA_ADDENDUM", ValorDb(fechaVal));
                        cmd.Parameters.AddWithValue("@TIPO_ADDENDUM", ValorDb(tipoVal));
                        cmd.Parameters.AddWithValue("@TONELADAS_ADDENDUM", ValorDb(toneladasVal));
                        cmd.Parameters.AddWithValue("@PRECIO_ADDENDUM", ValorDb(precioVal));
                        cmd.Parameters.AddWithValue("@OBSERVACIONES", ValorDb(observacionesVal));
                        cmd.Parameters.AddWithValue("@ESTADO", estado);
                        cmd.Parameters.AddWithValue("@MODIFICADO_POR", UsuarioActual);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int handle = gridView1.FocusedRowHandle;
            if (handle < 0)
            {
                XtraMessageBox.Show("Selecciona un addendum para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            object idContratoVal = gridView1.GetRowCellValue(handle, colIdContrato);
            object idAddendumVal = gridView1.GetRowCellValue(handle, colIdAddendum);

            if (idAddendumVal == null || idAddendumVal == DBNull.Value)
            {
                gridView1.DeleteRow(handle);
                return;
            }

            if (XtraMessageBox.Show("¿Deseas eliminar el addendum seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings[CONNECTION].ConnectionString))
                using (var cmd = new SqlCommand(
                    "[EEXPORTACION].[SP_EXP_ADDENDUM_ELIMINAR]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDCONTEXP", idContratoVal);
                    cmd.Parameters.AddWithValue("@IDADDEN", idAddendumVal);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                gridView1.DeleteRow(handle);

                XtraMessageBox.Show("Addendum eliminado con éxito.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"No se pudo eliminar el addendum: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
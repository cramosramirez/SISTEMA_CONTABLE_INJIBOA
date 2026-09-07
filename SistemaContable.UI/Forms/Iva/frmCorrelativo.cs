using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Dapper;
using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;

namespace SistemaContable.UI.Forms.Iva
{
    public partial class frmCorrelativo : Form
    {
        private readonly DALBase _dal = new DALBase();
        public FormBorderEffect FormBorderEffect { get; }
        public object IconOptions { get; }

        private void InicializarMeses()
        {
            cb_Mes.DataSource = FormHelper.ObtenerMeses();
            cb_Mes.DisplayMember = "Mes";
            cb_Mes.ValueMember = "Value";
            cb_Mes.SelectedValue = DateTime.Now.Month.ToString("00");
        }
        private void InicializarDatos()
        {
            txt_Anio.Text = DateTime.Now.Year.ToString();
            int mesAnterior = DateTime.Now.Month - 1;
            // Manejar el caso de enero (mes 1), para que reste y quede diciembre (12)
            if (mesAnterior == 0)
            {
                mesAnterior = 12;
            }
            cb_Mes.SelectedValue = mesAnterior.ToString("00");
        }

        private void ActivarSeleccionUnica(DevExpress.XtraEditors.GroupControl contenedor)
        {
            foreach (Control ctrl in contenedor.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.CheckEdit chk)
                {
                    chk.CheckedChanged += (s, e) =>
                    {
                        if (chk.Checked)
                        {
                            foreach (Control c in contenedor.Controls)
                            {
                                if (c is DevExpress.XtraEditors.CheckEdit otro && otro != chk)
                                {
                                    otro.Checked = false;
                                }
                            }
                        }
                    };
                }
            }
        }
        public frmCorrelativo()
        {
            InitializeComponent();
            InicializarMeses();
            InicializarDatos();
            ActivarSeleccionUnica(groupControl2);

        }

        private bool Update_Correlativo(string procedimiento, string anio, string mes)
        {
            try
            {
                var parametros = new DynamicParameters();

                parametros.Add("@anio", anio);
                parametros.Add("@mes", mes);

                parametros.Add("@Resultado",
                    dbType: DbType.Int32,
                    direction: ParameterDirection.Output);

                parametros.Add("@Mensaje",
                    dbType: DbType.String,
                    size: 500,
                    direction: ParameterDirection.Output);

                _dal.EjecutarConSalida(procedimiento, parametros);

                int? resultado = parametros.Get<int?>("@Resultado");
                string mensaje = parametros.Get<string>("@Mensaje");

                if (resultado == 1)
                {
                    Alertas.Exito(mensaje);
                    return true;
                }

                Alertas.Advertencia(mensaje);
                return false;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;

                if (mensaje.Contains("]:"))
                    mensaje = mensaje.Substring(mensaje.IndexOf("]:") + 2).Trim();

                Alertas.Error(mensaje);
                return false;
            }
        }

        private void bt_Procesar_Click(object sender, EventArgs e)
        {
            try
            {


                string anio = txt_Anio.Text;
                string mes = cb_Mes.SelectedValue?.ToString();



                if (string.IsNullOrWhiteSpace(anio))
                {
                    Alertas.Error("Año es requerido");
                    return;
                }

                if (string.IsNullOrWhiteSpace(mes))
                {
                    Alertas.Error("Mes es requerido");
                    return;
                }

                progressBar1.Visible = false;
                lblEstado.Visible = false;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;

                if (ck_Anexo1.Checked == false && ck_Anexo2.Checked == false && ck_Anexo3.Checked == false )
                {
                    Alertas.Error("Seleccione el Informe a Emitir");
                    return;
                }

                if (ck_Anexo1.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();

                    Update_Correlativo("[EIVA].[CRE_CORRELATIVO_COMPRAS]", anio,mes);
                    progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    
                }

                if (ck_Anexo2.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    Update_Correlativo("[EIVA].[CRE_CORRELATIVO_CCF]", anio, mes);
                    progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    
                }

                if (ck_Anexo3.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    Update_Correlativo("[EIVA].[CRE_CORRELATIVO_FA]", anio, mes);
                    progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    
                }

               

            }
            catch (Exception ex)
            {
                Alertas.Error(ex.Message);
            }
            finally
            {

            }
        }

       
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

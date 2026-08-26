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
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;

namespace SistemaContable.UI.Forms.Iva
{
    public partial class frmF987 : Form
    {
        private void InicializarMeses()
        {
            cb_Desde.DataSource = FormHelper.ObtenerMeses();
            cb_Desde.DisplayMember = "Mes";
            cb_Desde.ValueMember = "Value";
            cb_Desde.SelectedValue = DateTime.Now.Month.ToString("00");

            cb_Hasta.DataSource = FormHelper.ObtenerMeses();
            cb_Hasta.DisplayMember = "Mes";
            cb_Hasta.ValueMember = "Value";
            cb_Hasta.SelectedValue = DateTime.Now.Month.ToString("00");

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
            cb_Desde.SelectedValue = mesAnterior.ToString("00");
            cb_Hasta.SelectedValue = mesAnterior.ToString("00");
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

        public frmF987()
        {
            InitializeComponent();
            InicializarMeses();
            InicializarDatos();
            ActivarSeleccionUnica(groupControl2);
        }
        #region Generar Anexos
        private DataTable ObtenerDatosAnexo1(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO1_INSCRIPTOS_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo2(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO2_EXTRANJEROS_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo3(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO3_SUJETO_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo4(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO4_CLIENTES_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo5(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO5_FACMENORES_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo6(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO6_MANDANTE_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);

                    // ✅ Ejecutar y llenar DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // ✅ Validación resultado del SP
                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
        private DataTable ObtenerDatosAnexo7(string anio, string Desde, string Hasta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO7_MANDATARIOS_F987]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@Desde", Desde);
                    cmd.Parameters.AddWithValue("@Hasta", Hasta);
                    cmd.Parameters.AddWithValue("@USER_CREA", Configuracion.UsuarioActual);

                    // ✅ Parámetros OUTPUT
                    SqlParameter msg = new SqlParameter("@msg", SqlDbType.NVarChar, 800)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(msg);

                    SqlParameter code = new SqlParameter("@pResCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(code);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);


                    int res = code.Value != DBNull.Value ? (int)code.Value : -1;
                    string mensaje = msg.Value?.ToString();

                    if (res != 0)
                    {
                        Alertas.Error(mensaje);
                        return null;
                    }
                }
            }

            return dt;
        }
      
        #endregion

        #region Exportar
        private void ExportarExcel(DataTable dt, string nombre, string anio, string desde, string hasta, string anexo)
        {
            //if (dt.Rows.Count == 0)
            //{
            //    Alertas.Error("No hay datos para exportar");
            //    return;
            //}

            // Convertir mes a nombre en español
            string nombreMes = ObtenerMesEspanol(desde, hasta);

            // Ruta base: Escritorio
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa","F987", anio, nombreMes, "Xls");

            // Crear carpetas si no existen
            Directory.CreateDirectory(rutaBase);

            // Nombre base del archivo
            string nombreArchivoBase = $"{nombre}_{anio}_{nombreMes}.xlsx";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivoBase);

            // ✅ Evitar sobrescribir archivos
            int contador = 1;
            while (File.Exists(rutaCompleta))
            {
                string nuevoNombre = $"{nombre}_{anio}_{nombreMes}_{contador}.xlsx";
                rutaCompleta = Path.Combine(rutaBase, nuevoNombre);
                contador++;
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, anexo);

                // Estilo pro
                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;

                ws.Columns().AdjustToContents();

                wb.SaveAs(rutaCompleta);
            }

            Alertas.Exito($"Excel generado ✅\nRuta: {rutaCompleta}");
        }
        private void ExportarCsv(DataTable dt, string nombre, string anio, string desde, string hasta, string anexo)
        {
            if (dt.Rows.Count == 0)
            {
                Alertas.Error("No hay datos para exportar");
                return;
            }

            string nombreMes = ObtenerMesEspanol(desde, hasta);

            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa", "F987", anio, nombreMes, "Csv");

            Directory.CreateDirectory(rutaBase);

            string nombreArchivoBase = $"{nombre}_{anio}_{nombreMes}.csv";
            string rutaCompleta = Path.Combine(rutaBase, nombreArchivoBase);

            // ✅ Evitar sobrescribir
            int contador = 1;
            while (File.Exists(rutaCompleta))
            {
                string nuevoNombre = $"{nombre}_{anio}_{nombreMes}_{contador}.csv";
                rutaCompleta = Path.Combine(rutaBase, nuevoNombre);
                contador++;
            }

            // ✅ UTF-8 sin BOM (IMPORTANTE para MH)
            using (StreamWriter sw = new StreamWriter(rutaCompleta, false, new UTF8Encoding(false)))
            {
                foreach (DataRow row in dt.Rows)
                {
                    List<string> campos = new List<string>();

                    foreach (var item in row.ItemArray)
                    {
                        string valor = "";

                        if (item == DBNull.Value)
                        {
                            valor = "";
                        }
                        else if (item is DateTime fecha)
                        {
                            // ✅ Formato exigido MH
                            valor = fecha.ToString("dd/MM/yyyy");
                        }
                        else if (item is decimal dec)
                        {
                            valor = dec.ToString("0.00", CultureInfo.InvariantCulture);
                        }
                        else if (item is double dbl)
                        {
                            valor = dbl.ToString("0.00", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            valor = item.ToString().Trim();
                        }

                        // ✅ Limpiar datos (CRÍTICO MH)
                        valor = valor.Replace("\r", " ")
                                     .Replace("\n", " ")
                                     .Replace("|", "")
                                     .Replace(";", "");

                        // ✅ Limpiar NIT/DUI (quitar guiones)
                        if (EsPosibleNit(valor))
                            valor = valor.Replace("-", "");

                        campos.Add(valor);
                    }

                    // ✅ Separador oficial
                    string linea = string.Join(";", campos);

                    sw.WriteLine(linea);
                }
            }

            Alertas.Exito($"CSV F07 generado ✅\nRuta: {rutaCompleta}");
        }
        private bool EsPosibleNit(string valor)
        {
            // NIT Salvador: 14 dígitos (con o sin guiones)
            string limpio = valor.Replace("-", "");
            return limpio.Length == 14 && limpio.All(char.IsDigit);
        }
        private string ObtenerMesEspanol(string Desde, string Hasta)
        {
            CultureInfo cultura = new CultureInfo("es-ES");

            if (int.TryParse(Desde, out int mesDesde) &&
                int.TryParse(Hasta, out int mesHasta))
            {
                return $"{cultura.DateTimeFormat.GetMonthName(mesDesde).ToUpper()}_{cultura.DateTimeFormat.GetMonthName(mesHasta).ToUpper()}";
            }

            return $"{Desde.ToUpper()}_{Hasta.ToUpper()}";
        }
        #endregion
        private void bt_DescargaXls_Click(object sender, EventArgs e)
        {
            try
            {


                string anio = txt_Anio.Text;
                string Desde = cb_Desde.SelectedValue?.ToString();
                string Hasta = cb_Hasta.SelectedValue?.ToString();



                if (string.IsNullOrWhiteSpace(anio))
                {
                    Alertas.Error("Año es requerido");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Desde))
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

                if (ck_Anexo1.Checked == false && ck_Anexo2.Checked == false && ck_Anexo3.Checked == false && ck_Anexo4.Checked == false
                    && ck_Anexo5.Checked == false && ck_Anexo6.Checked == false && ck_Anexo7.Checked == false )
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

                    var dt = ObtenerDatosAnexo1(anio, Desde, Hasta);

                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_1_Inscritos", anio, Desde, Hasta, "Anexo_1");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

               if (ck_Anexo2.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo2(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_2_Extranjeros", anio, Desde, Hasta, "Anexo_2");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                  if (ck_Anexo3.Checked)
                  {
                      progressBar1.Visible = true;
                      progressBar1.Value = 65;
                      lblEstado.Visible = true;
                      lblEstado.Text = "Procesando... 65%";
                      Application.DoEvents();
                      var dt = ObtenerDatosAnexo3(anio, Desde, Hasta);
                      if (dt != null)
                      {
                          ExportarExcel(dt, "Anexo_3_Sujeto", anio, Desde, Hasta, "Anexo_3");
                          progressBar1.Value = 100; // Excel generado
                          lblEstado.Text = "Completado 100%";
                      }
                  }
                
                  if (ck_Anexo4.Checked)
                  {
                      progressBar1.Visible = true;
                      progressBar1.Value = 65;
                      lblEstado.Visible = true;
                      lblEstado.Text = "Procesando... 65%";
                      Application.DoEvents();
                      var dt = ObtenerDatosAnexo4(anio, Desde, Hasta);
                      if (dt != null)
                      {
                          ExportarExcel(dt, "Anexo_4_Clientes", anio, Desde, Hasta, "Anexo_4");
                          progressBar1.Value = 100; // Excel generado
                          lblEstado.Text = "Completado 100%";
                      }
                  }
             
                  if (ck_Anexo5.Checked)
                  {
                      progressBar1.Visible = true;
                      progressBar1.Value = 65;
                      lblEstado.Visible = true;
                      lblEstado.Text = "Procesando... 65%";
                      Application.DoEvents();
                      var dt = ObtenerDatosAnexo5(anio, Desde, Hasta);
                      if (dt != null)
                      {
                          ExportarExcel(dt, "Anexo_5_FA_Menores_A", anio, Desde, Hasta, "Anexo_5");
                          progressBar1.Value = 100; // Excel generado
                          lblEstado.Text = "Completado 100%";
                      }
                  }
                  
                  if (ck_Anexo6.Checked)
                  {
                      progressBar1.Visible = true;
                      progressBar1.Value = 65;
                      lblEstado.Visible = true;
                      lblEstado.Text = "Procesando... 65%";
                      Application.DoEvents();
                      var dt = ObtenerDatosAnexo6(anio, Desde, Hasta);
                      if (dt != null)
                      {
                          ExportarExcel(dt, "Anexo_6_Mandatario", anio, Desde, Hasta, "Anexo_7");
                          progressBar1.Value = 100; // Excel generado
                          lblEstado.Text = "Completado 100%";
                      }
                  }
                  
                  if (ck_Anexo7.Checked)
                  {
                      progressBar1.Visible = true;
                      progressBar1.Value = 65;
                      lblEstado.Visible = true;
                      lblEstado.Text = "Procesando... 65%";
                      Application.DoEvents();
                      var dt = ObtenerDatosAnexo7(anio, Desde, Hasta);
                      if (dt != null)
                      {
                          ExportarExcel(dt, "Anexo12_retencion13_sujeto_excluido", anio, Desde, Hasta, "Anexo_12");
                          progressBar1.Value = 100; // Excel generado
                          lblEstado.Text = "Completado 100%";
                      }
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

        private void bt_DescargarCsv_Click(object sender, EventArgs e)
        {
            try
            {


                string anio = txt_Anio.Text;
                string Desde = cb_Desde.SelectedValue?.ToString();
                string Hasta = cb_Hasta.SelectedValue?.ToString();



                if (string.IsNullOrWhiteSpace(anio))
                {
                    Alertas.Error("Año es requerido");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Desde))
                {
                    Alertas.Error("Desde es requerido");
                    return;
                }
                if (string.IsNullOrWhiteSpace(Hasta))
                    {
                        Alertas.Error("Hasta es requerido");
                        return;
                    }

                progressBar1.Visible = false;
                lblEstado.Visible = false;
                progressBar1.Style = ProgressBarStyle.Blocks;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;

                if (ck_Anexo1.Checked == false && ck_Anexo2.Checked == false && ck_Anexo3.Checked == false && ck_Anexo4.Checked == false
                    && ck_Anexo5.Checked == false && ck_Anexo6.Checked == false && ck_Anexo7.Checked == false )
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

                    var dt = ObtenerDatosAnexo1(anio, Desde, Hasta);

                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_1_Inscritos", anio, Desde, Hasta, "Anexo_1");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo2.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo2(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_2_Extranjeros", anio, Desde, Hasta, "Anexo_2");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo3.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo3(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_3_Sujeto", anio, Desde, Hasta, "Anexo_3");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo4.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo4(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_4_Clientes", anio, Desde, Hasta, "Anexo_4");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo5.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo5(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_5_FA_Menores_A", anio, Desde, Hasta, "Anexo_5");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo6.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo6(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_6_Mandante", anio, Desde, Hasta, "Anexo_6");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo7.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo7(anio, Desde, Hasta);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_7_Mandatarios", anio, Desde, Hasta, "Anexo_7");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
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

        private void bt_AbrirDirectorio_Click(object sender, EventArgs e)
        {
            string Desde = cb_Desde.SelectedValue?.ToString();
            string Hasta = cb_Hasta.SelectedValue?.ToString();

            string nombreMes = ObtenerMesEspanol(Desde, Hasta);
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa", "F987", txt_Anio.Text, nombreMes);

            if (Directory.Exists(rutaBase))
            {
                Process.Start("explorer.exe", rutaBase);
            }
            else
            {
                MessageBox.Show(
                    "La carpeta no existe:\n" + rutaBase,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

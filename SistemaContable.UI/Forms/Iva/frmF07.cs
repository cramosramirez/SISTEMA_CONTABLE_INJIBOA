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
using DevExpress.XtraEditors;
using SistemaContable.DAL;
using SistemaContable.UI.Helpers;
namespace SistemaContable.UI.Forms.Iva
{
    public partial class frmF07 : Form
    {
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
        public frmF07()
        {
            InitializeComponent();
            InicializarMeses();
            InicializarDatos();
            ActivarSeleccionUnica(groupControl2);
           
        }
        #region Generar Anexos
        private DataTable ObtenerDatosAnexo1(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_1_VTAS_CONTRIBUYENTES_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo2(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_2_VTAS_CONSUMIDOR_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo3(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_3_COMPRAS_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo5(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_5_COMPRAS_SUJETOS_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo9(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_9_VTAS_CONTRIBUYENTES_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo10(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_10_COMPRAS_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexo12(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_12_COMPRAS_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private DataTable ObtenerDatosAnexoAnulados(string anio, string mes)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(Configuracion.CadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("[EIVA].[VIEW_ANEXO_ANULADOS_F07]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ Parámetros de entrada
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@mes", mes);
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
        private void ExportarExcel(DataTable dt, string nombre, string anio, string mes, string anexo)
        {
            //if (dt.Rows.Count == 0)
            //{
            //    Alertas.Error("No hay datos para exportar");
            //    return;
            //}

            // Convertir mes a nombre en español
            string nombreMes = ObtenerMesEspanol(mes);

            // Ruta base: Escritorio
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa", "F07", anio, nombreMes, "Xls");

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
        private void ExportarCsv(DataTable dt, string nombre, string anio, string mes, string anexo)
        {
            if (dt.Rows.Count == 0)
            {
                Alertas.Error("No hay datos para exportar");
                return;
            }

            string nombreMes = ObtenerMesEspanol(mes);

            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa", "F07", anio, nombreMes, "Csv");

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
        private string ObtenerMesEspanol(string mes)
        {
            // Si viene como número (ej: "6" o "06")
            if (int.TryParse(mes, out int numeroMes))
            {
                return new CultureInfo("es-ES")
                    .DateTimeFormat
                    .GetMonthName(numeroMes)
                    .ToUpper(); // opcional: MAYÚSCULAS
            }

            // Si ya viene como texto, lo devolvemos
            return mes.ToUpper();
        }
        #endregion
        private void bt_DescargaXls_Click(object sender, EventArgs e)
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

                if (ck_Anexo1.Checked == false && ck_Anexo2.Checked == false && ck_Anexo3.Checked == false && ck_Anexo5.Checked == false
                    && ck_Anexo9.Checked == false && ck_Anexo10.Checked == false && ck_Anexo12.Checked == false && ck_AnexoAnulados.Checked == false)
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

                    var dt = ObtenerDatosAnexo1(anio, mes);

                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_1_Vtas_Contribuyentes", anio, mes, "Anexo_1");
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
                    var dt = ObtenerDatosAnexo2(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_2_Vtas_Consumidor", anio, mes, "Anexo_2");
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
                    var dt = ObtenerDatosAnexo3(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_3_Compras", anio, mes, "Anexo_3");
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
                    var dt = ObtenerDatosAnexo5(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_5_Compras_Sujetos", anio, mes, "Anexo_5");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo9.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo9(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_9_Ret_1pct_AlDecl", anio, mes, "Anexo_7");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo10.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo10(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo_7_Ret_1pct_AlDecl", anio, mes, "Anexo_7");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo12.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo12(anio, mes);
                    if (dt != null)
                    {
                        ExportarExcel(dt, "Anexo12_retencion13_sujeto_excluido", anio, mes, "Anexo_12");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_AnexoAnulados.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    //var dt = ObtenerDatosAnexoAnulados(anio, mes);

                    //if (dt != null)
                    //{
                    //    RenombrarColumnasAnexoAnulados(dt);
                    //    ExportarExcel(dt, "Anexo_Anulados", anio, mes, "Anexo_Anulados");
                    //    progressBar1.Value = 100; // Excel generado
                    //    lblEstado.Text = "Completado 100%";
                    //}
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

                if (ck_Anexo1.Checked == false && ck_Anexo2.Checked == false && ck_Anexo3.Checked == false && ck_Anexo5.Checked == false
                    && ck_Anexo9.Checked == false && ck_Anexo10.Checked == false && ck_Anexo12.Checked == false && ck_AnexoAnulados.Checked == false)
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
                    var dt = ObtenerDatosAnexo1(anio, mes);

                    if (dt != null)
                    {
                      
                        ExportarCsv(dt, "Anexo_1_Vtas_Contribuyentes", anio, mes, "Anexo_1");
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
                    var dt = ObtenerDatosAnexo2(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_2_Vtas_Consumidor", anio, mes, "Anexo_2");
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
                    var dt = ObtenerDatosAnexo3(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_3_Compras", anio, mes, "Anexo_3");
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
                    var dt = ObtenerDatosAnexo5(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_5_Compras_Sujetos", anio, mes, "Anexo_5");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo9.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo9(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_9_Ret_1pct_AlDecl", anio, mes, "Anexo_7");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo10.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo10(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo_7_Ret_1pct_AlDecl", anio, mes, "Anexo_7");
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_Anexo12.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    var dt = ObtenerDatosAnexo12(anio, mes);
                    if (dt != null)
                    {
                        ExportarCsv(dt, "Anexo12_retencion13_sujeto_excluido", anio, mes, "Anexo_12");
                        progressBar1.Value = 100; // Excel generado
                        progressBar1.Value = 100; // Excel generado
                        lblEstado.Text = "Completado 100%";
                    }
                }

                if (ck_AnexoAnulados.Checked)
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 65;
                    lblEstado.Visible = true;
                    lblEstado.Text = "Procesando... 65%";
                    Application.DoEvents();
                    //var dt = ObtenerDatosAnexoAnulados(anio, mes);

                    //if (dt != null)
                    //{
                    //    RenombrarColumnasAnexoAnulados(dt);
                    //    ExportarExcel(dt, "Anexo_Anulados", anio, mes, "Anexo_Anulados");
                    //    progressBar1.Value = 100; // Excel generado
                    //    lblEstado.Text = "Completado 100%";
                    //}
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
            string nombreMes = ObtenerMesEspanol(cb_Mes.SelectedValue.ToString());
            string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string rutaBase = Path.Combine(escritorio, "AnexosMH", "Injiboa", "F07", txt_Anio.Text, nombreMes);

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

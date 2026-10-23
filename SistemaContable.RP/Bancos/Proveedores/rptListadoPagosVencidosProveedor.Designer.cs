namespace SistemaContable.RP.Bancos.Proveedores
{
    partial class rptListadoPagosVencidosProveedor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.pageInfoNumero = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pageInfoFecha = new DevExpress.XtraReports.UI.XRPageInfo();
            this.lblEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTITULO = new DevExpress.XtraReports.UI.XRLabel();
            this.xrFECHA = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lineEncabezadoArriba = new DevExpress.XtraReports.UI.XRLine();
            this.hTipo = new DevExpress.XtraReports.UI.XRLabel();
            this.hQuedan = new DevExpress.XtraReports.UI.XRLabel();
            this.hVence = new DevExpress.XtraReports.UI.XRLabel();
            this.hSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.hSinVencer = new DevExpress.XtraReports.UI.XRLabel();
            this.hD1_30 = new DevExpress.XtraReports.UI.XRLabel();
            this.hDias = new DevExpress.XtraReports.UI.XRLabel();
            this.lineEncabezadoAbajo = new DevExpress.XtraReports.UI.XRLine();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblCodigoEntidad = new DevExpress.XtraReports.UI.XRLabel();
            this.lblProveedor = new DevExpress.XtraReports.UI.XRLabel();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.dTipo = new DevExpress.XtraReports.UI.XRLabel();
            this.dComprobante = new DevExpress.XtraReports.UI.XRLabel();
            this.dQuedan = new DevExpress.XtraReports.UI.XRLabel();
            this.dVence = new DevExpress.XtraReports.UI.XRLabel();
            this.dSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.dSinVencer = new DevExpress.XtraReports.UI.XRLabel();
            this.dD1_30 = new DevExpress.XtraReports.UI.XRLabel();
            this.dDias = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.lblTotalProveedor = new DevExpress.XtraReports.UI.XRLabel();
            this.gfSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.gfSinVencer = new DevExpress.XtraReports.UI.XRLabel();
            this.gfD1_30 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTotalGeneral = new DevExpress.XtraReports.UI.XRLabel();
            this.rfSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.rfSinVencer = new DevExpress.XtraReports.UI.XRLabel();
            this.rfD1_30 = new DevExpress.XtraReports.UI.XRLabel();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.DsPagosVencidosProveedor = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            this.prmFechaCorte = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.DsPagosVencidosProveedor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 30F;
            this.TopMargin.Name = "TopMargin";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pageInfoNumero,
            this.pageInfoFecha,
            this.lblEmpresa,
            this.xrTITULO,
            this.xrFECHA});
            this.ReportHeader.HeightF = 118.4348F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // pageInfoNumero
            // 
            this.pageInfoNumero.Font = new System.Drawing.Font("Arial", 7F);
            this.pageInfoNumero.LocationFloat = new DevExpress.Utils.PointFloat(600F, 33F);
            this.pageInfoNumero.Name = "pageInfoNumero";
            this.pageInfoNumero.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.pageInfoNumero.SizeF = new System.Drawing.SizeF(200F, 18F);
            this.pageInfoNumero.StylePriority.UseFont = false;
            this.pageInfoNumero.StylePriority.UsePadding = false;
            this.pageInfoNumero.StylePriority.UseTextAlignment = false;
            this.pageInfoNumero.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.pageInfoNumero.TextFormatString = "Página {0} de {1}";
            // 
            // pageInfoFecha
            // 
            this.pageInfoFecha.Font = new System.Drawing.Font("Arial", 7F);
            this.pageInfoFecha.LocationFloat = new DevExpress.Utils.PointFloat(500F, 15F);
            this.pageInfoFecha.Name = "pageInfoFecha";
            this.pageInfoFecha.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.pageInfoFecha.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.pageInfoFecha.SizeF = new System.Drawing.SizeF(300F, 18F);
            this.pageInfoFecha.StylePriority.UseFont = false;
            this.pageInfoFecha.StylePriority.UsePadding = false;
            this.pageInfoFecha.StylePriority.UseTextAlignment = false;
            this.pageInfoFecha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.pageInfoFecha.TextFormatString = "Impreso: {0:dd/MM/yyyy hh:mm tt}";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.CanGrow = false;
            this.lblEmpresa.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 57.08697F);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblEmpresa.SizeF = new System.Drawing.SizeF(800F, 28F);
            this.lblEmpresa.StylePriority.UseFont = false;
            this.lblEmpresa.StylePriority.UseTextAlignment = false;
            this.lblEmpresa.Text = "INGENIO CENTRAL AZUCARERO JIBOA, S.A. DE C.V.";
            this.lblEmpresa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblEmpresa.WordWrap = false;
            // 
            // xrTITULO
            // 
            this.xrTITULO.CanGrow = false;
            this.xrTITULO.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTITULO.LocationFloat = new DevExpress.Utils.PointFloat(220F, 88.08697F);
            this.xrTITULO.Name = "xrTITULO";
            this.xrTITULO.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 5, 0, 0, 100F);
            this.xrTITULO.SizeF = new System.Drawing.SizeF(265F, 24F);
            this.xrTITULO.StylePriority.UseFont = false;
            this.xrTITULO.StylePriority.UsePadding = false;
            this.xrTITULO.StylePriority.UseTextAlignment = false;
            this.xrTITULO.Text = "Reporte de Pagos Vencidos al";
            this.xrTITULO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTITULO.WordWrap = false;
            // 
            // xrFECHA
            // 
            this.xrFECHA.CanGrow = false;
            this.xrFECHA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrFECHA.LocationFloat = new DevExpress.Utils.PointFloat(485F, 88.08697F);
            this.xrFECHA.Name = "xrFECHA";
            this.xrFECHA.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrFECHA.SizeF = new System.Drawing.SizeF(100F, 24F);
            this.xrFECHA.StylePriority.UseFont = false;
            this.xrFECHA.StylePriority.UseTextAlignment = false;
            this.xrFECHA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrFECHA.TextFormatString = "{0:dd/MM/yyyy}";
            this.xrFECHA.WordWrap = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lineEncabezadoArriba,
            this.hTipo,
            this.hQuedan,
            this.hVence,
            this.hSaldo,
            this.hSinVencer,
            this.hD1_30,
            this.hDias,
            this.lineEncabezadoAbajo});
            this.PageHeader.HeightF = 31F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lineEncabezadoArriba
            // 
            this.lineEncabezadoArriba.LineWidth = 2F;
            this.lineEncabezadoArriba.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lineEncabezadoArriba.Name = "lineEncabezadoArriba";
            this.lineEncabezadoArriba.SizeF = new System.Drawing.SizeF(800F, 4F);
            // 
            // hTipo
            // 
            this.hTipo.CanGrow = false;
            this.hTipo.Font = new System.Drawing.Font("Arial", 8F);
            this.hTipo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.hTipo.Name = "hTipo";
            this.hTipo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hTipo.SizeF = new System.Drawing.SizeF(190F, 20F);
            this.hTipo.StylePriority.UseFont = false;
            this.hTipo.StylePriority.UseTextAlignment = false;
            this.hTipo.Text = "COMPROBANTE";
            this.hTipo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hTipo.WordWrap = false;
            // 
            // hQuedan
            // 
            this.hQuedan.CanGrow = false;
            this.hQuedan.Font = new System.Drawing.Font("Arial", 8F);
            this.hQuedan.LocationFloat = new DevExpress.Utils.PointFloat(190F, 6F);
            this.hQuedan.Name = "hQuedan";
            this.hQuedan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hQuedan.SizeF = new System.Drawing.SizeF(75F, 20F);
            this.hQuedan.StylePriority.UseFont = false;
            this.hQuedan.StylePriority.UseTextAlignment = false;
            this.hQuedan.Text = "QUEDAN";
            this.hQuedan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hQuedan.WordWrap = false;
            // 
            // hVence
            // 
            this.hVence.CanGrow = false;
            this.hVence.Font = new System.Drawing.Font("Arial", 8F);
            this.hVence.LocationFloat = new DevExpress.Utils.PointFloat(265F, 6F);
            this.hVence.Name = "hVence";
            this.hVence.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hVence.SizeF = new System.Drawing.SizeF(90F, 20F);
            this.hVence.StylePriority.UseFont = false;
            this.hVence.StylePriority.UseTextAlignment = false;
            this.hVence.Text = "VENCE";
            this.hVence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hVence.WordWrap = false;
            // 
            // hSaldo
            // 
            this.hSaldo.CanGrow = false;
            this.hSaldo.Font = new System.Drawing.Font("Arial", 8F);
            this.hSaldo.LocationFloat = new DevExpress.Utils.PointFloat(355F, 6F);
            this.hSaldo.Name = "hSaldo";
            this.hSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hSaldo.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.hSaldo.StylePriority.UseFont = false;
            this.hSaldo.StylePriority.UseTextAlignment = false;
            this.hSaldo.Text = "SALDO";
            this.hSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSaldo.WordWrap = false;
            // 
            // hSinVencer
            // 
            this.hSinVencer.CanGrow = false;
            this.hSinVencer.Font = new System.Drawing.Font("Arial", 8F);
            this.hSinVencer.LocationFloat = new DevExpress.Utils.PointFloat(455F, 6F);
            this.hSinVencer.Name = "hSinVencer";
            this.hSinVencer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hSinVencer.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.hSinVencer.StylePriority.UseFont = false;
            this.hSinVencer.StylePriority.UseTextAlignment = false;
            this.hSinVencer.Text = "SIN VENCER";
            this.hSinVencer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSinVencer.WordWrap = false;
            // 
            // hD1_30
            // 
            this.hD1_30.CanGrow = false;
            this.hD1_30.Font = new System.Drawing.Font("Arial", 8F);
            this.hD1_30.LocationFloat = new DevExpress.Utils.PointFloat(565F, 6F);
            this.hD1_30.Name = "hD1_30";
            this.hD1_30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hD1_30.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.hD1_30.StylePriority.UseFont = false;
            this.hD1_30.StylePriority.UseTextAlignment = false;
            this.hD1_30.Text = "1 <=> 30";
            this.hD1_30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hD1_30.WordWrap = false;
            // 
            // hDias
            // 
            this.hDias.CanGrow = false;
            this.hDias.Font = new System.Drawing.Font("Arial", 8F);
            this.hDias.LocationFloat = new DevExpress.Utils.PointFloat(675F, 6F);
            this.hDias.Name = "hDias";
            this.hDias.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hDias.SizeF = new System.Drawing.SizeF(125F, 20F);
            this.hDias.StylePriority.UseFont = false;
            this.hDias.StylePriority.UseTextAlignment = false;
            this.hDias.Text = "DIAS";
            this.hDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hDias.WordWrap = false;
            // 
            // lineEncabezadoAbajo
            // 
            this.lineEncabezadoAbajo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 27F);
            this.lineEncabezadoAbajo.Name = "lineEncabezadoAbajo";
            this.lineEncabezadoAbajo.SizeF = new System.Drawing.SizeF(800F, 4F);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblCodigoEntidad,
            this.lblProveedor});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CODIGO", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 24.00001F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // lblCodigoEntidad
            // 
            this.lblCodigoEntidad.CanGrow = false;
            this.lblCodigoEntidad.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblCodigoEntidad.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4F);
            this.lblCodigoEntidad.Name = "lblCodigoEntidad";
            this.lblCodigoEntidad.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCodigoEntidad.SizeF = new System.Drawing.SizeF(130F, 20F);
            this.lblCodigoEntidad.StylePriority.UseFont = false;
            this.lblCodigoEntidad.StylePriority.UseTextAlignment = false;
            this.lblCodigoEntidad.Text = "[CODIGO]";
            this.lblCodigoEntidad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblCodigoEntidad.WordWrap = false;
            // 
            // lblProveedor
            // 
            this.lblProveedor.CanGrow = false;
            this.lblProveedor.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PROVEEDOR]")});
            this.lblProveedor.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblProveedor.LocationFloat = new DevExpress.Utils.PointFloat(135F, 4.000005F);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblProveedor.SizeF = new System.Drawing.SizeF(665F, 20F);
            this.lblProveedor.StylePriority.UseFont = false;
            this.lblProveedor.StylePriority.UseTextAlignment = false;
            this.lblProveedor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblProveedor.WordWrap = false;
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.dTipo,
            this.dComprobante,
            this.dQuedan,
            this.dVence,
            this.dSaldo,
            this.dSinVencer,
            this.dD1_30,
            this.dDias});
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("VENCE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            // 
            // dTipo
            // 
            this.dTipo.CanGrow = false;
            this.dTipo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TIPO]")});
            this.dTipo.Font = new System.Drawing.Font("Arial", 8F);
            this.dTipo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.dTipo.Name = "dTipo";
            this.dTipo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dTipo.SizeF = new System.Drawing.SizeF(55F, 18F);
            this.dTipo.StylePriority.UseFont = false;
            this.dTipo.StylePriority.UseTextAlignment = false;
            this.dTipo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dTipo.WordWrap = false;
            // 
            // dComprobante
            // 
            this.dComprobante.CanGrow = false;
            this.dComprobante.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[COMPROBANTE]")});
            this.dComprobante.Font = new System.Drawing.Font("Arial", 8F);
            this.dComprobante.LocationFloat = new DevExpress.Utils.PointFloat(55F, 0F);
            this.dComprobante.Name = "dComprobante";
            this.dComprobante.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dComprobante.SizeF = new System.Drawing.SizeF(135F, 18F);
            this.dComprobante.StylePriority.UseFont = false;
            this.dComprobante.StylePriority.UseTextAlignment = false;
            this.dComprobante.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dComprobante.WordWrap = false;
            // 
            // dQuedan
            // 
            this.dQuedan.CanGrow = false;
            this.dQuedan.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[QUEDAN]")});
            this.dQuedan.Font = new System.Drawing.Font("Arial", 8F);
            this.dQuedan.LocationFloat = new DevExpress.Utils.PointFloat(190F, 0F);
            this.dQuedan.Name = "dQuedan";
            this.dQuedan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dQuedan.SizeF = new System.Drawing.SizeF(75F, 18F);
            this.dQuedan.StylePriority.UseFont = false;
            this.dQuedan.StylePriority.UseTextAlignment = false;
            this.dQuedan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dQuedan.WordWrap = false;
            // 
            // dVence
            // 
            this.dVence.CanGrow = false;
            this.dVence.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[VENCE]")});
            this.dVence.Font = new System.Drawing.Font("Arial", 8F);
            this.dVence.LocationFloat = new DevExpress.Utils.PointFloat(265F, 0F);
            this.dVence.Name = "dVence";
            this.dVence.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dVence.SizeF = new System.Drawing.SizeF(90F, 18F);
            this.dVence.StylePriority.UseFont = false;
            this.dVence.StylePriority.UseTextAlignment = false;
            this.dVence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dVence.TextFormatString = "{0:dd/MM/yyyy}";
            this.dVence.WordWrap = false;
            // 
            // dSaldo
            // 
            this.dSaldo.CanGrow = false;
            this.dSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SALDO]")});
            this.dSaldo.Font = new System.Drawing.Font("Arial", 8F);
            this.dSaldo.LocationFloat = new DevExpress.Utils.PointFloat(355F, 0F);
            this.dSaldo.Name = "dSaldo";
            this.dSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dSaldo.SizeF = new System.Drawing.SizeF(100F, 18F);
            this.dSaldo.StylePriority.UseFont = false;
            this.dSaldo.StylePriority.UseTextAlignment = false;
            this.dSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dSaldo.WordWrap = false;
            // 
            // dSinVencer
            // 
            this.dSinVencer.CanGrow = false;
            this.dSinVencer.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIN VENCER]")});
            this.dSinVencer.Font = new System.Drawing.Font("Arial", 8F);
            this.dSinVencer.LocationFloat = new DevExpress.Utils.PointFloat(455F, 0F);
            this.dSinVencer.Name = "dSinVencer";
            this.dSinVencer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dSinVencer.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.dSinVencer.StylePriority.UseFont = false;
            this.dSinVencer.StylePriority.UseTextAlignment = false;
            this.dSinVencer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSinVencer.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dSinVencer.WordWrap = false;
            // 
            // dD1_30
            // 
            this.dD1_30.CanGrow = false;
            this.dD1_30.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PERIODO]")});
            this.dD1_30.Font = new System.Drawing.Font("Arial", 8F);
            this.dD1_30.LocationFloat = new DevExpress.Utils.PointFloat(565F, 0F);
            this.dD1_30.Name = "dD1_30";
            this.dD1_30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dD1_30.SizeF = new System.Drawing.SizeF(110F, 18F);
            this.dD1_30.StylePriority.UseFont = false;
            this.dD1_30.StylePriority.UseTextAlignment = false;
            this.dD1_30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dD1_30.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dD1_30.WordWrap = false;
            // 
            // dDias
            // 
            this.dDias.CanGrow = false;
            this.dDias.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[DIAS]")});
            this.dDias.Font = new System.Drawing.Font("Arial", 8F);
            this.dDias.LocationFloat = new DevExpress.Utils.PointFloat(675F, 0F);
            this.dDias.Name = "dDias";
            this.dDias.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dDias.SizeF = new System.Drawing.SizeF(125F, 18F);
            this.dDias.StylePriority.UseFont = false;
            this.dDias.StylePriority.UseTextAlignment = false;
            this.dDias.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.dDias.TextFormatString = "{0:#,##0;(#,##0);0}";
            this.dDias.WordWrap = false;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotalProveedor,
            this.gfSaldo,
            this.gfSinVencer,
            this.gfD1_30});
            this.GroupFooter1.HeightF = 26F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // lblTotalProveedor
            // 
            this.lblTotalProveedor.CanGrow = false;
            this.lblTotalProveedor.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lblTotalProveedor.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.lblTotalProveedor.Name = "lblTotalProveedor";
            this.lblTotalProveedor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTotalProveedor.SizeF = new System.Drawing.SizeF(355F, 20F);
            this.lblTotalProveedor.StylePriority.UseFont = false;
            this.lblTotalProveedor.StylePriority.UseTextAlignment = false;
            this.lblTotalProveedor.Text = "Total proveedor";
            this.lblTotalProveedor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblTotalProveedor.WordWrap = false;
            // 
            // gfSaldo
            // 
            this.gfSaldo.CanGrow = false;
            this.gfSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SALDO])")});
            this.gfSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfSaldo.LocationFloat = new DevExpress.Utils.PointFloat(355F, 2F);
            this.gfSaldo.Name = "gfSaldo";
            this.gfSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfSaldo.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.gfSaldo.StylePriority.UseFont = false;
            this.gfSaldo.StylePriority.UseTextAlignment = false;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.gfSaldo.Summary = xrSummary1;
            this.gfSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfSaldo.WordWrap = false;
            // 
            // gfSinVencer
            // 
            this.gfSinVencer.CanGrow = false;
            this.gfSinVencer.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SIN VENCER])")});
            this.gfSinVencer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfSinVencer.LocationFloat = new DevExpress.Utils.PointFloat(455F, 2F);
            this.gfSinVencer.Name = "gfSinVencer";
            this.gfSinVencer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfSinVencer.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.gfSinVencer.StylePriority.UseFont = false;
            this.gfSinVencer.StylePriority.UseTextAlignment = false;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.gfSinVencer.Summary = xrSummary2;
            this.gfSinVencer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfSinVencer.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfSinVencer.WordWrap = false;
            // 
            // gfD1_30
            // 
            this.gfD1_30.CanGrow = false;
            this.gfD1_30.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([PERIODO])")});
            this.gfD1_30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfD1_30.LocationFloat = new DevExpress.Utils.PointFloat(565F, 2F);
            this.gfD1_30.Name = "gfD1_30";
            this.gfD1_30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfD1_30.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.gfD1_30.StylePriority.UseFont = false;
            this.gfD1_30.StylePriority.UseTextAlignment = false;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.gfD1_30.Summary = xrSummary3;
            this.gfD1_30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfD1_30.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfD1_30.WordWrap = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotalGeneral,
            this.rfSaldo,
            this.rfSinVencer,
            this.rfD1_30});
            this.ReportFooter.HeightF = 34F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblTotalGeneral
            // 
            this.lblTotalGeneral.CanGrow = false;
            this.lblTotalGeneral.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalGeneral.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.lblTotalGeneral.Name = "lblTotalGeneral";
            this.lblTotalGeneral.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTotalGeneral.SizeF = new System.Drawing.SizeF(355F, 20F);
            this.lblTotalGeneral.StylePriority.UseFont = false;
            this.lblTotalGeneral.StylePriority.UseTextAlignment = false;
            this.lblTotalGeneral.Text = "TOTAL GENERAL";
            this.lblTotalGeneral.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblTotalGeneral.WordWrap = false;
            // 
            // rfSaldo
            // 
            this.rfSaldo.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfSaldo.CanGrow = false;
            this.rfSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SALDO])")});
            this.rfSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfSaldo.LocationFloat = new DevExpress.Utils.PointFloat(355F, 6F);
            this.rfSaldo.Name = "rfSaldo";
            this.rfSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfSaldo.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.rfSaldo.StylePriority.UseFont = false;
            this.rfSaldo.StylePriority.UseTextAlignment = false;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.rfSaldo.Summary = xrSummary4;
            this.rfSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfSaldo.WordWrap = false;
            // 
            // rfSinVencer
            // 
            this.rfSinVencer.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfSinVencer.CanGrow = false;
            this.rfSinVencer.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SIN VENCER])")});
            this.rfSinVencer.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfSinVencer.LocationFloat = new DevExpress.Utils.PointFloat(455F, 6F);
            this.rfSinVencer.Name = "rfSinVencer";
            this.rfSinVencer.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfSinVencer.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.rfSinVencer.StylePriority.UseFont = false;
            this.rfSinVencer.StylePriority.UseTextAlignment = false;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.rfSinVencer.Summary = xrSummary5;
            this.rfSinVencer.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfSinVencer.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfSinVencer.WordWrap = false;
            // 
            // rfD1_30
            // 
            this.rfD1_30.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfD1_30.CanGrow = false;
            this.rfD1_30.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([PERIODO])")});
            this.rfD1_30.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfD1_30.LocationFloat = new DevExpress.Utils.PointFloat(565F, 6F);
            this.rfD1_30.Name = "rfD1_30";
            this.rfD1_30.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfD1_30.SizeF = new System.Drawing.SizeF(110F, 20F);
            this.rfD1_30.StylePriority.UseFont = false;
            this.rfD1_30.StylePriority.UseTextAlignment = false;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.rfD1_30.Summary = xrSummary6;
            this.rfD1_30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfD1_30.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfD1_30.WordWrap = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // DsPagosVencidosProveedor
            // 
            this.DsPagosVencidosProveedor.DataMember = "Crear";
            this.DsPagosVencidosProveedor.DataSource = typeof(SistemaContable.RP.Esquemas.ListadoPagosVencidosProveedorEsquema);
            this.DsPagosVencidosProveedor.Name = "DsPagosVencidosProveedor";
            // 
            // prmFechaCorte
            // 
            this.prmFechaCorte.Description = "Fecha de corte";
            this.prmFechaCorte.Name = "prmFechaCorte";
            this.prmFechaCorte.Type = typeof(System.DateTime);
            this.prmFechaCorte.Visible = false;
            // 
            // rptListadoPagosVencidosProveedor
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.ReportHeader,
            this.PageHeader,
            this.GroupHeader1,
            this.Detail,
            this.GroupFooter1,
            this.ReportFooter,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.DsPagosVencidosProveedor});
            this.DataSource = this.DsPagosVencidosProveedor;
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 30, 40);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.prmFechaCorte});
            this.RequestParameters = false;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.DsPagosVencidosProveedor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel lblEmpresa;
        private DevExpress.XtraReports.UI.XRLabel xrTITULO;
        private DevExpress.XtraReports.UI.XRLabel xrFECHA;
        private DevExpress.XtraReports.UI.XRLine lineEncabezadoArriba;
        private DevExpress.XtraReports.UI.XRLabel hTipo;
        private DevExpress.XtraReports.UI.XRLabel hQuedan;
        private DevExpress.XtraReports.UI.XRLabel hVence;
        private DevExpress.XtraReports.UI.XRLabel hSaldo;
        private DevExpress.XtraReports.UI.XRLabel hSinVencer;
        private DevExpress.XtraReports.UI.XRLabel hD1_30;
        private DevExpress.XtraReports.UI.XRLabel hDias;
        private DevExpress.XtraReports.UI.XRLine lineEncabezadoAbajo;
        private DevExpress.XtraReports.UI.XRLabel lblCodigoEntidad;
        private DevExpress.XtraReports.UI.XRLabel lblProveedor;
        private DevExpress.XtraReports.UI.XRLabel dTipo;
        private DevExpress.XtraReports.UI.XRLabel dComprobante;
        private DevExpress.XtraReports.UI.XRLabel dQuedan;
        private DevExpress.XtraReports.UI.XRLabel dVence;
        private DevExpress.XtraReports.UI.XRLabel dSaldo;
        private DevExpress.XtraReports.UI.XRLabel dSinVencer;
        private DevExpress.XtraReports.UI.XRLabel dD1_30;
        private DevExpress.XtraReports.UI.XRLabel dDias;
        private DevExpress.XtraReports.UI.XRLabel lblTotalProveedor;
        private DevExpress.XtraReports.UI.XRLabel gfSaldo;
        private DevExpress.XtraReports.UI.XRLabel gfSinVencer;
        private DevExpress.XtraReports.UI.XRLabel gfD1_30;
        private DevExpress.XtraReports.UI.XRLabel lblTotalGeneral;
        private DevExpress.XtraReports.UI.XRLabel rfSaldo;
        private DevExpress.XtraReports.UI.XRLabel rfSinVencer;
        private DevExpress.XtraReports.UI.XRLabel rfD1_30;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoFecha;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoNumero;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource DsPagosVencidosProveedor;
        private DevExpress.XtraReports.Parameters.Parameter prmFechaCorte;
    }
}

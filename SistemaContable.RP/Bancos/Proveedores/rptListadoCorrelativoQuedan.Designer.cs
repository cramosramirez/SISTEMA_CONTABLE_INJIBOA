namespace SistemaContable.RP.Bancos.Proveedores
{
    partial class rptListadoCorrelativoQuedan
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
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lblEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTITULO = new DevExpress.XtraReports.UI.XRLabel();
            this.lineEncabezadoArriba = new DevExpress.XtraReports.UI.XRLine();
            this.hQuedan = new DevExpress.XtraReports.UI.XRLabel();
            this.hNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.hTipoDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.hRecibido = new DevExpress.XtraReports.UI.XRLabel();
            this.hVence = new DevExpress.XtraReports.UI.XRLabel();
            this.hOriginal = new DevExpress.XtraReports.UI.XRLabel();
            this.hAbono = new DevExpress.XtraReports.UI.XRLabel();
            this.hSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.hDiasV = new DevExpress.XtraReports.UI.XRLabel();
            this.hSinV = new DevExpress.XtraReports.UI.XRLabel();
            this.lineEncabezadoAbajo = new DevExpress.XtraReports.UI.XRLine();
            this.lblQuedan = new DevExpress.XtraReports.UI.XRLabel();
            this.dQuedan = new DevExpress.XtraReports.UI.XRLabel();
            this.dNombre = new DevExpress.XtraReports.UI.XRLabel();
            this.dTipoDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.dNumDoc = new DevExpress.XtraReports.UI.XRLabel();
            this.dRecibido = new DevExpress.XtraReports.UI.XRLabel();
            this.dVence = new DevExpress.XtraReports.UI.XRLabel();
            this.dOriginal = new DevExpress.XtraReports.UI.XRLabel();
            this.dAbono = new DevExpress.XtraReports.UI.XRLabel();
            this.dSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.dDiasV = new DevExpress.XtraReports.UI.XRLabel();
            this.dSinV = new DevExpress.XtraReports.UI.XRLabel();
            this.gfOriginal = new DevExpress.XtraReports.UI.XRLabel();
            this.gfAbono = new DevExpress.XtraReports.UI.XRLabel();
            this.gfSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.gfSinV = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTotalGeneral = new DevExpress.XtraReports.UI.XRLabel();
            this.rfOriginal = new DevExpress.XtraReports.UI.XRLabel();
            this.rfAbono = new DevExpress.XtraReports.UI.XRLabel();
            this.rfSaldo = new DevExpress.XtraReports.UI.XRLabel();
            this.rfSinV = new DevExpress.XtraReports.UI.XRLabel();
            this.pageInfoFecha = new DevExpress.XtraReports.UI.XRPageInfo();
            this.pageInfoNumero = new DevExpress.XtraReports.UI.XRPageInfo();
            this.DsCorrelativoQuedan = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DsCorrelativoQuedan)).BeginInit();
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
            this.lblEmpresa,
            this.xrTITULO});
            this.ReportHeader.HeightF = 68F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lineEncabezadoArriba,
            this.hQuedan,
            this.hNombre,
            this.hTipoDoc,
            this.hRecibido,
            this.hVence,
            this.hOriginal,
            this.hAbono,
            this.hSaldo,
            this.hDiasV,
            this.hSinV,
            this.lineEncabezadoAbajo});
            this.PageHeader.HeightF = 32F;
            this.PageHeader.Name = "PageHeader";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblQuedan});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("QUEDAN", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 24F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.dQuedan,
            this.dNombre,
            this.dTipoDoc,
            this.dNumDoc,
            this.dRecibido,
            this.dVence,
            this.dOriginal,
            this.dAbono,
            this.dSaldo,
            this.dDiasV,
            this.dSinV});
            this.Detail.HeightF = 18F;
            this.Detail.Name = "Detail";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.gfOriginal,
            this.gfAbono,
            this.gfSaldo,
            this.gfSinV});
            this.GroupFooter1.HeightF = 26F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTotalGeneral,
            this.rfOriginal,
            this.rfAbono,
            this.rfSaldo,
            this.rfSinV});
            this.ReportFooter.HeightF = 34F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pageInfoFecha,
            this.pageInfoNumero});
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.CanGrow = false;
            this.lblEmpresa.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
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
            this.xrTITULO.LocationFloat = new DevExpress.Utils.PointFloat(0F, 36F);
            this.xrTITULO.Name = "xrTITULO";
            this.xrTITULO.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTITULO.SizeF = new System.Drawing.SizeF(800F, 24F);
            this.xrTITULO.StylePriority.UseFont = false;
            this.xrTITULO.StylePriority.UseTextAlignment = false;
            this.xrTITULO.Text = "Listado Correlativo de Quedan";
            this.xrTITULO.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTITULO.WordWrap = false;
            // 
            // lineEncabezadoArriba
            // 
            this.lineEncabezadoArriba.LineWidth = 2F;
            this.lineEncabezadoArriba.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lineEncabezadoArriba.Name = "lineEncabezadoArriba";
            this.lineEncabezadoArriba.SizeF = new System.Drawing.SizeF(800F, 4F);
            // 
            // hQuedan
            // 
            this.hQuedan.CanGrow = false;
            this.hQuedan.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hQuedan.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.hQuedan.Name = "hQuedan";
            this.hQuedan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hQuedan.SizeF = new System.Drawing.SizeF(60F, 20F);
            this.hQuedan.StylePriority.UseFont = false;
            this.hQuedan.StylePriority.UseTextAlignment = false;
            this.hQuedan.Text = "QUEDAN";
            this.hQuedan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hQuedan.WordWrap = false;
            // 
            // hNombre
            // 
            this.hNombre.CanGrow = false;
            this.hNombre.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hNombre.LocationFloat = new DevExpress.Utils.PointFloat(60F, 6F);
            this.hNombre.Name = "hNombre";
            this.hNombre.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hNombre.SizeF = new System.Drawing.SizeF(175F, 20F);
            this.hNombre.StylePriority.UseFont = false;
            this.hNombre.StylePriority.UseTextAlignment = false;
            this.hNombre.Text = "NOMBRE";
            this.hNombre.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hNombre.WordWrap = false;
            // 
            // hTipoDoc
            // 
            this.hTipoDoc.CanGrow = false;
            this.hTipoDoc.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hTipoDoc.LocationFloat = new DevExpress.Utils.PointFloat(235F, 6F);
            this.hTipoDoc.Name = "hTipoDoc";
            this.hTipoDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hTipoDoc.SizeF = new System.Drawing.SizeF(95F, 20F);
            this.hTipoDoc.StylePriority.UseFont = false;
            this.hTipoDoc.StylePriority.UseTextAlignment = false;
            this.hTipoDoc.Text = "COMPROBANTE";
            this.hTipoDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hTipoDoc.WordWrap = false;
            // 
            // hRecibido
            // 
            this.hRecibido.CanGrow = false;
            this.hRecibido.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hRecibido.LocationFloat = new DevExpress.Utils.PointFloat(330F, 6F);
            this.hRecibido.Name = "hRecibido";
            this.hRecibido.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hRecibido.SizeF = new System.Drawing.SizeF(65F, 20F);
            this.hRecibido.StylePriority.UseFont = false;
            this.hRecibido.StylePriority.UseTextAlignment = false;
            this.hRecibido.Text = "RECIBIDO";
            this.hRecibido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hRecibido.WordWrap = false;
            // 
            // hVence
            // 
            this.hVence.CanGrow = false;
            this.hVence.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hVence.LocationFloat = new DevExpress.Utils.PointFloat(395F, 6F);
            this.hVence.Name = "hVence";
            this.hVence.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hVence.SizeF = new System.Drawing.SizeF(65F, 20F);
            this.hVence.StylePriority.UseFont = false;
            this.hVence.StylePriority.UseTextAlignment = false;
            this.hVence.Text = "VENCE";
            this.hVence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.hVence.WordWrap = false;
            // 
            // hOriginal
            // 
            this.hOriginal.CanGrow = false;
            this.hOriginal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hOriginal.LocationFloat = new DevExpress.Utils.PointFloat(460F, 6F);
            this.hOriginal.Name = "hOriginal";
            this.hOriginal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hOriginal.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.hOriginal.StylePriority.UseFont = false;
            this.hOriginal.StylePriority.UseTextAlignment = false;
            this.hOriginal.Text = "ORIG.";
            this.hOriginal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hOriginal.WordWrap = false;
            // 
            // hAbono
            // 
            this.hAbono.CanGrow = false;
            this.hAbono.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hAbono.LocationFloat = new DevExpress.Utils.PointFloat(530F, 6F);
            this.hAbono.Name = "hAbono";
            this.hAbono.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hAbono.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.hAbono.StylePriority.UseFont = false;
            this.hAbono.StylePriority.UseTextAlignment = false;
            this.hAbono.Text = "ABONO";
            this.hAbono.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hAbono.WordWrap = false;
            // 
            // hSaldo
            // 
            this.hSaldo.CanGrow = false;
            this.hSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hSaldo.LocationFloat = new DevExpress.Utils.PointFloat(600F, 6F);
            this.hSaldo.Name = "hSaldo";
            this.hSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hSaldo.SizeF = new System.Drawing.SizeF(60F, 20F);
            this.hSaldo.StylePriority.UseFont = false;
            this.hSaldo.StylePriority.UseTextAlignment = false;
            this.hSaldo.Text = "SALDO";
            this.hSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSaldo.WordWrap = false;
            // 
            // hDiasV
            // 
            this.hDiasV.CanGrow = false;
            this.hDiasV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hDiasV.LocationFloat = new DevExpress.Utils.PointFloat(660F, 6F);
            this.hDiasV.Name = "hDiasV";
            this.hDiasV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hDiasV.SizeF = new System.Drawing.SizeF(75F, 20F);
            this.hDiasV.StylePriority.UseFont = false;
            this.hDiasV.StylePriority.UseTextAlignment = false;
            this.hDiasV.Text = "Dias V.";
            this.hDiasV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.hDiasV.WordWrap = false;
            // 
            // hSinV
            // 
            this.hSinV.CanGrow = false;
            this.hSinV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.hSinV.LocationFloat = new DevExpress.Utils.PointFloat(735F, 6F);
            this.hSinV.Name = "hSinV";
            this.hSinV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.hSinV.SizeF = new System.Drawing.SizeF(65F, 20F);
            this.hSinV.StylePriority.UseFont = false;
            this.hSinV.StylePriority.UseTextAlignment = false;
            this.hSinV.Text = "SIN V.";
            this.hSinV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.hSinV.WordWrap = false;
            // 
            // lineEncabezadoAbajo
            // 
            this.lineEncabezadoAbajo.LineWidth = 1F;
            this.lineEncabezadoAbajo.LocationFloat = new DevExpress.Utils.PointFloat(0F, 27F);
            this.lineEncabezadoAbajo.Name = "lineEncabezadoAbajo";
            this.lineEncabezadoAbajo.SizeF = new System.Drawing.SizeF(800F, 4F);
            // 
            // lblQuedan
            // 
            this.lblQuedan.CanGrow = false;
            this.lblQuedan.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[QUEDAN]")});
            this.lblQuedan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblQuedan.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2F);
            this.lblQuedan.Name = "lblQuedan";
            this.lblQuedan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblQuedan.SizeF = new System.Drawing.SizeF(200F, 20F);
            this.lblQuedan.StylePriority.UseFont = false;
            this.lblQuedan.StylePriority.UseTextAlignment = false;
            this.lblQuedan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblQuedan.WordWrap = false;
            // 
            // dQuedan
            // 
            this.dQuedan.CanGrow = false;
            this.dQuedan.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[QUEDAN]")});
            this.dQuedan.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dQuedan.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.dQuedan.Name = "dQuedan";
            this.dQuedan.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dQuedan.SizeF = new System.Drawing.SizeF(60F, 18F);
            this.dQuedan.StylePriority.UseFont = false;
            this.dQuedan.StylePriority.UseTextAlignment = false;
            this.dQuedan.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dQuedan.WordWrap = false;
            // 
            // dNombre
            // 
            this.dNombre.CanGrow = false;
            this.dNombre.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NOMBRE]")});
            this.dNombre.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dNombre.LocationFloat = new DevExpress.Utils.PointFloat(60F, 0F);
            this.dNombre.Name = "dNombre";
            this.dNombre.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dNombre.SizeF = new System.Drawing.SizeF(175F, 18F);
            this.dNombre.StylePriority.UseFont = false;
            this.dNombre.StylePriority.UseTextAlignment = false;
            this.dNombre.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dNombre.WordWrap = false;
            // 
            // dTipoDoc
            // 
            this.dTipoDoc.CanGrow = false;
            this.dTipoDoc.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TIPO_DOC]")});
            this.dTipoDoc.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dTipoDoc.LocationFloat = new DevExpress.Utils.PointFloat(235F, 0F);
            this.dTipoDoc.Name = "dTipoDoc";
            this.dTipoDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dTipoDoc.SizeF = new System.Drawing.SizeF(35F, 18F);
            this.dTipoDoc.StylePriority.UseFont = false;
            this.dTipoDoc.StylePriority.UseTextAlignment = false;
            this.dTipoDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dTipoDoc.WordWrap = false;
            // 
            // dNumDoc
            // 
            this.dNumDoc.CanGrow = false;
            this.dNumDoc.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NUM_DOC]")});
            this.dNumDoc.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dNumDoc.LocationFloat = new DevExpress.Utils.PointFloat(270F, 0F);
            this.dNumDoc.Name = "dNumDoc";
            this.dNumDoc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dNumDoc.SizeF = new System.Drawing.SizeF(60F, 18F);
            this.dNumDoc.StylePriority.UseFont = false;
            this.dNumDoc.StylePriority.UseTextAlignment = false;
            this.dNumDoc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dNumDoc.WordWrap = false;
            // 
            // dRecibido
            // 
            this.dRecibido.CanGrow = false;
            this.dRecibido.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FECHA_RECIBIDO]")});
            this.dRecibido.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dRecibido.LocationFloat = new DevExpress.Utils.PointFloat(330F, 0F);
            this.dRecibido.Name = "dRecibido";
            this.dRecibido.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dRecibido.SizeF = new System.Drawing.SizeF(65F, 18F);
            this.dRecibido.StylePriority.UseFont = false;
            this.dRecibido.StylePriority.UseTextAlignment = false;
            this.dRecibido.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dRecibido.TextFormatString = "{0:d/M/yyyy}";
            this.dRecibido.WordWrap = false;
            // 
            // dVence
            // 
            this.dVence.CanGrow = false;
            this.dVence.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[FECHA_VENCE]")});
            this.dVence.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dVence.LocationFloat = new DevExpress.Utils.PointFloat(395F, 0F);
            this.dVence.Name = "dVence";
            this.dVence.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dVence.SizeF = new System.Drawing.SizeF(65F, 18F);
            this.dVence.StylePriority.UseFont = false;
            this.dVence.StylePriority.UseTextAlignment = false;
            this.dVence.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.dVence.TextFormatString = "{0:d/M/yyyy}";
            this.dVence.WordWrap = false;
            // 
            // dOriginal
            // 
            this.dOriginal.CanGrow = false;
            this.dOriginal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[MONTO_ORIGINAL]")});
            this.dOriginal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dOriginal.LocationFloat = new DevExpress.Utils.PointFloat(460F, 0F);
            this.dOriginal.Name = "dOriginal";
            this.dOriginal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dOriginal.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.dOriginal.StylePriority.UseFont = false;
            this.dOriginal.StylePriority.UseTextAlignment = false;
            this.dOriginal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dOriginal.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dOriginal.WordWrap = false;
            // 
            // dAbono
            // 
            this.dAbono.CanGrow = false;
            this.dAbono.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ABONO]")});
            this.dAbono.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dAbono.LocationFloat = new DevExpress.Utils.PointFloat(530F, 0F);
            this.dAbono.Name = "dAbono";
            this.dAbono.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dAbono.SizeF = new System.Drawing.SizeF(70F, 18F);
            this.dAbono.StylePriority.UseFont = false;
            this.dAbono.StylePriority.UseTextAlignment = false;
            this.dAbono.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dAbono.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dAbono.WordWrap = false;
            // 
            // dSaldo
            // 
            this.dSaldo.CanGrow = false;
            this.dSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SALDO]")});
            this.dSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dSaldo.LocationFloat = new DevExpress.Utils.PointFloat(600F, 0F);
            this.dSaldo.Name = "dSaldo";
            this.dSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dSaldo.SizeF = new System.Drawing.SizeF(60F, 18F);
            this.dSaldo.StylePriority.UseFont = false;
            this.dSaldo.StylePriority.UseTextAlignment = false;
            this.dSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dSaldo.WordWrap = false;
            // 
            // dDiasV
            // 
            this.dDiasV.CanGrow = false;
            this.dDiasV.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([SALDO] = 0, '***PAGADO***', [DIAS_VENCIDO])")});
            this.dDiasV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dDiasV.LocationFloat = new DevExpress.Utils.PointFloat(660F, 0F);
            this.dDiasV.Name = "dDiasV";
            this.dDiasV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dDiasV.SizeF = new System.Drawing.SizeF(75F, 18F);
            this.dDiasV.StylePriority.UseFont = false;
            this.dDiasV.StylePriority.UseTextAlignment = false;
            this.dDiasV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.dDiasV.WordWrap = false;
            // 
            // dSinV
            // 
            this.dSinV.CanGrow = false;
            this.dSinV.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SIN_VENCER]")});
            this.dSinV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.dSinV.LocationFloat = new DevExpress.Utils.PointFloat(735F, 0F);
            this.dSinV.Name = "dSinV";
            this.dSinV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.dSinV.SizeF = new System.Drawing.SizeF(65F, 18F);
            this.dSinV.StylePriority.UseFont = false;
            this.dSinV.StylePriority.UseTextAlignment = false;
            this.dSinV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.dSinV.TextFormatString = "{0:#,##0.00;-#,##0.00;#}";
            this.dSinV.WordWrap = false;
            // 
            // gfOriginal
            // 
            this.gfOriginal.CanGrow = false;
            this.gfOriginal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([MONTO_ORIGINAL])")});
            this.gfOriginal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfOriginal.LocationFloat = new DevExpress.Utils.PointFloat(460F, 2F);
            this.gfOriginal.Name = "gfOriginal";
            this.gfOriginal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfOriginal.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.gfOriginal.StylePriority.UseFont = false;
            this.gfOriginal.StylePriority.UseTextAlignment = false;
            this.gfOriginal.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Group);
            this.gfOriginal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfOriginal.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfOriginal.WordWrap = false;
            // 
            // gfAbono
            // 
            this.gfAbono.CanGrow = false;
            this.gfAbono.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ABONO])")});
            this.gfAbono.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfAbono.LocationFloat = new DevExpress.Utils.PointFloat(530F, 2F);
            this.gfAbono.Name = "gfAbono";
            this.gfAbono.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfAbono.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.gfAbono.StylePriority.UseFont = false;
            this.gfAbono.StylePriority.UseTextAlignment = false;
            this.gfAbono.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Group);
            this.gfAbono.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfAbono.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfAbono.WordWrap = false;
            // 
            // gfSaldo
            // 
            this.gfSaldo.CanGrow = false;
            this.gfSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SALDO])")});
            this.gfSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfSaldo.LocationFloat = new DevExpress.Utils.PointFloat(600F, 2F);
            this.gfSaldo.Name = "gfSaldo";
            this.gfSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfSaldo.SizeF = new System.Drawing.SizeF(60F, 20F);
            this.gfSaldo.StylePriority.UseFont = false;
            this.gfSaldo.StylePriority.UseTextAlignment = false;
            this.gfSaldo.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Group);
            this.gfSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfSaldo.WordWrap = false;
            // 
            // gfSinV
            // 
            this.gfSinV.CanGrow = false;
            this.gfSinV.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SIN_VENCER])")});
            this.gfSinV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.gfSinV.LocationFloat = new DevExpress.Utils.PointFloat(735F, 2F);
            this.gfSinV.Name = "gfSinV";
            this.gfSinV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.gfSinV.SizeF = new System.Drawing.SizeF(65F, 20F);
            this.gfSinV.StylePriority.UseFont = false;
            this.gfSinV.StylePriority.UseTextAlignment = false;
            this.gfSinV.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Group);
            this.gfSinV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.gfSinV.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.gfSinV.WordWrap = false;
            // 
            // lblTotalGeneral
            // 
            this.lblTotalGeneral.CanGrow = false;
            this.lblTotalGeneral.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalGeneral.LocationFloat = new DevExpress.Utils.PointFloat(0F, 6F);
            this.lblTotalGeneral.Name = "lblTotalGeneral";
            this.lblTotalGeneral.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTotalGeneral.SizeF = new System.Drawing.SizeF(460F, 20F);
            this.lblTotalGeneral.StylePriority.UseFont = false;
            this.lblTotalGeneral.StylePriority.UseTextAlignment = false;
            this.lblTotalGeneral.Text = "TOTAL GENERAL";
            this.lblTotalGeneral.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblTotalGeneral.WordWrap = false;
            // 
            // rfOriginal
            // 
            this.rfOriginal.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfOriginal.CanGrow = false;
            this.rfOriginal.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([MONTO_ORIGINAL])")});
            this.rfOriginal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfOriginal.LocationFloat = new DevExpress.Utils.PointFloat(460F, 6F);
            this.rfOriginal.Name = "rfOriginal";
            this.rfOriginal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfOriginal.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.rfOriginal.StylePriority.UseFont = false;
            this.rfOriginal.StylePriority.UseTextAlignment = false;
            this.rfOriginal.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Report);
            this.rfOriginal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfOriginal.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfOriginal.WordWrap = false;
            // 
            // rfAbono
            // 
            this.rfAbono.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfAbono.CanGrow = false;
            this.rfAbono.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([ABONO])")});
            this.rfAbono.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfAbono.LocationFloat = new DevExpress.Utils.PointFloat(530F, 6F);
            this.rfAbono.Name = "rfAbono";
            this.rfAbono.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfAbono.SizeF = new System.Drawing.SizeF(70F, 20F);
            this.rfAbono.StylePriority.UseFont = false;
            this.rfAbono.StylePriority.UseTextAlignment = false;
            this.rfAbono.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Report);
            this.rfAbono.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfAbono.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfAbono.WordWrap = false;
            // 
            // rfSaldo
            // 
            this.rfSaldo.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfSaldo.CanGrow = false;
            this.rfSaldo.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SALDO])")});
            this.rfSaldo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfSaldo.LocationFloat = new DevExpress.Utils.PointFloat(600F, 6F);
            this.rfSaldo.Name = "rfSaldo";
            this.rfSaldo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfSaldo.SizeF = new System.Drawing.SizeF(60F, 20F);
            this.rfSaldo.StylePriority.UseFont = false;
            this.rfSaldo.StylePriority.UseTextAlignment = false;
            this.rfSaldo.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Report);
            this.rfSaldo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfSaldo.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfSaldo.WordWrap = false;
            // 
            // rfSinV
            // 
            this.rfSinV.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rfSinV.CanGrow = false;
            this.rfSinV.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SIN_VENCER])")});
            this.rfSinV.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.rfSinV.LocationFloat = new DevExpress.Utils.PointFloat(735F, 6F);
            this.rfSinV.Name = "rfSinV";
            this.rfSinV.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rfSinV.SizeF = new System.Drawing.SizeF(65F, 20F);
            this.rfSinV.StylePriority.UseFont = false;
            this.rfSinV.StylePriority.UseTextAlignment = false;
            this.rfSinV.Summary = new DevExpress.XtraReports.UI.XRSummary(DevExpress.XtraReports.UI.SummaryRunning.Report);
            this.rfSinV.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rfSinV.TextFormatString = "{0:#,##0.00;-#,##0.00;0.00}";
            this.rfSinV.WordWrap = false;
            // 
            // pageInfoFecha
            // 
            this.pageInfoFecha.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular);
            this.pageInfoFecha.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.pageInfoFecha.Name = "pageInfoFecha";
            this.pageInfoFecha.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.pageInfoFecha.SizeF = new System.Drawing.SizeF(300F, 18F);
            this.pageInfoFecha.StylePriority.UseFont = false;
            this.pageInfoFecha.TextFormatString = "Impreso: {0:dd/MM/yyyy hh:mm tt}";
            // 
            // pageInfoNumero
            // 
            this.pageInfoNumero.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular);
            this.pageInfoNumero.LocationFloat = new DevExpress.Utils.PointFloat(600F, 8F);
            this.pageInfoNumero.Name = "pageInfoNumero";
            this.pageInfoNumero.SizeF = new System.Drawing.SizeF(200F, 18F);
            this.pageInfoNumero.StylePriority.UseFont = false;
            this.pageInfoNumero.StylePriority.UseTextAlignment = false;
            this.pageInfoNumero.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.pageInfoNumero.TextFormatString = "Página {0} de {1}";
            // 
            // DsCorrelativoQuedan
            // 
            this.DsCorrelativoQuedan.DataMember = "Crear";
            this.DsCorrelativoQuedan.DataSource = typeof(SistemaContable.RP.Esquemas.ListadoCorrelativoQuedanEsquema);
            this.DsCorrelativoQuedan.Name = "DsCorrelativoQuedan";
            // 
            // rptListadoCorrelativoQuedan
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
            this.DsCorrelativoQuedan});
            this.DataSource = this.DsCorrelativoQuedan;
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 30, 40);
            this.PageHeight = 1100;
            this.PageWidth = 850;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this.DsCorrelativoQuedan)).EndInit();
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
        private DevExpress.XtraReports.UI.XRLine lineEncabezadoArriba;
        private DevExpress.XtraReports.UI.XRLabel hQuedan;
        private DevExpress.XtraReports.UI.XRLabel hNombre;
        private DevExpress.XtraReports.UI.XRLabel hTipoDoc;
        private DevExpress.XtraReports.UI.XRLabel hRecibido;
        private DevExpress.XtraReports.UI.XRLabel hVence;
        private DevExpress.XtraReports.UI.XRLabel hOriginal;
        private DevExpress.XtraReports.UI.XRLabel hAbono;
        private DevExpress.XtraReports.UI.XRLabel hSaldo;
        private DevExpress.XtraReports.UI.XRLabel hDiasV;
        private DevExpress.XtraReports.UI.XRLabel hSinV;
        private DevExpress.XtraReports.UI.XRLine lineEncabezadoAbajo;
        private DevExpress.XtraReports.UI.XRLabel lblQuedan;
        private DevExpress.XtraReports.UI.XRLabel dQuedan;
        private DevExpress.XtraReports.UI.XRLabel dNombre;
        private DevExpress.XtraReports.UI.XRLabel dTipoDoc;
        private DevExpress.XtraReports.UI.XRLabel dNumDoc;
        private DevExpress.XtraReports.UI.XRLabel dRecibido;
        private DevExpress.XtraReports.UI.XRLabel dVence;
        private DevExpress.XtraReports.UI.XRLabel dOriginal;
        private DevExpress.XtraReports.UI.XRLabel dAbono;
        private DevExpress.XtraReports.UI.XRLabel dSaldo;
        private DevExpress.XtraReports.UI.XRLabel dDiasV;
        private DevExpress.XtraReports.UI.XRLabel dSinV;
        private DevExpress.XtraReports.UI.XRLabel gfOriginal;
        private DevExpress.XtraReports.UI.XRLabel gfAbono;
        private DevExpress.XtraReports.UI.XRLabel gfSaldo;
        private DevExpress.XtraReports.UI.XRLabel gfSinV;
        private DevExpress.XtraReports.UI.XRLabel lblTotalGeneral;
        private DevExpress.XtraReports.UI.XRLabel rfOriginal;
        private DevExpress.XtraReports.UI.XRLabel rfAbono;
        private DevExpress.XtraReports.UI.XRLabel rfSaldo;
        private DevExpress.XtraReports.UI.XRLabel rfSinV;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoFecha;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfoNumero;
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource DsCorrelativoQuedan;
    }
}

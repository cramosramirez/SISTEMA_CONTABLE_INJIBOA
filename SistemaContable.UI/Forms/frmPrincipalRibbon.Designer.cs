
namespace SistemaContable.UI.Forms
{
    partial class frmPrincipalRibbon
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipalRibbon));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.pnlMensajeRibbon = new System.Windows.Forms.Panel();
            this.lblMensajeRibbon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.pnlMensajeRibbon.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barStaticItem1,
            this.barButtonItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1167, 188);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.139131F, System.Drawing.FontStyle.Bold);
            this.barStaticItem1.ItemAppearance.Normal.Options.UseFont = true;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 5;
            this.barButtonItem1.ImageOptions.Image = global::SistemaContable.UI.Properties.Resources.quedan48x48;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageGroup1.ImageOptions.SvgImage")));
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 595);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1167, 29);
            // 
            // pnlMensajeRibbon
            // 
            this.pnlMensajeRibbon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMensajeRibbon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlMensajeRibbon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMensajeRibbon.Controls.Add(this.lblMensajeRibbon);
            this.pnlMensajeRibbon.Location = new System.Drawing.Point(756, 193);
            this.pnlMensajeRibbon.Name = "pnlMensajeRibbon";
            this.pnlMensajeRibbon.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pnlMensajeRibbon.Size = new System.Drawing.Size(411, 43);
            this.pnlMensajeRibbon.TabIndex = 2;
            this.pnlMensajeRibbon.Visible = false;
            // 
            // lblMensajeRibbon
            // 
            this.lblMensajeRibbon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMensajeRibbon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMensajeRibbon.Font = new System.Drawing.Font("Arial", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeRibbon.Location = new System.Drawing.Point(5, 5);
            this.lblMensajeRibbon.Name = "lblMensajeRibbon";
            this.lblMensajeRibbon.Size = new System.Drawing.Size(399, 31);
            this.lblMensajeRibbon.TabIndex = 0;
            this.lblMensajeRibbon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPrincipalRibbon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 624);
            this.Controls.Add(this.pnlMensajeRibbon);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "frmPrincipalRibbon";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Sistema Contable INJIBOA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrincipalRibbon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.pnlMensajeRibbon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.Panel pnlMensajeRibbon;
        private System.Windows.Forms.Label lblMensajeRibbon;
    }
}
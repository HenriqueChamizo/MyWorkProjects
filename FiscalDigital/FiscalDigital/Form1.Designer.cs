namespace FiscalDigital
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblFinalidade = new System.Windows.Forms.Label();
            this.cmbFinalidade = new System.Windows.Forms.ComboBox();
            this.lblCnae = new System.Windows.Forms.Label();
            this.lblIeIntima = new System.Windows.Forms.Label();
            this.txtCnae = new System.Windows.Forms.TextBox();
            this.txtIeIntima = new System.Windows.Forms.TextBox();
            this.txtArqSped = new System.Windows.Forms.TextBox();
            this.lblCaminhoSped = new System.Windows.Forms.Label();
            this.txtArqDigital = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCaminDest = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ferramentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmResetItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.divergênciasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.lstParticipantes = new System.Windows.Forms.ListBox();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtArqClientes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuscarClie = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIvaMediana = new System.Windows.Forms.TextBox();
            this.lblIvaMediana = new System.Windows.Forms.Label();
            this.lstVParticipantes = new System.Windows.Forms.ListView();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBuscar
            // 
            resources.ApplyResources(this.btnBuscar, "btnBuscar");
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblFinalidade
            // 
            resources.ApplyResources(this.lblFinalidade, "lblFinalidade");
            this.lblFinalidade.Name = "lblFinalidade";
            // 
            // cmbFinalidade
            // 
            this.cmbFinalidade.BackColor = System.Drawing.SystemColors.Control;
            this.cmbFinalidade.FormattingEnabled = true;
            resources.ApplyResources(this.cmbFinalidade, "cmbFinalidade");
            this.cmbFinalidade.Name = "cmbFinalidade";
            // 
            // lblCnae
            // 
            resources.ApplyResources(this.lblCnae, "lblCnae");
            this.lblCnae.Name = "lblCnae";
            // 
            // lblIeIntima
            // 
            resources.ApplyResources(this.lblIeIntima, "lblIeIntima");
            this.lblIeIntima.Name = "lblIeIntima";
            // 
            // txtCnae
            // 
            this.txtCnae.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.txtCnae, "txtCnae");
            this.txtCnae.Name = "txtCnae";
            // 
            // txtIeIntima
            // 
            this.txtIeIntima.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.txtIeIntima, "txtIeIntima");
            this.txtIeIntima.Name = "txtIeIntima";
            // 
            // txtArqSped
            // 
            this.txtArqSped.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.txtArqSped, "txtArqSped");
            this.txtArqSped.Name = "txtArqSped";
            // 
            // lblCaminhoSped
            // 
            resources.ApplyResources(this.lblCaminhoSped, "lblCaminhoSped");
            this.lblCaminhoSped.Name = "lblCaminhoSped";
            // 
            // txtArqDigital
            // 
            this.txtArqDigital.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.txtArqDigital, "txtArqDigital");
            this.txtArqDigital.Name = "txtArqDigital";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnSalvar
            // 
            resources.ApplyResources(this.btnSalvar, "btnSalvar");
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCaminDest
            // 
            resources.ApplyResources(this.btnCaminDest, "btnCaminDest");
            this.btnCaminDest.Name = "btnCaminDest";
            this.btnCaminDest.UseVisualStyleBackColor = true;
            this.btnCaminDest.Click += new System.EventHandler(this.btnCaminDest_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ferramentasToolStripMenuItem,
            this.divergênciasToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // ferramentasToolStripMenuItem
            // 
            resources.ApplyResources(this.ferramentasToolStripMenuItem, "ferramentasToolStripMenuItem");
            this.ferramentasToolStripMenuItem.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ferramentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmResetItem,
            this.manualToolStripMenuItem});
            this.ferramentasToolStripMenuItem.Name = "ferramentasToolStripMenuItem";
            this.ferramentasToolStripMenuItem.Tag = "";
            // 
            // tsmResetItem
            // 
            this.tsmResetItem.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tsmResetItem.Name = "tsmResetItem";
            resources.ApplyResources(this.tsmResetItem, "tsmResetItem");
            this.tsmResetItem.Click += new System.EventHandler(this.tsmResetItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            resources.ApplyResources(this.manualToolStripMenuItem, "manualToolStripMenuItem");
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // divergênciasToolStripMenuItem
            // 
            this.divergênciasToolStripMenuItem.Name = "divergênciasToolStripMenuItem";
            resources.ApplyResources(this.divergênciasToolStripMenuItem, "divergênciasToolStripMenuItem");
            this.divergênciasToolStripMenuItem.Click += new System.EventHandler(this.divergênciasToolStripMenuItem_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstParticipantes
            // 
            this.lstParticipantes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstParticipantes.FormattingEnabled = true;
            resources.ApplyResources(this.lstParticipantes, "lstParticipantes");
            this.lstParticipantes.Name = "lstParticipantes";
            this.lstParticipantes.SelectedIndexChanged += new System.EventHandler(this.lstParticipantes_SelectedIndexChanged);
            // 
            // btnVerificar
            // 
            resources.ApplyResources(this.btnVerificar, "btnVerificar");
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.UseVisualStyleBackColor = true;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.txtArqClientes);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnBuscarClie);
            this.panel1.Controls.Add(this.txtArqSped);
            this.panel1.Controls.Add(this.lblCaminhoSped);
            this.panel1.Controls.Add(this.btnBuscar);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // txtArqClientes
            // 
            this.txtArqClientes.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.txtArqClientes, "txtArqClientes");
            this.txtArqClientes.Name = "txtArqClientes";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnBuscarClie
            // 
            resources.ApplyResources(this.btnBuscarClie, "btnBuscarClie");
            this.btnBuscarClie.Name = "btnBuscarClie";
            this.btnBuscarClie.UseVisualStyleBackColor = true;
            this.btnBuscarClie.Click += new System.EventHandler(this.btnBuscarClie_Click);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            this.label4.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Name = "label4";
            // 
            // txtIvaMediana
            // 
            this.txtIvaMediana.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.txtIvaMediana, "txtIvaMediana");
            this.txtIvaMediana.Name = "txtIvaMediana";
            // 
            // lblIvaMediana
            // 
            resources.ApplyResources(this.lblIvaMediana, "lblIvaMediana");
            this.lblIvaMediana.Name = "lblIvaMediana";
            // 
            // lstVParticipantes
            // 
            this.lstVParticipantes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstVParticipantes.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lstVParticipantes.FullRowSelect = true;
            resources.ApplyResources(this.lstVParticipantes, "lstVParticipantes");
            this.lstVParticipantes.Name = "lstVParticipantes";
            this.lstVParticipantes.UseCompatibleStateImageBehavior = false;
            this.lstVParticipantes.SelectedIndexChanged += new System.EventHandler(this.lstVParticipantes_SelectedIndexChanged);
            // 
            // FrmPrincipal
            // 
            this.AcceptButton = this.btnSalvar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.lstVParticipantes);
            this.Controls.Add(this.txtIvaMediana);
            this.Controls.Add(this.lblIvaMediana);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnVerificar);
            this.Controls.Add(this.lstParticipantes);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCaminDest);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtArqDigital);
            this.Controls.Add(this.txtIeIntima);
            this.Controls.Add(this.txtCnae);
            this.Controls.Add(this.lblIeIntima);
            this.Controls.Add(this.lblCnae);
            this.Controls.Add(this.cmbFinalidade);
            this.Controls.Add(this.lblFinalidade);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblFinalidade;
        private System.Windows.Forms.ComboBox cmbFinalidade;
        private System.Windows.Forms.Label lblCnae;
        private System.Windows.Forms.Label lblIeIntima;
        private System.Windows.Forms.TextBox txtCnae;
        private System.Windows.Forms.TextBox txtIeIntima;
        private System.Windows.Forms.TextBox txtArqSped;
        private System.Windows.Forms.Label lblCaminhoSped;
        private System.Windows.Forms.TextBox txtArqDigital;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCaminDest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ferramentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmResetItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstParticipantes;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtArqClientes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarClie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem divergênciasToolStripMenuItem;
        private System.Windows.Forms.TextBox txtIvaMediana;
        private System.Windows.Forms.Label lblIvaMediana;
        private System.Windows.Forms.ListView lstVParticipantes;
    }
}


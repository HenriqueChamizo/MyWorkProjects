namespace ExportFormat
{
    partial class frmInicial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicial));
            this.lblSelecione = new System.Windows.Forms.Label();
            this.btnSeleciona = new System.Windows.Forms.Button();
            this.cmbModulos = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblSelecione
            // 
            this.lblSelecione.AutoSize = true;
            this.lblSelecione.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelecione.Location = new System.Drawing.Point(12, 9);
            this.lblSelecione.Name = "lblSelecione";
            this.lblSelecione.Size = new System.Drawing.Size(251, 24);
            this.lblSelecione.TabIndex = 0;
            this.lblSelecione.Text = "Selecione um dos módulos: ";
            // 
            // btnSeleciona
            // 
            this.btnSeleciona.Location = new System.Drawing.Point(354, 95);
            this.btnSeleciona.Name = "btnSeleciona";
            this.btnSeleciona.Size = new System.Drawing.Size(75, 23);
            this.btnSeleciona.TabIndex = 2;
            this.btnSeleciona.Text = "Selecionar";
            this.btnSeleciona.UseVisualStyleBackColor = true;
            this.btnSeleciona.Click += new System.EventHandler(this.btnSeleciona_Click);
            // 
            // cmbModulos
            // 
            this.cmbModulos.FormattingEnabled = true;
            this.cmbModulos.Location = new System.Drawing.Point(16, 46);
            this.cmbModulos.Name = "cmbModulos";
            this.cmbModulos.Size = new System.Drawing.Size(413, 21);
            this.cmbModulos.TabIndex = 3;
            // 
            // frmInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 130);
            this.Controls.Add(this.cmbModulos);
            this.Controls.Add(this.btnSeleciona);
            this.Controls.Add(this.lblSelecione);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInicial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExportFormat";
            this.Load += new System.EventHandler(this.frmInicial_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelecione;
        private System.Windows.Forms.Button btnSeleciona;
        private System.Windows.Forms.ComboBox cmbModulos;
    }
}
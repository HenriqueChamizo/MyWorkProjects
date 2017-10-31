namespace FiscalDigital
{
    partial class Divergencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Divergencias));
            this.lstVDivergencias = new System.Windows.Forms.ListView();
            this.btnFechar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstVDivergencias
            // 
            this.lstVDivergencias.AllowColumnReorder = true;
            this.lstVDivergencias.FullRowSelect = true;
            this.lstVDivergencias.GridLines = true;
            resources.ApplyResources(this.lstVDivergencias, "lstVDivergencias");
            this.lstVDivergencias.Name = "lstVDivergencias";
            this.lstVDivergencias.UseCompatibleStateImageBehavior = false;
            // 
            // btnFechar
            // 
            resources.ApplyResources(this.btnFechar, "btnFechar");
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // Divergencias
            // 
            this.AcceptButton = this.btnFechar;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lstVDivergencias);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Divergencias";
            this.Load += new System.EventHandler(this.Divergencias_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstVDivergencias;
        private System.Windows.Forms.Button btnFechar;
    }
}
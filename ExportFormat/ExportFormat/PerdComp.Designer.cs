namespace ExportFormat
{
    partial class PerdComp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerdComp));
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnSource = new System.Windows.Forms.Button();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.btnDestination = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.rdbTypeFileYes = new System.Windows.Forms.RadioButton();
            this.rdbTypeFileNo = new System.Windows.Forms.RadioButton();
            this.lblMonth = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Enabled = false;
            this.txtSource.Location = new System.Drawing.Point(12, 37);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(457, 20);
            this.txtSource.TabIndex = 0;
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(475, 37);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(53, 20);
            this.btnSource.TabIndex = 1;
            this.btnSource.Text = "...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(12, 21);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(79, 13);
            this.lblSource.TabIndex = 2;
            this.lblSource.Text = "Arquivo Fonte: ";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(12, 115);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(49, 13);
            this.lblDestination.TabIndex = 5;
            this.lblDestination.Text = "Destino: ";
            // 
            // btnDestination
            // 
            this.btnDestination.Location = new System.Drawing.Point(475, 131);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(53, 20);
            this.btnDestination.TabIndex = 4;
            this.btnDestination.Text = "...";
            this.btnDestination.UseVisualStyleBackColor = true;
            this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Enabled = false;
            this.txtDestination.Location = new System.Drawing.Point(12, 131);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(457, 20);
            this.txtDestination.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(453, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Salvar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rdbTypeFileYes
            // 
            this.rdbTypeFileYes.AutoSize = true;
            this.rdbTypeFileYes.Location = new System.Drawing.Point(123, 65);
            this.rdbTypeFileYes.Name = "rdbTypeFileYes";
            this.rdbTypeFileYes.Size = new System.Drawing.Size(42, 17);
            this.rdbTypeFileYes.TabIndex = 7;
            this.rdbTypeFileYes.TabStop = true;
            this.rdbTypeFileYes.Text = "Sim";
            this.rdbTypeFileYes.UseVisualStyleBackColor = true;
            // 
            // rdbTypeFileNo
            // 
            this.rdbTypeFileNo.AutoSize = true;
            this.rdbTypeFileNo.Location = new System.Drawing.Point(171, 65);
            this.rdbTypeFileNo.Name = "rdbTypeFileNo";
            this.rdbTypeFileNo.Size = new System.Drawing.Size(45, 17);
            this.rdbTypeFileNo.TabIndex = 8;
            this.rdbTypeFileNo.TabStop = true;
            this.rdbTypeFileNo.Text = "Não";
            this.rdbTypeFileNo.UseVisualStyleBackColor = true;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(12, 67);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(101, 13);
            this.lblMonth.TabIndex = 9;
            this.lblMonth.Text = "Separar por meses: ";
            // 
            // PerdComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 193);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.rdbTypeFileNo);
            this.Controls.Add(this.rdbTypeFileYes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txtSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PerdComp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PER DCOMP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PerdComp_FormClosing);
            this.Load += new System.EventHandler(this.PerdComp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rdbTypeFileYes;
        private System.Windows.Forms.RadioButton rdbTypeFileNo;
        private System.Windows.Forms.Label lblMonth;
    }
}
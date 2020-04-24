namespace PIC_Simulator
{
    partial class GUI_Simu
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI_Simu));
            this.grpBSFRW = new System.Windows.Forms.GroupBox();
            this.grpBProgramm = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnDatei = new System.Windows.Forms.ToolStripButton();
            this.tsBtnEinstellungen = new System.Windows.Forms.ToolStripButton();
            this.tsBtnHilfe = new System.Windows.Forms.ToolStripButton();
            this.tBProgramm = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpBControls = new System.Windows.Forms.GroupBox();
            this.grpBProgramm.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpBControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBSFRW
            // 
            this.grpBSFRW.Location = new System.Drawing.Point(12, 28);
            this.grpBSFRW.Name = "grpBSFRW";
            this.grpBSFRW.Size = new System.Drawing.Size(274, 144);
            this.grpBSFRW.TabIndex = 0;
            this.grpBSFRW.TabStop = false;
            this.grpBSFRW.Text = "SFR + W";
            // 
            // grpBProgramm
            // 
            this.grpBProgramm.Controls.Add(this.tBProgramm);
            this.grpBProgramm.Location = new System.Drawing.Point(12, 178);
            this.grpBProgramm.Name = "grpBProgramm";
            this.grpBProgramm.Size = new System.Drawing.Size(274, 260);
            this.grpBProgramm.TabIndex = 1;
            this.grpBProgramm.TabStop = false;
            this.grpBProgramm.Text = "Programm";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnDatei,
            this.tsBtnEinstellungen,
            this.tsBtnHilfe});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnDatei
            // 
            this.tsBtnDatei.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnDatei.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnDatei.Image")));
            this.tsBtnDatei.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDatei.Name = "tsBtnDatei";
            this.tsBtnDatei.Size = new System.Drawing.Size(38, 22);
            this.tsBtnDatei.Text = "Datei";
            // 
            // tsBtnEinstellungen
            // 
            this.tsBtnEinstellungen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnEinstellungen.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnEinstellungen.Image")));
            this.tsBtnEinstellungen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnEinstellungen.Name = "tsBtnEinstellungen";
            this.tsBtnEinstellungen.Size = new System.Drawing.Size(82, 22);
            this.tsBtnEinstellungen.Text = "Einstellungen";
            // 
            // tsBtnHilfe
            // 
            this.tsBtnHilfe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnHilfe.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnHilfe.Image")));
            this.tsBtnHilfe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnHilfe.Name = "tsBtnHilfe";
            this.tsBtnHilfe.Size = new System.Drawing.Size(36, 22);
            this.tsBtnHilfe.Text = "Hilfe";
            // 
            // tBProgramm
            // 
            this.tBProgramm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBProgramm.Location = new System.Drawing.Point(0, 19);
            this.tBProgramm.Multiline = true;
            this.tBProgramm.Name = "tBProgramm";
            this.tBProgramm.ReadOnly = true;
            this.tBProgramm.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tBProgramm.Size = new System.Drawing.Size(274, 241);
            this.tBProgramm.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(6, 54);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 83);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStep
            // 
            this.btnStep.Location = new System.Drawing.Point(6, 112);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(75, 23);
            this.btnStep.TabIndex = 6;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 141);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpBControls
            // 
            this.grpBControls.Controls.Add(this.btnReset);
            this.grpBControls.Controls.Add(this.btnExit);
            this.grpBControls.Controls.Add(this.btnStart);
            this.grpBControls.Controls.Add(this.btnStep);
            this.grpBControls.Controls.Add(this.btnStop);
            this.grpBControls.Location = new System.Drawing.Point(700, 267);
            this.grpBControls.Name = "grpBControls";
            this.grpBControls.Size = new System.Drawing.Size(88, 171);
            this.grpBControls.TabIndex = 8;
            this.grpBControls.TabStop = false;
            this.grpBControls.Text = "Controls";
            // 
            // GUI_Simu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpBControls);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grpBProgramm);
            this.Controls.Add(this.grpBSFRW);
            this.Name = "GUI_Simu";
            this.Text = "Simulation PIC";
            this.grpBProgramm.ResumeLayout(false);
            this.grpBProgramm.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpBControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBSFRW;
        private System.Windows.Forms.GroupBox grpBProgramm;
        private System.Windows.Forms.TextBox tBProgramm;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnDatei;
        private System.Windows.Forms.ToolStripButton tsBtnEinstellungen;
        private System.Windows.Forms.ToolStripButton tsBtnHilfe;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox grpBControls;
    }
}


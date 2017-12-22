namespace MIDI_YAMAHA
{
    partial class FormMIDI2PC
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
            this.textBoxDebug = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.txtWriteTxt = new System.Windows.Forms.Button();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblInCaps = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxDebug
            // 
            this.textBoxDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDebug.Location = new System.Drawing.Point(13, 81);
            this.textBoxDebug.Multiline = true;
            this.textBoxDebug.Name = "textBoxDebug";
            this.textBoxDebug.ReadOnly = true;
            this.textBoxDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDebug.Size = new System.Drawing.Size(592, 275);
            this.textBoxDebug.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(434, 15);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // txtWriteTxt
            // 
            this.txtWriteTxt.Location = new System.Drawing.Point(530, 15);
            this.txtWriteTxt.Name = "txtWriteTxt";
            this.txtWriteTxt.Size = new System.Drawing.Size(75, 23);
            this.txtWriteTxt.TabIndex = 2;
            this.txtWriteTxt.Text = "WriteText";
            this.txtWriteTxt.UseVisualStyleBackColor = true;
            this.txtWriteTxt.Click += new System.EventHandler(this.txtWriteTxt_Click);
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(11, 20);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(77, 12);
            this.lblMes.TabIndex = 3;
            this.lblMes.Text = "Devices-sum:";
            // 
            // lblInCaps
            // 
            this.lblInCaps.Location = new System.Drawing.Point(12, 52);
            this.lblInCaps.Name = "lblInCaps";
            this.lblInCaps.Size = new System.Drawing.Size(593, 26);
            this.lblInCaps.TabIndex = 4;
            this.lblInCaps.Text = "Message";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(530, 371);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormMIDI2PC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 406);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblInCaps);
            this.Controls.Add(this.lblMes);
            this.Controls.Add(this.txtWriteTxt);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMIDI2PC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIDI to PC";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMIDI2PC_FormClosed);
            this.Load += new System.EventHandler(this.FormMIDI2PC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDebug;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button txtWriteTxt;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Label lblInCaps;
        private System.Windows.Forms.Button btnClose;
    }
}


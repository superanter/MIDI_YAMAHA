namespace MIDI_YAMAHA
{
    partial class FormMain
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
            this.btnMIDI2PC = new System.Windows.Forms.Button();
            this.btnText2Message = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMIDI2PC
            // 
            this.btnMIDI2PC.Location = new System.Drawing.Point(49, 38);
            this.btnMIDI2PC.Name = "btnMIDI2PC";
            this.btnMIDI2PC.Size = new System.Drawing.Size(101, 54);
            this.btnMIDI2PC.TabIndex = 0;
            this.btnMIDI2PC.Text = "MIDI -> PC";
            this.btnMIDI2PC.UseVisualStyleBackColor = true;
            this.btnMIDI2PC.Click += new System.EventHandler(this.btnMIDI2PC_Click);
            // 
            // btnText2Message
            // 
            this.btnText2Message.Location = new System.Drawing.Point(214, 38);
            this.btnText2Message.Name = "btnText2Message";
            this.btnText2Message.Size = new System.Drawing.Size(101, 54);
            this.btnText2Message.TabIndex = 1;
            this.btnText2Message.Text = "Text Message";
            this.btnText2Message.UseVisualStyleBackColor = true;
            this.btnText2Message.Click += new System.EventHandler(this.btnText2Message_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 140);
            this.Controls.Add(this.btnText2Message);
            this.Controls.Add(this.btnMIDI2PC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIDI YAMAHA";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMIDI2PC;
        private System.Windows.Forms.Button btnText2Message;
    }
}
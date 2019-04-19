namespace pingCheck
{
    partial class Overlay
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
            this.Adress = new System.Windows.Forms.Label();
            this.Ping = new System.Windows.Forms.Label();
            this.ActualAdress = new System.Windows.Forms.Label();
            this.ActualPing = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Adress
            // 
            this.Adress.AutoSize = true;
            this.Adress.BackColor = System.Drawing.Color.Transparent;
            this.Adress.Font = new System.Drawing.Font("Onyx", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.Adress.ForeColor = System.Drawing.Color.Transparent;
            this.Adress.Location = new System.Drawing.Point(12, 9);
            this.Adress.Name = "Adress";
            this.Adress.Size = new System.Drawing.Size(58, 18);
            this.Adress.TabIndex = 0;
            this.Adress.Text = "Adress";
            // 
            // Ping
            // 
            this.Ping.AutoSize = true;
            this.Ping.BackColor = System.Drawing.Color.Transparent;
            this.Ping.Font = new System.Drawing.Font("Onyx", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.Ping.ForeColor = System.Drawing.Color.Transparent;
            this.Ping.Location = new System.Drawing.Point(194, 8);
            this.Ping.Name = "Ping";
            this.Ping.Size = new System.Drawing.Size(40, 18);
            this.Ping.TabIndex = 1;
            this.Ping.Text = "Ping";
            // 
            // ActualAdress
            // 
            this.ActualAdress.AutoSize = true;
            this.ActualAdress.BackColor = System.Drawing.Color.Transparent;
            this.ActualAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActualAdress.ForeColor = System.Drawing.Color.Transparent;
            this.ActualAdress.Location = new System.Drawing.Point(76, 8);
            this.ActualAdress.Name = "ActualAdress";
            this.ActualAdress.Size = new System.Drawing.Size(0, 20);
            this.ActualAdress.TabIndex = 2;
            // 
            // ActualPing
            // 
            this.ActualPing.AutoSize = true;
            this.ActualPing.BackColor = System.Drawing.Color.Transparent;
            this.ActualPing.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ActualPing.ForeColor = System.Drawing.Color.Transparent;
            this.ActualPing.Location = new System.Drawing.Point(240, 9);
            this.ActualPing.Name = "ActualPing";
            this.ActualPing.Size = new System.Drawing.Size(0, 20);
            this.ActualPing.TabIndex = 3;
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(331, 38);
            this.Controls.Add(this.ActualPing);
            this.Controls.Add(this.ActualAdress);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.Adress);
            this.Name = "Overlay";
            this.Text = "Overlay";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Adress;
        private System.Windows.Forms.Label Ping;
        private System.Windows.Forms.Label ActualAdress;
        private System.Windows.Forms.Label ActualPing;
    }
}
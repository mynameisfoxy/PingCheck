namespace pingCheck.Misc
{
    partial class UpdateConfirmer
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
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.DeclineButton = new System.Windows.Forms.Button();
            this.SkipButton = new System.Windows.Forms.Button();
            this.UpdateInformer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.ConfirmButton.Location = new System.Drawing.Point(12, 37);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 0;
            this.ConfirmButton.Text = "Да";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            // 
            // DeclineButton
            // 
            this.DeclineButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.DeclineButton.Location = new System.Drawing.Point(93, 37);
            this.DeclineButton.Name = "DeclineButton";
            this.DeclineButton.Size = new System.Drawing.Size(75, 23);
            this.DeclineButton.TabIndex = 1;
            this.DeclineButton.Text = "Нет";
            this.DeclineButton.UseVisualStyleBackColor = true;
            // 
            // SkipButton
            // 
            this.SkipButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.SkipButton.Location = new System.Drawing.Point(174, 37);
            this.SkipButton.Name = "SkipButton";
            this.SkipButton.Size = new System.Drawing.Size(75, 23);
            this.SkipButton.TabIndex = 2;
            this.SkipButton.Text = "Пропустить";
            this.SkipButton.UseVisualStyleBackColor = true;
            // 
            // UpdateInformer
            // 
            this.UpdateInformer.AutoSize = true;
            this.UpdateInformer.Location = new System.Drawing.Point(31, 13);
            this.UpdateInformer.Name = "UpdateInformer";
            this.UpdateInformer.Size = new System.Drawing.Size(197, 13);
            this.UpdateInformer.TabIndex = 3;
            this.UpdateInformer.Text = "Обнаружено обновление! Загрузить?";
            // 
            // UpdateConfirmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 75);
            this.Controls.Add(this.UpdateInformer);
            this.Controls.Add(this.SkipButton);
            this.Controls.Add(this.DeclineButton);
            this.Controls.Add(this.ConfirmButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateConfirmer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновление!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Button DeclineButton;
        private System.Windows.Forms.Button SkipButton;
        private System.Windows.Forms.Label UpdateInformer;
    }
}
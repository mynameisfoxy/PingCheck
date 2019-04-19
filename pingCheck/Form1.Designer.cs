namespace pingCheck
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowProgramFromTray = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateCase = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitButtonFromTray = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.ComponentModel.BackgroundWorker();
            this.GoOrStopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.CheckErrorSound = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.CheckNormalizaSound = new System.Windows.Forms.CheckBox();
            this.CheckSoundsOn = new System.Windows.Forms.CheckBox();
            this.AdditionalButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CheckLog = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Overlay = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "123";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PingChecker";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowProgramFromTray,
            this.UpdateCase,
            this.ExitButtonFromTray});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(132, 70);
            // 
            // ShowProgramFromTray
            // 
            this.ShowProgramFromTray.Enabled = false;
            this.ShowProgramFromTray.Name = "ShowProgramFromTray";
            this.ShowProgramFromTray.Size = new System.Drawing.Size(131, 22);
            this.ShowProgramFromTray.Text = "Открыть";
            this.ShowProgramFromTray.Click += new System.EventHandler(this.ShowProgramFromTray_Clicked);
            // 
            // UpdateCase
            // 
            this.UpdateCase.Name = "UpdateCase";
            this.UpdateCase.Size = new System.Drawing.Size(131, 22);
            this.UpdateCase.Text = "Обновить!";
            this.UpdateCase.Visible = false;
            this.UpdateCase.Click += new System.EventHandler(this.NotifyIcon1_BaloonClicked);
            // 
            // ExitButtonFromTray
            // 
            this.ExitButtonFromTray.Name = "ExitButtonFromTray";
            this.ExitButtonFromTray.Size = new System.Drawing.Size(131, 22);
            this.ExitButtonFromTray.Text = "Выход";
            this.ExitButtonFromTray.Click += new System.EventHandler(this.ExitButtonFromTray_Clicked);
            // 
            // GoOrStopButton
            // 
            this.GoOrStopButton.Location = new System.Drawing.Point(225, 10);
            this.GoOrStopButton.Name = "GoOrStopButton";
            this.GoOrStopButton.Size = new System.Drawing.Size(97, 23);
            this.GoOrStopButton.TabIndex = 1;
            this.GoOrStopButton.Text = "Запуск";
            this.GoOrStopButton.UseVisualStyleBackColor = true;
            this.GoOrStopButton.Click += new System.EventHandler(this.RunOrStop);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.Location = new System.Drawing.Point(225, 36);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(97, 23);
            this.MinimizeButton.TabIndex = 2;
            this.MinimizeButton.Text = "Свернуть";
            this.MinimizeButton.UseVisualStyleBackColor = true;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButtonClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(74, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Адрес:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Интервал";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(74, 38);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(111, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.CheckErrorSound);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.CheckNormalizaSound);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 126);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры звуковых уведомлений";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "выше";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(197, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "раз(а) подряд";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(139, 41);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(52, 20);
            this.numericUpDown3.TabIndex = 16;
            this.numericUpDown3.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // CheckErrorSound
            // 
            this.CheckErrorSound.AutoSize = true;
            this.CheckErrorSound.Location = new System.Drawing.Point(9, 90);
            this.CheckErrorSound.Name = "CheckErrorSound";
            this.CheckErrorSound.Size = new System.Drawing.Size(150, 17);
            this.CheckErrorSound.TabIndex = 15;
            this.CheckErrorSound.Text = "Уведомлять об ошибках";
            this.CheckErrorSound.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Уведомление о нестабильном соединении при отклике";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "мс.";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(47, 41);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(59, 20);
            this.numericUpDown2.TabIndex = 11;
            this.numericUpDown2.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // CheckNormalizaSound
            // 
            this.CheckNormalizaSound.AutoSize = true;
            this.CheckNormalizaSound.Location = new System.Drawing.Point(9, 67);
            this.CheckNormalizaSound.Name = "CheckNormalizaSound";
            this.CheckNormalizaSound.Size = new System.Drawing.Size(235, 17);
            this.CheckNormalizaSound.TabIndex = 10;
            this.CheckNormalizaSound.Text = "Уведомлять о стабилизации соединения";
            this.CheckNormalizaSound.UseVisualStyleBackColor = true;
            // 
            // CheckSoundsOn
            // 
            this.CheckSoundsOn.AutoSize = true;
            this.CheckSoundsOn.Location = new System.Drawing.Point(12, 94);
            this.CheckSoundsOn.Name = "CheckSoundsOn";
            this.CheckSoundsOn.Size = new System.Drawing.Size(197, 17);
            this.CheckSoundsOn.TabIndex = 8;
            this.CheckSoundsOn.Text = "Включить звуковые уведомления";
            this.CheckSoundsOn.UseVisualStyleBackColor = true;
            this.CheckSoundsOn.CheckedChanged += new System.EventHandler(this.CheckSoundsOn_CheckedChanged);
            // 
            // AdditionalButton
            // 
            this.AdditionalButton.Location = new System.Drawing.Point(15, 65);
            this.AdditionalButton.Name = "AdditionalButton";
            this.AdditionalButton.Size = new System.Drawing.Size(204, 23);
            this.AdditionalButton.TabIndex = 9;
            this.AdditionalButton.Text = "Дополнительно";
            this.AdditionalButton.UseVisualStyleBackColor = true;
            this.AdditionalButton.Click += new System.EventHandler(this.AdditionalButtonClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.Location = new System.Drawing.Point(6, 19);
            this.richTextBox1.MaxLength = 20;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(212, 228);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Location = new System.Drawing.Point(328, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(224, 253);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Лог";
            // 
            // CheckLog
            // 
            this.CheckLog.AutoSize = true;
            this.CheckLog.Location = new System.Drawing.Point(12, 249);
            this.CheckLog.Name = "CheckLog";
            this.CheckLog.Size = new System.Drawing.Size(110, 17);
            this.CheckLog.TabIndex = 12;
            this.CheckLog.Text = "Показать лог -->";
            this.CheckLog.UseVisualStyleBackColor = true;
            this.CheckLog.CheckedChanged += new System.EventHandler(this.CheckLog_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "сек.";
            // 
            // Overlay
            // 
            this.Overlay.Location = new System.Drawing.Point(225, 65);
            this.Overlay.Name = "Overlay";
            this.Overlay.Size = new System.Drawing.Size(96, 23);
            this.Overlay.TabIndex = 14;
            this.Overlay.Text = "Оверлей";
            this.Overlay.UseVisualStyleBackColor = true;
            this.Overlay.Click += new System.EventHandler(this.Overlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 275);
            this.Controls.Add(this.Overlay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CheckLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.AdditionalButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CheckSoundsOn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.GoOrStopButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PingCheck";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker Exit;
        private System.Windows.Forms.Button GoOrStopButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button MinimizeButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem UpdateCase;
        private System.Windows.Forms.ToolStripMenuItem ShowProgramFromTray;
        private System.Windows.Forms.ToolStripMenuItem ExitButtonFromTray;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CheckSoundsOn;
        private System.Windows.Forms.Button AdditionalButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.CheckBox CheckNormalizaSound;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CheckErrorSound;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox CheckLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Overlay;
    }
}


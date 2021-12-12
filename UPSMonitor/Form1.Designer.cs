namespace UPSMonitor
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbInVoltage = new System.Windows.Forms.Label();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.lbFrequency = new System.Windows.Forms.Label();
            this.lbAccVoltage = new System.Windows.Forms.Label();
            this.lbTemperature = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbOutVoltage = new System.Windows.Forms.Label();
            this.lblErr = new System.Windows.Forms.Label();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btSaveButton = new System.Windows.Forms.Button();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.updateLabel = new System.Windows.Forms.LinkLabel();
            this.about = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.settingsPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(319, 177);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(311, 151);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Состояние";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.18518F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.81482F));
            this.tableLayoutPanel1.Controls.Add(this.lbInVoltage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbCurrent, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbFrequency, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbAccVoltage, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbTemperature, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbOutVoltage, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblErr, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(305, 145);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbInVoltage
            // 
            this.lbInVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lbInVoltage.Location = new System.Drawing.Point(3, 29);
            this.lbInVoltage.Name = "lbInVoltage";
            this.tableLayoutPanel1.SetRowSpan(this.lbInVoltage, 3);
            this.lbInVoltage.Size = new System.Drawing.Size(131, 87);
            this.lbInVoltage.TabIndex = 0;
            this.lbInVoltage.Text = "0V";
            this.lbInVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCurrent
            // 
            this.lbCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCurrent.Location = new System.Drawing.Point(140, 0);
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(162, 29);
            this.lbCurrent.TabIndex = 3;
            this.lbCurrent.Text = "Нагрузка:";
            this.lbCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbFrequency
            // 
            this.lbFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFrequency.Location = new System.Drawing.Point(140, 29);
            this.lbFrequency.Name = "lbFrequency";
            this.lbFrequency.Size = new System.Drawing.Size(162, 29);
            this.lbFrequency.TabIndex = 4;
            this.lbFrequency.Text = "Частота:";
            this.lbFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAccVoltage
            // 
            this.lbAccVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAccVoltage.Location = new System.Drawing.Point(140, 87);
            this.lbAccVoltage.Name = "lbAccVoltage";
            this.lbAccVoltage.Size = new System.Drawing.Size(162, 29);
            this.lbAccVoltage.TabIndex = 5;
            this.lbAccVoltage.Text = "Напр. аккумулятора:";
            this.lbAccVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTemperature
            // 
            this.lbTemperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTemperature.Location = new System.Drawing.Point(140, 58);
            this.lbTemperature.Name = "lbTemperature";
            this.lbTemperature.Size = new System.Drawing.Size(162, 29);
            this.lbTemperature.TabIndex = 6;
            this.lbTemperature.Text = "Температура:";
            this.lbTemperature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Входное напряжение:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbOutVoltage
            // 
            this.lbOutVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOutVoltage.Location = new System.Drawing.Point(140, 116);
            this.lbOutVoltage.Name = "lbOutVoltage";
            this.lbOutVoltage.Size = new System.Drawing.Size(162, 29);
            this.lbOutVoltage.TabIndex = 8;
            this.lbOutVoltage.Text = "Входное напряжение:";
            this.lbOutVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblErr
            // 
            this.lblErr.AutoSize = true;
            this.lblErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblErr.ForeColor = System.Drawing.Color.Red;
            this.lblErr.Location = new System.Drawing.Point(3, 116);
            this.lblErr.Name = "lblErr";
            this.lblErr.Size = new System.Drawing.Size(131, 29);
            this.lblErr.TabIndex = 9;
            this.lblErr.Text = "Сбой подключения";
            this.lblErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblErr.Visible = false;
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.tableLayoutPanel2);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(311, 151);
            this.settingsPage.TabIndex = 2;
            this.settingsPage.Text = "Настройки";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btSaveButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbPortName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.nudInterval, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbLog, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(305, 145);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "COM порт:";
            // 
            // btSaveButton
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.btSaveButton, 2);
            this.btSaveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btSaveButton.Location = new System.Drawing.Point(3, 119);
            this.btSaveButton.Name = "btSaveButton";
            this.btSaveButton.Size = new System.Drawing.Size(299, 23);
            this.btSaveButton.TabIndex = 0;
            this.btSaveButton.Text = "Сохранить";
            this.btSaveButton.UseVisualStyleBackColor = true;
            this.btSaveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cbPortName
            // 
            this.cbPortName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(155, 3);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(147, 21);
            this.cbPortName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Интервал обновления (мс):";
            // 
            // nudInterval
            // 
            this.nudInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudInterval.Location = new System.Drawing.Point(155, 33);
            this.nudInterval.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudInterval.Minimum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(147, 20);
            this.nudInterval.TabIndex = 3;
            this.nudInterval.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.cbLog, 2);
            this.cbLog.Location = new System.Drawing.Point(3, 56);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(89, 17);
            this.cbLog.TabIndex = 4;
            this.cbLog.Text = "Запись лога";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.updateLabel);
            this.tabPage2.Controls.Add(this.about);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(311, 151);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "О программе";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.updateLabel.Location = new System.Drawing.Point(3, 135);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(179, 13);
            this.updateLabel.TabIndex = 1;
            this.updateLabel.TabStop = true;
            this.updateLabel.Text = "Проверить наличие новых версий";
            this.updateLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateLabel_LinkClicked);
            // 
            // about
            // 
            this.about.AutoSize = true;
            this.about.Dock = System.Windows.Forms.DockStyle.Fill;
            this.about.Location = new System.Drawing.Point(3, 3);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(16, 13);
            this.about.TabIndex = 0;
            this.about.Text = "...";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "UPSMonitor";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(110, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 177);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "UPSMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.settingsPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbInVoltage;
        private System.Windows.Forms.Label lbCurrent;
        private System.Windows.Forms.Label lbFrequency;
        private System.Windows.Forms.Label lbAccVoltage;
        private System.Windows.Forms.Label lbTemperature;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbOutVoltage;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btSaveButton;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.Label lblErr;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label about;
        private System.Windows.Forms.CheckBox cbLog;
        private System.Windows.Forms.LinkLabel updateLabel;
    }
}


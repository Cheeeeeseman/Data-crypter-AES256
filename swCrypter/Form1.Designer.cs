namespace swLocker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Btn_ChooseSW = new Button();
            Btn_LockSW = new Button();
            progressBar = new ProgressBar();
            label1 = new Label();
            logTextBox = new TextBox();
            _encryptOpenFileDialog = new OpenFileDialog();
            SuspendLayout();
            // 
            // Btn_ChooseSW
            // 
            Btn_ChooseSW.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Btn_ChooseSW.BackColor = SystemColors.ButtonHighlight;
            Btn_ChooseSW.FlatAppearance.BorderColor = Color.DimGray;
            Btn_ChooseSW.Font = new Font("Segoe Print", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_ChooseSW.Location = new Point(247, 13);
            Btn_ChooseSW.Name = "Btn_ChooseSW";
            Btn_ChooseSW.Size = new Size(150, 50);
            Btn_ChooseSW.TabIndex = 0;
            Btn_ChooseSW.Text = "Выбрать прошивку";
            Btn_ChooseSW.UseVisualStyleBackColor = false;
            Btn_ChooseSW.MouseClick += Btn_ChooseSW_MouseClick;
            // 
            // Btn_LockSW
            // 
            Btn_LockSW.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Btn_LockSW.BackColor = SystemColors.ButtonHighlight;
            Btn_LockSW.Enabled = false;
            Btn_LockSW.FlatAppearance.BorderColor = Color.DimGray;
            Btn_LockSW.Font = new Font("Segoe Print", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_LockSW.Location = new Point(247, 69);
            Btn_LockSW.Name = "Btn_LockSW";
            Btn_LockSW.Size = new Size(150, 50);
            Btn_LockSW.TabIndex = 1;
            Btn_LockSW.Text = "Зашифровать";
            Btn_LockSW.UseVisualStyleBackColor = false;
            Btn_LockSW.Click += Btn_LockSW_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(247, 176);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(150, 23);
            progressBar.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe Print", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(247, 152);
            label1.Name = "label1";
            label1.Size = new Size(59, 21);
            label1.TabIndex = 3;
            label1.Text = "Процесс";
            // 
            // logTextBox
            // 
            logTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            logTextBox.Font = new Font("Segoe Print", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            logTextBox.Location = new Point(12, 13);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.Size = new Size(218, 187);
            logTextBox.TabIndex = 4;
            // 
            // _encryptOpenFileDialog
            // 
            _encryptOpenFileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 211);
            Controls.Add(logTextBox);
            Controls.Add(label1);
            Controls.Add(progressBar);
            Controls.Add(Btn_LockSW);
            Controls.Add(Btn_ChooseSW);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(450, 250);
            MinimumSize = new Size(450, 250);
            Name = "Form1";
            Text = "crypter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_ChooseSW;
        private Button Btn_LockSW;
        private ProgressBar progressBar;
        private Label label1;
        private TextBox logTextBox;
        private OpenFileDialog _encryptOpenFileDialog;
    }
}

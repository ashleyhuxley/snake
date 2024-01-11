namespace Snake.Ui
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            mainImage = new PictureBox();
            tickTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            scoreLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)mainImage).BeginInit();
            SuspendLayout();
            // 
            // mainImage
            // 
            mainImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            mainImage.BackColor = Color.White;
            mainImage.Location = new Point(12, 12);
            mainImage.Name = "mainImage";
            mainImage.Size = new Size(500, 500);
            mainImage.TabIndex = 0;
            mainImage.TabStop = false;
            mainImage.Paint += mainImage_Paint;
            // 
            // tickTimer
            // 
            tickTimer.Enabled = true;
            tickTimer.Interval = 200;
            tickTimer.Tick += tickTimer_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(602, 12);
            label1.Name = "label1";
            label1.Size = new Size(84, 32);
            label1.TabIndex = 1;
            label1.Text = "Score:";
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            scoreLabel.Location = new Point(602, 55);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(22, 25);
            scoreLabel.TabIndex = 2;
            scoreLabel.Text = "0";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(813, 596);
            Controls.Add(scoreLabel);
            Controls.Add(label1);
            Controls.Add(mainImage);
            Name = "MainForm";
            Text = "Snake!";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            PreviewKeyDown += MainForm_PreviewKeyDown;
            Resize += MainForm_Resize;
            ((System.ComponentModel.ISupportInitialize)mainImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox mainImage;
        private System.Windows.Forms.Timer tickTimer;
        private Label label1;
        private Label scoreLabel;
    }
}
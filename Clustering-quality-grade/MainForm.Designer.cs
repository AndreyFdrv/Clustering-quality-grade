namespace Генератор_кластеризованных_множеств
{
    partial class MainForm
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
            this.HighQualityButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.MiddleQualityButton = new System.Windows.Forms.Button();
            this.LowQualityButton = new System.Windows.Forms.Button();
            this.QualityGradeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HighQualityButton
            // 
            this.HighQualityButton.Location = new System.Drawing.Point(535, 12);
            this.HighQualityButton.Name = "HighQualityButton";
            this.HighQualityButton.Size = new System.Drawing.Size(139, 47);
            this.HighQualityButton.TabIndex = 0;
            this.HighQualityButton.Text = "Кластеризация высокого качества";
            this.HighQualityButton.UseVisualStyleBackColor = true;
            this.HighQualityButton.Click += new System.EventHandler(this.HighQualityButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(517, 517);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // MiddleQualityButton
            // 
            this.MiddleQualityButton.Location = new System.Drawing.Point(535, 65);
            this.MiddleQualityButton.Name = "MiddleQualityButton";
            this.MiddleQualityButton.Size = new System.Drawing.Size(139, 47);
            this.MiddleQualityButton.TabIndex = 3;
            this.MiddleQualityButton.Text = "Кластеризация среднего качества";
            this.MiddleQualityButton.UseVisualStyleBackColor = true;
            this.MiddleQualityButton.Click += new System.EventHandler(this.MiddleQualityButton_Click);
            // 
            // LowQualityButton
            // 
            this.LowQualityButton.Location = new System.Drawing.Point(535, 118);
            this.LowQualityButton.Name = "LowQualityButton";
            this.LowQualityButton.Size = new System.Drawing.Size(139, 47);
            this.LowQualityButton.TabIndex = 4;
            this.LowQualityButton.Text = "Кластеризация низкого качества";
            this.LowQualityButton.UseVisualStyleBackColor = true;
            this.LowQualityButton.Click += new System.EventHandler(this.LowQualityButton_Click);
            // 
            // QualityGradeButton
            // 
            this.QualityGradeButton.Location = new System.Drawing.Point(535, 180);
            this.QualityGradeButton.Name = "QualityGradeButton";
            this.QualityGradeButton.Size = new System.Drawing.Size(139, 47);
            this.QualityGradeButton.TabIndex = 5;
            this.QualityGradeButton.Text = "Оценка качества";
            this.QualityGradeButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 541);
            this.Controls.Add(this.QualityGradeButton);
            this.Controls.Add(this.LowQualityButton);
            this.Controls.Add(this.MiddleQualityButton);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.HighQualityButton);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HighQualityButton;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button MiddleQualityButton;
        private System.Windows.Forms.Button LowQualityButton;
        private System.Windows.Forms.Button QualityGradeButton;
    }
}
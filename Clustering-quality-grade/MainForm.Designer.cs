namespace Clustering_quality_grade
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
            this.TimeCalculationButton = new System.Windows.Forms.Button();
            this.boundary_big = new System.Windows.Forms.Button();
            this.boundary_small = new System.Windows.Forms.Button();
            this.clustering_button = new System.Windows.Forms.Button();
            this.Non_clustered_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // HighQualityButton
            // 
            this.HighQualityButton.Location = new System.Drawing.Point(567, 12);
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
            this.pictureBox.Size = new System.Drawing.Size(549, 551);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // MiddleQualityButton
            // 
            this.MiddleQualityButton.Location = new System.Drawing.Point(567, 65);
            this.MiddleQualityButton.Name = "MiddleQualityButton";
            this.MiddleQualityButton.Size = new System.Drawing.Size(139, 47);
            this.MiddleQualityButton.TabIndex = 3;
            this.MiddleQualityButton.Text = "Кластеризация среднего качества";
            this.MiddleQualityButton.UseVisualStyleBackColor = true;
            this.MiddleQualityButton.Click += new System.EventHandler(this.MiddleQualityButton_Click);
            // 
            // LowQualityButton
            // 
            this.LowQualityButton.Location = new System.Drawing.Point(567, 118);
            this.LowQualityButton.Name = "LowQualityButton";
            this.LowQualityButton.Size = new System.Drawing.Size(139, 47);
            this.LowQualityButton.TabIndex = 4;
            this.LowQualityButton.Text = "Кластеризация низкого качества";
            this.LowQualityButton.UseVisualStyleBackColor = true;
            this.LowQualityButton.Click += new System.EventHandler(this.LowQualityButton_Click);
            // 
            // QualityGradeButton
            // 
            this.QualityGradeButton.Location = new System.Drawing.Point(567, 463);
            this.QualityGradeButton.Name = "QualityGradeButton";
            this.QualityGradeButton.Size = new System.Drawing.Size(139, 47);
            this.QualityGradeButton.TabIndex = 5;
            this.QualityGradeButton.Text = "Оценка качества";
            this.QualityGradeButton.UseVisualStyleBackColor = true;
            this.QualityGradeButton.Click += new System.EventHandler(this.QualityGradeButton_Click);
            // 
            // TimeCalculationButton
            // 
            this.TimeCalculationButton.Location = new System.Drawing.Point(567, 516);
            this.TimeCalculationButton.Name = "TimeCalculationButton";
            this.TimeCalculationButton.Size = new System.Drawing.Size(139, 47);
            this.TimeCalculationButton.TabIndex = 6;
            this.TimeCalculationButton.Text = "Расчёт времени работы";
            this.TimeCalculationButton.UseVisualStyleBackColor = true;
            this.TimeCalculationButton.Click += new System.EventHandler(this.TimeCalculationButton_Click);
            // 
            // boundary_big
            // 
            this.boundary_big.Location = new System.Drawing.Point(567, 236);
            this.boundary_big.Name = "boundary_big";
            this.boundary_big.Size = new System.Drawing.Size(139, 47);
            this.boundary_big.TabIndex = 8;
            this.boundary_big.Text = "Граничнй случай(большой радиус)";
            this.boundary_big.UseVisualStyleBackColor = true;
            this.boundary_big.Click += new System.EventHandler(this.boundary_big_Click);
            // 
            // boundary_small
            // 
            this.boundary_small.Location = new System.Drawing.Point(567, 183);
            this.boundary_small.Name = "boundary_small";
            this.boundary_small.Size = new System.Drawing.Size(139, 47);
            this.boundary_small.TabIndex = 7;
            this.boundary_small.Text = "Граничный случай(малый радиус)";
            this.boundary_small.UseVisualStyleBackColor = true;
            this.boundary_small.Click += new System.EventHandler(this.boundary_small_Click);
            // 
            // clustering_button
            // 
            this.clustering_button.Location = new System.Drawing.Point(567, 359);
            this.clustering_button.Name = "clustering_button";
            this.clustering_button.Size = new System.Drawing.Size(139, 47);
            this.clustering_button.TabIndex = 10;
            this.clustering_button.Text = "Применить алгоритм кластеризации";
            this.clustering_button.UseVisualStyleBackColor = true;
            this.clustering_button.Click += new System.EventHandler(this.clustering_button_Click);
            //
            // Non_clustered_button
            //
            this.Non_clustered_button.Location = new System.Drawing.Point(567, 306);
            this.Non_clustered_button.Name = "Non_clustered_button";
            this.Non_clustered_button.Size = new System.Drawing.Size(139, 47);
            this.Non_clustered_button.TabIndex = 9;
            this.Non_clustered_button.Text = "Некластеризованное множество";
            this.Non_clustered_button.UseVisualStyleBackColor = true;
            this.Non_clustered_button.Click += new System.EventHandler(this.Non_clustered_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 575);
            this.Controls.Add(this.clustering_button);
            this.Controls.Add(this.Non_clustered_button);
            this.Controls.Add(this.boundary_big);
            this.Controls.Add(this.boundary_small);
            this.Controls.Add(this.TimeCalculationButton);
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
        private System.Windows.Forms.Button TimeCalculationButton;
        private System.Windows.Forms.Button boundary_big;
        private System.Windows.Forms.Button boundary_small;
        private System.Windows.Forms.Button clustering_button;
        private System.Windows.Forms.Button Non_clustered_button;
    }
}
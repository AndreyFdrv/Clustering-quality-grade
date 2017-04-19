﻿namespace Clustering_quality_grade
{
    partial class TimeCalculationForm
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
            this.F1_meassure_rb = new System.Windows.Forms.RadioButton();
            this.Start = new System.Windows.Forms.Button();
            this.Calinski_Harabasz_rb = new System.Windows.Forms.RadioButton();
            this.Dunn_rb = new System.Windows.Forms.RadioButton();
            this.DBI_rb = new System.Windows.Forms.RadioButton();
            this.Scatter_Distance_rb = new System.Windows.Forms.RadioButton();
            this.Scatter_Density_rb = new System.Windows.Forms.RadioButton();
            this.RMSSTD_rb = new System.Windows.Forms.RadioButton();
            this.rand_rb = new System.Windows.Forms.RadioButton();
            this.jaccard_rb = new System.Windows.Forms.RadioButton();
            this.FM_rb = new System.Windows.Forms.RadioButton();
            this.hubert_rb = new System.Windows.Forms.RadioButton();
            this.modified_hubert_rb = new System.Windows.Forms.RadioButton();
            this.normalized_hubert_rb = new System.Windows.Forms.RadioButton();
            this.RS_index_rb = new System.Windows.Forms.RadioButton();
            this.Silhouette_rb = new System.Windows.Forms.RadioButton();
            this.Maulik_Bandyopadhyay_rb = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // F1_meassure_rb
            // 
            this.F1_meassure_rb.AutoSize = true;
            this.F1_meassure_rb.Location = new System.Drawing.Point(12, 12);
            this.F1_meassure_rb.Name = "F1_meassure_rb";
            this.F1_meassure_rb.Size = new System.Drawing.Size(66, 17);
            this.F1_meassure_rb.TabIndex = 0;
            this.F1_meassure_rb.TabStop = true;
            this.F1_meassure_rb.Text = "F1-мера";
            this.F1_meassure_rb.UseVisualStyleBackColor = true;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 384);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 1;
            this.Start.Text = "Начать";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Calinski_Harabasz_rb
            // 
            this.Calinski_Harabasz_rb.AutoSize = true;
            this.Calinski_Harabasz_rb.Location = new System.Drawing.Point(12, 104);
            this.Calinski_Harabasz_rb.Name = "Calinski_Harabasz_rb";
            this.Calinski_Harabasz_rb.Size = new System.Drawing.Size(160, 17);
            this.Calinski_Harabasz_rb.TabIndex = 2;
            this.Calinski_Harabasz_rb.TabStop = true;
            this.Calinski_Harabasz_rb.Text = "Критерий Calinski-Harabasz";
            this.Calinski_Harabasz_rb.UseVisualStyleBackColor = true;
            // 
            // Dunn_rb
            // 
            this.Dunn_rb.AutoSize = true;
            this.Dunn_rb.Location = new System.Drawing.Point(12, 127);
            this.Dunn_rb.Name = "Dunn_rb";
            this.Dunn_rb.Size = new System.Drawing.Size(99, 17);
            this.Dunn_rb.TabIndex = 3;
            this.Dunn_rb.TabStop = true;
            this.Dunn_rb.Text = "Индекс Данна";
            this.Dunn_rb.UseVisualStyleBackColor = true;
            // 
            // DBI_rb
            // 
            this.DBI_rb.AutoSize = true;
            this.DBI_rb.Location = new System.Drawing.Point(12, 150);
            this.DBI_rb.Name = "DBI_rb";
            this.DBI_rb.Size = new System.Drawing.Size(156, 17);
            this.DBI_rb.TabIndex = 4;
            this.DBI_rb.TabStop = true;
            this.DBI_rb.Text = "Индекс Девиса-Болдуина";
            this.DBI_rb.UseVisualStyleBackColor = true;
            // 
            // Scatter_Distance_rb
            // 
            this.Scatter_Distance_rb.AutoSize = true;
            this.Scatter_Distance_rb.Location = new System.Drawing.Point(12, 173);
            this.Scatter_Distance_rb.Name = "Scatter_Distance_rb";
            this.Scatter_Distance_rb.Size = new System.Drawing.Size(145, 17);
            this.Scatter_Distance_rb.TabIndex = 5;
            this.Scatter_Distance_rb.TabStop = true;
            this.Scatter_Distance_rb.Text = "Индекс Scatter-Distance";
            this.Scatter_Distance_rb.UseVisualStyleBackColor = true;
            // 
            // Scatter_Density_rb
            // 
            this.Scatter_Density_rb.AutoSize = true;
            this.Scatter_Density_rb.Location = new System.Drawing.Point(12, 196);
            this.Scatter_Density_rb.Name = "Scatter_Density_rb";
            this.Scatter_Density_rb.Size = new System.Drawing.Size(138, 17);
            this.Scatter_Density_rb.TabIndex = 6;
            this.Scatter_Density_rb.TabStop = true;
            this.Scatter_Density_rb.Text = "Индекс Scatter-Density";
            this.Scatter_Density_rb.UseVisualStyleBackColor = true;
            // 
            // RMSSTD_rb
            // 
            this.RMSSTD_rb.AutoSize = true;
            this.RMSSTD_rb.Location = new System.Drawing.Point(12, 219);
            this.RMSSTD_rb.Name = "RMSSTD_rb";
            this.RMSSTD_rb.Size = new System.Drawing.Size(112, 17);
            this.RMSSTD_rb.TabIndex = 7;
            this.RMSSTD_rb.TabStop = true;
            this.RMSSTD_rb.Text = "Индекс RMSSTD";
            this.RMSSTD_rb.UseVisualStyleBackColor = true;
            // 
            // rand_rb
            // 
            this.rand_rb.AutoSize = true;
            this.rand_rb.Location = new System.Drawing.Point(12, 35);
            this.rand_rb.Name = "rand_rb";
            this.rand_rb.Size = new System.Drawing.Size(92, 17);
            this.rand_rb.TabIndex = 8;
            this.rand_rb.TabStop = true;
            this.rand_rb.Text = "Индекс Rand";
            this.rand_rb.UseVisualStyleBackColor = true;
            // 
            // jaccard_rb
            // 
            this.jaccard_rb.AutoSize = true;
            this.jaccard_rb.Location = new System.Drawing.Point(12, 58);
            this.jaccard_rb.Name = "jaccard_rb";
            this.jaccard_rb.Size = new System.Drawing.Size(104, 17);
            this.jaccard_rb.TabIndex = 9;
            this.jaccard_rb.TabStop = true;
            this.jaccard_rb.Text = "Индекс Jaccard";
            this.jaccard_rb.UseVisualStyleBackColor = true;
            // 
            // FM_rb
            // 
            this.FM_rb.AutoSize = true;
            this.FM_rb.Location = new System.Drawing.Point(12, 81);
            this.FM_rb.Name = "FM_rb";
            this.FM_rb.Size = new System.Drawing.Size(81, 17);
            this.FM_rb.TabIndex = 10;
            this.FM_rb.TabStop = true;
            this.FM_rb.Text = "Индекс FM";
            this.FM_rb.UseVisualStyleBackColor = true;
            // 
            // hubert_rb
            // 
            this.hubert_rb.AutoSize = true;
            this.hubert_rb.Location = new System.Drawing.Point(12, 242);
            this.hubert_rb.Name = "hubert_rb";
            this.hubert_rb.Size = new System.Drawing.Size(133, 17);
            this.hubert_rb.TabIndex = 11;
            this.hubert_rb.TabStop = true;
            this.hubert_rb.Text = "Hubert\'s Г статистика";
            this.hubert_rb.UseVisualStyleBackColor = true;
            // 
            // modified_hubert_rb
            // 
            this.modified_hubert_rb.AutoSize = true;
            this.modified_hubert_rb.Location = new System.Drawing.Point(12, 265);
            this.modified_hubert_rb.Name = "modified_hubert_rb";
            this.modified_hubert_rb.Size = new System.Drawing.Size(237, 17);
            this.modified_hubert_rb.TabIndex = 12;
            this.modified_hubert_rb.TabStop = true;
            this.modified_hubert_rb.Text = "Модифицированная Hubert\'s Г статистика";
            this.modified_hubert_rb.UseVisualStyleBackColor = true;
            // 
            // normalized_hubert_rb
            // 
            this.normalized_hubert_rb.AutoSize = true;
            this.normalized_hubert_rb.Location = new System.Drawing.Point(12, 288);
            this.normalized_hubert_rb.Name = "normalized_hubert_rb";
            this.normalized_hubert_rb.Size = new System.Drawing.Size(230, 17);
            this.normalized_hubert_rb.TabIndex = 13;
            this.normalized_hubert_rb.TabStop = true;
            this.normalized_hubert_rb.Text = "Нормализованная Hubert\'s Г статистика";
            this.normalized_hubert_rb.UseVisualStyleBackColor = true;
            // 
            // RS_index_rb
            // 
            this.RS_index_rb.AutoSize = true;
            this.RS_index_rb.Location = new System.Drawing.Point(12, 311);
            this.RS_index_rb.Name = "RS_index_rb";
            this.RS_index_rb.Size = new System.Drawing.Size(81, 17);
            this.RS_index_rb.TabIndex = 14;
            this.RS_index_rb.TabStop = true;
            this.RS_index_rb.Text = "Индекс RS";
            this.RS_index_rb.UseVisualStyleBackColor = true;
            // 
            // Silhouette_rb
            // 
            this.Silhouette_rb.AutoSize = true;
            this.Silhouette_rb.Location = new System.Drawing.Point(12, 334);
            this.Silhouette_rb.Name = "Silhouette_rb";
            this.Silhouette_rb.Size = new System.Drawing.Size(107, 17);
            this.Silhouette_rb.TabIndex = 15;
            this.Silhouette_rb.TabStop = true;
            this.Silhouette_rb.Text = "Индекс Силуэта";
            this.Silhouette_rb.UseVisualStyleBackColor = true;
            // 
            // Maulik_Bandyopadhyay_rb
            // 
            this.Maulik_Bandyopadhyay_rb.AutoSize = true;
            this.Maulik_Bandyopadhyay_rb.Location = new System.Drawing.Point(12, 357);
            this.Maulik_Bandyopadhyay_rb.Name = "Maulik_Bandyopadhyay_rb";
            this.Maulik_Bandyopadhyay_rb.Size = new System.Drawing.Size(180, 17);
            this.Maulik_Bandyopadhyay_rb.TabIndex = 16;
            this.Maulik_Bandyopadhyay_rb.TabStop = true;
            this.Maulik_Bandyopadhyay_rb.Text = "Индекс Маулика-Бандиопадия";
            this.Maulik_Bandyopadhyay_rb.UseVisualStyleBackColor = true;
            // 
            // TimeCalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 419);
            this.Controls.Add(this.Maulik_Bandyopadhyay_rb);
            this.Controls.Add(this.Silhouette_rb);
            this.Controls.Add(this.RS_index_rb);
            this.Controls.Add(this.normalized_hubert_rb);
            this.Controls.Add(this.modified_hubert_rb);
            this.Controls.Add(this.hubert_rb);
            this.Controls.Add(this.FM_rb);
            this.Controls.Add(this.jaccard_rb);
            this.Controls.Add(this.rand_rb);
            this.Controls.Add(this.RMSSTD_rb);
            this.Controls.Add(this.Scatter_Density_rb);
            this.Controls.Add(this.Scatter_Distance_rb);
            this.Controls.Add(this.DBI_rb);
            this.Controls.Add(this.Dunn_rb);
            this.Controls.Add(this.Calinski_Harabasz_rb);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.F1_meassure_rb);
            this.Name = "TimeCalculationForm";
            this.Text = "Расчёт времени работы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton F1_meassure_rb;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.RadioButton Calinski_Harabasz_rb;
        private System.Windows.Forms.RadioButton Dunn_rb;
        private System.Windows.Forms.RadioButton DBI_rb;
        private System.Windows.Forms.RadioButton Scatter_Distance_rb;
        private System.Windows.Forms.RadioButton Scatter_Density_rb;
        private System.Windows.Forms.RadioButton RMSSTD_rb;
        private System.Windows.Forms.RadioButton rand_rb;
        private System.Windows.Forms.RadioButton jaccard_rb;
        private System.Windows.Forms.RadioButton FM_rb;
        private System.Windows.Forms.RadioButton hubert_rb;
        private System.Windows.Forms.RadioButton modified_hubert_rb;
        private System.Windows.Forms.RadioButton normalized_hubert_rb;
        private System.Windows.Forms.RadioButton RS_index_rb;
        private System.Windows.Forms.RadioButton Silhouette_rb;
        private System.Windows.Forms.RadioButton Maulik_Bandyopadhyay_rb;
    }
}
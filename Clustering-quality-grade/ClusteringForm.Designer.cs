namespace Clustering_quality_grade
{
    partial class ClusteringForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.k_means_rb = new System.Windows.Forms.RadioButton();
            this.DBSCAN_rb = new System.Windows.Forms.RadioButton();
            this.neighbor_method_rb = new System.Windows.Forms.RadioButton();
            this.c_means_rb = new System.Windows.Forms.RadioButton();
            this.EM_algorithm_rb = new System.Windows.Forms.RadioButton();
            this.Run_clustering_buttun = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите алгоритм кластеризации:";
            // 
            // k_means_rb
            // 
            this.k_means_rb.AutoSize = true;
            this.k_means_rb.Location = new System.Drawing.Point(16, 29);
            this.k_means_rb.Name = "k_means_rb";
            this.k_means_rb.Size = new System.Drawing.Size(65, 17);
            this.k_means_rb.TabIndex = 1;
            this.k_means_rb.TabStop = true;
            this.k_means_rb.Text = "k-means";
            this.k_means_rb.UseVisualStyleBackColor = true;
            // 
            // DBSCAN_rb
            // 
            this.DBSCAN_rb.AutoSize = true;
            this.DBSCAN_rb.Location = new System.Drawing.Point(16, 52);
            this.DBSCAN_rb.Name = "DBSCAN_rb";
            this.DBSCAN_rb.Size = new System.Drawing.Size(69, 17);
            this.DBSCAN_rb.TabIndex = 2;
            this.DBSCAN_rb.TabStop = true;
            this.DBSCAN_rb.Text = "DBSCAN";
            this.DBSCAN_rb.UseVisualStyleBackColor = true;
            // 
            // neighbor_method_rb
            // 
            this.neighbor_method_rb.AutoSize = true;
            this.neighbor_method_rb.Location = new System.Drawing.Point(16, 75);
            this.neighbor_method_rb.Name = "neighbor_method_rb";
            this.neighbor_method_rb.Size = new System.Drawing.Size(161, 17);
            this.neighbor_method_rb.TabIndex = 3;
            this.neighbor_method_rb.TabStop = true;
            this.neighbor_method_rb.Text = "метод ближайшего соседа";
            this.neighbor_method_rb.UseVisualStyleBackColor = true;
            // 
            // c_means_rb
            // 
            this.c_means_rb.AutoSize = true;
            this.c_means_rb.Location = new System.Drawing.Point(16, 98);
            this.c_means_rb.Name = "c_means_rb";
            this.c_means_rb.Size = new System.Drawing.Size(65, 17);
            this.c_means_rb.TabIndex = 4;
            this.c_means_rb.TabStop = true;
            this.c_means_rb.Text = "с-means";
            this.c_means_rb.UseVisualStyleBackColor = true;
            // 
            // EM_algorithm_rb
            // 
            this.EM_algorithm_rb.AutoSize = true;
            this.EM_algorithm_rb.Location = new System.Drawing.Point(16, 121);
            this.EM_algorithm_rb.Name = "EM_algorithm_rb";
            this.EM_algorithm_rb.Size = new System.Drawing.Size(92, 17);
            this.EM_algorithm_rb.TabIndex = 5;
            this.EM_algorithm_rb.TabStop = true;
            this.EM_algorithm_rb.Text = "EM-алгоритм";
            this.EM_algorithm_rb.UseVisualStyleBackColor = true;
            // 
            // Run_clustering_buttun
            // 
            this.Run_clustering_buttun.Location = new System.Drawing.Point(12, 144);
            this.Run_clustering_buttun.Name = "Run_clustering_buttun";
            this.Run_clustering_buttun.Size = new System.Drawing.Size(98, 37);
            this.Run_clustering_buttun.TabIndex = 6;
            this.Run_clustering_buttun.Text = "Запуск кластеризации";
            this.Run_clustering_buttun.UseVisualStyleBackColor = true;
            this.Run_clustering_buttun.Click += new System.EventHandler(this.Run_clustering_buttun_Click);
            // 
            // ClusteringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 189);
            this.Controls.Add(this.Run_clustering_buttun);
            this.Controls.Add(this.EM_algorithm_rb);
            this.Controls.Add(this.c_means_rb);
            this.Controls.Add(this.neighbor_method_rb);
            this.Controls.Add(this.DBSCAN_rb);
            this.Controls.Add(this.k_means_rb);
            this.Controls.Add(this.label1);
            this.Name = "ClusteringForm";
            this.Load += new System.EventHandler(this.ClusteringForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton k_means_rb;
        private System.Windows.Forms.RadioButton DBSCAN_rb;
        private System.Windows.Forms.RadioButton neighbor_method_rb;
        private System.Windows.Forms.RadioButton c_means_rb;
        private System.Windows.Forms.RadioButton EM_algorithm_rb;
        private System.Windows.Forms.Button Run_clustering_buttun;
    }
}
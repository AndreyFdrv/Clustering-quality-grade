namespace Clustering_quality_grade
{
    partial class TestClusteringForm
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
            this.LikeClassInfo_rb = new System.Windows.Forms.RadioButton();
            this.ClusteringAlgorithms_rb = new System.Windows.Forms.RadioButton();
            this.Random_rb = new System.Windows.Forms.RadioButton();
            this.Cluster_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LikeClassInfo_rb
            // 
            this.LikeClassInfo_rb.AutoSize = true;
            this.LikeClassInfo_rb.Location = new System.Drawing.Point(13, 13);
            this.LikeClassInfo_rb.Name = "LikeClassInfo_rb";
            this.LikeClassInfo_rb.Size = new System.Drawing.Size(382, 17);
            this.LikeClassInfo_rb.TabIndex = 0;
            this.LikeClassInfo_rb.TabStop = true;
            this.LikeClassInfo_rb.Text = "Создать множество кластеров, совпадающее с множеством классов";
            this.LikeClassInfo_rb.UseVisualStyleBackColor = true;
            // 
            // ClusteringAlgorithms_rb
            // 
            this.ClusteringAlgorithms_rb.AutoSize = true;
            this.ClusteringAlgorithms_rb.Location = new System.Drawing.Point(13, 36);
            this.ClusteringAlgorithms_rb.Name = "ClusteringAlgorithms_rb";
            this.ClusteringAlgorithms_rb.Size = new System.Drawing.Size(213, 17);
            this.ClusteringAlgorithms_rb.TabIndex = 1;
            this.ClusteringAlgorithms_rb.TabStop = true;
            this.ClusteringAlgorithms_rb.Text = "Применить алгоритм кластеризации";
            this.ClusteringAlgorithms_rb.UseVisualStyleBackColor = true;
            // 
            // Random_rb
            // 
            this.Random_rb.AutoSize = true;
            this.Random_rb.Location = new System.Drawing.Point(13, 59);
            this.Random_rb.Name = "Random_rb";
            this.Random_rb.Size = new System.Drawing.Size(293, 17);
            this.Random_rb.TabIndex = 2;
            this.Random_rb.TabStop = true;
            this.Random_rb.Text = "Разбить объекты по кластерам случайным образом";
            this.Random_rb.UseVisualStyleBackColor = true;
            // 
            // Cluster_button
            // 
            this.Cluster_button.Location = new System.Drawing.Point(13, 83);
            this.Cluster_button.Name = "Cluster_button";
            this.Cluster_button.Size = new System.Drawing.Size(108, 44);
            this.Cluster_button.TabIndex = 3;
            this.Cluster_button.Text = "Провести кластеризацию";
            this.Cluster_button.UseVisualStyleBackColor = true;
            this.Cluster_button.Click += new System.EventHandler(this.Cluster_button_Click);
            // 
            // TestClusteringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 132);
            this.Controls.Add(this.Cluster_button);
            this.Controls.Add(this.Random_rb);
            this.Controls.Add(this.ClusteringAlgorithms_rb);
            this.Controls.Add(this.LikeClassInfo_rb);
            this.Name = "TestClusteringForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton LikeClassInfo_rb;
        private System.Windows.Forms.RadioButton ClusteringAlgorithms_rb;
        private System.Windows.Forms.RadioButton Random_rb;
        private System.Windows.Forms.Button Cluster_button;
    }
}
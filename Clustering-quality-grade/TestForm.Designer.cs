namespace Clustering_quality_grade
{
    partial class TestForm
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
            this.ClusterButton = new System.Windows.Forms.Button();
            this.QualityButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ClusterButton
            // 
            this.ClusterButton.Location = new System.Drawing.Point(13, 13);
            this.ClusterButton.Name = "ClusterButton";
            this.ClusterButton.Size = new System.Drawing.Size(213, 44);
            this.ClusterButton.TabIndex = 0;
            this.ClusterButton.Text = "Провести кластеризацию";
            this.ClusterButton.UseVisualStyleBackColor = true;
            this.ClusterButton.Click += new System.EventHandler(this.ClusterButton_Click);
            // 
            // QualityButton
            // 
            this.QualityButton.Location = new System.Drawing.Point(13, 63);
            this.QualityButton.Name = "QualityButton";
            this.QualityButton.Size = new System.Drawing.Size(213, 44);
            this.QualityButton.TabIndex = 1;
            this.QualityButton.Text = "Оценить качество";
            this.QualityButton.UseVisualStyleBackColor = true;
            this.QualityButton.Click += new System.EventHandler(this.QualityButton_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 118);
            this.Controls.Add(this.QualityButton);
            this.Controls.Add(this.ClusterButton);
            this.Name = "TestForm";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ClusterButton;
        private System.Windows.Forms.Button QualityButton;
    }
}
namespace Clustering_quality_grade
{
    partial class QualityForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.Iterative_rb = new System.Windows.Forms.RadioButton();
            this.Density_rb = new System.Windows.Forms.RadioButton();
            this.Hierarchical_rb = new System.Windows.Forms.RadioButton();
            this.Fuzzy_rb = new System.Windows.Forms.RadioButton();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите группу модификаций критериев";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "оценки качества кластеризации";
            // 
            // Iterative_rb
            // 
            this.Iterative_rb.AutoSize = true;
            this.Iterative_rb.Location = new System.Drawing.Point(13, 43);
            this.Iterative_rb.Name = "Iterative_rb";
            this.Iterative_rb.Size = new System.Drawing.Size(190, 17);
            this.Iterative_rb.TabIndex = 2;
            this.Iterative_rb.TabStop = true;
            this.Iterative_rb.Text = "для итеративной кластеризации";
            this.Iterative_rb.UseVisualStyleBackColor = true;
            // 
            // Density_rb
            // 
            this.Density_rb.AutoSize = true;
            this.Density_rb.Location = new System.Drawing.Point(12, 66);
            this.Density_rb.Name = "Density_rb";
            this.Density_rb.Size = new System.Drawing.Size(190, 17);
            this.Density_rb.TabIndex = 3;
            this.Density_rb.TabStop = true;
            this.Density_rb.Text = "для плотностной кластеризации";
            this.Density_rb.UseVisualStyleBackColor = true;
            // 
            // Hierarchical_rb
            // 
            this.Hierarchical_rb.AutoSize = true;
            this.Hierarchical_rb.Location = new System.Drawing.Point(13, 89);
            this.Hierarchical_rb.Name = "Hierarchical_rb";
            this.Hierarchical_rb.Size = new System.Drawing.Size(202, 17);
            this.Hierarchical_rb.TabIndex = 4;
            this.Hierarchical_rb.TabStop = true;
            this.Hierarchical_rb.Text = "для иерархической кластеризации";
            this.Hierarchical_rb.UseVisualStyleBackColor = true;
            // 
            // Fuzzy_rb
            // 
            this.Fuzzy_rb.AutoSize = true;
            this.Fuzzy_rb.Location = new System.Drawing.Point(12, 112);
            this.Fuzzy_rb.Name = "Fuzzy_rb";
            this.Fuzzy_rb.Size = new System.Drawing.Size(172, 17);
            this.Fuzzy_rb.TabIndex = 5;
            this.Fuzzy_rb.TabStop = true;
            this.Fuzzy_rb.Text = "для нечёткой кластеризации";
            this.Fuzzy_rb.UseVisualStyleBackColor = true;
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(12, 135);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 6;
            this.ApplyButton.Text = "Применить";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // QualityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 165);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.Fuzzy_rb);
            this.Controls.Add(this.Hierarchical_rb);
            this.Controls.Add(this.Density_rb);
            this.Controls.Add(this.Iterative_rb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "QualityForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Iterative_rb;
        private System.Windows.Forms.RadioButton Density_rb;
        private System.Windows.Forms.RadioButton Hierarchical_rb;
        private System.Windows.Forms.RadioButton Fuzzy_rb;
        private System.Windows.Forms.Button ApplyButton;
    }
}
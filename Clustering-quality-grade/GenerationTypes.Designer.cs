namespace Clustering_quality_grade
{
    partial class GenerationTypes
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
            this.without_noise_rb = new System.Windows.Forms.RadioButton();
            this.with_noise_rb = new System.Windows.Forms.RadioButton();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // without_noise_rb
            // 
            this.without_noise_rb.AutoSize = true;
            this.without_noise_rb.Location = new System.Drawing.Point(12, 33);
            this.without_noise_rb.Name = "without_noise_rb";
            this.without_noise_rb.Size = new System.Drawing.Size(73, 17);
            this.without_noise_rb.TabIndex = 0;
            this.without_noise_rb.TabStop = true;
            this.without_noise_rb.Text = "без шума";
            this.without_noise_rb.UseVisualStyleBackColor = true;
            // 
            // with_noise_rb
            // 
            this.with_noise_rb.AutoSize = true;
            this.with_noise_rb.Location = new System.Drawing.Point(12, 56);
            this.with_noise_rb.Name = "with_noise_rb";
            this.with_noise_rb.Size = new System.Drawing.Size(69, 17);
            this.with_noise_rb.TabIndex = 1;
            this.with_noise_rb.TabStop = true;
            this.with_noise_rb.Text = "с шумом";
            this.with_noise_rb.UseVisualStyleBackColor = true;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(12, 83);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(106, 32);
            this.GenerateButton.TabIndex = 2;
            this.GenerateButton.Text = "Сгенерировать";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите тип множества";
            // 
            // GenerationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 126);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GenerateButton);
            this.Controls.Add(this.with_noise_rb);
            this.Controls.Add(this.without_noise_rb);
            this.Name = "GenerationTypes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton without_noise_rb;
        private System.Windows.Forms.RadioButton with_noise_rb;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.Label label1;
    }
}
namespace Clustering_quality_grade
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
            this.Start.Location = new System.Drawing.Point(12, 141);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 1;
            this.Start.Text = "Начать";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // TimeCalculationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 176);
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
    }
}
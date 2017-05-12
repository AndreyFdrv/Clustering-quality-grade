using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clustering_quality_grade
{
    public partial class DendrogramForm : Form
    {
        Dendrogram dendrogram;
        Bitmap bitmap;
        Graphics gr;
        public DendrogramForm(Dendrogram dendrogram)
        {
            InitializeComponent();
            this.dendrogram = dendrogram;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(bitmap);
        }
        private void DrawDendrogram(Dendrogram dendrogram, int left_x, int y, int width, Pen pen, SolidBrush brush)
        {
            if(dendrogram.HasValue)
            {
                gr.DrawString(dendrogram.value.ToString(), new Font("Arial", 8), brush, left_x+width/2-5, y);
                return;
            }
            gr.DrawLine(pen, left_x, y, left_x + width, y);
            gr.DrawLine(pen, left_x, y, left_x, y+50);
            gr.DrawLine(pen, left_x+width, y, left_x+width, y + 50);
            DrawDendrogram(dendrogram.left, left_x-width/4, y+50, width/2, pen, brush);
            DrawDendrogram(dendrogram.right, left_x+width-width/4, y + 50, width / 2, pen, brush);
        }
        private void DendrogramForm_Load(object sender, EventArgs e)
        {
            Pen pen = new Pen(System.Drawing.Color.Black);
            SolidBrush brush=new SolidBrush(System.Drawing.Color.Black);
            DrawDendrogram(dendrogram, pictureBox.Width / 4, 10, pictureBox.Width / 2, pen, brush);
            pictureBox.Image = bitmap;
        }
    }
}

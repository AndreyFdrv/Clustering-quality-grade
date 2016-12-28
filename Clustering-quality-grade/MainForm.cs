using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Генератор_кластеризованных_множеств
{
    public partial class MainForm : Form
    {
        ArrayList points=new ArrayList();
        const int point_radius = 3;
        const int cluster_size = 10;
        Random rand = new Random();
        Bitmap bitmap;
        Graphics gr;
        public MainForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(bitmap);
        }
        void CreateCluster(Point center, int radius, SolidBrush myBrush)
        {
            for (int i = 0; i < cluster_size; i++)
            {
                int r = rand.Next(0, radius);
                double alpha = rand.Next(0, 359);
                int x =center.x+(int)(r * Math.Cos(alpha));
                int y =center.y+(int)(r * Math.Sin(alpha));
                gr.FillEllipse(myBrush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point=new Point(x, y);
                if (myBrush.Color == Color.Red)
                    point.cluster_number = 1;
                else if (myBrush.Color == Color.Green)
                    point.cluster_number = 2;
                else
                    point.cluster_number = 3;
                points.Add(point);
            }
        }
        void CreateRandomizedCluster(Point center, int radius)
        {
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            for (int i = 0; i < cluster_size; i++)
            {
                int r = rand.Next(0, radius);
                double alpha = rand.Next(0, 359);
                int x = center.x + (int)(r * Math.Cos(alpha));
                int y = center.y + (int)(r * Math.Sin(alpha));
                int color_number = rand.Next(0, 3);
                if (color_number == 0)
                    myBrush.Color = Color.Red;
                if (color_number == 1)
                    myBrush.Color = Color.Green;
                if (color_number == 2)
                    myBrush.Color = Color.Blue;
                gr.FillEllipse(myBrush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point = new Point(x, y);
                point.cluster_number = color_number + 1;
                points.Add(point);
            }
        }
        private void HighQualityButton_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            int radius = rand.Next(20, 60);
            int x = rand.Next(radius, pictureBox.Width - radius);
            int y = rand.Next(radius, pictureBox.Width - radius);
            Point p1=new Point(x, y);
            CreateCluster(p1, radius, myBrush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p2 = new Point(x, y);
            myBrush.Color = Color.Green;
            CreateCluster(p2, radius, myBrush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p3 = new Point(x, y);
            myBrush.Color = Color.Blue;
            CreateCluster(p3, radius, myBrush);
            pictureBox.Image = bitmap;
        }
        private void MiddleQualityButton_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            int radius = rand.Next(20, 60);
            int x = rand.Next(radius, pictureBox.Width - radius);
            int y = rand.Next(radius, pictureBox.Width - radius);
            Point p1 = new Point(x, y);
            CreateCluster(p1, radius, myBrush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p2 = new Point(x, y);
            myBrush.Color = Color.Green;
            CreateCluster(p2, radius, myBrush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p3 = new Point(x, y);
            CreateRandomizedCluster(p3, radius);
            pictureBox.Image = bitmap;
        }

        private void LowQualityButton_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);
            int radius = rand.Next(20, 60);
            int x = rand.Next(radius, pictureBox.Width - radius);
            int y = rand.Next(radius, pictureBox.Width - radius);
            Point p1 = new Point(x, y);
            CreateRandomizedCluster(p1, radius);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p2 = new Point(x, y);
            CreateRandomizedCluster(p2, radius);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p3 = new Point(x, y);
            CreateRandomizedCluster(p3, radius);
            pictureBox.Image = bitmap;
        }
    }
}
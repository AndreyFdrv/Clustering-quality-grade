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
using System.IO;
namespace Clustering_quality_grade
{
    public partial class MainForm : Form
    {
        ArrayList points=new ArrayList();
        const int point_radius = 3;
        int cluster_size = 10;
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
                int x =(int)center.coordinates[0]+(int)(r * Math.Cos(alpha));
                int y =(int)center.coordinates[1]+(int)(r * Math.Sin(alpha));
                gr.FillEllipse(myBrush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point=new Point(x, y);
                if (myBrush.Color == Color.Red)
                    point.cluster_number = 1;
                else if (myBrush.Color == Color.Green)
                    point.cluster_number = 2;
                else if(myBrush.Color==Color.Blue)
                    point.cluster_number = 3;
                else
                    point.cluster_number = 0;
                points.Add(point);
            }
        }
        void CreateNormalDistributionCluster(Point center, int radius)
        {
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            NormalRandom nr = new NormalRandom();
            for (int i = 0; i < cluster_size; i++)
            {
                int dispersion=rand.Next(10, 30);
                double x = nr.RandomGauss((int)center.coordinates[0], dispersion);
                dispersion = rand.Next(10, 30);
                double y = nr.RandomGauss((int)center.coordinates[1], dispersion);
                gr.FillEllipse(myBrush, new Rectangle((int)x - point_radius, (int)y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point = new Point((int)x, (int)y);
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
                int x = (int)center.coordinates[0] + (int)(r * Math.Cos(alpha));
                int y = (int)center.coordinates[1] + (int)(r * Math.Sin(alpha));
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
            cluster_size = 10;
            points.Clear();
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
            cluster_size = 10;
            points.Clear();
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
            cluster_size = 10;
            points.Clear();
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
        private ArrayList CreateClusterInfo()
        {
            ArrayList ClusterInfo = new ArrayList();
            for (int i = 0; i < points.Count; i++)
                ClusterInfo.Add(((Point)points[i]).cluster_number);
            return ClusterInfo;
        }
        private ArrayList CreateClassInfo()
        {
            ArrayList ClassInfo = new ArrayList();
            for (int i = 0; i < cluster_size; i++)
                ClassInfo.Add(1);
            for (int i = cluster_size; i < 2 * cluster_size; i++)
                ClassInfo.Add(2);
            for (int i = 2 * cluster_size; i < 3 * cluster_size; i++)
                ClassInfo.Add(3);
            return ClassInfo;
        }
        private void QualityGradeButton_Click(object sender, EventArgs e)
        {
            ArrayList ClusterInfo = CreateClusterInfo();
            ArrayList ClassInfo = CreateClassInfo();
            F1_meassure f1_meassure = new F1_meassure(ClusterInfo, ClassInfo);
            String output = "F1-мера: " + f1_meassure.F1() + "\r\n";
            Rand_Jaccard_FM rand_jaccard_fm = new Rand_Jaccard_FM(ClusterInfo, ClassInfo);
            output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
            output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
            output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
            /*Calinski_Harabasz_criterion CHC = new Calinski_Harabasz_criterion(points);
            output += "Критерий Calinski Harabasz: " + CHC.compute_criterion() + "\r\n";
            Dunn_index dunn = new Dunn_index(points);
            output += "Индекс Данна: " + dunn.compute() + "\r\n";
            Davies_Bouldin_index DBI= new Davies_Bouldin_index(points);
            output += "Индекс Девиса-Болдуина: " + DBI.compute() + "\r\n";
            Scatter_Distance_index Scatter_Distance = new Scatter_Distance_index(points);
            output += "Индекс Scatter-Distance: " + Scatter_Distance.compute(1) + "\r\n";
            Scatter_Density_index Scatter_Density = new Scatter_Density_index(points);
            output += "Индекс Scatter-Density: " + Scatter_Density.compute() + "\r\n";
            RMSSTD rmsstd = new RMSSTD(points);
            output += "Индекс RMSSTD: " + rmsstd.compute() + "\r\n";
            Hubert_Gamma_Statistic hubert = new Hubert_Gamma_Statistic(points);
            output += "Huberts's Г статистика: " + hubert.compute() + "\r\n";
            Modified_Hubert_Gamma_Statistic modified_hubert = new Modified_Hubert_Gamma_Statistic(points);
            output += "Модифицированная Huberts's Г статистика: " + modified_hubert.compute() + "\r\n";
            Normalized_Hubert_Gamma_Statistic normalized_hubert = new Normalized_Hubert_Gamma_Statistic(points);
            output += "Нормализованная Huberts's Г статистика: " + normalized_hubert.compute() + "\r\n";
            RS_Index rs_index = new RS_Index(points);
            output += "RS индекс: " + rs_index.compute() + "\r\n";
            SilhouetteIndex silhouette_index = new SilhouetteIndex(points);
            output += "Индекс Силуэта: " + silhouette_index.compute() + "\r\n";
            Maulik_Bandyopadhyay_index maulik_bandyopadhyay_index = new Maulik_Bandyopadhyay_index(points);
            output += "Индекс Маулика-Бандиопадия: " + maulik_bandyopadhyay_index.compute() + "\r\n";*/
            MessageBox.Show(output);
        }

        private void TimeCalculationButton_Click(object sender, EventArgs e)
        {
            TimeCalculationForm form = new TimeCalculationForm();
            form.ShowDialog();
        }
        private void generate_boundary(int radius)
        {
            points.Clear();
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            int x = radius;
            int y = radius;
            Point p1 = new Point(x, y);
            CreateCluster(p1, radius, myBrush);
            x = 3 *radius;
            y = radius;
            Point p2 = new Point(x, y);
            myBrush.Color = Color.Green;
            CreateCluster(p2, radius, myBrush);
            x = 2*radius;
            y = radius+(int)(2*(double)radius*Math.Sin(Math.PI/3));
            Point p3 = new Point(x, y);
            myBrush.Color = Color.Blue;
            CreateCluster(p3, radius, myBrush);
            pictureBox.Image = bitmap;
        }
        private void boundary_small_Click(object sender, EventArgs e)
        {
            cluster_size = 100;
            generate_boundary(20);
        }

        private void boundary_big_Click(object sender, EventArgs e)
        {
            cluster_size = 100;
            generate_boundary(100);
        }

        private void Non_clustered_button_Click(object sender, EventArgs e)
        {
            cluster_size = 10;
            points.Clear();
            gr.Clear(Color.White);
            SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            int radius = rand.Next(20, 60);
            int x = rand.Next(radius, pictureBox.Width - radius);
            int y = rand.Next(radius, pictureBox.Width - radius);
            Point p1 = new Point(x, y);
            CreateCluster(p1, radius, brush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p2 = new Point(x, y);
            CreateCluster(p2, radius, brush);
            radius = rand.Next(20, 60);
            x = rand.Next(radius, pictureBox.Width - radius);
            y = rand.Next(radius, pictureBox.Width - radius);
            Point p3 = new Point(x, y);
            CreateCluster(p3, radius, brush);
            pictureBox.Image = bitmap;
        }
        private void ColorizeClusters()
        {
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            for (int i = 0; i < points.Count; i++)
            {
                if(((Point)points[i]).cluster_number==1)
                    myBrush.Color = Color.Red;
                else if(((Point)points[i]).cluster_number==2)
                    myBrush.Color = Color.Green;
                else if(((Point)points[i]).cluster_number==3)
                    myBrush.Color = Color.Blue;
                else
                    myBrush.Color = Color.Black;
                int x = (int)((Point)points[i]).coordinates[0];
                int y = (int)((Point)points[i]).coordinates[1];
                gr.FillEllipse(myBrush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
            }
            pictureBox.Image = bitmap;
        }
        private void EM_clustering_button_Click(object sender, EventArgs e)
        {
            ClusteringForm form = new ClusteringForm(points, pictureBox.Width, pictureBox.Height);
            form.ShowDialog();
            if (form.isClustered)
            {
                points = form.getPoints();
                ColorizeClusters();
            }
        }
    }
}
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
        int point_radius = 3;
        int cluster_size = 10;
        Random rand = new Random();
        Bitmap bitmap;
        Graphics gr;
        int noise_count = 10;
        Dendrogram dendrogram;
        bool isHierarchicalClustering = false;
        bool isFuzzyClustering = false;
        ArrayList MembershipMatrix;
        public MainForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(bitmap);
        }
        void CreateCluster(Point center, int radius, SolidBrush myBrush, bool isFuzzyClustering)
        {
            for (int i = 0; i < cluster_size; i++)
            {
                int r = rand.Next(0, radius);
                double alpha = rand.Next(0, 359);
                int x =(int)center.coordinates[0]+(int)(r * Math.Cos(alpha));
                int y =(int)center.coordinates[1]+(int)(r * Math.Sin(alpha));
                gr.FillEllipse(myBrush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
                if (isFuzzyClustering)
                {
                    FuzzyClusteringPoint point = new FuzzyClusteringPoint(x, y);
                    points.Add(point);
                }
                else
                {
                    Point point = new Point(x, y);
                    if (myBrush.Color == Color.Red)
                        point.cluster_number = 1;
                    else if (myBrush.Color == Color.Green)
                        point.cluster_number = 2;
                    else if (myBrush.Color == Color.Blue)
                        point.cluster_number = 3;
                    else
                        point.cluster_number = 0;
                    points.Add(point);
                }
            }
        }
        void CreateNoise(SolidBrush brush)
        {
            for (int i = 0; i < 10; i++)
            {
                int x = rand.Next(0, pictureBox.Width);
                int y = rand.Next(0, pictureBox.Height);
                gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point = new Point(x, y);
                if (brush.Color == Color.Red)
                    point.cluster_number = 1;
                else if (brush.Color == Color.Green)
                    point.cluster_number = 2;
                else if (brush.Color == Color.Blue)
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
            if (isHierarchicalClustering)
            {
                for(int i=0; i<points.Count; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(i / 9 + 1);
                    row.Add(i / 3 + 1);
                    ClassInfo.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < cluster_size; i++)
                    ClassInfo.Add(1);
                for (int i = cluster_size; i < 2 * cluster_size; i++)
                    ClassInfo.Add(2);
                for (int i = 2 * cluster_size; i < 3 * cluster_size; i++)
                    ClassInfo.Add(3);
                for (int i = 3 * cluster_size; i < 3 * cluster_size + noise_count; i++)
                    ClassInfo.Add(0);
            }
            return ClassInfo;
        }
        private void QualityGradeButton_Click(object sender, EventArgs e)
        {
            String output;
            if (isHierarchicalClustering)
            {
                ArrayList ClassInfo = CreateClassInfo();
                Hierarchical_F1_meassure f1_meassure = new Hierarchical_F1_meassure(dendrogram, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Hierarchical_Rand_Jaccard_FM rand_jaccard_fm = new Hierarchical_Rand_Jaccard_FM(dendrogram, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
            }
            else
            {
                ArrayList ClusterInfo = CreateClusterInfo();
                ArrayList ClassInfo = CreateClassInfo();
                F1_meassure f1_meassure = new F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Rand_Jaccard_FM rand_jaccard_fm = new Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
            }
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
        private void GenerateForHierarchicalClustering(int depth, int x0, int y0, int width, int height, SolidBrush brush)
        {
            double offset_factor = 0.2;
            x0 += (int)(offset_factor * width);
            y0 += (int)(offset_factor * height);
            width -= 2*(int)(offset_factor * width);
            height -= 2*(int)(offset_factor * height);
            if(depth==0)
            {
                int x = rand.Next(x0, x0+width);
                int y = rand.Next(y0, y0+height);
                FuzzyClusteringPoint point = new FuzzyClusteringPoint(x, y);
                points.Add(point);
                gr.DrawString((points.Count-1).ToString(), new Font("Arial", 8), brush, x-6, y-15);
                gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                    2 * point_radius, 2 * point_radius));
            }
            else
            {
                GenerateForHierarchicalClustering(depth - 1, x0, y0, width / 2, height / 2, brush);
                GenerateForHierarchicalClustering(depth - 1, x0 + width / 2, y0, width / 2, height / 2, brush);
                GenerateForHierarchicalClustering(depth - 1, x0+width/4, y0 + height / 2, width/2, height / 2, brush);
            }
        }
        private void Non_clustered_button_Click(object sender, EventArgs e)
        {
            GenerationTypes form = new GenerationTypes();
            form.ShowDialog();
            if (!form.isGenerationButtonPressed)
                return;
            points.Clear();
            gr.Clear(Color.White);
            SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            if (form.isForHierarchicalClustering)
                GenerateForHierarchicalClustering(3, 0, 0, pictureBox.Width, pictureBox.Height, brush);
            else
            {
                cluster_size = 10;
                int radius = rand.Next(20, 60);
                int x = rand.Next(radius, pictureBox.Width - radius);
                int y = rand.Next(radius, pictureBox.Width - radius);
                Point p1 = new Point(x, y);
                CreateCluster(p1, radius, brush, form.isForFuzzyClustering);
                radius = rand.Next(20, 60);
                x = rand.Next(radius, pictureBox.Width - radius);
                y = rand.Next(radius, pictureBox.Width - radius);
                Point p2 = new Point(x, y);
                CreateCluster(p2, radius, brush, form.isForFuzzyClustering);
                radius = rand.Next(20, 60);
                x = rand.Next(radius, pictureBox.Width - radius);
                y = rand.Next(radius, pictureBox.Width - radius);
                Point p3 = new Point(x, y);
                CreateCluster(p3, radius, brush, form.isForFuzzyClustering);
                if(form.isForFuzzyClustering)
                {
                    x=((int)p1.coordinates[0]+(int)p2.coordinates[0])/2;
                    y=((int)p1.coordinates[1]+(int)p2.coordinates[1])/2;
                    FuzzyClusteringPoint p12 = new FuzzyClusteringPoint(x, y);
                    gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius));
                    points.Add(p12);
                    x = ((int)p1.coordinates[0] + (int)p3.coordinates[0]) / 2;
                    y = ((int)p1.coordinates[1] + (int)p3.coordinates[1]) / 2;
                    FuzzyClusteringPoint p13 = new FuzzyClusteringPoint(x, y);
                    gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius));
                    points.Add(p13);
                    x = ((int)p2.coordinates[0] + (int)p3.coordinates[0]) / 2;
                    y = ((int)p2.coordinates[1] + (int)p3.coordinates[1]) / 2;
                    FuzzyClusteringPoint p23 = new FuzzyClusteringPoint(x, y);
                    gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius));
                    points.Add(p23);
                }
                if (form.isWithNoise)
                {
                    noise_count = 10;
                    CreateNoise(brush);
                }
                else
                    noise_count = 0;
            }
            pictureBox.Image = bitmap;
        }
        private void ColorizeClusters()
        {
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            for (int i = 0; i < points.Count; i++)
            {
                if (((Point)points[i]).cluster_number == 1)
                    myBrush.Color = Color.Red;
                else if (((Point)points[i]).cluster_number == 2)
                    myBrush.Color = Color.Green;
                else if (((Point)points[i]).cluster_number == 3)
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
        private void ColorizeFuzzyClusters()
        {
            point_radius = 6;
            gr.Clear(Color.White);
            SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            for (int i = 0; i < points.Count; i++)
            {
                int x = (int)((FuzzyClusteringPoint)points[i]).coordinates[0];
                int y = (int)((FuzzyClusteringPoint)points[i]).coordinates[1];
                int start_angle = 0;
                for(int j=0; j<((ArrayList)MembershipMatrix[0]).Count; j++)
                {
                    if (j == 0)
                        myBrush.Color = Color.Red;
                    else if (j == 1)
                        myBrush.Color = Color.Green;
                    else if (j == 2)
                        myBrush.Color = Color.Blue;
                    else
                        myBrush.Color = Color.Black;
                    double membership = (double)((ArrayList)MembershipMatrix[i])[j];
                    int end_angle = (int)(membership * 360);
                    gr.FillPie(myBrush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius), start_angle, end_angle);
                    start_angle += end_angle;
                }
            }
            point_radius = 3;
            pictureBox.Image = bitmap;
        }
        private void clustering_button_Click(object sender, EventArgs e)
        {
            ClusteringForm form = new ClusteringForm(points, noise_count, pictureBox.Width, pictureBox.Height);
            form.ShowDialog();
            if (form.isClustered)
            {
                if(form.isHierarchicalClustering)
                {
                    isHierarchicalClustering = true;
                    isFuzzyClustering = false;
                    dendrogram = form.getDendrogram();
                    DendrogramForm dendrogram_form = new DendrogramForm(dendrogram);
                    dendrogram_form.Show();
                }
                if(form.isFuzzyClustering)
                {
                    isHierarchicalClustering = false;
                    isFuzzyClustering = true;
                    MembershipMatrix = form.getMemebershipMatrix();
                    ColorizeFuzzyClusters();
                }
                else
                {
                    isHierarchicalClustering = false;
                    isFuzzyClustering = false;
                    points = form.getPoints();
                    ColorizeClusters();
                }
            }
        }
    }
}
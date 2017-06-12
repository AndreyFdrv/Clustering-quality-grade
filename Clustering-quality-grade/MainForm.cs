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
                double x =(double)center.coordinates[0]+(r * Math.Cos(alpha));
                double y =(double)center.coordinates[1]+(r * Math.Sin(alpha));
                gr.FillEllipse(myBrush, new Rectangle((int)x - point_radius, (int)y - point_radius,
                   2 * point_radius, 2 * point_radius));
                Point point = new Point((double)x, (double)y);
                points.Add(point);
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
                Point point = new Point((double)x, (double)y);
                points.Add(point);
            }
        }
         private ArrayList CreateClusterInfo(double eps=0.1)
        {
            ArrayList ClusterInfo = new ArrayList();
            if (isFuzzyClustering)
            {
                for(int i=0; i<points.Count; i++)
                {
                    double max = 0;
                    for(int j=0; j<((ArrayList)MembershipMatrix[i]).Count; j++)
                    {
                        if ((double)((ArrayList)MembershipMatrix[i])[j] > max)
                            max = (double)((ArrayList)MembershipMatrix[i])[j];
                    }
                    ArrayList row=new ArrayList();
                    for(int j=0; j<((ArrayList)MembershipMatrix[i]).Count; j++)
                    {
                        double membership = (double)((ArrayList)MembershipMatrix[i])[j];
                        if (max - membership < eps)
                            row.Add(j + 1);
                    }
                    ClusterInfo.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(((Point)points[i]).cluster_numbers[0]);
                    ClusterInfo.Add(row);
                }
            }
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
            else if(isFuzzyClustering)
            {
                for(int i=0; i<points.Count; i++)
                {
                    ArrayList row = new ArrayList();
                    if (i < 10)
                        //row.Add(1);
                        continue;
                    else if (i < 20)
                        //row.Add(2);
                        continue;
                    else if (i == 29)
                    /*{
                        row.Add(1);
                        row.Add(2);
                    }*/
                    {
                        row.Add(1);
                        row.Add(2);
                        row.Add(7);
                    }
                    else if (i < 30)
                        //row.Add(3);
                        continue;
                    else if (i == 30)
                        /*{
                            row.Add(1);
                            row.Add(2);
                        }*/
                    {
                        row.Add(1);
                        row.Add(3);
                        row.Add(7);
                    }
                    else if (i == 31)
                        /*{
                            row.Add(1);
                            row.Add(3);
                        }*/
                    {
                        row.Add(1);
                        row.Add(4);
                        row.Add(7);
                    }
                    else if (i == 32)
                    /*{
                        row.Add(2);
                        row.Add(3);
                    }*/
                    {
                        row.Add(1);
                        row.Add(5);
                    }
                    ClassInfo.Add(row);
                }
            }
            else
            {
                Random rand = new Random();
                for (int i = 0; i < cluster_size; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(1);
                    ClassInfo.Add(row);
                }
                for (int i = cluster_size; i < 2 * cluster_size; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(2);
                    ClassInfo.Add(row);
                }
                for (int i = 2 * cluster_size; i < 3 * cluster_size; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(3);
                    ClassInfo.Add(row);
                }
                for (int i = 3 * cluster_size; i < 3 * cluster_size + noise_count; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(0);
                    ClassInfo.Add(row);
                }
            }
            return ClassInfo;
        }
        private ArrayList ElementsList(Dendrogram local_dendrogram)
        {
            ArrayList result;
            if (local_dendrogram.HasValue)
            {
                result = new ArrayList();
                result.Add(local_dendrogram.value);
                return result;
            }
            result = ElementsList(local_dendrogram.left);
            ArrayList right = ElementsList(local_dendrogram.right);
            for (int i = 0; i < right.Count; i++)
                result.Add((int)right[i]);
            return result;
        }
        private void CreateClustersList(Dendrogram local_dendrogram, ref ArrayList ClustersList)
        {
            ClustersList.Add(ElementsList(local_dendrogram));
            if (!local_dendrogram.HasValue)
            {
                CreateClustersList(local_dendrogram.left, ref ClustersList);
                CreateClustersList(local_dendrogram.right, ref ClustersList);
            }
        }
        private ArrayList CreateClusterInfoFromDendrogram(ArrayList ClassInfo)
        {
            ArrayList ClusterInfo = new ArrayList();
            ArrayList ClustersList = new ArrayList();
            CreateClustersList(dendrogram, ref ClustersList);
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < ClustersList.Count; j++)
                {
                    for (int k = 0; k < ((ArrayList)ClustersList[j]).Count; k++)
                    {
                        if ((int)((ArrayList)ClustersList[j])[k] == i)
                            row.Add(j + 1);
                    }
                }
                ClusterInfo.Add(row);
            }
            return ClusterInfo;
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
                HierarchicalAdjustedMutualInformation mutual_information = new HierarchicalAdjustedMutualInformation(dendrogram, ClassInfo);
                output += "Взаимная информация: " + mutual_information.Compute() + "\r\n";
            }
            else if(isFuzzyClustering)
            {
                ArrayList ClusterInfo = CreateClassInfo();
                ArrayList ClassInfo = CreateClassInfo();
                Fuzzy_F1_meassure f1_meassure = new Fuzzy_F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Fuzzy_Rand_Jaccard_FM rand_jaccard_fm = new Fuzzy_Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
                FuzzyAdjustedMutualInformation mutual_information = new FuzzyAdjustedMutualInformation(ClusterInfo, ClassInfo);
                output += "Взаимная информация: " + mutual_information.Compute() + "\r\n";
            }
            else
            {
                ArrayList ClusterInfo = CreateClusterInfo();
                ArrayList ClassInfo = CreateClassInfo();
                Density_F1_meassure f1_meassure = new Density_F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Density_Rand_Jaccard_FM rand_jaccard_fm = new Density_Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
                AdjustedMutualInformation mutual_information = new AdjustedMutualInformation(ClusterInfo, ClassInfo);
                output += "Взаимная информация: " + mutual_information.Compute() + "\r\n";
            }
            MessageBox.Show(output);
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
                Point point = new Point((double)x, (double)y);
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
                    x = (int)(((double)p1.coordinates[0] + (double)p2.coordinates[0]) / 2);
                    y = (int)(((double)p1.coordinates[1] + (double)p2.coordinates[1]) / 2);
                    Point p12 = new Point(x, y);
                    gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius));
                    points.Add(p12);
                    x = (int)(((double)p1.coordinates[0] + (double)p3.coordinates[0]) / 2);
                    y = (int)(((double)p1.coordinates[1] + (double)p3.coordinates[1]) / 2);
                    Point p13 = new Point(x, y);
                    gr.FillEllipse(brush, new Rectangle(x - point_radius, y - point_radius,
                        2 * point_radius, 2 * point_radius));
                    points.Add(p13);
                    x = (int)(((double)p2.coordinates[0] + (double)p3.coordinates[0]) / 2);
                    y = (int)(((double)p2.coordinates[1] + (double)p3.coordinates[1]) / 2);
                    Point p23 = new Point(x, y);
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
                if ((int)((Point)points[i]).cluster_numbers[0] == 1)
                    myBrush.Color = Color.Red;
                else if ((int)((Point)points[i]).cluster_numbers[0] == 2)
                    myBrush.Color = Color.Green;
                else if ((int)((Point)points[i]).cluster_numbers[0] == 3)
                    myBrush.Color = Color.Blue;
                else
                    myBrush.Color = Color.Black;
                int x = (int)(double)((Point)points[i]).coordinates[0];
                int y = (int)(double)((Point)points[i]).coordinates[1];
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
                double x = (double)((Point)points[i]).coordinates[0];
                double y = (double)((Point)points[i]).coordinates[1];
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
                    gr.FillPie(myBrush, new Rectangle((int)x - point_radius, (int)y - point_radius,
                        2 * point_radius, 2 * point_radius), start_angle, end_angle);
                    start_angle += end_angle;
                }
            }
            point_radius = 3;
            pictureBox.Image = bitmap;
        }
        private void clustering_button_Click(object sender, EventArgs e)
        {
            ArrayList LowerBorders = new ArrayList();
            LowerBorders.Add(0.0);
            LowerBorders.Add(0.0);
            ArrayList UpperBorders = new ArrayList();
            UpperBorders.Add((double)pictureBox.Width);
            UpperBorders.Add((double)pictureBox.Height);
            ClusteringForm form = new ClusteringForm(points, LowerBorders, UpperBorders);
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
                else if(form.isFuzzyClustering)
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
        private void LoadTestButton_Click(object sender, EventArgs e)
        {
            FileStream file = new FileStream("Test.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            ArrayList test_points = new ArrayList();
            ArrayList ClassInfo = new ArrayList();
            string line = reader.ReadLine();
            int dimension=0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                    dimension++;

            }
            reader.DiscardBufferedData();
            reader.BaseStream.Position = 0;
            ArrayList LowerBorders = new ArrayList();
            for (int i = 0; i < dimension; i++)
                LowerBorders.Add(Double.MaxValue);
            ArrayList UpperBorders = new ArrayList();
            for (int i = 0; i < dimension; i++)
                UpperBorders.Add(-Double.MaxValue);
            while (true)
            {
                line = reader.ReadLine();
                if (line == null)
                    break;
                Point point = new Point();
                int begin = 0;
                int coordinate_number = 0;
                while (true)
                {
                    int end;
                    for(end=begin; line[end]!=','; end++)
                    {
                        if (end == line.Length - 1)
                            break;
                    }
                    if (end == line.Length - 1)
                        break;
                    end--;
                    string number_str = line.Substring(begin, end - begin+1);
                    double number = double.Parse(number_str);
                    if (number > (double)UpperBorders[coordinate_number])
                        UpperBorders[coordinate_number] = number;
                    if (number < (double)LowerBorders[coordinate_number])
                        LowerBorders[coordinate_number] = number;
                    coordinate_number++;
                    point.coordinates.Add(number);
                    begin = end + 2;
                }
                test_points.Add(point);
                string class_number_str = line.Substring(begin, line.Length-begin);
                int class_number = int.Parse(class_number_str);
                ArrayList row = new ArrayList();
                row.Add(class_number + 2);
                ClassInfo.Add(row);
            }
            file.Close();
            TestForm form = new TestForm(test_points, ClassInfo, LowerBorders, UpperBorders);
            form.ShowDialog();
        }
    }
}
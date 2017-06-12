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
namespace Clustering_quality_grade
{
    public partial class TestClusteringForm : Form
    {
        private ArrayList points;
        private ArrayList ClassInfo;
        private ArrayList LowerBorders, UpperBorders;
        public bool isClustered = false;
        private Dendrogram dendrogram;
        private ArrayList MembershipMatrix;
        public bool isHierarchicalClustering = false;
        public bool isFuzzyClustering = false;
        public TestClusteringForm(ArrayList points, ArrayList ClassInfo, ArrayList LowerBorders, ArrayList UpperBorders)
        {
            InitializeComponent();
            this.points = points;
            this.ClassInfo = ClassInfo;
            this.LowerBorders = LowerBorders;
            this.UpperBorders = UpperBorders;
        }
        private void CreateRandomClustersWithoutNoise(int cluster_number=3)
        {
            Random rand=new Random();
            for(int i=0; i<points.Count; i++)
                ((Point)points[i]).cluster_numbers.Add(rand.Next(1, cluster_number));
        }
        private void CreateRandomClustersWithNoise(int cluster_number = 3)
        {
            Random rand = new Random();
            for (int i = 0; i < points.Count; i++)
                ((Point)points[i]).cluster_numbers.Add(rand.Next(0, cluster_number));
        }
        private void ChangeDendrogramParts(ref ArrayList dendrogram_parts_list, ArrayList clusters_list, int min_i, int min_j)
        {
            Dendrogram root = new Dendrogram();
            root.HasValue = false;
            if (((ArrayList)clusters_list[min_i]).Count == 1)
            {
                Dendrogram left = new Dendrogram();
                left.left = new Dendrogram();
                left.left.HasValue = false;
                left.right = new Dendrogram();
                left.right.HasValue = false;
                left.HasValue = true;
                left.value = (int)((ArrayList)clusters_list[min_i])[0];
                root.left = left;
            }
            else
            {
                Dendrogram cur_dendrogram = (Dendrogram)dendrogram_parts_list[0];
                int cur_dendrogram_index = 0;
                for (int i = 0; i < dendrogram_parts_list.Count; i++)
                {
                    cur_dendrogram = (Dendrogram)dendrogram_parts_list[i];
                    Dendrogram cur_left = cur_dendrogram;
                    while (!cur_left.HasValue)
                        cur_left = cur_left.left;
                    bool isLeftDendrogramFound = false;
                    for (int j = 0; j < ((ArrayList)clusters_list[min_i]).Count; j++)
                    {
                        if ((int)((ArrayList)clusters_list[min_i])[j] == cur_left.value)
                        {
                            isLeftDendrogramFound = true;
                            break;
                        }
                    }
                    if (isLeftDendrogramFound)
                    {
                        cur_dendrogram_index = i;
                        break;
                    }
                }
                root.left = cur_dendrogram;
                dendrogram_parts_list.RemoveAt(cur_dendrogram_index);
            }
            if (((ArrayList)clusters_list[min_j]).Count == 1)
            {
                Dendrogram right = new Dendrogram();
                right.left = new Dendrogram();
                right.left.HasValue = false;
                right.right = new Dendrogram();
                right.right.HasValue = false;
                right.HasValue = true;
                right.value = (int)((ArrayList)clusters_list[min_j])[0];
                root.right = right;
            }
            else
            {
                Dendrogram cur_dendrogram = (Dendrogram)dendrogram_parts_list[0];
                int cur_dendrogram_index = 0;
                for (int i = 0; i < dendrogram_parts_list.Count; i++)
                {
                    cur_dendrogram = (Dendrogram)dendrogram_parts_list[i];
                    Dendrogram cur_left = cur_dendrogram;
                    while (!cur_left.HasValue)
                        cur_left = cur_left.left;
                    bool isRightDendrogramFound = false;
                    for (int j = 0; j < ((ArrayList)clusters_list[min_j]).Count; j++)
                    {
                        if ((int)((ArrayList)clusters_list[min_j])[j] == cur_left.value)
                        {
                            isRightDendrogramFound = true;
                            break;
                        }
                    }
                    if (isRightDendrogramFound)
                    {
                        cur_dendrogram_index = i;
                        break;
                    }
                }
                root.right = cur_dendrogram;
                dendrogram_parts_list.RemoveAt(cur_dendrogram_index);
            }
            dendrogram_parts_list.Add(root);
        }
        private void ChangeCurClustersList(ref ArrayList cur_cluster_list, int min_i, int min_j)
        {
            ArrayList new_cluster_list = new ArrayList();
            for (int i = 0; i < cur_cluster_list.Count; i++)
            {
                ArrayList cluster_points = new ArrayList();
                if (i == min_j)
                    continue;
                for (int k = 0; k < ((ArrayList)cur_cluster_list[i]).Count; k++)
                    cluster_points.Add(((ArrayList)cur_cluster_list[i])[k]);
                if (i == min_i)
                {
                    for (int k = 0; k < ((ArrayList)cur_cluster_list[min_j]).Count; k++)
                        cluster_points.Add(((ArrayList)cur_cluster_list[min_j])[k]);
                }
                new_cluster_list.Add(cluster_points);
            }
            cur_cluster_list = new_cluster_list;
        }
        private void ConsolidateClusters(ref ArrayList cur_clusters_list, ref ArrayList dendrogram_parts_list)
        {
            Random rand = new Random();
            int i = rand.Next(0, cur_clusters_list.Count);
            int j = 0;
            while(j==i)
                j = rand.Next(0, cur_clusters_list.Count);
            ChangeDendrogramParts(ref dendrogram_parts_list, cur_clusters_list, i, j);
            ChangeCurClustersList(ref cur_clusters_list, i, j);
        }
        private void CreateRandomDendrogram()
        {
            ArrayList cur_clusters_list = new ArrayList();
            for (int i = 0; i < points.Count; i++)
            {
                ArrayList row = new ArrayList();
                row.Add(i);
                cur_clusters_list.Add(row);
            }
            ArrayList dendrogram_parts_list = new ArrayList();
            while (cur_clusters_list.Count != 1)
                ConsolidateClusters(ref cur_clusters_list, ref dendrogram_parts_list);
            dendrogram=(Dendrogram)dendrogram_parts_list[0];
        }
        private void CreateRandomMembershipMatrix(int clusters_count=3)
        {
            MembershipMatrix = new ArrayList();
            Random rand = new Random();
            for (int i = 0; i < points.Count; i++)
            {
                ArrayList row = new ArrayList();
                double sum = 0;
                for (int j = 0; j < clusters_count; j++)
                {
                    double element = rand.NextDouble();
                    sum += element;
                    row.Add(element);
                }
                for (int j = 0; j < clusters_count; j++)
                    row[j] = (double)row[j] / sum;
                MembershipMatrix.Add(row);
            }
        }
        private ArrayList CreateClassList(int class_number)
        {
            ArrayList ClassList=new ArrayList();
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[0] == class_number)
                    ClassList.Add(i);
            }
            return ClassList;
        }
        private Dendrogram CreateClassDendrogram(ArrayList ClassList, int element_number=0)
        {
            Dendrogram local_dendrogram = new Dendrogram();
            local_dendrogram.HasValue = false;
            local_dendrogram.left = new Dendrogram();
            local_dendrogram.left.HasValue = true;
            local_dendrogram.left.value = (int)ClassList[element_number];
            if (ClassList.Count - element_number == 2)
            {
                local_dendrogram.right = new Dendrogram();
                local_dendrogram.right.HasValue = true;
                local_dendrogram.right.value = (int)ClassList[element_number + 1];
            }
            else
                local_dendrogram.right = CreateClassDendrogram(ClassList, element_number + 1);
            return local_dendrogram;
        }
        private Dendrogram CreateClassesDendrogram(int class_number=1, int class_count=3)
        {
            ArrayList ClassList = CreateClassList(class_number);
            if (ClassList.Count == 0)
                return CreateClassesDendrogram(class_number + 1);
            Dendrogram local_dendrogram = new Dendrogram();
            local_dendrogram.HasValue = false;
            if (ClassList.Count == 1)
            {
                local_dendrogram.left.HasValue = true;
                local_dendrogram.left.value = (int)ClassList[0];
            }
            else
                local_dendrogram.left = CreateClassDendrogram(ClassList);
            if (class_count - class_number + 1 == 2)
            {
                ClassList = CreateClassList(class_number+1);
                local_dendrogram.right = CreateClassDendrogram(ClassList);
            }
            else
                local_dendrogram.right = CreateClassesDendrogram(class_number + 1);
            return local_dendrogram;
        }
        private void CreateClassesMembershipMatrix(int class_count=3)
        {
            MembershipMatrix = new ArrayList();
            for (int i = 0; i < points.Count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < class_count; j++)
                {
                    double element;
                    if (j + 1 == (int)((ArrayList)ClassInfo[i])[0])
                        element = 1.0;
                    else
                        element = 0.0;
                    row.Add(element);
                }
                MembershipMatrix.Add(row);
            }
        }
        public ArrayList getPoints()
        {
            return points;
        }
        public Dendrogram getDendrogram()
        {
            return dendrogram;
        }
        public ArrayList getMemebershipMatrix()
        {
            return MembershipMatrix;
        }
        private void Cluster_button_Click(object sender, EventArgs e)
        {
            isFuzzyClustering = false;
            isHierarchicalClustering = false;
            for (int i = 0; i < points.Count; i++)
                ((Point)points[i]).cluster_numbers.Clear();
            if(LikeClassInfo_rb.Checked)
            {
                GenerationTypes form = new GenerationTypes();
                form.ShowDialog();
                if (form.isForHierarchicalClustering)
                {
                    isHierarchicalClustering = true;
                    dendrogram = CreateClassesDendrogram();
                }
                else if (form.isForFuzzyClustering)
                {
                    isFuzzyClustering = true;
                    CreateClassesMembershipMatrix();
                }
                else
                {
                    for (int i = 0; i < points.Count; i++)
                        ((Point)points[i]).cluster_numbers.Add((int)((ArrayList)ClassInfo[i])[0]);
                }
            }
            else if(ClusteringAlgorithms_rb.Checked)
            {
                ClusteringForm form = new ClusteringForm(points, LowerBorders, UpperBorders, true);
                form.ShowDialog();
                if (form.isHierarchicalClustering)
                {
                    isHierarchicalClustering = true;
                    dendrogram = form.getDendrogram();
                }
                else if (form.isFuzzyClustering)
                {
                    isFuzzyClustering = true;
                    MembershipMatrix = form.getMemebershipMatrix();
                }
                else
                    points = form.getPoints();
            }
            else if(Random_rb.Checked)
            {
                GenerationTypes form = new GenerationTypes();
                form.ShowDialog();
                if (form.isWithoutNoise)
                    CreateRandomClustersWithoutNoise();
                else if (form.isWithNoise)
                    CreateRandomClustersWithNoise();
                else if (form.isForHierarchicalClustering)
                {
                    isHierarchicalClustering = true;
                    CreateRandomDendrogram();
                }
                else if (form.isForFuzzyClustering)
                {
                    isFuzzyClustering = true;
                    CreateRandomMembershipMatrix();
                }
            }
            isClustered = true;
            Close();
        }
    }
}
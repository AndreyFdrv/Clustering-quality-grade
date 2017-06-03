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
    public partial class TestForm : Form
    {
        ArrayList points;
        ArrayList ClassInfo;
        ArrayList LowerBorders;
        ArrayList UpperBorders;
        Dendrogram dendrogram;
        ArrayList ClusterInfo;
        bool isHierarchicalClustering = false;
        public TestForm(ArrayList points, ArrayList ClassInfo, ArrayList LowerBorders, ArrayList UpperBorders)
        {
            InitializeComponent();
            this.points = points;
            this.ClassInfo = ClassInfo;
            this.LowerBorders = LowerBorders;
            this.UpperBorders = UpperBorders;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
        private void ClusterButton_Click(object sender, EventArgs e)
        {
            isHierarchicalClustering = false;
            TestClusteringForm form = new TestClusteringForm(points, ClassInfo, LowerBorders, UpperBorders);
            form.ShowDialog();
            if (!form.isClustered)
                return;
            if (form.isHierarchicalClustering)
            {
                dendrogram = form.getDendrogram();
                isHierarchicalClustering = true;
            }
            else if (form.isFuzzyClustering)
            {
                ClusterInfo = new ArrayList();
                ArrayList MembershipMatrix = form.getMemebershipMatrix();
                double eps = 0.1;
                for (int i = 0; i < points.Count; i++)
                {
                    double max = 0;
                    for (int j = 0; j < ((ArrayList)MembershipMatrix[i]).Count; j++)
                    {
                        if ((double)((ArrayList)MembershipMatrix[i])[j] > max)
                            max = (double)((ArrayList)MembershipMatrix[i])[j];
                    }
                    ArrayList row = new ArrayList();
                    for (int j = 0; j < ((ArrayList)MembershipMatrix[i]).Count; j++)
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
                ClusterInfo = new ArrayList();
                for (int i = 0; i < points.Count; i++)
                {
                    ArrayList row = new ArrayList();
                    row.Add(((Point)points[i]).cluster_numbers[0]);
                    ClusterInfo.Add(row);
                }
                points = form.getPoints();
            }
        }

        private void QualityButton_Click(object sender, EventArgs e)
        {
            QualityForm form;
            if (isHierarchicalClustering)
                form = new QualityForm(dendrogram, ClassInfo);
            else
                form = new QualityForm(ClusterInfo, ClassInfo);
            form.ShowDialog();
        }
    }
}
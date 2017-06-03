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
    public partial class ClusteringForm : Form
    {
        private ArrayList points;
        private Dendrogram dendrogram;
        public bool isHierarchicalClustering = false;
        public bool isFuzzyClustering = false;
        public bool isClustered=false;
        private ArrayList LowerBorders, UpperBorders;
        ArrayList MembershipMatrix;
        bool isForExperiment;
        public ClusteringForm(ArrayList points, ArrayList LowerBorders, ArrayList UpperBorders, bool isForExperiment=false)
        {
            InitializeComponent();
            this.points = points;
            this.LowerBorders = LowerBorders;
            this.UpperBorders = UpperBorders;
            this.isForExperiment = isForExperiment;
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
        private void Run_clustering_buttun_Click(object sender, EventArgs e)
        {
            if (k_means_rb.Checked)
            {
                K_Means algorithm = new K_Means(points, LowerBorders, UpperBorders);
                points = algorithm.Cluster();
            }    
            else if (DBSCAN_rb.Checked)
            {
                DBSCAN algorithm;
                if(isForExperiment)
                    algorithm = new DBSCAN(points, 1, 3);
                else
                    algorithm = new DBSCAN(points);
                points = algorithm.Cluster();
            }   
            else if (neighbor_method_rb.Checked)
            {
                isHierarchicalClustering = true;
                ClosestNeighborMethod algorithm = new ClosestNeighborMethod(points);
                dendrogram = algorithm.Cluster();
            }
            else if (c_means_rb.Checked)
            {
                isFuzzyClustering = true;
                C_Means algorithm = new C_Means(points);
                MembershipMatrix = algorithm.Cluster();
            }
            else if (EM_algorithm_rb.Checked)
            {
                EM_Clustering algorithm = new EM_Clustering(points);
                points = algorithm.Cluster();
            }
            isClustered=true;
            this.Close();
        }

        private void ClusteringForm_Load(object sender, EventArgs e)
        {

        }
    }
}
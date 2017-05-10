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
        public bool isClustered=false;
        private int cluster_size;
        private int width;
        private int height;
        private int noise_count;
        public ClusteringForm(ArrayList points, int noise_count, int width, int height)
        {
            InitializeComponent();
            this.points = points;
            this.noise_count = noise_count;
            this.width = width;
            this.height = height;
        }
        public ArrayList getPoints()
        {
            return points;
        }
        private void Run_clustering_buttun_Click(object sender, EventArgs e)
        {
            if (k_means_rb.Checked)
            {
                K_Means algorithm = new K_Means(points, width, height);
                points = algorithm.Cluster();
            }    
            else if (DBSCAN_rb.Checked)
            {
                DBSCAN algorithm = new DBSCAN(points);
                points = algorithm.Cluster();
            }   
            else if (neighbor_method_rb.Checked)
            {
                ClosestNeighborMethod algorithm = new ClosestNeighborMethod(points);
                points = algorithm.Cluster();
            }
            else if (c_means_rb.Checked)
                ;
            else if (EM_algorithm_rb.Checked)
            {
                EM_Clustering algorithm = new EM_Clustering(points, noise_count);
                points = algorithm.Cluster();
            }
            isClustered=true;
            this.Close();
        }
    }
}
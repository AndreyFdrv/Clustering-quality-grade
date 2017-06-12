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
    public partial class GenerationTypes : Form
    {
        public bool isGenerationButtonPressed=false;
        public bool isWithNoise = false;
        public bool isWithoutNoise = false;
        public bool isForHierarchicalClustering = false;
        public bool isForFuzzyClustering = false;
        public GenerationTypes()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (with_noise_rb.Checked)
                isWithNoise = true;
            if (without_noise_rb.Checked)
                isWithoutNoise = true;
            if (for_hierarchical_clustering_rb.Checked)
                isForHierarchicalClustering = true;
            if (for_fuzzy_clustering_rb.Checked)
                isForFuzzyClustering = true;
            isGenerationButtonPressed = true;
            Close();
        }
    }
}

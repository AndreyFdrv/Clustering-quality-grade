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
        public TestForm(ArrayList points, ArrayList ClassInfo)
        {
            InitializeComponent();
            this.points = points;
            this.ClassInfo = ClassInfo;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void ClusterButton_Click(object sender, EventArgs e)
        {

        }
    }
}

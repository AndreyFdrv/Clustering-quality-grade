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
    public partial class QualityForm : Form
    {
        Dendrogram dendrogram;
        ArrayList ClassInfo;
        ArrayList ClusterInfo;
        public QualityForm(ArrayList ClusterInfo, ArrayList ClassInfo)
        {
            InitializeComponent();
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        public QualityForm(Dendrogram ClusterInfo, ArrayList ClassInfo)
        {
            InitializeComponent();
            dendrogram = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        private ArrayList ElementsList(Dendrogram local_dendrogram)
        {
            ArrayList result;
            if(local_dendrogram.HasValue)
            {
                result= new ArrayList();
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
            if(!local_dendrogram.HasValue)
            {
                CreateClustersList(local_dendrogram.left, ref ClustersList);
                CreateClustersList(local_dendrogram.right, ref ClustersList);
            }
        }
        private void CreateClusterInfoFromDendrogram()
        {
            ClusterInfo = new ArrayList();
            ArrayList ClustersList = new ArrayList();
            CreateClustersList(dendrogram, ref ClustersList);
            for(int i=0; i<ClassInfo.Count; i++)
            {
                ArrayList row=new ArrayList();
                for(int j=0; j<ClustersList.Count; j++)
                {
                    for(int k=0; k<((ArrayList)ClustersList[j]).Count; k++)
                    {
                        if ((int)((ArrayList)ClustersList[j])[k] == i)
                            row.Add(j + 1);
                    }
                }
                ClusterInfo.Add(row);
            }
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            String output;
            if(Iterative_rb.Checked)
            {
                /*F1_meassure f1_meassure = new F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Rand_Jaccard_FM rand_jaccard_fm = new Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";*/
                AdjustedMutualInformation mutual_information = new AdjustedMutualInformation(ClassInfo, ClusterInfo);
                output = "Взаимная информация: " + mutual_information.Compute() + "\r\n";
            }
            else if(Density_rb.Checked)
            {
                Density_F1_meassure f1_meassure = new Density_F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Density_Rand_Jaccard_FM rand_jaccard_fm = new Density_Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
            }
            else if (Hierarchical_rb.Checked)
            {
                Hierarchical_F1_meassure f1_meassure = new Hierarchical_F1_meassure(dendrogram, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                Hierarchical_Rand_Jaccard_FM rand_jaccard_fm = new Hierarchical_Rand_Jaccard_FM(dendrogram, ClassInfo);
                output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
                HierarchicalAdjustedMutualInformation mutual_information = new HierarchicalAdjustedMutualInformation(dendrogram, ClassInfo);
                output += "Взаимная информация: " + mutual_information.Compute() + "\r\n";
            }
            else
            {
                bool OnlyF1Measuure = false;
                if (ClusterInfo == null)
                {
                    CreateClusterInfoFromDendrogram();
                    OnlyF1Measuure = true;
                }
                Fuzzy_F1_meassure f1_meassure = new Fuzzy_F1_meassure(ClusterInfo, ClassInfo);
                output = "F1-мера: " + f1_meassure.F1() + "\r\n";
                if (!OnlyF1Measuure)
                {
                    Fuzzy_Rand_Jaccard_FM rand_jaccard_fm = new Fuzzy_Rand_Jaccard_FM(ClusterInfo, ClassInfo);
                    output += "Индекс Rand: " + rand_jaccard_fm.Rand_index() + "\r\n";
                    output += "Индекс Jaccard: " + rand_jaccard_fm.Jaccard_index() + "\r\n";
                    output += "Индекс FM: " + rand_jaccard_fm.FM_index() + "\r\n";
                }
            }
            MessageBox.Show(output);
        }
    }
}

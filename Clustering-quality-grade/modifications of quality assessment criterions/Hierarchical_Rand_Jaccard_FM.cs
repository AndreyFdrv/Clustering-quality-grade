using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Hierarchical_Rand_Jaccard_FM
    {
        private Dendrogram ClusterInfo;
        private ArrayList ClassInfo;
        public Hierarchical_Rand_Jaccard_FM(Dendrogram ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        private bool isInOneCluster(ArrayList ClustersList, int number_1, int number_2)
        {
            for(int i=0; i<ClustersList.Count; i++)
            {
                bool isNumber1=false, isNumber2=false;
                for(int j=0; j<((ArrayList)ClustersList[i]).Count; j++)
                {
                    if ((int)((ArrayList)ClustersList[i])[j] == number_1)
                        isNumber1 = true;
                    if ((int)((ArrayList)ClustersList[i])[j] == number_2)
                        isNumber2 = true;
                }
                if (isNumber1 && isNumber2)
                    return true;
            }
            return false;
        }
        private int SS(ArrayList ClustersList, int clusters_order)
        {
            int sum = 0;
            for(int i=0; i<ClassInfo.Count; i++)
            {
                for (int j = i + 1; j < ClassInfo.Count; j++)
                {
                    if (isInOneCluster(ClustersList, i, j) && ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] ==
                        (int)((ArrayList)ClassInfo[j])[clusters_order - 1]))
                        sum++;
                }
            }
            return sum;
        }
        private int SD(ArrayList ClustersList, int clusters_order)
        {
            int sum = 0;
            for (int i = 0; i<ClassInfo.Count; i++)
            {
                for (int j = i + 1; j<ClassInfo.Count; j++)
                {
                    if (isInOneCluster(ClustersList, i, j) && ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] !=
                        (int)((ArrayList)ClassInfo[j])[clusters_order - 1]))
                        sum++;
                }
            }
            return sum;
        }
        private bool isInDifferentClusters(ArrayList ClustersList, int number_1, int number_2)
        {
            bool isNumber1 = false;
            int number1_cluster = 0;
            for (int i = 0; i < ClustersList.Count; i++)
            {
                for (int j = 0; j < ((ArrayList)ClustersList[i]).Count; j++)
                {
                    if ((int)((ArrayList)ClustersList[i])[j] == number_1)
                    {
                        isNumber1 = true;
                        number1_cluster = i + 1;
                        break;
                    }
                }
                if (isNumber1)
                    break;
            }
            if (!isNumber1)
                return false;
            bool isNumber2 = false;
            int number2_cluster = 0;
            for (int i = 0; i < ClustersList.Count; i++)
            {
                for (int j = 0; j < ((ArrayList)ClustersList[i]).Count; j++)
                {
                    if ((int)((ArrayList)ClustersList[i])[j] == number_1)
                    {
                        isNumber2 = true;
                        number2_cluster = i + 1;
                        break;
                    }
                }
                if (isNumber2)
                    break;
            }
            if (!isNumber2)
                return false;
            if (number1_cluster == number2_cluster)
                return false;
            return true;
        }
        private int DS(ArrayList ClustersList, int clusters_order)
        {
            int sum = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                for (int j = i + 1; j < ClassInfo.Count; j++)
                {
                    if (isInDifferentClusters(ClustersList, i, j) && ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] ==
                        (int)((ArrayList)ClassInfo[j])[clusters_order - 1]))
                        sum++;
                }
            }
            return sum;
        }
        private int DD(ArrayList ClustersList, int clusters_order)
        {
            int sum = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                for (int j = i + 1; j < ClassInfo.Count; j++)
                {
                    if (isInDifferentClusters(ClustersList, i, j) && ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] !=
                        (int)((ArrayList)ClassInfo[j])[clusters_order - 1]))
                        sum++;
                }
            }
            return sum;
        }
        private ArrayList CreateClassElements(int class_number, int clusters_order)
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] == class_number)
                    result.Add(i);
            }
            return result;
        }
        private ArrayList CreateDendrogramElements(Dendrogram dendrogram)
        {
            ArrayList result = new ArrayList();
            if (dendrogram.HasValue)
            {
                result.Add(dendrogram.value);
                return result;
            }
            ArrayList left_elements = CreateDendrogramElements(dendrogram.left);
            ArrayList right_elements = CreateDendrogramElements(dendrogram.right);
            result = left_elements;
            for (int i = 0; i < right_elements.Count; i++)
                result.Add(right_elements[i]);
            return result;
        }
        private double ComplianceDegree(Dendrogram dendrogram, ArrayList ClassElements)
        {
            ArrayList DendrogramElements = CreateDendrogramElements(dendrogram);
            int coincidences_count = 0;
            for (int i = 0; i < ClassElements.Count; i++)
            {
                for (int j = 0; j < DendrogramElements.Count; j++)
                {
                    if ((int)ClassElements[i] == (int)DendrogramElements[j])
                    {
                        coincidences_count++;
                        break;
                    }
                }
            }
            return ((double)Math.Pow(coincidences_count, 2)) / (DendrogramElements.Count * ClassElements.Count);
        }
        private ArrayList CreateClusterElements(Dendrogram dendrogram, int cluster_number, int clusters_order)
        {
            ArrayList ClassElements = CreateClassElements(cluster_number, clusters_order);
            double compliance_degree = ComplianceDegree(dendrogram, ClassElements);
            if (dendrogram.HasValue)
                return CreateDendrogramElements(dendrogram);
            double left_compliance_degree = ComplianceDegree(dendrogram.left, ClassElements);
            double right_compliance_degree = ComplianceDegree(dendrogram.right, ClassElements);
            if (compliance_degree > left_compliance_degree)
            {
                if (compliance_degree > right_compliance_degree)
                    return CreateDendrogramElements(dendrogram);
                else
                    return CreateClusterElements(dendrogram.right, cluster_number, clusters_order);
            }
            else
            {
                if (left_compliance_degree > right_compliance_degree)
                    return CreateClusterElements(dendrogram.left, cluster_number, clusters_order);
                else
                    return CreateClusterElements(dendrogram.right, cluster_number, clusters_order);
            }
        }
        private ArrayList CreateClustersList(int clusters_order)
        {
            ArrayList ClustersList = new ArrayList();
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[clusters_order - 1];
            }
            for(int i=1; i<=class_max_number; i++)
            {
                ArrayList row = CreateClusterElements(ClusterInfo, i, clusters_order);
                ClustersList.Add(row);
            }
            return ClustersList;
        }
        public double Rand_index()
        {
            double sum = 0;
            for (int i = 0; i < ((ArrayList)ClassInfo[0]).Count; i++)
            {
                ArrayList ClustersList = CreateClustersList(i + 1);
                int SS_value = SS(ClustersList, i+1);
                int SD_value = SD(ClustersList, i + 1);
                int DS_value = DS(ClustersList, i + 1);
                int DD_value = DD(ClustersList, i + 1);
                sum += (double)(SS_value + DD_value) / (SS_value + SD_value + DS_value + DD_value);
            }
            return sum / ((ArrayList)ClassInfo[0]).Count;
        }
        public double Jaccard_index()
        {
            double sum = 0;
            for (int i = 0; i < ((ArrayList)ClassInfo[0]).Count; i++)
            {
                ArrayList ClustersList = CreateClustersList(i + 1);
                int SS_value = SS(ClustersList, i + 1);
                int SD_value = SD(ClustersList, i + 1);
                int DS_value = DS(ClustersList, i + 1);
                sum += (double)SS_value / (SS_value + SD_value + DS_value);
            }
            return sum / ((ArrayList)ClassInfo[0]).Count;
        }
        public double FM_index()
        {
            double sum = 0;
            for (int i = 0; i < ((ArrayList)ClassInfo[0]).Count; i++)
            {
                ArrayList ClustersList = CreateClustersList(i + 1);
                int SS_value = SS(ClustersList, i + 1);
                int SD_value = SD(ClustersList, i + 1);
                int DS_value = DS(ClustersList, i + 1);
                sum += Math.Sqrt(((double)SS_value/(SS_value+SD_value))*((double)SS_value/(SS_value+DS_value)));
            }
            return sum / ((ArrayList)ClassInfo[0]).Count;
        }
    }
}
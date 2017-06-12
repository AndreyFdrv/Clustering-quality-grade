using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Numerics;
namespace Clustering_quality_grade
{
    class HierarchicalAdjustedMutualInformation
    {
        private Dendrogram ClusterInfo;
        private ArrayList ClassInfo;
        public HierarchicalAdjustedMutualInformation(Dendrogram ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        private double Probability(int class_number, int cluster_number, ArrayList ClustersList, int clusters_order)
        {
            int sum=0;
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if (((int)((ArrayList)ClassInfo[i])[clusters_order-1] == class_number) && 
                    (getClusterNumber(ClustersList, i) == cluster_number))
                    sum++;
            }
            return (double)sum / ClassInfo.Count;
        }
        private double ClassProbability(int class_number, int clusters_order)
        {
            int sum = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order-1] == class_number)
                    sum++;
            }
            return (double)sum / ClassInfo.Count;
        }
        private double ClusterProbability(int cluster_number, ArrayList ClustersList, int clusters_order)
        {
            int sum = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if (getClusterNumber(ClustersList, i) == cluster_number)
                    sum++;
            }
            return (double)sum / ClassInfo.Count;
        }
        private double MutualInformation(ArrayList ClustersList, int clusters_order)
        {
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order-1] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[clusters_order-1];
            }
            int cluster_max_number = ClustersList.Count;
            double sum = 0;
            for(int i=1; i<=class_max_number; i++)
            {
                for(int j=1; j<=cluster_max_number; j++)
                {
                    double probabilty_i_j = Probability(i, j, ClustersList, clusters_order);
                    if (probabilty_i_j != 0)
                        sum += probabilty_i_j * Math.Log(probabilty_i_j / (ClassProbability(i, clusters_order) * 
                            ClusterProbability(j, ClustersList, clusters_order)));
                }
            }
            return sum;
        }
        private int getClusterNumber(ArrayList ClustersList, int object_number)
        {
            for (int i = 0; i < ClustersList.Count; i++)
            {
                for (int j = 0; j < ((ArrayList)ClustersList[i]).Count; j++)
                    if ((int)((ArrayList)ClustersList[i])[j] == object_number)
                        return i + 1;
            }
            return -1;
        }
        private int IntersectionSize(int class_number, int cluster_number, ArrayList ClustersList, int clusters_order)
        {
            int sum=0;
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if (((int)((ArrayList)ClassInfo[i])[clusters_order-1] == class_number) &&
                    (getClusterNumber(ClustersList, i) == cluster_number))
                    sum++;
            }
            return sum;
        }
        private ArrayList ContingencyTable(int class_max_number, int cluster_max_number, ArrayList ClustersList, int clusters_order)
        {
            ArrayList result = new ArrayList();
            for (int i = 1; i <= class_max_number; i++)
            {
                ArrayList row = new ArrayList();
                for(int j=1; j <= cluster_max_number; j++)
                    row.Add(IntersectionSize(i, j, ClustersList, clusters_order));
                result.Add(row);
            }
            return result;
        }
        private int ContingencyTableRowSum(ArrayList contingency_table, int row_number)
        {
            int sum = 0;
            for (int i = 0; i < ((ArrayList)contingency_table[row_number]).Count; i++)
                sum += (int)((ArrayList)contingency_table[row_number])[i];
            return sum;
        }
        private int ContingencyTableColumnSum(ArrayList contingency_table, int column_number)
        {
            int sum = 0;
            for (int i = 0; i < contingency_table.Count; i++)
                sum += (int)((ArrayList)contingency_table[i])[column_number];
            return sum;
        }
        private BigInteger factorial(int N)
        {
            if (N == 0)
                return 1;
            return N * factorial(N - 1);
        }
        private double ExpectedMutualInformation(ArrayList ClustersList, int clusters_order)
        {
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order-1] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[clusters_order - 1];
            }
            int cluster_max_number = ClustersList.Count;
            ArrayList contingency_table = ContingencyTable(class_max_number, cluster_max_number, ClustersList, clusters_order);
            double sum = 0;
            int N=ClassInfo.Count;
            for (int i = 1; i <= class_max_number; i++)
            {
                for (int j = 1; j <= cluster_max_number; j++)
                {
                    int a_i = ContingencyTableRowSum(contingency_table, i-1);
                    int b_j = ContingencyTableColumnSum(contingency_table, j-1);
                    int begin=a_i+b_j-ClassInfo.Count;
                    if (begin < 1)
                        begin = 1;
                    int end = a_i;
                    if (b_j < end)
                        end = b_j;
                    for(int k=begin; k<=end; k++)
                    {
                        sum += ((double)k / N) * Math.Log((double)N * k / (a_i * b_j)) * (double)(1000*(factorial(a_i) * 
                            factorial(b_j)*factorial(N - a_i) * factorial(N - b_j)) / 
                            (factorial(N) * factorial(k) * factorial(a_i - k) * 
                            factorial(b_j - k) * factorial(N - a_i - b_j + k)))/1000;
                    }
                }
            }
            return sum;
        }
        private double ClassEntropy(int clusters_order)
        {
            double sum = 0;
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order-1] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[clusters_order-1];
            }
            for (int i = 1; i <= class_max_number; i++)
            {
                double class_probability = ClassProbability(i, clusters_order);
                if(class_probability!=0)
                    sum += class_probability * Math.Log(class_probability);
            }
            return -sum;
        }
        private double ClusterEntropy(ArrayList ClustersList, int clusters_order)
        {
            double sum = 0;
            int cluster_max_number = ClustersList.Count;
            for (int i = 1; i <= cluster_max_number; i++)
            {
                double cluster_probability = ClusterProbability(i, ClustersList, clusters_order);
                if (cluster_probability != 0)
                sum += cluster_probability * Math.Log(cluster_probability);
            }
            return -sum;
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
            for (int i = 1; i <= class_max_number; i++)
            {
                ArrayList row = CreateClusterElements(ClusterInfo, i, clusters_order);
                ClustersList.Add(row);
            }
            return ClustersList;
        }
        private double Compute(int clusters_order)
        {
            ArrayList ClustersList = CreateClustersList(clusters_order);
            double expected_mutual_information = ExpectedMutualInformation(ClustersList, clusters_order);
            double max_entropy = ClassEntropy(clusters_order);
            double cluster_entropy = ClusterEntropy(ClustersList, clusters_order);
            if (cluster_entropy > max_entropy)
                max_entropy = cluster_entropy;
            return (MutualInformation(ClustersList, clusters_order) - expected_mutual_information) / (max_entropy - expected_mutual_information);
        }
        public double Compute()
        {
            double sum = 0;
            for (int i = 0; i < ((ArrayList)ClassInfo[0]).Count; i++)
                sum += Compute(i + 1);
            return sum / ((ArrayList)ClassInfo[0]).Count;
        }
    }
}
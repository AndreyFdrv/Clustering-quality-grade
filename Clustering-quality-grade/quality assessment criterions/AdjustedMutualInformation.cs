using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Numerics;
namespace Clustering_quality_grade
{
    class AdjustedMutualInformation
    {
        private ArrayList ClusterInfo, ClassInfo;
        public AdjustedMutualInformation(ArrayList ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        private double Probability(int class_number, int cluster_number)
        {
            int sum=0;
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if (((int)((ArrayList)ClassInfo[i])[0] == class_number) && ((int)((ArrayList)ClusterInfo[i])[0] == cluster_number))
                    sum++;
            }
            return (double)sum / ClassInfo.Count;
        }
        private double ClassProbability(int class_number)
        {
            int sum = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[0] == class_number)
                    sum++;
            }
            return (double)sum / ClassInfo.Count;
        }
        private double ClusterProbability(int cluster_number)
        {
            int sum = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                if ((int)((ArrayList)ClusterInfo[i])[0] == cluster_number)
                    sum++;
            }
            return (double)sum / ClusterInfo.Count;
        }
        private double MutualInformation()
        {
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[0] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[0];
            }
            int cluster_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClusterInfo[i])[0] > cluster_max_number)
                    cluster_max_number = (int)((ArrayList)ClusterInfo[i])[0];
            }
            double sum = 0;
            for(int i=1; i<=class_max_number; i++)
            {
                for(int j=1; j<=cluster_max_number; j++)
                {
                    double probabilty_i_j = Probability(i, j);
                    if (probabilty_i_j != 0)
                        sum += probabilty_i_j * Math.Log(probabilty_i_j / (ClassProbability(i) * ClusterProbability(j)));
                }
            }
            return sum;
        }
        private int IntersectionSize(int class_number, int cluster_number)
        {
            int sum=0;
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if (((int)((ArrayList)ClassInfo[i])[0] == class_number) && ((int)((ArrayList)ClusterInfo[i])[0] == cluster_number))
                    sum++;
            }
            return sum;
        }
        private ArrayList ContingencyTable(int class_max_number, int cluster_max_number)
        {
            ArrayList result = new ArrayList();
            for (int i = 1; i <= class_max_number; i++)
            {
                ArrayList row = new ArrayList();
                for(int j=1; j <= cluster_max_number; j++)
                    row.Add(IntersectionSize(i, j));
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
        private double ExpectedMutualInformation()
        {
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[0] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[0];
            }
            int cluster_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClusterInfo[i])[0] > cluster_max_number)
                    cluster_max_number = (int)((ArrayList)ClusterInfo[i])[0];
            }
            ArrayList contingency_table = ContingencyTable(class_max_number, cluster_max_number);
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
        private double ClassEntropy()
        {
            double sum = 0;
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[0] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[0];
            }
            for (int i = 1; i <= class_max_number; i++)
            {
                double class_probability = ClassProbability(i);
                if(class_probability!=0)
                    sum += class_probability * Math.Log(class_probability);
            }
            return -sum;
        }
        private double ClusterEntropy()
        {
            double sum = 0;
            int cluster_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClusterInfo[i])[0] > cluster_max_number)
                    cluster_max_number = (int)((ArrayList)ClusterInfo[i])[0];
            }
            for (int i = 1; i <= cluster_max_number; i++)
            {
                double cluster_probability = ClusterProbability(i);
                if (cluster_probability != 0)
                sum += cluster_probability * Math.Log(cluster_probability);
            }
            return -sum;
        }
        public double Compute()
        {
            double expected_mutual_information = ExpectedMutualInformation();
            double max_entropy = ClassEntropy();
            double cluster_entropy = ClusterEntropy();
            if (cluster_entropy > max_entropy)
                max_entropy = cluster_entropy;
            return (MutualInformation()-expected_mutual_information)/(max_entropy-expected_mutual_information);
        }
    }
}
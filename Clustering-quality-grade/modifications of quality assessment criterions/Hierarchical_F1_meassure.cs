using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Clustering_quality_grade
{
    class Hierarchical_F1_meassure
    {
        private Dendrogram ClusterInfo;
        private ArrayList ClassInfo;
        public Hierarchical_F1_meassure(Dendrogram ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        public double Precession(ArrayList ClusterElements, ArrayList ClassElements)
        {
            double nij=0;
            for (int i = 0; i < ClusterElements.Count; i++)
            {
                for (int j = 0; j < ClassElements.Count; j++)
                {
                    if ((int)((ArrayList)ClusterElements)[i] == (int)((ArrayList)ClassElements)[j])
                    {
                        nij++;
                        break;
                    }
                }
            }
            return nij/ClusterElements.Count;
        }
        public double Recall(ArrayList ClusterElements, ArrayList ClassElements)
        {
            double nij = 0;
            for (int i = 0; i < ClusterElements.Count; i++)
            {
                for (int j = 0; j < ClassElements.Count; j++)
                {
                    if ((int)((ArrayList)ClusterElements)[i] == (int)((ArrayList)ClassElements)[j])
                    {
                        nij++;
                        break;
                    }
                }
            }
            return nij / ClassElements.Count;
        }
        private double F1(ArrayList ClusterElements, ArrayList ClassElements)
        {
            double precession, recall;
            precession = Precession(ClusterElements, ClassElements);
            recall = Recall(ClusterElements, ClassElements);
            if ((precession == 0) && (recall == 0))
                return 0;
            return 2*precession*recall/(precession+recall);
        }
        private ArrayList CreateClassElements(int class_number, int clusters_order)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] == class_number)
                    result.Add(i);
            }
            return result;
        }
        private ArrayList CreateDendrogramElements(Dendrogram dendrogram)
        {
            ArrayList result=new ArrayList();
            if(dendrogram.HasValue)
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
            ArrayList DendrogramElements=CreateDendrogramElements(dendrogram);
            int coincidences_count = 0;
            for (int i=0; i < ClassElements.Count; i++)
            {
                for(int j=0; j<DendrogramElements.Count; j++)
                {
                    if ((int)ClassElements[i] == (int)DendrogramElements[j])
                    {
                        coincidences_count++;
                        break;
                    }
                }
            }
            return ((double)Math.Pow(coincidences_count, 2))/(DendrogramElements.Count*ClassElements.Count);
        }
        private ArrayList CreateClusterElements(Dendrogram dendrogram, int cluster_number, int clusters_order)
        {
            ArrayList ClassElements = CreateClassElements(cluster_number, clusters_order);
            double compliance_degree = ComplianceDegree(dendrogram, ClassElements);
            if(dendrogram.HasValue)
                return CreateDendrogramElements(dendrogram);
            double left_compliance_degree = ComplianceDegree(dendrogram.left, ClassElements);
            double right_compliance_degree = ComplianceDegree(dendrogram.right, ClassElements);
            if(compliance_degree>left_compliance_degree)
            {
                if (compliance_degree > right_compliance_degree)
                    return CreateDendrogramElements(dendrogram);
                else
                    return CreateClusterElements(dendrogram.right, cluster_number, clusters_order);
            }
            else
            {
                if(left_compliance_degree>right_compliance_degree)
                    return CreateClusterElements(dendrogram.left, cluster_number, clusters_order);
                else
                    return CreateClusterElements(dendrogram.right, cluster_number, clusters_order);
            }
        }
        private double F1(int clusters_order)
        {
            ArrayList ClassSides = new ArrayList();
            ArrayList ClassF1Maximumes = new ArrayList();
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                if ((int)((ArrayList)ClassInfo[i])[clusters_order-1] > class_max_number)
                    class_max_number = (int)((ArrayList)ClassInfo[i])[clusters_order - 1];
            }
            for (int j = 1; j <= class_max_number; j++)
            {
                double side = 0;
                double F1_max = -10000;
                for (int i = 0; i < ClassInfo.Count; i++)
                {
                    if ((int)((ArrayList)ClassInfo[i])[clusters_order - 1] == j)
                        side++;
                }
                ArrayList ClassElements = CreateClassElements(j, clusters_order);
                for (int i = 1; i <= class_max_number; i++)
                {
                    ArrayList ClusterElements = CreateClusterElements(ClusterInfo, i, clusters_order);
                    double cur_res = F1(ClusterElements, ClassElements);
                    if (cur_res > F1_max)
                        F1_max = cur_res;
                }
                ClassSides.Add(side);
                ClassF1Maximumes.Add(F1_max);
            }
            double res = 0;
            for (int j = 0; j < ClassSides.Count; j++)
                res += (double)ClassSides[j] / ClassInfo.Count * (double)ClassF1Maximumes[j];
            return res;
        }
        public double F1()
        {
            double sum=0;
            for (int i = 0; i < ((ArrayList)ClassInfo[0]).Count; i++)
                sum += F1(i+1);
            return sum / ((ArrayList)ClassInfo[0]).Count;
        }
    }
}
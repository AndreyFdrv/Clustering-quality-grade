using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Clustering_quality_grade
{
    class Fuzzy_F1_meassure
    {
        private ArrayList ClusterInfo, ClassInfo;
        public Fuzzy_F1_meassure(ArrayList ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        public double Precession(int ClusterNumber, int ClassNumber)
        {
            double nij=0, ni=0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                if ((((ArrayList)ClusterInfo[i]).Contains(ClusterNumber)) && (((ArrayList)ClassInfo[i]).Contains(ClassNumber)))
                    nij++;
                if (((ArrayList)ClusterInfo[i]).Contains(ClusterNumber))
                    ni++;
            }
            return nij/ni;
        }
        public double Recall(int ClusterNumber, int ClassNumber)
        {
            double nij = 0, nj = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                if ((((ArrayList)ClusterInfo[i]).Contains(ClusterNumber)) && (((ArrayList)ClassInfo[i]).Contains(ClassNumber)))
                    nij++;
                if (((ArrayList)ClassInfo[i]).Contains(ClassNumber))
                    nj++;
            }
            return nij / nj;
        }
        public double F1(int ClusterNumber, int ClassNumber)
        {
            double precession, recall;
            precession = Precession(ClusterNumber, ClassNumber);
            recall = Recall(ClusterNumber, ClassNumber);
            if ((precession == 0) && (recall == 0))
                return 0;
            return 2*precession*recall/(precession+recall);
        }
        public double F1()
        {
            ArrayList ClassSides = new ArrayList();
            ArrayList ClassF1Maximumes = new ArrayList();
            int class_max_number = 0;
            for (int i = 0; i < ClassInfo.Count; i++)
            {
                for (int j = 0; j < ((ArrayList)ClassInfo[i]).Count; j++)
                {
                    if ((int)((ArrayList)ClassInfo[i])[j] > class_max_number)
                        class_max_number = (int)((ArrayList)ClassInfo[i])[j];
                }
            }
            int cluster_max_number = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                for (int j = 0; j < ((ArrayList)ClusterInfo[i]).Count; j++)
                {
                    if ((int)((ArrayList)ClusterInfo[i])[j] > cluster_max_number)
                        cluster_max_number = (int)((ArrayList)ClusterInfo[i])[j];
                }
            }
            for (int j = 1; j <= class_max_number; j++)
            {
                double side = 0;
                double F1_max = -10000;
                for (int i = 0; i < ClassInfo.Count; i++)
                {
                    if (((ArrayList)ClassInfo[i]).Contains(j))
                        side++;
                }
                for (int i = 1; i <= cluster_max_number; i++)
                {
                    double cur_res = F1(i, j);
                    if (cur_res > F1_max)
                        F1_max = cur_res;
                }
                ClassSides.Add(side);
                ClassF1Maximumes.Add(F1_max);
            }
            double res=0;
            double class_sides_sum = 0;
            for (int i = 0; i < ClassSides.Count; i++)
                class_sides_sum += (double)ClassSides[i];
            for (int j = 0; j < ClassSides.Count; j++)
                res += (double)ClassSides[j] / class_sides_sum * (double)ClassF1Maximumes[j];
            return res;
        }
    }
}
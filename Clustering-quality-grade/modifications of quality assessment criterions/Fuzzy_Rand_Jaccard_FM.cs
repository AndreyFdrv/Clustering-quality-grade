using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Fuzzy_Rand_Jaccard_FM
    {
        private ArrayList ClusterInfo, ClassInfo;
        public Fuzzy_Rand_Jaccard_FM(ArrayList ClusterInfo, ArrayList ClassInfo)
        {
            this.ClusterInfo = ClusterInfo;
            this.ClassInfo = ClassInfo;
        }
        private int SS()
        {
            int sum = 0;
            for(int i=0; i<ClusterInfo.Count; i++)
            {
                for (int j = i + 1; j < ClusterInfo.Count; j++)
                {
                    bool isCluster=false;
                    for (int k = 0; k < ((ArrayList)ClusterInfo[i]).Count; k++)
                    {
                        if(((ArrayList)ClusterInfo[j]).Contains(((ArrayList)ClusterInfo[i])[k]))
                        {
                            isCluster=true;
                            break;
                        }
                    }
                    bool isClass = false;
                    for (int k = 0; k < ((ArrayList)ClassInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClassInfo[j]).Contains(((ArrayList)ClassInfo[i])[k]))
                        {
                            isClass = true;
                            break;
                        }
                    }
                    if (isCluster && isClass)
                        sum++;
                }
            }
            return sum;
        }
        private int SD()
        {
            int sum = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                for (int j = i + 1; j < ClusterInfo.Count; j++)
                {
                    bool isCluster = false;
                    for (int k = 0; k < ((ArrayList)ClusterInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClusterInfo[j]).Contains(((ArrayList)ClusterInfo[i])[k]))
                        {
                            isCluster = true;
                            break;
                        }
                    }
                    bool isClass = false;
                    for (int k = 0; k < ((ArrayList)ClassInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClassInfo[j]).Contains(((ArrayList)ClassInfo[i])[k]))
                        {
                            isClass = true;
                            break;
                        }
                    }
                    if (isCluster && !isClass)
                        sum++;
                }
            }
            return sum;
        }
        private int DS()
        {
            int sum = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                for (int j = i + 1; j < ClusterInfo.Count; j++)
                {
                    bool isCluster = false;
                    for (int k = 0; k < ((ArrayList)ClusterInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClusterInfo[j]).Contains(((ArrayList)ClusterInfo[i])[k]))
                        {
                            isCluster = true;
                            break;
                        }
                    }
                    bool isClass = false;
                    for (int k = 0; k < ((ArrayList)ClassInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClassInfo[j]).Contains(((ArrayList)ClassInfo[i])[k]))
                        {
                            isClass = true;
                            break;
                        }
                    }
                    if (!isCluster && isClass)
                        sum++;
                }
            }
            return sum;
        }
        private int DD()
        {
            int sum = 0;
            for (int i = 0; i < ClusterInfo.Count; i++)
            {
                for (int j = i + 1; j < ClusterInfo.Count; j++)
                {
                    bool isCluster = false;
                    for (int k = 0; k < ((ArrayList)ClusterInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClusterInfo[j]).Contains(((ArrayList)ClusterInfo[i])[k]))
                        {
                            isCluster = true;
                            break;
                        }
                    }
                    bool isClass = false;
                    for (int k = 0; k < ((ArrayList)ClassInfo[i]).Count; k++)
                    {
                        if (((ArrayList)ClassInfo[j]).Contains(((ArrayList)ClassInfo[i])[k]))
                        {
                            isClass = true;
                            break;
                        }
                    }
                    if (!isCluster && !isClass)
                        sum++;
                }
            }
            return sum;
        }
        public double Rand_index()
        {
            int SS_value=SS();
            int SD_value=SD();
            int DS_value=DS();
            int DD_value=DD();
            return (double)(SS_value+DD_value)/(SS_value+SD_value+DS_value+DD_value);
        }
        public double Jaccard_index()
        {
            int SS_value = SS();
            int SD_value = SD();
            int DS_value = DS();
            return (double)SS_value/ (SS_value + SD_value + DS_value);
        }
        public double FM_index()
        {
            int SS_value = SS();
            int SD_value = SD();
            int DS_value = DS();
            return Math.Sqrt(((double)SS_value/(SS_value+SD_value))*((double)SS_value/(SS_value+DS_value)));
        }
    }
}
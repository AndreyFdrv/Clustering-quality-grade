using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Hubert_Gamma_Statistic
    {
        private ArrayList objects;
        public Hubert_Gamma_Statistic(ArrayList objects)
        {
            this.objects = objects;
        }
        private double M()
        {
            return objects.Count*(objects.Count-1)/2;
        }
        private int Y(int i, int j)
        {
            if(((Point)objects[i]).cluster_number!=((Point)objects[j]).cluster_number)
                return 1;
            else
                return 0;
        }
        public double compute()
        {
            double sum = 0;
            for(int i=0; i<objects.Count-1; i++)
            {
                for(int j=i+1; j<objects.Count; j++)
                {
                    int Y_value = Y(i, j);
                    if (Y_value == 0)
                        continue;
                    double distance = 0;
                    int dimension = ((Point)objects[0]).coordinates.Count;
                    for (int k = 0; k < dimension; k++)
                        distance += Math.Pow((int)((Point)objects[i]).coordinates[k] - (int)((Point)objects[j]).coordinates[k], 2);
                    distance = Math.Sqrt(distance);
                    sum += distance * Y_value;
                }
            }
            return sum / M();
        }
    }
}
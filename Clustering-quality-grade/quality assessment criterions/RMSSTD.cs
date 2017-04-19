using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class RMSSTD
    {
        private ArrayList objects;
        public RMSSTD(ArrayList objects)
        {
            this.objects = objects;
        }
        private ArrayList cluster_center(int cluster_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            ArrayList center_coordinates = new ArrayList();
            int dimension = ((Point)objects[0]).coordinates.Count;
            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number == cluster_number)
                        sum += (int)((Point)objects[j]).coordinates[i];
                }
                center_coordinates.Add(sum / cluster_size);
            }
            return center_coordinates;
        }
        private double sum_of_squares()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum = 0;
            int dimension=((Point)objects[0]).coordinates.Count;
            for(int i=1; i<clusters_count; i++)
            {
                ArrayList center = cluster_center(i);
                for(int j=0; j<objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != i)
                        continue;
                    for(int k=0; k<dimension; k++)
                        sum+=Math.Pow((int)((Point)objects[j]).coordinates[k]-(double)center[k], 2);
                }
            }
            return sum;
        }
        public double compute()
        {
            int dimension=((Point)objects[0]).coordinates.Count;
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            return Math.Sqrt(sum_of_squares()/(dimension*(objects.Count-clusters_count)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Modified_Hubert_Gamma_Statistic
    {
        private ArrayList objects;
        private ArrayList clusters_centers=new ArrayList();
        public Modified_Hubert_Gamma_Statistic(ArrayList objects)
        {
            this.objects = objects;
            clusters_centers.Clear();
        }
        private double M()
        {
            return objects.Count * (objects.Count - 1) / 2;
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
        private double distance_between_clusters(int object_i, int object_j)
        {
            int cluster_i = ((Point)objects[object_i]).cluster_number;
            int cluster_j=((Point)objects[object_j]).cluster_number;
            ArrayList center_i = (ArrayList)clusters_centers[cluster_i-1];
            ArrayList center_j = (ArrayList)clusters_centers[cluster_j-1];
            double distance = 0;
            for(int i=0; i<center_i.Count; i++)
                distance += Math.Pow((double)center_i[i] - (double)center_j[i], 2);
            return Math.Sqrt(distance);
        }
        public double compute()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            for (int i = 1; i <= clusters_count; i++)
                clusters_centers.Add(cluster_center(i));
            double sum = 0;
            for (int i = 0; i < objects.Count - 1; i++)
            {
                for (int j = i + 1; j < objects.Count; j++)
                {
                    if (((Point)objects[i]).cluster_number == ((Point)objects[j]).cluster_number)
                        continue;
                    double distance = 0;
                    int dimension = ((Point)objects[0]).coordinates.Count;
                    for (int k = 0; k < dimension; k++)
                        distance += Math.Pow((int)((Point)objects[i]).coordinates[k] - (int)((Point)objects[j]).coordinates[k], 2);
                    distance = Math.Sqrt(distance);
                    sum += distance * distance_between_clusters(i, j);
                }
            }
            return sum / M();
        }
    }
}

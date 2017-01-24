using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Davies_Bouldin_index
    {
        private ArrayList objects;
        public Davies_Bouldin_index(ArrayList objects)
        {
            this.objects = objects;
        }
        private ArrayList cluster_center(int cluster_number)
        {
            ArrayList center_coordinates = new ArrayList();
            int dimension = ((Point)objects[0]).coordinates.Count;
            for(int i=0; i<dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number==cluster_number)
                        sum += (int)((Point)objects[j]).coordinates[i];
                }
                center_coordinates.Add(sum / objects.Count);
            }
            return center_coordinates;
        }
        private double S(int cluster_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            double sum = 0;
            ArrayList center = cluster_center(cluster_number);
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                ArrayList cur_point = ((Point)objects[i]).coordinates;
                for (int j = 0; j < center.Count; j++)
                    sum += ((int)cur_point[j] - (double)center[j]) * ((int)cur_point[j] - (double)center[j]);
            }
            return Math.Sqrt(sum / cluster_size);
        }
        private double d(int cluster_i, int cluster_j)
        {
            double sum = 0;
            ArrayList center_i = cluster_center(cluster_i);
            ArrayList center_j = cluster_center(cluster_j);
            for (int i = 0; i < center_i.Count; i++)
                sum += ((double)center_i[i] - (double)center_j[i]) * ((double)center_i[i] - (double)center_j[i]);
            return Math.Sqrt(sum);
        }
        private double R(int cluster_i, int cluster_j)
        {
            return (S(cluster_i) + S(cluster_j)) / d(cluster_i, cluster_j);
        }
        private double R(int cluster_i)
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number >clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double max = 0;
            for(int j=1; j<=clusters_count; j++)
            {
                if (cluster_i == j)
                    continue;
                double R_value = R(cluster_i, j);
                if (R_value > max)
                    max = R_value;
            }
            return max;
        }
        public double compute()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum = 0;
            for (int i = 1; i <= clusters_count; i++)
                sum += R(i);
            return sum / clusters_count;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class RS_Index
    {
        private ArrayList objects;
        public RS_Index(ArrayList objects)
        {
            this.objects = objects;
        }
        private ArrayList center()
        {
            ArrayList center_coordinates = new ArrayList();
            int dimension = ((Point)objects[0]).coordinates.Count;
            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < objects.Count; j++)
                    sum += (int)((Point)objects[j]).coordinates[i];
                center_coordinates.Add(sum / objects.Count);
            }
            return center_coordinates;
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
        private double sum_of_distance_squares()
        {
            ArrayList center_point = center();
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            for(int i=0; i<objects.Count; i++)
            {
                for(int j=0; j<dimension; j++)
                    sum += Math.Pow((int)((Point)objects[i]).coordinates[j] - (double)center_point[j], 2);
            }
            return sum;
        }
        private double clusters_sum_of_distance_squares()
        {
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            for(int i=1; i<=clusters_count; i++)
            {
                ArrayList center_point = cluster_center(i);
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != i)
                        continue;
                    for (int k = 0; k < dimension; k++)
                        sum += Math.Pow((int)((Point)objects[j]).coordinates[k] - (double)center_point[k], 2);
                }
            }
            return sum;
        }
        public double compute()
        {
            return (sum_of_distance_squares() - clusters_sum_of_distance_squares()) / sum_of_distance_squares();
        }
    }
}

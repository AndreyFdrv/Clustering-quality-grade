using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class SilhouetteIndex
    {
        private ArrayList objects;
        public SilhouetteIndex(ArrayList objects)
        {
            this.objects = objects;
        }
        private double this_cluster_distance(int cluster_number, int object_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            for(int i=0; i<objects.Count; i++)
            {
                if (i == object_number)
                    continue;
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                double distance = 0;
                for(int j=0; j<dimension; j++)
                    distance += Math.Pow((int)((Point)objects[object_number]).coordinates[j] - 
                        (int)((Point)objects[i]).coordinates[j], 2);
                distance = Math.Sqrt(distance);
                sum += distance;
            }
            return sum / (cluster_size - 1);
        }
        private double another_cluster_distance(int cluster_number, int object_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                double distance = 0;
                for (int j = 0; j < dimension; j++)
                    distance += Math.Pow((int)((Point)objects[object_number]).coordinates[j] -
                        (int)((Point)objects[i]).coordinates[j], 2);
                distance = Math.Sqrt(distance);
                sum += distance;
            }
            return sum / cluster_size;
        }
        private double min_another_cluster_distance(int cluster_number, int object_number)
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double min = Double.MaxValue;
            for(int i=1; i<=clusters_count; i++)
            {
                if (i == cluster_number)
                    continue;
                double another_cluster_distance_value=another_cluster_distance(i, object_number);
                if (another_cluster_distance_value < min)
                    min = another_cluster_distance_value;
            }
            return min;
        }
        private double silhouette(int object_number)
        {
            int cluster_number = ((Point)objects[object_number]).cluster_number;
            double this_cluster_distance_value=this_cluster_distance(cluster_number, object_number);
            double min_another_cluster_distance_value=min_another_cluster_distance(cluster_number, object_number);
            double max = this_cluster_distance_value;
            if (max < min_another_cluster_distance_value)
                max = min_another_cluster_distance_value;
            return (min_another_cluster_distance_value - this_cluster_distance_value) / max;
        }
        public double compute()
        {
            double sum = 0;
            for (int i = 0; i < objects.Count; i++)
                sum += silhouette(i);
            return sum / objects.Count;
        }
    }
}

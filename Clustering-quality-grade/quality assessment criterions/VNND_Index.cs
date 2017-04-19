using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class VNND_Index
    {
        private ArrayList objects;
        public VNND_Index(ArrayList objects)
        {
            this.objects = objects;
        }
        private double min_distance(int object_index)
        {
            int cluster_number = ((Point)objects[object_index]).cluster_number;
            double min = Double.MaxValue;
            int dimension=((Point)objects[0]).coordinates.Count;
            for(int i=0; i<objects.Count; i++)
            {
                if (i == object_index)
                    continue;
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                double distance = 0;
                for(int j=0; j<dimension; j++)
                    distance += Math.Pow((int)((Point)objects[object_index]).coordinates[j]-
                        (int)((Point)objects[i]).coordinates[j], 2);
                distance = Math.Sqrt(distance);
                if (distance < min)
                    min = distance;
            }
            return min;
        }
        private double mean_min_distance(int cluster_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            double sum = 0;
            for(int i=0; i<objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                sum += min_distance(i);
            }
            return sum / cluster_size;
        }
        private double V(int cluster_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            double sum = 0;
            double mean = mean_min_distance(cluster_number);
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                sum += Math.Pow(min_distance(i)-mean, 2);
            }
            return sum / (cluster_size - 1);
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
                sum += V(i);
            return sum;
        }
    }
}

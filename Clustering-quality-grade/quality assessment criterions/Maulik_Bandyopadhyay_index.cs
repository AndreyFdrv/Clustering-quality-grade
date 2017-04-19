using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Maulik_Bandyopadhyay_index
    {
        private ArrayList objects;
        public Maulik_Bandyopadhyay_index(ArrayList objects)
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
        private double clusters_sum()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            for(int i=1; i<=clusters_count; i++)
            {
                ArrayList center = cluster_center(i);
                for(int j=0; j<objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != i)
                        continue;
                    double distance = 0;
                    for(int k=0; k<dimension; k++)
                        distance += Math.Pow((int)((Point)objects[j]).coordinates[k]-(double)center[k], 2);
                    distance=Math.Sqrt(distance);
                    sum+=distance;
                }
            }
            return sum;
        }
        private double sum()
        {
            double sum = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            ArrayList center_point = center();
            for (int i = 0; i < objects.Count; i++)
            {
                double distance = 0;
                for (int k = 0; k < dimension; k++)
                    distance += Math.Pow((int)((Point)objects[i]).coordinates[k] - (double)center_point[k], 2);
                distance = Math.Sqrt(distance);
                sum += distance;
            }
            return sum;
        }
        private double max_distance()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double max = 0;
            int dimension = ((Point)objects[0]).coordinates.Count;
            for (int i = 1; i <= clusters_count; i++)
            {
                ArrayList center_i = cluster_center(i);
                for (int j = i + 1; j <= clusters_count; j++)
                {
                    ArrayList center_j = cluster_center(j);
                    double distance = 0;
                    for (int k = 0; k < dimension; k++)
                        distance += Math.Pow((double)center_i[k] - (double)center_j[k], 2);
                    distance = Math.Sqrt(distance);
                    if (distance > max)
                        max = distance;
                }
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
            return Math.Pow((sum()*max_distance())/(clusters_count*clusters_sum()), 2);
        }
    }
}
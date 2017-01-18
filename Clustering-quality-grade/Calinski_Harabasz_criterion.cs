using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace Clustering_quality_grade
{
    class Calinski_Harabasz_criterion
    {
        private ArrayList objects;
        public Calinski_Harabasz_criterion(ArrayList objects)
        {
            this.objects = objects;
        }
        private double mean_square_of_distance()
        {
            int count=0;
            double sum = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                ArrayList coordinates1 = ((Point)objects[i]).coordinates;
                for(int j=i+1; j<objects.Count; j++)
                {
                    ArrayList coordinates2 = ((Point)objects[j]).coordinates;
                    for (int k = 0; k < coordinates1.Count; k++)
                        sum += ((int)coordinates1[k] - (int)coordinates2[k]) * ((int)coordinates1[k] - (int)coordinates2[k]);
                    count++;
                }
            }
            return sum / count;
        }
        private double cluster_mean_square_of_distance(int cluster_number)
        {
            int count=0;
            double sum = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_number)
                    continue;
                ArrayList coordinates1 = ((Point)objects[i]).coordinates;
                for(int j=i+1; j<objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != cluster_number)
                        continue;
                    ArrayList coordinates2 = ((Point)objects[j]).coordinates;
                    for (int k = 0; k < coordinates1.Count; k++)
                        sum += ((int)coordinates1[k] - (int)coordinates2[k]) * ((int)coordinates1[k] - (int)coordinates2[k]);
                    count++;
                }
            }
            return sum / count;
        }
        private double sum_in()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number >clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum = 0;
            for (int i = 1; i < clusters_count; i++)
            {
                int cluster_size=0;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number == i)
                        cluster_size++;
                }
                sum += (cluster_size - 1) * cluster_mean_square_of_distance(i);
            }
            return sum / 2;
        }
        private double A_c()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum = 0;
            for (int i = 1; i < clusters_count; i++)
            {
                int cluster_size = 0;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number == i)
                        cluster_size++;
                }
                sum += (cluster_size - 1) * (mean_square_of_distance()-cluster_mean_square_of_distance(i));
            }
            return sum / (objects.Count-clusters_count);
        }
        private double sum_out()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            return 0.5 * ((clusters_count - 1) * mean_square_of_distance() + (objects.Count - clusters_count) * A_c());
        }
        public double compute_criterion()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            return (sum_out() * (objects.Count - clusters_count)) / (sum_in() * (clusters_count-1));
        }
    }
}
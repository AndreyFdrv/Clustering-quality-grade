using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Dunn_index
    {
        private ArrayList objects;
        public Dunn_index(ArrayList objects)
        {
            this.objects = objects;
        }
        private double distance_between_clusters(int cluster_i, int cluster_j)
        {
            double min_distance = Double.MaxValue;
            for(int i=0; i<objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number != cluster_i)
                    continue;
                ArrayList coordinates1 = ((Point)objects[i]).coordinates;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != cluster_j)
                        continue;
                    ArrayList coordinates2 = ((Point)objects[j]).coordinates;
                    double distance = 0;
                    for (int k = 0; k < coordinates1.Count; k++)
                        distance += ((int)coordinates1[k] - (int)coordinates2[k]) *
                            ((int)coordinates1[k] - (int)coordinates2[k]);
                    distance = Math.Sqrt((double)distance);
                    if (distance < min_distance)
                        min_distance = distance;
                }
            }
            return min_distance;
        }
        private double max_diameter()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double max_distance = 0;
            for (int i = 1; i <= clusters_count; i++)
            {
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number != i)
                        continue;
                    ArrayList coordinates1 = ((Point)objects[j]).coordinates;
                    for (int k = j + 1; k < objects.Count; k++)
                    {
                        if (((Point)objects[k]).cluster_number != i)
                            continue;
                        ArrayList coordinates2 = ((Point)objects[k]).coordinates;
                        double distance = 0;
                        for (int c = 0; c < coordinates1.Count; c++)
                            distance += ((int)coordinates1[c] - (int)coordinates2[c]) *
                                ((int)coordinates1[c] - (int)coordinates2[c]);
                        distance = Math.Sqrt((double)distance);
                        if (distance > max_distance)
                            max_distance = distance;
                    }
                }
            }
            return max_distance;
        }
        public double compute()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double min_distance = Double.MaxValue;
            for (int i = 1; i <= clusters_count; i++)
            {
                for(int j=i+1; j<=clusters_count; j++)
                {
                    double distance=distance_between_clusters(i, j);
                    if ((distance < min_distance) && (distance!=0))
                        min_distance = distance;
                }
            }
            return min_distance / max_diameter();
        }
    }
}
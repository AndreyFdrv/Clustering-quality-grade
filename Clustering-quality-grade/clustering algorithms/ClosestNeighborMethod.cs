using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade.clustering_algorithms
{
    class ClosestNeighborMethod
    {
        ArrayList points;
        public ClosestNeighborMethod(ArrayList points)
        {
            this.points=points;
        }
        private void ChangeCurClustersList(ref ArrayList cur_cluster_list, int min_i, int min_j)
        {
            //-------------
        }
        private double Distance(int cluster_number1, int cluster_number2, ArrayList cur_clusters_list)
        {
            double min_distance = Double.MaxValue;
            for(int i=0; i<((ArrayList)cur_clusters_list[cluster_number1]).Count; i++)
            {
                int point1_index = (int)((ArrayList)cur_clusters_list[cluster_number1])[i];
                HierircalClusteringPoint point1 = ((HierircalClusteringPoint)points[point1_index]);
                for(int j=0; j<((ArrayList)cur_clusters_list[cluster_number2]).Count; j++)
                {
                    int point2_index = (int)((ArrayList)cur_clusters_list[cluster_number2])[j];
                    HierircalClusteringPoint point2 = ((HierircalClusteringPoint)points[point2_index]);
                    double distance = 0;
                    for (int k = 0; k < point1.coordinates.Count; i++)
                        distance += Math.Pow((int)point1.coordinates[i] - (int)point2.coordinates[i], 2);
                    distance = Math.Sqrt(distance);
                    if (distance < min_distance)
                        min_distance = distance;
                }
            }
            return min_distance;
        }
        private ArrayList CreateDistanceMatrix(ArrayList cur_clusters_list)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<points.Count; i++)
            {
                ArrayList cluster_points = new ArrayList();
                cluster_points.Add(i);
                ArrayList row = new ArrayList();
                row.Add(cluster_points);
                for(int j=0; j<points.Count; j++)
                {
                    double distance = Distance(i, j, cur_clusters_list);
                    row.Add(distance);
                }
                result.Add(row);
            }
            return result;
        }
        private void ConsolidateClusters(ref ArrayList distance_matrix, ref ArrayList cur_clusters_list, ref ArrayList dendrograms_parts_list)
        {
            double min_distance = Double.MaxValue;
            int min_i = 0, min_j = 0;
            for(int i=0; i<distance_matrix.Count; i++)//поиск кластеров для объединения
            {
                for(int j=i+1; j<distance_matrix.Count; j++)
                {
                    double cur_distance = (double)((ArrayList)distance_matrix[i])[j];
                    if(cur_distance<min_distance)
                    {
                        min_distance = cur_distance;
                        min_i = i;
                        min_j = j;
                    }
                }
            }
            ChangeCurClustersList(ref cur_clusters_list, min_i, min_j);
            ArrayList new_distance_matrix = new ArrayList();
            for(int i=0; i<distance_matrix.Count; i++)
            {
                ArrayList cluster_points = new ArrayList();
                if (i == min_j)
                    continue;
                for (int k = 0; k < ((ArrayList)((ArrayList)distance_matrix[i])[0]).Count; k++)
                    cluster_points.Add(((ArrayList)((ArrayList)distance_matrix[i])[0])[k]);
                if(i==min_i)
                {
                    for (int k = 0; k < ((ArrayList)((ArrayList)distance_matrix[min_j])[0]).Count; k++)
                        cluster_points.Add(((ArrayList)((ArrayList)distance_matrix[min_j])[0])[k]);
                }
                ArrayList row = new ArrayList();
                row.Add(cluster_points);
                for(int j=0; j<distance_matrix.Count; j++)
                {
                    if (j == min_j)
                        continue;
                }
            }
        }
        public ArrayList Cluster()
        {
            ArrayList cur_clusters_list=new ArrayList();
            for(int i=0; i<points.Count; i++)
            {
                ArrayList row = new ArrayList();
                row.Add(i);
                cur_clusters_list.Add(row);
            }
            ArrayList distance_matrix = CreateDistanceMatrix(cur_clusters_list);
            ArrayList dendrograms_parts_list = new ArrayList();
            while (distance_matrix.Count != 1)
                ConsolidateClusters(ref distance_matrix, ref cur_clusters_list, ref dendrograms_parts_list);
            return points;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    public class Dendrogram
    {
        public Dendrogram left;
        public Dendrogram right;
        public bool HasValue;
        public int value;
    }
    class ClosestNeighborMethod
    {
        ArrayList points;
        public ClosestNeighborMethod(ArrayList points)
        {
            this.points=points;
        }
        private void ChangeCurClustersList(ref ArrayList cur_cluster_list, int min_i, int min_j)
        {
            ArrayList new_cluster_list=new ArrayList();
            for (int i = 0; i < cur_cluster_list.Count; i++)
            {
                ArrayList cluster_points = new ArrayList();
                if (i == min_j)
                    continue;
                for (int k = 0; k < ((ArrayList)cur_cluster_list[i]).Count; k++)
                    cluster_points.Add(((ArrayList)cur_cluster_list[i])[k]);
                if (i == min_i)
                {
                    for (int k = 0; k < ((ArrayList)cur_cluster_list[min_j]).Count; k++)
                        cluster_points.Add(((ArrayList)cur_cluster_list[min_j])[k]);
                }
                new_cluster_list.Add(cluster_points);
            }
            cur_cluster_list = new_cluster_list;
        }
        private double Distance(int cluster_number1, int cluster_number2, ArrayList cur_clusters_list)
        {
            double min_distance = Double.MaxValue;
            for(int i=0; i<((ArrayList)cur_clusters_list[cluster_number1]).Count; i++)
            {
                int point1_index = (int)((ArrayList)cur_clusters_list[cluster_number1])[i];
                Point point1 = ((Point)points[point1_index]);
                for(int j=0; j<((ArrayList)cur_clusters_list[cluster_number2]).Count; j++)
                {
                    int point2_index = (int)((ArrayList)cur_clusters_list[cluster_number2])[j];
                    Point point2 = ((Point)points[point2_index]);
                    double distance = 0;
                    for (int k = 0; k < point1.coordinates.Count; k++)
                        distance += Math.Pow((int)point1.coordinates[k] - (int)point2.coordinates[k], 2);
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
            for(int i=0; i<cur_clusters_list.Count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < cur_clusters_list.Count; j++)
                {
                    double distance = Distance(i, j, cur_clusters_list);
                    row.Add(distance);
                }
                result.Add(row);
            }
            return result;
        }
        private void ChangeDendrogramParts(ref ArrayList dendrogram_parts_list, ArrayList clusters_list, int min_i, int min_j)
        {
            Dendrogram root = new Dendrogram();
            root.HasValue = false;
            if(((ArrayList)clusters_list[min_i]).Count==1)
            {
                Dendrogram left = new Dendrogram();
                left.left = new Dendrogram();
                left.left.HasValue=false;
                left.right = new Dendrogram();
                left.right.HasValue = false;
                left.HasValue = true;
                left.value = (int)((ArrayList)clusters_list[min_i])[0];
                root.left = left;
            }
            else
            {
                Dendrogram cur_dendrogram = (Dendrogram)dendrogram_parts_list[0];
                int cur_dendrogram_index = 0;
                for(int i=0; i<dendrogram_parts_list.Count; i++)
                {
                    cur_dendrogram = (Dendrogram)dendrogram_parts_list[i];
                    Dendrogram cur_left = cur_dendrogram;
                    while (!cur_left.HasValue)
                        cur_left = cur_left.left;
                    bool isLeftDendrogramFound=false;
                    for (int j = 0; j < ((ArrayList)clusters_list[min_i]).Count; j++)
                    {
                        if((int)((ArrayList)clusters_list[min_i])[j]==cur_left.value)
                        {
                            isLeftDendrogramFound = true;
                            break;
                        }
                    }
                    if (isLeftDendrogramFound)
                    {
                        cur_dendrogram_index = i;
                        break;
                    }
                }
                root.left = cur_dendrogram;
                dendrogram_parts_list.RemoveAt(cur_dendrogram_index);
            }
            if (((ArrayList)clusters_list[min_j]).Count == 1)
            {
                Dendrogram right = new Dendrogram();
                right.left = new Dendrogram();
                right.left.HasValue = false;
                right.right = new Dendrogram();
                right.right.HasValue = false;
                right.HasValue = true;
                right.value = (int)((ArrayList)clusters_list[min_j])[0];
                root.right = right;
            }
            else
            {
                Dendrogram cur_dendrogram = (Dendrogram)dendrogram_parts_list[0];
                int cur_dendrogram_index = 0;
                for (int i = 0; i < dendrogram_parts_list.Count; i++)
                {
                    cur_dendrogram = (Dendrogram)dendrogram_parts_list[i];
                    Dendrogram cur_left = cur_dendrogram;
                    while (!cur_left.HasValue)
                        cur_left = cur_left.left;
                    bool isRightDendrogramFound = false;
                    for (int j = 0; j < ((ArrayList)clusters_list[min_j]).Count; j++)
                    {
                        if ((int)((ArrayList)clusters_list[min_j])[j] == cur_left.value)
                        {
                            isRightDendrogramFound = true;
                            break;
                        }
                    }
                    if (isRightDendrogramFound)
                    {
                        cur_dendrogram_index = i;
                        break;
                    }
                }
                root.right = cur_dendrogram;
                dendrogram_parts_list.RemoveAt(cur_dendrogram_index);
            }
            dendrogram_parts_list.Add(root);
        }
        private void ConsolidateClusters(ref ArrayList distance_matrix, ref ArrayList cur_clusters_list, ref ArrayList dendrogram_parts_list)
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
            ChangeDendrogramParts(ref dendrogram_parts_list, cur_clusters_list, min_i, min_j);
            ChangeCurClustersList(ref cur_clusters_list, min_i, min_j);
            ArrayList new_distance_matrix = CreateDistanceMatrix(cur_clusters_list);
            distance_matrix = new_distance_matrix;
        }
        public Dendrogram Cluster()
        {
            ArrayList cur_clusters_list=new ArrayList();
            for(int i=0; i<points.Count; i++)
            {
                ArrayList row = new ArrayList();
                row.Add(i);
                cur_clusters_list.Add(row);
            }
            ArrayList distance_matrix = CreateDistanceMatrix(cur_clusters_list);
            ArrayList dendrogram_parts_list = new ArrayList();
            while (distance_matrix.Count != 1)
                ConsolidateClusters(ref distance_matrix, ref cur_clusters_list, ref dendrogram_parts_list);
            return (Dendrogram)dendrogram_parts_list[0];
        }
    }
}
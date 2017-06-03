using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class K_Means
    {
        private ArrayList points;
        private int clusters_count;
        private ArrayList LowerBorders, UpperBorders;
        public K_Means(ArrayList points, ArrayList LowerBorders, ArrayList UpperBorders, int clusters_count=3)
        {
            this.points = points;
            this.clusters_count = clusters_count;
            this.LowerBorders = LowerBorders;
            this.UpperBorders = UpperBorders;
        }
        private void ChangeClusters(ArrayList centers)
        {
            for(int i=0; i<points.Count; i++)
            {
                double min_distance = Double.MaxValue;
                int min_distance_index=-1;
                double x = (double)((Point)points[i]).coordinates[0];
                double y = (double)((Point)points[i]).coordinates[1];
                for(int j=0; j<clusters_count; j++)
                {
                    double center_x = (double)((ArrayList)centers[j])[0];
                    double center_y = (double)((ArrayList)centers[j])[1];
                    double distance=0;
                    for (int k = 0; k < ((Point)points[0]).coordinates.Count; k++)
                        distance += Math.Pow((double)((Point)points[i]).coordinates[k] - (double)((ArrayList)centers[j])[k], 2);
                    distance = Math.Sqrt(distance);
                    if(distance<min_distance)
                    {
                        min_distance = distance;
                        min_distance_index = j;
                    }
                }
                ArrayList cluster_numbers = new ArrayList();
                cluster_numbers.Add(min_distance_index + 1);
                Point point = new Point(((Point)points[i]).coordinates, cluster_numbers);
                points.RemoveAt(i);
                points.Insert(i, point);
            }
        }
        private void ChangeCenters(ref ArrayList centers)
        {
            for(int i=0; i<clusters_count; i++)
            {
                for (int j = 0; j < ((Point)points[0]).coordinates.Count; j++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        if ((int)((Point)points[k]).cluster_numbers[0] == i + 1)
                        {
                            sum += (double)((Point)points[k]).coordinates[j];
                            count++;
                        }
                    }
                    ((ArrayList)centers[i])[j] = sum / count;
                }
            }
        }
        private ArrayList CopyCenters(ArrayList centers)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<clusters_count; i++)
            {
                ArrayList cluster_center = new ArrayList();
                cluster_center.Add((double)((ArrayList)centers[i])[0]);
                cluster_center.Add((double)((ArrayList)centers[i])[1]);
                result.Add(cluster_center);
            }
            return result;
        }
        private bool isEqualCenters(ArrayList centers1, ArrayList centers2)
        {
            for (int i = 0; i < clusters_count; i++)
            {
                if ((double)((ArrayList)centers1[i])[0] != (double)((ArrayList)centers2[i])[0])
                    return false;
                if ((double)((ArrayList)centers1[i])[1] != (double)((ArrayList)centers2[i])[1])
                    return false;
            }
            return true;
        }
        public ArrayList Cluster()
        {
            ArrayList centers=new ArrayList();
            Random rand = new Random();
            bool isEmptyClusters = true;
            while (isEmptyClusters)
            {
                centers.Clear();
                for (int i = 0; i < clusters_count; i++)
                {
                    ArrayList center_coordinates = new ArrayList();
                    for (int j = 0; j < ((Point)points[0]).coordinates.Count; j++)
                        center_coordinates.Add((double)UpperBorders[j] * rand.NextDouble() + (double)LowerBorders[j]);
                    centers.Add(center_coordinates);
                }
                ChangeClusters(centers);
                for(int i=1; i<=clusters_count; i++)
                {
                    for(int j=0; j<points.Count; j++)
                    {
                        if ((int)((Point)points[j]).cluster_numbers[0] == i)
                        {
                            isEmptyClusters = false;
                            break;
                        }
                        isEmptyClusters = true;
                    }
                    if (isEmptyClusters)
                        break;
                }
            }
            ArrayList old_centers = CopyCenters(centers);
            ChangeCenters(ref centers);
            while (!isEqualCenters(old_centers, centers))
            {
                ChangeClusters(centers);
                old_centers = CopyCenters(centers);
                ChangeCenters(ref centers);
            }
            return points;
        }
    }
}
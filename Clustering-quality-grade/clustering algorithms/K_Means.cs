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
        private int width;
        private int height;
        public K_Means(ArrayList points, int width, int height, int clusters_count=3)
        {
            this.points = points;
            this.clusters_count = clusters_count;
            this.width = width;
            this.height = height;
        }
        private void ChangeClusters(ArrayList centers)
        {
            for(int i=0; i<points.Count; i++)
            {
                double min_distance = Double.MaxValue;
                int min_distance_index=-1;
                int x = (int)((Point)points[i]).coordinates[0];
                int y = (int)((Point)points[i]).coordinates[1];
                for(int j=0; j<clusters_count; j++)
                {
                    int center_x = (int)((ArrayList)centers[j])[0];
                    int center_y = (int)((ArrayList)centers[j])[1];
                    double distance = Math.Sqrt(Math.Pow(x - center_x, 2) + Math.Pow(y - center_y, 2));
                    if(distance<min_distance)
                    {
                        min_distance = distance;
                        min_distance_index = j;
                    }
                }
                Point point=new Point((int)((Point)points[i]).coordinates[0], (int)((Point)points[i]).coordinates[1]);
                point.cluster_number=min_distance_index+1;
                points.RemoveAt(i);
                points.Insert(i, point);
            }
        }
        private void ChangeCenters(ref ArrayList centers)
        {
            for(int i=0; i<clusters_count; i++)
            {
                double sum_x = 0;
                double sum_y = 0;
                int count = 0;
                for(int j=0; j<points.Count; j++)
                {
                    if(((Point)points[j]).cluster_number==i+1)
                    {
                        sum_x += (int)((Point)points[j]).coordinates[0];
                        sum_y += (int)((Point)points[j]).coordinates[1];
                        count++;
                    }
                }
                ((ArrayList)centers[i])[0] = (int)(sum_x / count);
                ((ArrayList)centers[i])[1] = (int)(sum_y / count);
            }
        }
        private ArrayList CopyCenters(ArrayList centers)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<clusters_count; i++)
            {
                ArrayList cluster_center = new ArrayList();
                cluster_center.Add((int)((ArrayList)centers[i])[0]);
                cluster_center.Add((int)((ArrayList)centers[i])[1]);
                result.Add(cluster_center);
            }
            return result;
        }
        private bool isEqualCenters(ArrayList centers1, ArrayList centers2)
        {
            for (int i = 0; i < clusters_count; i++)
            {
                if((int)((ArrayList)centers1[i])[0]!=(int)((ArrayList)centers2[i])[0])
                    return false;
                if ((int)((ArrayList)centers1[i])[1] != (int)((ArrayList)centers2[i])[1])
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
                    center_coordinates.Add(rand.Next(0, width));
                    center_coordinates.Add(rand.Next(0, height));
                    centers.Add(center_coordinates);
                }
                ChangeClusters(centers);
                for(int i=1; i<=clusters_count; i++)
                {
                    for(int j=0; j<points.Count; j++)
                    {
                        if(((Point)points[j]).cluster_number==i)
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

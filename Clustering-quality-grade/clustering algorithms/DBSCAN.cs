using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class DBSCAN
    {
        private ArrayList points;//номер кластера 0 - шум, номер кластера -1 - непосещённая точка
        private int eps;
        private int min_points_count;
        public DBSCAN(ArrayList points, int eps=50, int min_points_count=3)
        {
            this.points=points;
            this.eps=eps;
            this.min_points_count=min_points_count;
        }
        private void CarryPointToCluster(int point_number, int cluster_number)
        {
            Point point = (Point)points[point_number];
            point.cluster_number = cluster_number;
            points.RemoveAt(point_number);
            points.Insert(point_number, point);
        }
        private void MakeAllPointsUnvisited()
        {
            for (int i = 0; i < points.Count; i++)
                CarryPointToCluster(i, -1);
        }
        private ArrayList NeightbourPointsNumbers(int point_number)
        {
            ArrayList result = new ArrayList();
            int dimension = ((Point)points[0]).coordinates.Count;
            for(int i=0; i<points.Count; i++)
            {
                double distance = 0;
                for (int j = 0; j < dimension; j++)
                    distance += Math.Pow((int)((Point)points[i]).coordinates[j] - 
                        (int)((Point)points[point_number]).coordinates[j], 2);
                distance = Math.Sqrt(distance);
                if (distance <= eps)
                    result.Add(i);
            }
            return result;
        }
        private void CombineNeightbourPoints(ref ArrayList neightbour_points, ArrayList new_neightbour_points)
        {
            for(int i=0; i<new_neightbour_points.Count; i++)
            {
                bool isNewElement = true;
                for(int j=0; j<neightbour_points.Count; j++)
                {
                    if(new_neightbour_points[i]==neightbour_points[j])
                    {
                        isNewElement = false;
                        break;
                    }
                }
                if (isNewElement)
                    neightbour_points.Add(new_neightbour_points[i]);
            }
        }
        private void ExpandCluster(int point_number, ArrayList neightbour_points, int cluster_number)
        {
            CarryPointToCluster(point_number, cluster_number);
            for(int i=0; i<neightbour_points.Count; i++)
            {
                int cur_point_number=(int)neightbour_points[i];
                if (((Point)points[cur_point_number]).cluster_number == -1)
                {
                    ArrayList new_neightbour_points = NeightbourPointsNumbers(cur_point_number);
                    if (new_neightbour_points.Count >= min_points_count)
                        CombineNeightbourPoints(ref neightbour_points, new_neightbour_points);
                }
                if (((Point)points[cur_point_number]).cluster_number < 1)
                    CarryPointToCluster(cur_point_number, cluster_number);
            }
        }
        public ArrayList Cluster()
        {
            int cluster_number = 0;
            MakeAllPointsUnvisited();
            for (int i = 0; i < points.Count; i++)
            {
                if (((Point)points[i]).cluster_number != -1)
                    continue;
                ArrayList neightbour_points = NeightbourPointsNumbers(i);
                if (neightbour_points.Count < min_points_count)
                    CarryPointToCluster(i, 0);
                else
                {
                    cluster_number++;
                    ExpandCluster(i, neightbour_points, cluster_number);
                }
            }
            return points;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class C_Means
    {
        ArrayList points;
        int clusters_count;
        double eps = 0.001;
        public C_Means(ArrayList points, int clusters_count = 3)
        {
            this.points=points;
            this.clusters_count = clusters_count;
        }
        private ArrayList ComputeClustersCenters(ArrayList MembershipMatrix, int dimension)
        {
            ArrayList clusters_centers = new ArrayList();
            for (int i = 0; i < clusters_count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < dimension; j++)
                {
                    double sum1 = 0, sum2 = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        sum1 += Math.Pow((double)((ArrayList)MembershipMatrix[k])[i], 2) *
                            (double)((Point)points[k]).coordinates[j];
                        sum2 += Math.Pow((double)((ArrayList)MembershipMatrix[k])[i], 2);
                    }
                    row.Add(sum1 / sum2);
                }
                clusters_centers.Add(row);
            }
            return clusters_centers;
        }
        private double ComputeDistance(ArrayList clusters_centers, int cluster_number, int point_number, int dimension)
        {
            double result = 0;
            for(int i=0; i<dimension; i++)
                result += Math.Pow((double)((ArrayList)clusters_centers[cluster_number])[i] -
                    (double)((Point)points[point_number]).coordinates[i], 2);
            result = Math.Sqrt(result);
            return result;
        }
        private ArrayList ComputeMembershipMatrix(ArrayList clusters_centers, int dimension)
        {
            ArrayList result = new ArrayList();
            for (int i = 0; i < points.Count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < clusters_count; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < clusters_count; k++)
                        sum += Math.Pow(ComputeDistance(clusters_centers, j, i, dimension) /
                            ComputeDistance(clusters_centers, k, i, dimension), 2);
                    row.Add(1 / sum);
                }
                result.Add(row);
            }
            return result;
        }
        private double MatrixDifference(ArrayList matrix1, ArrayList matrix2)
        {
            double result = 0;
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < clusters_count; j++)
                {
                    double element1 = (double)((ArrayList)matrix1[i])[j];
                    double element2 = (double)((ArrayList)matrix2[i])[j];
                    double difference = Math.Abs(element1 - element2);
                    if (difference > result)
                        result = difference;
                }
            }
            return result;
        }
        public ArrayList Cluster()
        {
            ArrayList MembershipMatrix = new ArrayList();
            Random rand = new Random();
            for (int i = 0; i < points.Count; i++)
            {
                ArrayList row = new ArrayList();
                double sum = 0;
                for (int j = 0; j < clusters_count; j++)
                {
                    double element = rand.NextDouble();
                    sum += element;
                    row.Add(element);
                }
                for (int j = 0; j < clusters_count; j++)
                    row[j] = (double)row[j] / sum;
                MembershipMatrix.Add(row);
            }
            int dimension = ((Point)points[0]).coordinates.Count;
            ArrayList clusters_centers = ComputeClustersCenters(MembershipMatrix, dimension);
            ArrayList new_MembershipMatrix = ComputeMembershipMatrix(clusters_centers, dimension);
            while(MatrixDifference(MembershipMatrix, new_MembershipMatrix)>=eps)
            {
                clusters_centers = ComputeClustersCenters(new_MembershipMatrix, dimension);
                MembershipMatrix = new_MembershipMatrix;
                new_MembershipMatrix = ComputeMembershipMatrix(clusters_centers, dimension);
            }
            return MembershipMatrix;
        }
    }
}
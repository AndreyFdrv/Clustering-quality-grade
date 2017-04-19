using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Scatter_Distance_index
    {
        private ArrayList objects;
        public Scatter_Distance_index(ArrayList objects)
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
        private ArrayList dispersion()
        {
            int dimension = ((Point)objects[0]).coordinates.Count;
            ArrayList center_point = center();
            ArrayList result = new ArrayList();
            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < objects.Count; j++)
                    sum += Math.Pow((int)((Point)objects[j]).coordinates[i] - (double)center_point[i], 2.0);
                result.Add(sum / objects.Count);
            }
            return result;
        }
        private ArrayList cluster_dispersion(int cluster_number)
        {
            int cluster_size = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_number)
                    cluster_size++;
            }
            int dimension = ((Point)objects[0]).coordinates.Count;
            ArrayList center_point = cluster_center(cluster_number);
            ArrayList result = new ArrayList();
            for (int i = 0; i < dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < objects.Count; j++)
                {
                    if (((Point)objects[j]).cluster_number == cluster_number)
                        sum += Math.Pow((int)((Point)objects[j]).coordinates[i] - (double)center_point[i], 2.0);
                }
                result.Add(sum / cluster_size);
            }
            return result;
        }
        private double Scatter()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double dispersion_module = 0;
            ArrayList dispersion_vector = dispersion();
            for (int i = 0; i < dispersion_vector.Count; i++)
                dispersion_module += (double)dispersion_vector[i] * (double)dispersion_vector[i];
            dispersion_module = Math.Sqrt(dispersion_module);
            double sum = 0;
            for (int i = 1; i <= clusters_count; i++)
            {
                double cluster_dispersion_module = 0;
                ArrayList cluster_dispersion_vector = cluster_dispersion(i);
                for (int j = 0; j < cluster_dispersion_vector.Count; j++)
                    cluster_dispersion_module += (double)cluster_dispersion_vector[j] * (double)cluster_dispersion_vector[j];
                cluster_dispersion_module = Math.Sqrt(cluster_dispersion_module);
                sum += cluster_dispersion_module;
            }
            return sum / (clusters_count * dispersion_module);
        }
        private double Distance()
        {
            double max_distance = 0, min_distance=Double.MaxValue;
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            int dimension = ((Point)objects[0]).coordinates.Count;
            for(int i=1; i<clusters_count; i++)
            {
                for(int j=1; j<clusters_count; j++)
                {
                    if(i==j)
                        continue;
                    double distance = 0;
                    for(int k=0; k<dimension; k++)
                        distance+=Math.Pow((double)cluster_center(i)[k]-(double)cluster_center(j)[k], 2);
                    distance = Math.Sqrt(distance);
                    if (distance > max_distance)
                        max_distance = distance;
                    if (distance < min_distance)
                        min_distance = distance;
                }
            }
            double sum = 0;
            for(int i=1; i<=clusters_count; i++)
            {
                double temp = 0;
                for(int j=1; j<=clusters_count; j++)
                {
                    if (i == j)
                        continue;
                    double distance = 0;
                    for (int k = 0; k < dimension; k++)
                        distance += Math.Pow((double)cluster_center(i)[k] - (double)cluster_center(j)[k], 2);
                    distance = Math.Sqrt(distance);
                    temp += distance;
                }
                sum += 1 / temp;
            }
            return max_distance * sum / min_distance;
        }
        public double compute(double alpha)
        {
            return alpha * Scatter() + Distance();
        }
    }
}
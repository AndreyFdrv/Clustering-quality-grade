using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class Scatter_Density_index
    {
        private ArrayList objects;
        public Scatter_Density_index(ArrayList objects)
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
        private ArrayList center_between_clusters(ArrayList center1, ArrayList center2)
        {
            ArrayList result = new ArrayList();
            int dimension = center1.Count;
            for (int i = 0; i < dimension; i++)
            {
                double coordinate = ((double)center1[i]+(double)center2[i])/2;
                result.Add(coordinate);
            }
            return result;
        }
        private int f(Point point1, ArrayList point2)
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
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
            double min_distance = Math.Sqrt(sum) / clusters_count;
            double distance = 0;
            double dimension=point2.Count;
            for (int i = 0; i < dimension; i++)
                distance += ((int)point1.coordinates[i] - (double)point2[i]) * ((int)point1.coordinates[i] - (double)point2[i]);
            distance = Math.Sqrt(distance);
            if (distance > min_distance)
                return 0;
            else
                return 1;
        }
        private double dens(int cluster_i, int cluster_j)
        {
            double sum = 0;
            ArrayList center1=cluster_center(cluster_i);
            ArrayList center2=cluster_center(cluster_j);
            ArrayList center_point=center_between_clusters(center1, center2);
            for(int i=0; i<objects.Count; i++)
            {
                if((((Point)objects[i]).cluster_number==cluster_i)||(((Point)objects[i]).cluster_number==cluster_j))
                    sum += f((Point)objects[i], center_point);
            }
            return sum;
        }
        private double dens(int cluster_i)
        {
            double sum = 0;
            ArrayList center_point = cluster_center(cluster_i);
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number == cluster_i) 
                    sum += f((Point)objects[i], center_point);
            }
            return sum;
        }
        private double Density()
        {
            int clusters_count = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (((Point)objects[i]).cluster_number > clusters_count)
                    clusters_count = ((Point)objects[i]).cluster_number;
            }
            double sum=0;
            for(int i=1; i<=clusters_count; i++)
            {
                for(int j=1; j<=clusters_count; j++)
                {
                    if (i == j)
                        continue;
                    double max_dens;
                    if (dens(i) > dens(j))
                        max_dens = dens(i);
                    else
                        max_dens = dens(j);
                    if (max_dens != 0)
                        sum += dens(i, j) / max_dens;
                    else
                        sum += dens(i, j);
                }
            }
            return sum / (clusters_count * (clusters_count-1));
        }
        public double compute()
        {
            return Density() + Scatter();
        }
    }
}
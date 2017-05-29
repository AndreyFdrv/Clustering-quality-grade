using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Clustering_quality_grade
{
    class Point
    {
        public ArrayList coordinates;
        public ArrayList cluster_numbers;
        public Point()
        {
            coordinates = new ArrayList();
            cluster_numbers = new ArrayList();
        }
        public Point(int x, int y)
        {
            coordinates = new ArrayList();
            coordinates.Add(x);
            coordinates.Add(y);
            cluster_numbers = new ArrayList();
        }
        public Point(ArrayList coordinates, ArrayList cluster_numbers)
        {
            this.coordinates = coordinates;
            this.cluster_numbers = cluster_numbers;
        }
    }
}
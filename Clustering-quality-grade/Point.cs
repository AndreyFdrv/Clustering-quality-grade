using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Clustering_quality_grade
{
    struct Point
    {
            public ArrayList coordinates;
            public int cluster_number;
            public Point(int x, int y)
            {
                coordinates = new ArrayList();
                coordinates.Add(x);
                coordinates.Add(y);
                cluster_number = 0;
            }
            public Point(ArrayList coordinates, int cluster_number)
            {
                this.coordinates = coordinates;
                this.cluster_number = cluster_number;
            }
    }
}
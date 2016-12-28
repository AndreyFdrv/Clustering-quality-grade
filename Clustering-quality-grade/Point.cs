using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генератор_кластеризованных_множеств
{
    struct Point
    {
            public int x;
            public int y;
            public int cluster_number;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
                cluster_number = 0;
            }
    }
}
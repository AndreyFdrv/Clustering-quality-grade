﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.IO;
namespace Clustering_quality_grade
{
    public partial class TimeCalculationForm : Form
    {
        Random rand = new Random();
        public TimeCalculationForm()
        {
            InitializeComponent();
        }
        ArrayList GenerateObjects(int clusters_count, int dimension, long objects_count)
        {
            ArrayList objects = new ArrayList();
            for(int i=0; i<objects_count; i++)
            {
                ArrayList coordinates = new ArrayList();
                for (int j = 0; j<dimension; j++)
                    coordinates.Add(rand.Next(0, 1000));
                int cluster_number = rand.Next(1, clusters_count);
                Point point=new Point(coordinates, cluster_number);
                objects.Add(point);
            }
            return objects;
        }
        ArrayList GenerateClassInfo(int classes_count, long objects_count)
        {
            ArrayList class_info = new ArrayList();
            for (int i = 0; i < objects_count; i++)
            {
                int class_number = rand.Next(1, classes_count);
                class_info.Add(class_number);
            }
            return class_info;
        }
        void F1_meassure_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("f1-meassure.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("F1-мера\r\n");
            writer.Write("Размерность: "+dimension.ToString()+"\t");
            writer.Write("Количество объектов: " + objects_count.ToString()+"\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for(clusters_count=2; clusters_count<=10; clusters_count++)
            {
                ArrayList objects=GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info=GenerateClassInfo(clusters_count, objects_count);
                F1_meassure f1_meassure = new F1_meassure(objects, class_info);
                float sum_time=0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    f1_meassure.F1();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                F1_meassure f1_meassure = new F1_meassure(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    f1_meassure.F1();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count+=1000)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                F1_meassure f1_meassure = new F1_meassure(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    f1_meassure.F1();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        void Rand_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Rand.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Rand\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Rand_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Rand_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Rand_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        void Jaccard_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Jaccard.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Jaccard\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Jaccard_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Jaccard_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.Jaccard_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        void FM_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс FM.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс FM\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.FM_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.FM_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateClassInfo(clusters_count, objects_count);
                ArrayList class_info = GenerateClassInfo(clusters_count, objects_count);
                Rand_Jaccard_FM index = new Rand_Jaccard_FM(objects, class_info);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.FM_index();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Calinski_Harabasz_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Calinski-Harabasz criterion.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Критерий Calinski-Harabasz\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Calinski_Harabasz_criterion criterion = new Calinski_Harabasz_criterion(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    criterion.compute_criterion();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Calinski_Harabasz_criterion criterion = new Calinski_Harabasz_criterion(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    criterion.compute_criterion();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Calinski_Harabasz_criterion criterion = new Calinski_Harabasz_criterion(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    criterion.compute_criterion();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Dunn_index_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Данна.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Данна\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Dunn_index index = new Dunn_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Dunn_index index = new Dunn_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Dunn_index index = new Dunn_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void DBI_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Девиса-Болдуина.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Девиса-Болдуина\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Davies_Bouldin_index index = new Davies_Bouldin_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Davies_Bouldin_index index = new Davies_Bouldin_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Davies_Bouldin_index index = new Davies_Bouldin_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Scatter_Distance_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Scatter_Distance.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Scatter-Distance\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Distance_index index = new Scatter_Distance_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute(1);
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Distance_index index = new Scatter_Distance_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute(1);
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Distance_index index = new Scatter_Distance_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute(1);
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Scatter_Density_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Scatter_Density.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Scatter_Density\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Density_index index = new Scatter_Density_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Density_index index = new Scatter_Density_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Scatter_Density_index index = new Scatter_Density_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void RMSSTD_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс RMSSTD.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс RMSSTD\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RMSSTD index = new RMSSTD(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RMSSTD index = new RMSSTD(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RMSSTD index = new RMSSTD(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void hubert_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Hubert's Г статистика.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Hubert's Г статистика\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Hubert_Gamma_Statistic index = new Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Hubert_Gamma_Statistic index = new Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Hubert_Gamma_Statistic index = new Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void modified_hubert_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Модифицированная Hubert's Г статистика.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Модифицированная Hubert's Г статистика\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Modified_Hubert_Gamma_Statistic index = new Modified_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Modified_Hubert_Gamma_Statistic index = new Modified_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Modified_Hubert_Gamma_Statistic index = new Modified_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void normalized_hubert_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Нормализованная Hubert's Г статистика.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Нормализованная Hubert's Г статистика\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Normalized_Hubert_Gamma_Statistic index = new Normalized_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Normalized_Hubert_Gamma_Statistic index = new Normalized_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Normalized_Hubert_Gamma_Statistic index = new Normalized_Hubert_Gamma_Statistic(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void RS_index_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс RS.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс RS\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RS_Index index = new RS_Index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RS_Index index = new RS_Index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                RS_Index index = new RS_Index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void silhouette_index_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Силуэта.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Силуэта\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                SilhouetteIndex index = new SilhouetteIndex(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                SilhouetteIndex index = new SilhouetteIndex(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                SilhouetteIndex index = new SilhouetteIndex(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Maulik_Bandyopadhyay_time_calculation()
        {
            int clusters_count, dimension;
            long objects_count;
            dimension = 3;
            objects_count = 1000;
            Stopwatch sw = new Stopwatch();
            FileStream file = new FileStream("Индекс Маулика-Бандиопадия.txt", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(file);
            writer.Write(" \t");
            writer.Write("Индекс Маулика-Бандиопадия\r\n");
            writer.Write("Размерность: " + dimension.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Количество кластеров:\tВремя, с:\r\n");
            for (clusters_count = 2; clusters_count <= 10; clusters_count++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Maulik_Bandyopadhyay_index index = new Maulik_Bandyopadhyay_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(clusters_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
            writer.Write("Размерность:\tВремя, с:\r\n");
            for (dimension = 1; dimension <= 10; dimension++)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Maulik_Bandyopadhyay_index index = new Maulik_Bandyopadhyay_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(dimension.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
            writer.Write("Количество объектов:\tВремя, с:\r\n");
            for (objects_count = 1000; objects_count <= 10000; objects_count += 1000)
            {
                ArrayList objects = GenerateObjects(clusters_count, dimension, objects_count);
                Maulik_Bandyopadhyay_index index = new Maulik_Bandyopadhyay_index(objects);
                float sum_time = 0;
                for (int i = 0; i < 5; i++)
                {
                    sw.Restart();
                    index.compute();
                    sw.Stop();
                    sum_time += (float)sw.ElapsedMilliseconds / 1000;
                }
                writer.Write(objects_count.ToString() + "\t");
                writer.Write((sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Start_Click(object sender, EventArgs e)
        {
            if (F1_meassure_rb.Checked)
                F1_meassure_time_calculation();
            else if (rand_rb.Checked)
                Rand_time_calculation();
            else if (jaccard_rb.Checked)
                Jaccard_time_calculation();
            else if (FM_rb.Checked)
                FM_time_calculation();
            else if (Calinski_Harabasz_rb.Checked)
                Calinski_Harabasz_time_calculation();
            else if (Dunn_rb.Checked)
                Dunn_index_time_calculation();
            else if (DBI_rb.Checked)
                DBI_time_calculation();
            else if (Scatter_Distance_rb.Checked)
                Scatter_Distance_time_calculation();
            else if (Scatter_Density_rb.Checked)
                Scatter_Density_time_calculation();
            else if (RMSSTD_rb.Checked)
                RMSSTD_time_calculation();
            else if (hubert_rb.Checked)
                hubert_time_calculation();
            else if (modified_hubert_rb.Checked)
                modified_hubert_time_calculation();
            else if (normalized_hubert_rb.Checked)
                normalized_hubert_time_calculation();
            else if (RS_index_rb.Checked)
                RS_index_time_calculation();
            else if (Silhouette_rb.Checked)
                silhouette_index_time_calculation();
            else if (Maulik_Bandyopadhyay_rb.Checked)
                Maulik_Bandyopadhyay_time_calculation();
            MessageBox.Show("Расчёт времени окончен, результат находится в папке с исполнимым файлом");
        }
    }
}
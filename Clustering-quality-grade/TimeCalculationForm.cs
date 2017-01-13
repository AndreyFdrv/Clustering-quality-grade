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
                writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
                writer.Write("Время, с: " + (sum_time / 5).ToString() + "\r\n");
            }
            clusters_count = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Количество объектов: " + objects_count.ToString() + "\r\n");
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
                writer.Write("Размерность: " + dimension.ToString() + "\t");
                writer.Write("Время, с: " + (sum_time / 5).ToString() + "\r\n");
            }
            dimension = 3;
            writer.Write("Количество кластеров: " + clusters_count.ToString() + "\t");
            writer.Write("Размерность: " + dimension.ToString() + "\r\n");
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
                writer.Write("Количество объектов: " + objects_count.ToString() + "\t");
                writer.Write("Время, с: " + (sum_time / 5).ToString() + "\r\n");
            }
            writer.Close();
        }
        private void Start_Click(object sender, EventArgs e)
        {
            if (F1_meassure_rb.Checked)
                F1_meassure_time_calculation();
            MessageBox.Show("Расчёт времени окончен, результат находится в папке с исполнимым файлом");
        }
    }
}
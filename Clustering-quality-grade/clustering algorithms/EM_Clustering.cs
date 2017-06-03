using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Clustering_quality_grade
{
    class EM_Clustering
    {
        private ArrayList points;
        private int clusters_count;
        private double log_likelihood_deviation = 0.01;
        private long max_iteration_number = 5;//10000;
        private ArrayList expected_value_matrix = new ArrayList();
        private ArrayList covariance_matrix = new ArrayList();
        private ArrayList weights = new ArrayList();
        public EM_Clustering(ArrayList points, int clusters_count = 3)
        {
            this.points = points;
            this.clusters_count = clusters_count;
        }
        private ArrayList MatrixMultiplication(ArrayList matrix1, ArrayList matrix2)
        {
            ArrayList result = new ArrayList();
            for (int matrix1_row_number = 0; matrix1_row_number < matrix1.Count; matrix1_row_number++)
            {
                ArrayList row = new ArrayList();
                for (int matrix2_col_number = 0; matrix2_col_number < ((ArrayList)matrix2[0]).Count; matrix2_col_number++)
                {
                    double sum = 0;
                    for (int i = 0; i < matrix2.Count; i++)
                    {
                        sum += (double)((ArrayList)matrix1[matrix1_row_number])[i] *
                            (double)((ArrayList)matrix2[i])[matrix2_col_number];
                    }
                    row.Add(sum);
                }
                result.Add(row);
            }
            return result;
        }
        private ArrayList MatrixInvert(ArrayList matrix)
        {
            int N = matrix.Count;
            int[] row = new int[N];
            int[] col = new int[N];
            double[] temp = new double[N];
            int hold, I_pivot, J_pivot;
            double pivot, abs_pivot;
            for (int i = 0; i < N; i++)
            {
                row[i] = i;
                col[i] = i;
            }
            ArrayList result = new ArrayList();
            for (int i = 0; i < N; i++)
            {
                ArrayList result_row = new ArrayList();
                for (int j = 0; j < N; j++)
                    result_row.Add(((ArrayList)matrix[i])[j]);
                result.Add(result_row);
            }
            for (int k = 0; k < N; k++)
            {
                pivot = (double)((ArrayList)result[row[k]])[col[k]];
                I_pivot = k;
                J_pivot = k;
                for (int i = k; i < N; i++)
                {
                    for (int j = k; j < N; j++)
                    {
                        abs_pivot = Math.Abs(pivot);
                        if (Math.Abs((double)((ArrayList)result[row[i]])[col[j]]) > abs_pivot)
                        {
                            I_pivot = i;
                            J_pivot = j;
                            pivot = (double)((ArrayList)result[row[i]])[col[j]];
                        }
                    }
                }
                if (Math.Abs(pivot) < 1.0E-10)
                    return null;
                hold = row[k];
                row[k] = row[I_pivot];
                row[I_pivot] = hold;
                hold = col[k];
                col[k] = col[J_pivot];
                col[J_pivot] = hold;
                ((ArrayList)result[row[k]])[col[k]] = 1.0 / pivot;
                for (int j = 0; j < N; j++)
                {
                    if (j != k)
                        ((ArrayList)result[row[k]])[col[j]] = (double)((ArrayList)result[row[k]])[col[j]] *
                            (double)((ArrayList)result[row[k]])[col[k]];
                }
                for (int i = 0; i < N; i++)
                {
                    if (k != i)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (k != j)
                            {
                                ((ArrayList)result[row[i]])[col[j]] = (double)((ArrayList)result[row[i]])[col[j]] -
                                   (double)((ArrayList)result[row[i]])[col[k]] * (double)((ArrayList)result[row[k]])[col[j]];
                            }
                        }
                        ((ArrayList)result[row[i]])[col[k]] = -(double)((ArrayList)result[row[i]])[col[k]] *
                            (double)((ArrayList)result[row[k]])[col[k]];
                    }
                }
            }
            for (int j = 0; j < N; j++)
            {
                for (int i = 0; i < N; i++)
                    temp[col[i]] = (double)((ArrayList)result[row[i]])[j];
                for (int i = 0; i < N; i++)
                    ((ArrayList)result[i])[j] = temp[i];
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    temp[row[j]] = (double)((ArrayList)result[i])[col[j]];
                for (int j = 0; j < N; j++)
                    ((ArrayList)result[i])[j] = temp[j];
            }
            return result;
        }
        private double MatrixMinor(ArrayList matrix, int row_number, int col_number)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<matrix.Count; i++)
            {
                if (i != row_number)
                {
                    ArrayList row = new ArrayList();
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        if (j != col_number)
                            row.Add((double)((ArrayList)matrix[i])[j]);
                    }
                    result.Add(row);
                }
            }
            return MatrixDeterminant(result);
        }
        private double MatrixDeterminant(ArrayList matrix)
        {
            if (matrix.Count == 1)
                return (double)((ArrayList)matrix[0])[0];
            double sum = 0.000001;
            for (int i = 0; i < matrix.Count; i++)
                sum += Math.Pow(-1, i) * (double)((ArrayList)matrix[0])[i] * MatrixMinor(matrix, 0, i);
            return sum;
        }
        private ArrayList MatrixAddition(ArrayList matrix1, ArrayList matrix2)
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<matrix1.Count; i++)
            {
                ArrayList row1=(ArrayList)matrix1[i];
                ArrayList row2=(ArrayList)matrix2[i];
                ArrayList result_row=new ArrayList();
                for (int j = 0; j < row1.Count; j++)
                    result_row.Add((double)row1[j] + (double)row2[j]);
                result.Add(result_row);
            }
            return result;
        }
        private void Initialization()
        {
            for(int i=0; i<((Point)points[0]).coordinates.Count; i++)
            {
                ArrayList row = new ArrayList();
                int cluster_size = (points.Count) / clusters_count;
                for(int j=0; j<clusters_count; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cluster_size; k++)
                    {
                        sum += (double)((Point)points[j * cluster_size + k]).coordinates[i];
                    }
                    row.Add(sum / cluster_size);
                }
                expected_value_matrix.Add(row);
            }
            for (int i = 0; i < ((Point)points[0]).coordinates.Count; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < ((Point)points[0]).coordinates.Count; j++)
                {
                    if(i==j)
                        row.Add(1.0);
                    else
                        row.Add(0.0);
                }
                covariance_matrix.Add(row);
            }
            for (int i = 0; i < clusters_count; i++)
                weights.Add(1.0/clusters_count);
        }
        private double E(ref ArrayList temp_expected_value_matrix, ref ArrayList temp_covariance_matrix, ref ArrayList temp_weights, ref ArrayList probabilities)
        {
            temp_expected_value_matrix.Clear();
            temp_covariance_matrix.Clear();
            temp_weights.Clear();
            probabilities.Clear();
            int dimension = ((Point)points[0]).coordinates.Count;
            for (int i = 0; i < dimension; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < clusters_count; j++)
                {
                    row.Add(0.0);
                }
                temp_expected_value_matrix.Add(row);
            }
            for (int i = 0; i < dimension; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < dimension; j++)
                    row.Add(0.0);
                temp_covariance_matrix.Add(row);
            }
            for (int i = 0; i < clusters_count; i++)
            {
                ArrayList row = new ArrayList();
                row.Add(0.0);
                temp_weights.Add(row);
            }
            double log_likelihood = 0;
            for (int i = 0; i < points.Count; i++)
            {
                double sum_probability = 0.000001;
                ArrayList object_probabilities = new ArrayList();
                for(int j=0; j<clusters_count; j++)
                {
                    ArrayList matrix1 = new ArrayList();
                    ArrayList matrix3 = new ArrayList();
                    ArrayList row1 = new ArrayList();
                    for (int k = 0; k < dimension; k++)
                    {
                        row1.Add(((double)((Point)points[i]).coordinates[k]) - (double)((ArrayList)expected_value_matrix[k])[j]);
                        ArrayList row3 = new ArrayList();
                        row3.Add(((double)((Point)points[i]).coordinates[k]) - (double)((ArrayList)expected_value_matrix[k])[j]);
                        matrix3.Add(row3);
                    }
                    matrix1.Add(row1);
                    ArrayList matrix2 = MatrixInvert(covariance_matrix);
                    double power;
                    if (matrix2 == null)
                        power = -0.5 * (double)((ArrayList)(MatrixMultiplication(matrix1, matrix3))[0])[0];
                    else
                        power=-0.5*(double)((ArrayList)(MatrixMultiplication
                            (MatrixMultiplication(matrix1, matrix2), matrix3))[0])[0];
                    double test = Math.Exp(power);
                    double probability = (double)weights[j]*Math.Exp(power)/(Math.Pow(2*Math.PI, (double)dimension/2)*
                        Math.Sqrt(MatrixDeterminant(covariance_matrix)));
                    object_probabilities.Add(probability);
                    sum_probability += probability;
                }
                ArrayList probabilities_row = new ArrayList();
                for (int k = 0; k < clusters_count; k++)
                    probabilities_row.Add((double)object_probabilities[k] / sum_probability);
                probabilities.Add(probabilities_row);
                log_likelihood += Math.Log(sum_probability);
                ArrayList coordinates_matrix=new ArrayList();
                for(int k=0; k<dimension; k++)
                {
                    ArrayList row=new ArrayList();
                    row.Add((double)((Point)points[i]).coordinates[k]);
                    coordinates_matrix.Add(row);
                }
                ArrayList weight_probabilities_row_matrix=new ArrayList();
                weight_probabilities_row_matrix.Add(probabilities_row);
                temp_expected_value_matrix = MatrixAddition(temp_expected_value_matrix, MatrixMultiplication(coordinates_matrix,
                    weight_probabilities_row_matrix));
                ArrayList weight_probabilities_column_matrix=new ArrayList();
                for(int k=0; k<clusters_count; k++)
                {
                    ArrayList row=new ArrayList();
                    row.Add(probabilities_row[k]);
                    weight_probabilities_column_matrix.Add(row);
                }
                temp_weights = MatrixAddition(temp_weights, weight_probabilities_column_matrix);
            }
            return log_likelihood;
        }
        private void M(ref ArrayList temp_expected_value_matrix, ref ArrayList temp_covariance_matrix, ref ArrayList temp_weights, ref ArrayList probabilities)
        {
            int dimension = ((Point)points[0]).coordinates.Count;
            for(int i=0; i<clusters_count; i++)
            {
                for (int j = 0; j < dimension; j++)
                    ((ArrayList)expected_value_matrix[j])[i] = (double)((ArrayList)temp_expected_value_matrix[j])[i]/
                        (double)((ArrayList)temp_weights[i])[0];
                for(int j=0; j<points.Count; j++)
                {
                    ArrayList matrix1 = new ArrayList();
                    ArrayList matrix3 = new ArrayList();
                    ArrayList row3 = new ArrayList();
                    for (int k = 0; k < dimension; k++)
                    {
                        row3.Add(((double)((Point)points[j]).coordinates[k]) - 
                            (double)((ArrayList)expected_value_matrix[k])[i]);
                        ArrayList row1 = new ArrayList();
                        row1.Add(((double)((Point)points[j]).coordinates[k]) - 
                            (double)((ArrayList)expected_value_matrix[k])[i]);
                        matrix1.Add(row1);
                    }
                    matrix3.Add(row3);
                    ArrayList matrix2 = new ArrayList();
                    ArrayList row2 = new ArrayList();
                    row2.Add(((ArrayList)probabilities[j])[i]);
                    matrix2.Add(row2);
                    temp_covariance_matrix = MatrixAddition(temp_covariance_matrix, MatrixMultiplication(MatrixMultiplication
                        (matrix1, matrix2), matrix3));
                }
            }
            for (int i = 0; i < dimension; i++)
            {
                ArrayList row = new ArrayList();
                for (int j = 0; j < dimension; j++)
                {
                    ((ArrayList)covariance_matrix[i])[j] = (double)((ArrayList)temp_covariance_matrix[i])[j] / points.Count;
                }
            }
            for (int i = 0; i < clusters_count; i++)
                weights[i] = (double)((ArrayList)temp_weights[i])[0] / points.Count;
        }
        public ArrayList Cluster()
        {
            Initialization();
            ArrayList temp_expected_value_matrix=new ArrayList();
            ArrayList temp_covariance_matrix=new ArrayList();
            ArrayList temp_weights=new ArrayList();
            ArrayList probabilities=new ArrayList();
            double prev_log_likelihood = E(ref temp_expected_value_matrix, ref temp_covariance_matrix, ref temp_weights, ref probabilities);
            M(ref temp_expected_value_matrix, ref temp_covariance_matrix, ref temp_weights, ref probabilities);
            for(int i=0; i<max_iteration_number-1; i++)
            {
                double log_likehood = E(ref temp_expected_value_matrix, ref temp_covariance_matrix, ref temp_weights, ref probabilities);
                double log_likelihood_change = log_likehood-prev_log_likelihood;
                prev_log_likelihood = log_likehood;
                M(ref temp_expected_value_matrix, ref temp_covariance_matrix, ref temp_weights, ref probabilities);
                if (Math.Abs(log_likelihood_change) <= log_likelihood_deviation)
                    break;
            }
            ArrayList result_points = new ArrayList();
            for (int i = 0; i < points.Count; i++)
            {
                int cluster_number = 0;
                double max_probability = 0;
                for(int j=0; j<clusters_count; j++)
                {
                    if ((double)((ArrayList)probabilities[i])[j] > max_probability)
                    {
                        max_probability = (double)((ArrayList)probabilities[i])[j];
                        cluster_number = j + 1;
                    }
                }
                Point point = (Point)points[i];
                point.cluster_numbers.Clear();
                point.cluster_numbers.Add(cluster_number);
                result_points.Add(point);
            }
            return result_points;
        }
    }
}
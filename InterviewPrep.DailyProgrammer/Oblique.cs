using System;

namespace InterviewPrep.DailyProgrammer
{
    public class ObliqueArrays
    {
        private int[,] _matrix;

        public ObliqueArrays()
        {
            GetMatrix(null);
        }

        public void ObliqueifyXY()
        {
            var outputArray = new int?[6, 12];
            for (var y = 0; y < _matrix.GetLength(1); y++)
            {
                for (var x = 0; x < _matrix.GetLength(0); x++)
                {
                    if (x + y <= 5)
                        outputArray[y, x + y] = _matrix[x, y];
                    else
                        outputArray[_matrix.GetLength(0) - x - 1, x + y] = _matrix[x, y];
                }
            }
            PrintMatrix(outputArray);
        }

        public void Obliqueify()
        {
            var outputArray = new int?[12, 6];
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    if (j + i <= 5)
                        outputArray[j + i, i] = _matrix[j, i];
                    else
                        outputArray[j + i, _matrix.GetLength(0) - j - 1] = _matrix[j, i];
                }
            }
            PrintMatrix(outputArray);
        }

        private void GetMatrix(string matrixString)
        {
            _matrix = new int[6, 6];
            var counter = 0;
            for (var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 6; x++)
                {
                    _matrix[x, y] = counter;
                    counter++;
                }
            }
            //PrintMatrix();
        }

        private void PrintMatrix(int?[,] matrix)
        {
            for (var y = 0; y < matrix.GetLength(1); y++)
            {
                for (var x = 0; x < matrix.GetLength(0); x++)
                {
                    Console.Write($"{matrix[x, y]}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    class Program
    {
        static int N, countOperRecurse = 0, countOperSingleMethod = 0;

        double[,] matrixA = new double[N, N];
        double[,] matrixB = new double[N, N];
        double[,,] resMatrixRecurse = new double[N + 1, N + 1, N + 1];
        double[,,] resSingleMethod;
        void setMatrixA(Program p)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i > j)
                    {
                        p.matrixA[i, j] = N - (i - j);
                    }
                    else if (i == j)
                    {
                        p.matrixA[i, j] = N;
                    }
                    else
                    {
                        p.matrixA[i, j] = 0;
                    }
                }
            }
        }

        void setMatrixB(Program p)
        {
            Random rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j >= i && j + i == N || j >= i && j + i == N + 1 || j + i == N - 1 && j >= i ||
                        ((i + j) - N) > 0 && j >= i && j + i == N + ((i + j) - N))
                    {
                        p.matrixB[i, j] = rnd.Next(1, 10);
                    }

                    else
                    {
                        p.matrixB[i, j] = 0;
                    }
                }
            }
        }

        void showMatrix(Program p)
        {
            Console.WriteLine("\nМатриця matrixA:");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(p.matrixA[i, j] + "        ");
                }
                Console.WriteLine();
            }


            Console.WriteLine("\nМатриця matrixB:");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(p.matrixB[i, j] + "        ");
                }
                Console.WriteLine();
            }
        }


        void localRecurseMultiply(int i, int j, int k)
        {
            if (i < N && j < N && k < N)
            {
                if (matrixA[i, k] != 0 && matrixB[k, j] != 0)
                {
                    resMatrixRecurse[i, j, N] += matrixA[i, k] * matrixB[k, j];
                    countOperRecurse += 2;
                }

                localRecurseMultiply(i, j, k + 1);
                if (k == N - 1)
                {
                    k = 0;
                    localRecurseMultiply(i, j + 1, k);

                    if (j == N - 1)
                    {
                        j = 0;
                        localRecurseMultiply(i + 1, j, k);
                    }

                }
            }
        }

        void singleMethod()
        {
            resSingleMethod = new double[N, N, N + 1];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    resSingleMethod[i, j, 0] = 0;

                    for (int k = 0; k < N; k++)
                    {
                        resSingleMethod[i, j, k + 1] = resSingleMethod[i, j, k] + matrixA[i, k] * matrixB[k, j];
                        countOperSingleMethod += 2;
                    }
                }
            }
        }


        void showResSingleMethod(double[,,] singleM)
        {
            Console.WriteLine("\n\nМетод одноразового присвоєння: ");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(singleM[i, j, N] + "         ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nКiлькiсть iтерацiй: " + countOperSingleMethod);

        }

        void showResRecurse(Program p)
        {
            Console.WriteLine("\n\nЛокально-рекурсивний метод: ");

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(p.resMatrixRecurse[i, j, N] + "         ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nКiлькiсть iтерацiй: " + countOperRecurse);
        }

        static void Main(string[] args)
        {
            Console.Write("Введiть N (розмiр матрицi): ");
            N = int.Parse(Console.ReadLine());

            Program p = new Program();

            p.setMatrixA(p);

            p.setMatrixB(p);

            p.showMatrix(p);

            p.singleMethod();

            p.showResSingleMethod(p.resSingleMethod);

            p.localRecurseMultiply(0, 0, 0);

            p.showResRecurse(p);

            Console.ReadKey();
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace Zadatak_1
{
    class Program
    {
        /// <summary>
        /// This method creates an identity matrix and writes a created matrix to file.
        /// </summary>
        static void CreateMatrix()
        {
            int[,] matrix = new int[100, 100];
            //creating identity matrix
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 1;
                    }                    
                }
            }
            //writing created matrix in txt
            StreamWriter str = new StreamWriter(@"../../FileByThread_1.txt");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    str.Write((matrix[i, j]));
                }
                str.WriteLine();
            }
            str.Close();
        }
        /// <summary>
        /// This method generates 1000 random odd numbers from 0 to 10000 and writes generated numbers to file.
        /// </summary>
        static void GenerateRandomNumbers()
        {
            Random random = new Random();
            int[] numbers = new int[1000];
            //filling array with 1000 random odd numbers
            for (int i = 0; i < numbers.Length; i++)
            {
                do
                {
                    numbers[i] = random.Next(0, 10000);
                } while ((numbers[i] % 2 == 0));
            }
            //writing array in txt
            StreamWriter str1 = new StreamWriter(@"../../FileByThread_22.txt");
            foreach (var item in numbers)
            {
                str1.WriteLine(item);
            }
            str1.Close();
        }
        /// <summary>
        /// This method reads rows of a matrix from a file and displays that on a console.
        /// </summary>
        static void DisplayMatrix()
        {
            string[] lines = File.ReadAllLines(@"../../FileByThread_1.txt");
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// This method reads numbers from a file and calculates their sum.
        /// </summary>
        static void CalculateSumOfNumbers()
        {
            //Thread.Sleep(1000);
            string[] lines = File.ReadAllLines(@"../../FileByThread_22.txt");
            int sum = 0;
            foreach (var item in lines)
            {
                sum += Int32.Parse(item);
            }
            Console.WriteLine("Sum:" + sum);
        }
        /// <summary>
        /// Creates four threads, sets their names, runs the first two, and after they executed their job, runs the second two.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            //creating array of threads
            Thread[] threads = {
            new Thread(() => CreateMatrix()),
            new Thread(() => GenerateRandomNumbers()),
            new Thread(() => DisplayMatrix()),
            new Thread(() => CalculateSumOfNumbers())
            };
            //setting name of every thread in array
            for (int i = 0; i < threads.Length; i++)
            {
                if (i % 2 == 0)
                {
                    threads[i].Name = string.Format("THREAD_{0}", i + 1);
                }
                else
                {
                    threads[i].Name = string.Format("THREAD_{0}{1}", i + 1, i + 1);
                }
                Console.WriteLine(threads[i].Name + " is created.");
            }
            stopWatch.Start();
            threads[0].Start();
            threads[1].Start();
            threads[0].Join();
            threads[1].Join();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Runtime of first two threads in miliseconds: {0} ", ts.Milliseconds);
            threads[2].Start();
            threads[3].Start();
            Console.ReadKey();
        }
    }
}

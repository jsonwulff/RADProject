using System;

namespace RADProject
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var tuple in Stream.CreateStream(100, 50)) {
                Console.WriteLine(tuple);
            }
            
            // var watch = System.Diagnostics.Stopwatch.StartNew();
            // // the code that you want to measure comes here
            // watch.Stop();
            // var elapsedMs = watch.ElapsedMilliseconds;
        }
    }
}
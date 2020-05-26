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
        }
    }
}
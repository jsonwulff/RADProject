using System;
using System.Numerics;
using RADProject.CountSketch;

namespace RADProject {

    public class Opgave7 {
        
        public static void Run(int streamSize, int l, int sL) {

            Console.WriteLine(">>>> OPGAVE 7 <<<<");
            Console.WriteLine(String.Format(">> Stream size: {0}\t l: {1}", streamSize, l));
            
            Console.WriteLine(">> Creating CSCalc with parameters.");
            
            CSCalc csc = new CSCalc(streamSize, sL, l);
            Tuple<ulong[], ulong> estimates = csc.Estimates();
            ulong[] chi_values = estimates.Item1;
            ulong s = estimates.Item2;
            
            Console.WriteLine(String.Format("Output>> True S value: {0}", s));

            BigInteger mse = 0;
            ulong[] m = new ulong[9];
            
            Console.WriteLine(">> Calculating M_i for i \\in [9]:");
            //Calculates the M_i=mean(g_i) for i \in [9]
            for (int i = 0; i < 9; i++){
                ulong[] g_i = new ulong[11];
                Array.Copy(chi_values, (i*11), g_i, 0, 11);
                Array.Sort(g_i);
                m[i] = g_i[5];

                String temp = String.Format("M_{0}: {1} \t G_{2}: ", i, m[i], i);
                Console.WriteLine(temp);
            }


            Console.WriteLine(">> Calculating Mean Squared Error:");
            //Calculates the mean squared error = mse
            for (int i = 0; i < 10; i++){
                int index = i * 10;
                for (int j = 0; j < 10; j++){
                    BigInteger error = (int) (chi_values[index+j] - s);
                    mse += (error * error);

                    Console.WriteLine(String.Format("current sum of squared errors: {0} \t error from S={1} at Chi[{2}]={3} is: {4}", mse, s, (index+j), chi_values[index+j], error));
                }
            }

            mse = (mse/100)/s;
            Console.WriteLine(String.Format("Final computed mean squared error: {0}", mse));
            
            //Sort the arrays
            //Array.Sort(chi_values);
            //Array.Sort(m);

        }
    }
}
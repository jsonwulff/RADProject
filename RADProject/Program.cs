using System;
using RADProject.HashFunctions;
using RADProject.HashTabel;
using System.Diagnostics;
using RADProject.CountSketch;

namespace RADProject {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(">>>> OPGAVE 1 <<<<");

            ModPrime multiplyModPrime = new ModPrime(63, false);
            multiplyModPrime.TestMultiplyModPrime(1000000, 63);

            MultiplyShift multiplyShift = new MultiplyShift(63, false);
            multiplyShift.TestMultiplyShift(1000000, 63);


            Hash mP = new ModPrime(8, false);
            Hash mS = new MultiplyShift(8, false);
            HashTable multShiftTable = new HashTable(256, mS);
            HashTable modPrimeTable = new HashTable(256, mP);

            foreach (var tuple in Stream.CreateStream(10000, 50, false)) {
                multShiftTable.Increment(tuple.Item1, tuple.Item2);
                modPrimeTable.Increment(tuple.Item1, tuple.Item2);
            }

            Console.WriteLine("multshift quadratic sum: " + multShiftTable.calcQuadSum());
            Console.WriteLine("modPrime  quadratic sum: " + modPrimeTable.calcQuadSum());

            int streamSize = 10000; //Should be updated to just below our limit.
            Console.WriteLine("Testing countsketch vs. true S value and outputting files");
            String s_str, mse_str, chi_str, m_str;
            s_str = mse_str = chi_str = m_str = "";
            
            for (int l = 6; l < 12; l += 2){
                CSCalc csc = new CSCalc(streamSize, 50, l);
                Tuple<ulong[], ulong> estimates = csc.Estimates();
                ulong[] chi_values = estimates.Item1;
                ulong s = estimates.Item2;
                ulong mse = 0UL;
                ulong[] m = new ulong[9];

                //Calculates the M_i=mean(g_i) for i \in [9]
                for (int i = 0; i < 9; i++){
                    ulong[] g_i = new ulong[11];
                    Array.Copy(chi_values, (i*11), g_i, 0, 11);
                    Array.Sort(g_i);
                    m[i] = g_i[5];
                }
                
                //Calculates the mean squared error = mse
                for (int i = 0; i < 10; i++){
                    int index = i * 10;
                    for (int j = 0; j < 10; j++){
                        mse += (ulong) Math.Pow(chi_values[index+j] - s, 2);
                    }
                }
                mse = mse/100;

                //Sort the arrays
                Array.Sort(chi_values);
                Array.Sort(m);

                //Updates the strings for the save files
                m_str += String.Join(",", m) + "\n";
                mse_str += String.Format("{0}", mse) + "\n";
                s_str += String.Format("{0}", s) + "\n";
                chi_str += String.Join(",", chi_values) + "\n";
            }

            //Not sure if it is a valid path
            System.IO.File.WriteAllText(@".\s_values.txt", s_str);
            System.IO.File.WriteAllText(@".\mse_values.txt", mse_str);
            System.IO.File.WriteAllText(@".\chi_values.txt", chi_str);
            System.IO.File.WriteAllText(@".\m_values.txt", m_str);
        }
    }
}
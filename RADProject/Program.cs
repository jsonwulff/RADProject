using System;
using RADProject.HashFunctions;
using RADProject.HashTabel;
using System.Diagnostics;
using System.IO;
using RADProject.CountSketch;

namespace RADProject {
    class Program {
        static void Main(string[] args) {
            Opgave1.Run();
            Opgave3.Run();
            
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
            
            string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string resultsDir = Path.Combine(projectDir, "Results");

            File.WriteAllText(Path.Combine(resultsDir,"s_values.txt"), s_str);
            File.WriteAllText(Path.Combine(resultsDir,"mse_values.txt"), mse_str);
            File.WriteAllText(Path.Combine(resultsDir,"chi_values.txt"), chi_str);
            File.WriteAllText(Path.Combine(resultsDir,"m_values.txt"), m_str);

        }
    }
}
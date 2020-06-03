using System;
using RADProject.HashFunctions;
using RADProject.Table;
using System.Diagnostics;
using System.IO;
using RADProject.CountSketch;

namespace RADProject {
    class Program {
        static void Main(string[] args) {
            Opgave1.Run(1048576, 12, 18);
            Opgave3.RunMultiplyModPrime();
            Opgave3.RunMultiplyShift();
            //Opgave 7 & 8
            for (int l = 6; l < 12; l += 2){
                Opgave7 instans = new Opgave7((1<<19), l, 13);
                instans.Run();
            }
            Opgave8.Run();

        }
    }
}
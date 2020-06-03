using System;
using System.Numerics;
using System.IO;
using RADProject.Table;
using RADProject.CountSketch;
using RADProject.HashFunctions;

namespace RADProject {

    public class Opgave7 {
        private HashTable hashTable;

        private CountSketch.CountSketch[] countSketchArray;

        private ulong[] chiValues;

        private ulong s;

        private int streamInputSize;

        private int streamImageSpace;

        private int hashImageSpace;
        private ulong m;
        
        public Opgave7(int streamSize, int l, int sL){
            this.m = 1UL << l;
            this.streamInputSize = streamSize;
            this.streamImageSpace = sL;
            this.hashImageSpace = l;
        }
        public void Run() {
            Console.WriteLine(">>>> OPGAVE 7 <<<<");
            Console.WriteLine(String.Format(">> Stream size: {0}\t l: {1}", streamInputSize, hashImageSpace));
            
            this.InstanciateCountSketchesAndHashTable();
            this.CalculateEstimates();

            BigInteger mse = 0;
            ulong[] m_i_array = new ulong[9];
            
            Console.WriteLine(">> Calculating M_i for i \\in [9]:");
            //Calculates the M_i=mean(g_i) for i \in [9]
            for (int i = 0; i < 9; i++){
                ulong[] g_i = new ulong[11];
                Array.Copy(chiValues, (i*11), g_i, 0, 11);
                Array.Sort(g_i);
                m_i_array[i] = g_i[5];

                String temp = String.Format("M_{0}: {1} \t G_{2}: ", i, m_i_array[i], i);
                Console.WriteLine(temp);
            }


            Console.WriteLine(">> Calculating Mean Squared Error:");
            //Calculates the mean squared error = mse
            for (int i = 0; i < 10; i++){
                int index = i * 10;
                for (int j = 0; j < 10; j++){
                    BigInteger error = (int) (chiValues[index+j] - s);
                    mse += (error * error);

                    Console.WriteLine(String.Format("current sum of squared errors: {0} \t error from S={1} at Chi[{2}]={3} is: {4})", mse, s, (index+j), chiValues[index+j], error));
                }
            }

            mse = (mse/100);
            Console.WriteLine(String.Format("Final computed mean squared error: {0}", mse));
            
            
            Array.Sort(chiValues);
            Array.Sort(m_i_array);
            this.FileGeneration(mse, m_i_array);
        }

        private void InstanciateCountSketchesAndHashTable(){
            Console.WriteLine("Instanciating the Countsketches and the hash table");
            MultiplyShift h = new MultiplyShift(hashImageSpace, true);
            hashTable = new HashTable(m, h);
            countSketchArray = new CountSketch.CountSketch[100];
            chiValues = new ulong[100];

            for (int i=0; i<100; i++){
                FourUniversal g = new FourUniversal(hashImageSpace, true);
                countSketchArray[i] = new CountSketch.CountSketch(m, g);
            }
        }

        private void CalculateEstimates(){
            String msg = String.Format("Running over a stream of size n={0},\t with an imagespace of 2^{1} \n Incrementing the hashtable, and adding to all countsketches, (x, delta)", streamInputSize, streamImageSpace);
            Console.WriteLine(msg);

            foreach (var tuple in Stream.CreateStream(streamInputSize, streamImageSpace, true)){
                foreach(CountSketch.CountSketch c in countSketchArray){
                    c.Add(tuple.Item1, tuple.Item2);
                }
            }

            foreach (var tuple in Stream.CreateStream(streamInputSize, streamImageSpace, true)){
                hashTable.Increment(tuple.Item1, tuple.Item2);
            }
            
            Console.WriteLine("Calculating S as quadSum");
            s = hashTable.CalcQuadSum();
            Console.WriteLine("S = " + s);

            Console.WriteLine("Calculating Chi values");
            for (int i = 0; i < 100; i++){
                chiValues[i] = countSketchArray[i].Chi();
            }
        }

        public void FileGeneration(BigInteger mse, ulong[] m_i){
            string resultsFile = Path.Combine("Results", String.Format("opgave7-output-w-l-{0}.csv", hashImageSpace));
            File.WriteAllText(resultsFile, "s; mse; chiValues; m_i\n");
            String result_str = String.Format("{0};{1};", s, mse) + "[" + String.Join(",", chiValues) + "];[" + String.Join(",", m_i) + "]";
            File.AppendAllText(resultsFile, result_str);
        }
    }
}
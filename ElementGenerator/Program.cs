using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementGenerator
{
    public class Program
    {
 
        static void Main(string[] args)
        {
            //var sw = new Stopwatch();
            //sw.Start();
            //var streamreader = new StreamReader(@"C:\Temp\hackathon\2018\CompressorsMaster.txt");
            //var delimiter = new char[] { '\t' };
            //var columnheaders = streamreader.ReadLine().Split(delimiter).ToList();
            //var facIdIndex = columnheaders.IndexOf("Facility ID");

            //var counter = 0;
            //var facIds = new HashSet<string>();

            //while (streamreader.Peek() > 0 && counter <= 10000000)
            //{
            //    //Console.WriteLine(streamreader.ReadLine().Split(delimiter)[facIdIndex]);
            //    var line = streamreader.ReadLine().Split(delimiter);
            //    if (line.Count() < facIdIndex)
            //        continue;

            //    facIds.Add(streamreader.ReadLine().Split(delimiter)[facIdIndex]);
            //    counter++;
            //}

            //sw.Stop();
            //Console.WriteLine(facIds.Count);
            //Console.WriteLine("Seconds Elapsed: " + sw.Elapsed.TotalSeconds);


            //Console.WriteLine($"Creating {facIds.Count} Elements");
            //var afDataAccess = new AfDataAccess("piserver", "Compressor", "admin123", "AdminPassword123");


            //foreach(var facId in facIds)
            //{
            //    Console.WriteLine($"Creating {facId}...");
            //    afDataAccess.CreateElement(facId);
            //}     

            var afValueWriter = new AfValueWriter();
            afValueWriter.CsvToPi();
            

        }
        
    }
}

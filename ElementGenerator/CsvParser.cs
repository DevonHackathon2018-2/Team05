using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementGenerator
{
    public class CsvParser
    {
        private readonly string _fileName;
        private int _currentLine;
        public const int ParseLineSize = 50000;
        private int _iteration;
        public CsvParser(string fileName)
        {
            _fileName = fileName;
            _currentLine = 1;
            _iteration = 1;
        }

        public CsvData Parse()
        {           

            var dictionary = new Dictionary<int, IList<string>>();
            var counter = 2;

            var csvData = new CsvData
            {
                Rows = new Dictionary<int, IList<string>>()
            };

            var delimiter = new char[] { '\t' };

            using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamreader = new StreamReader(fs))
                {
                    csvData.Headers = streamreader.ReadLine().Split(delimiter).ToList();
                    
                    //catch up to current line
                    while (counter < _currentLine)
                    {
                        streamreader.ReadLine();
                        counter++;
                    }

                    while (streamreader.Peek() > 0 && (_currentLine % (ParseLineSize * _iteration)) != 0 )
                    {

                        var row = streamreader.ReadLine().Split(delimiter).ToList();
                        csvData.Rows.Add(counter, row);
                        counter++;
                        _currentLine++;
                    }
                    _iteration++;
                }
            }

            return csvData;
        }
    }
}

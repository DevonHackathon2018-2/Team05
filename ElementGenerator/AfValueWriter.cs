using OSIsoft.AF.Asset;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementGenerator
{
    public class AfValueWriter
    {
        public static readonly string[] IgnoreColumns = { "Id", "AssetName", "Local Timestamp", "UTC Milliseconds" };
        public const string TimestampColumn = "UTC Milliseconds";
        public readonly AfDataAccess _afDataAccess;

        public AfValueWriter()
        {
            _afDataAccess = new AfDataAccess("piserver", "Compressor", "admin123", "AdminPassword123");
        }

        public void CsvToPi()
        {
            var csvParser = new CsvParser("C:\\Temp\\hackathon\\2018\\final\\warren.txt");
            var csvData = csvParser.Parse();

            while (csvData.Rows.Count > 0)
            {
                var afValues = new List<AFValues>();
                var currentFacName = "";
                AFElement afElement = null;

                foreach (var row in csvData.Rows)
                {
                    var facIdIndex = csvData.Headers.IndexOf("Facility ID");
                    var facName = row.Value[facIdIndex];
                    if (facName != currentFacName)
                    {
                        currentFacName = facName;
                        afElement = _afDataAccess.GetElement(currentFacName);
                        Console.WriteLine($"Adding values for element: {afElement}");
                    }

                    var vals = ConvertToAfValues(afElement, csvData.Headers, row.Value);
                    afValues.Add(vals);
                }

                _afDataAccess.AddValues(afValues);                

                csvData = csvParser.Parse();
            }

        }

        public AFValues ConvertToAfValues(AFElement afElement, IList<string> header, IList<string> values)
        {
            List<AFAttribute> afAttributes = new List<AFAttribute>();
            var timestampIndex = header.IndexOf(TimestampColumn);

            var indexTracker = -1;
            var newValues = new AFValues();
            foreach (var column in header)
            {
                indexTracker++;
                var afAttribute = afElement.Attributes[column];                

                //create state code "No Data" before beginning
                var attr = afElement.Attributes[column];
 
                //check for columns to ignore
                if (IgnoreColumns.Contains(column)) continue;

                if (attr == null) continue;

                var timestamp = GetTimestamp(values[timestampIndex]);


                if (String.IsNullOrWhiteSpace(values[indexTracker]))
                {
                    newValues.Add(AFValue.CreateSystemStateValue(attr, AFSystemStateCode.NoData, timestamp));
                    continue;
                }
                    

                newValues.Add(new AFValue
                {
                    Attribute = attr,
                    Value = values[indexTracker],
                    Timestamp = timestamp
                });

                                

            }

            return newValues;

            
        }

        private DateTime GetTimestamp(string timestamp)
        {
            Int64.TryParse(timestamp, out var timestampConverted);
            var epoch = new DateTime(1970, 1, 1);
            return epoch.AddMilliseconds(timestampConverted);
            
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementGenerator
{
    public class CsvData    {
        
        public IList<string> Headers { get; set; }
        public IDictionary<int, IList<string>> Rows { get; set; }

    }
}

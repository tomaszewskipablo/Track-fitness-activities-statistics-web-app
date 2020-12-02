using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportApp.Client.Helpers
{
    public class PieChartData
    {
        public PieChartData(int intValue, string strValue)
        {
            Value = intValue;
            Sport = strValue;
        }
        public PieChartData( string strValue)
        {            
            Sport = strValue;
        }

        public int Value { get; set; } = 0;
        public string Sport { get; set; }

    }
}

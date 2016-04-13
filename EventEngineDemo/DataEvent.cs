using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEngineDemo
{
    public class DataEvent : EventArgs
    {
        public string Type { get; set; }
        public object Data { get; set; }
    }
}

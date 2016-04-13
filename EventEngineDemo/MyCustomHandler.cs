using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEngineDemo
{
    public class MyCustomHandler:IHandler
    {
        public void Handle(DataEvent ev)
        {
            Console.WriteLine("Type: " + ev.Type);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventEngineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EventEngine ee = new EventEngine();
            ee.Register("timer", new TimerHandler());
            ee.Register("mycustom", new MyCustomHandler());
            ee.Start();


            DataEvent de = new DataEvent 
            {
                Type = "mycustom",
                Data = "my custom data string."
            };
            ee.Put(de);

            while (ee.IsActive())
            {
                //Thread.Sleep(10000);
                //ee.Stop();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEngineDemo
{
    public interface IHandler
    {
        void Handle(DataEvent ev);
    }
}

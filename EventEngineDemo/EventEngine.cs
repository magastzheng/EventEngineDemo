using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventEngineDemo
{
    public class DataWrap
    {
        public IHandler handler;
        public DataEvent dataEvent;
    }

    public class EventEngine
    {
        private bool _active = false;
        private Thread _thread;
        private System.Timers.Timer _timer;
        private ConcurrentQueue<DataEvent> _queue = new ConcurrentQueue<DataEvent>();
        private Dictionary<string, List<IHandler>> _handlerMap = new Dictionary<string, List<IHandler>>();
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public EventEngine()
        {
            //_thread = new Thread();
            //_timer = new System.Timers.Timer(1000);
        }

        public void Start()
        {
            _active = true;
            _thread = new Thread(Run);
            _thread.IsBackground = true;
            _thread.Start();

            //_timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            //_timer.Start();
        }

        public void Stop()
        {
            _active = false;
            _thread.Join();
        }

        public void Register(string type, IHandler handler)
        {
            if (!_handlerMap.ContainsKey(type))
            {
                var newHandlerList = new List<IHandler>();
                newHandlerList.Add(handler);
                _handlerMap.Add(type, newHandlerList);
            }
            else
            {
                if (!_handlerMap[type].Contains(handler))
                {
                    _handlerMap[type].Add(handler);
                }
            }
        }

        public void UnRegister(string type, IHandler handler)
        {
            if (_handlerMap.ContainsKey(type) && _handlerMap[type].Contains(handler))
            {
                _handlerMap[type].Remove(handler);
            }
        }

        public void Put(DataEvent ev)
        {
            _queue.Enqueue(ev);
            _autoResetEvent.Set();
        }

        public bool IsActive()
        {
            return _active;
        }

        private void Process(object ob)
        {
            DataEvent ev = (DataEvent)ob;
            if (!_handlerMap.ContainsKey(ev.Type))
            {
                return;
            }

            foreach (var handler in _handlerMap[ev.Type])
            {
                handler.Handle(ev);
            }
        }

        private void Run()
        {
            while (_active)
            {
                _autoResetEvent.WaitOne(10000);
                DataEvent ev;
                if (_queue.TryDequeue(out ev))
                {
                    //DataWrap dw = new DataWrap
                    //{
                    //    handler = handler,
                    //    dataEvent = ev
                    //};
                    //Task task = new Task(Task, dw);
                    //Task task = new Task(Process, ev);
                    //task.Start();

                    Process(ev);
                }
                
            }
        }

        private void Task(object de)
        {
            var dw = (DataWrap)de;
            dw.handler.Handle(dw.dataEvent);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var ev = new DataEvent
            {
                Type = "timer",
                Data = "timer"
            };

            Put(ev);
        }
    }
}

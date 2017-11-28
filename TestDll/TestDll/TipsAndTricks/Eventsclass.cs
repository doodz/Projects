using System;
using System.Windows;

namespace Ividatalink.TipsAndTricks
{
    public class Eventsclass
    {
        public Eventsclass()
        {
            var myEvent = new MyEvent();
            MyListener myListener = new MyListener(myEvent);

            myEvent.Raise();

            Console.WriteLine("          Set myListener to null");
            myListener = null;
            CallGarbageCollector();

            myEvent.Raise();

            Console.WriteLine("          Set myEvent to null");
            myEvent = null;

            CallGarbageCollector();
        }
        static void CallGarbageCollector()
        {
            Console.WriteLine("          Garbage collection running");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("          Collected");
        }
        public class MyEvent
        {
            public event EventHandler<EventArgs> Event = delegate { };

            public void Raise()
            {
                Event(this, EventArgs.Empty);
            }
        }

        public class MyListener
        {
            private readonly MyEvent _source;

            private void OnEvent(object source, EventArgs args)
            {
                Console.WriteLine("          The receiver receives event from the source.");
            }

            public MyListener(MyEvent source)
            {
                _source = source;
                WeakEventManager<MyEvent, EventArgs>.AddHandler(source, nameof(source.Event), OnEvent);
                //_source.Event += OnEvent;
            }

            ~MyListener()
            {
                //_source.Event -= OnEvent;
                Console.WriteLine("          Finalizer of MyListener executed.");
            }
        }
    }
    public class EventDisposable : IDisposable
    {
        private bool _disposed = false;
        private readonly Eventsclass.MyEvent _source;
        private void OnEvent(object source, EventArgs args)
        {
            Console.WriteLine("          The receiver receives event from the source.");
        }
        public EventDisposable(Eventsclass.MyEvent source)
        {
            _source = source;
            _source.Event += OnEvent;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //...
            }
            _source.Event -= OnEvent;
            _disposed = true;
        }
        ~EventDisposable()
        {
            Dispose(false);
        }
    }
}
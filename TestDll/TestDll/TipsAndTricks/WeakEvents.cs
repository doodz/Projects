using System;
using System.Windows;

namespace Ividatalink.TipsAndTricks
{
    public class WeakEvents
    {
        public WeakEvents()
        {
            var t = new System.Timers.Timer();
            t.Interval = 500;
            WeakEventManager<System.Timers.Timer, EventArgs>.AddHandler(t,
                nameof(t.Elapsed), onTick);

            t.Start();

            Console.ReadLine();
            t.Stop();
        }

        private static int cpt = 0;

        private void onTick(object sender, EventArgs e)
        {
            cpt++;
            Console.WriteLine($"Event # {cpt}");
        }
    }
}
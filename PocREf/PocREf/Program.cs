using System;

namespace PocREf
{
    class Program
    {


        static void Main(string[] args)
        {


            new MyClass();
        }
    }


    class MyClass
    {
        private bool b1;
        private bool b2;

        public MyClass()
        {
            using (new RunWithBool(b => b1 = b))
            {

            }
            using (new RunWithBool2(ref b2))
            {

            }

            Console.WriteLine($"b1 => {b1}, b2 => {b2}");
            Console.ReadKey();
        }
    }

    class RunWithBool2 : IDisposable
    {

        private object o;
        //private Func<bool> _get;
        private readonly Action<bool> _set;
        public RunWithBool2(ref bool myB)
        {
            o = (object)myB;
            //_get = @get;
            _set = b => o = b;
            _set(true);
        }



        public void Dispose()
        {
            //_set(false);
        }
    }


    public class RunWithBool : IDisposable
    {
        //private Func<bool> _get;
        private readonly Action<bool> _set;
        public RunWithBool(/*Func<bool> @get,*/ Action<bool> @set)
        {

            //_get = @get;
            _set = @set;
            _set(true);
        }



        public void Dispose()
        {
            //_set(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace pocList
{
    internal interface IMethods<T>
    {
        T Generate(int size, Func<int, string> generator);

    }

    internal class ArrayMethods : IMethods<string[]>
    {
        public string[] Generate(int size, Func<int, string> generator)
        {
            var items = new string[size];
            for (var i = 0; i < items.Length; ++i)
                items[i] = generator(i);
            return items;
        }


    }

    internal class ListMethods : IMethods<List<string>>
    {
        public List<string> Generate(int size, Func<int, string> generator)
        {
            var items = new List<string>(size);
            for (var i = 0; i < size; ++i)
                items.Add(generator(i));
            return items;
        }
    }

    internal class StringCollectionMethods : IMethods<StringCollection>
    {
        public StringCollection Generate(int size, Func<int, string> generator)
        {
            var items = new StringCollection();
            for (var i = 0; i < size; ++i)
                items.Add(generator(i));
            return items;
        }

    }

    internal class ArrayListMethods : IMethods<System.Collections.ArrayList>
    {
        public System.Collections.ArrayList Generate(int size, Func<int, string> generator)
        {
            return new System.Collections.ArrayList(Enumerable.Range(0, size).Select(generator).ToList());
        }


    }
    //
    internal static class Program
    {
        private static Tuple<string, TimeSpan>[] CheckPerformance<T>(IMethods<T> methods) where T : class
        {
            var stats = new List<Tuple<string, TimeSpan>>();

            T source = null;
            foreach (var info in new[]
                {
                    new
                    {
                        Name = "Generate",
                        Method = new Func<T, T>(items => methods.Generate(10000000, i => i % 2 == 0 ? (-i).ToString() : i.ToString()))
                    },

                }
            )
            {
                var count = 10;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                T res = null;
                for (var i = 0; i < count; ++i)
                    res = info.Method(source);
                stopwatch.Stop();
                source = res;
                stats.Add(new Tuple<string, TimeSpan>(info.Name, stopwatch.Elapsed));
            }
            return stats.ToArray();
        }

        private static void Main()
        {
            var arrayStats = CheckPerformance(new ArrayMethods());
            var listStats = CheckPerformance(new ListMethods());
            var rcStats = CheckPerformance(new ArrayListMethods());
            var stringCollectionStats = CheckPerformance(new StringCollectionMethods());

            Console.WriteLine(
                "Array                List                ArrayList         StringCollection           Method");
            for (var i = 0; i < arrayStats.Length; ++i)
                Console.WriteLine("{0}     {1}    {2}           {3}           {7}",
                    arrayStats[i].Item2,
                    listStats[i].Item2,
                    rcStats[i].Item2,
                    stringCollectionStats[i].Item2,
                    listStats[i].Item2.TotalSeconds / arrayStats[i].Item2.TotalSeconds,
                    stringCollectionStats[i].Item2.TotalSeconds / arrayStats[i].Item2.TotalSeconds,
                    rcStats[i].Item2.TotalSeconds / arrayStats[i].Item2.TotalSeconds,
                    arrayStats[i].Item1);

            Console.ReadKey();
        }
    }
}
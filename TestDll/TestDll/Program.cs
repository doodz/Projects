using Ividatalink.TipsAndTricks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ividatalink
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var myTipsAndTricksManager = new TipsAndTricks.TipsAndTricksManager();

            //myTipsAndTricksManager.ManagePrivateList();
            Console.WriteLine();
            Console.WriteLine();
            var res = new CircleRef();
            CircleRef.CallGarbageCollector();
            res = null;
            CircleRef.CallGarbageCollector();
            Console.ReadKey();
        }




        public class AsyncExemple
        {
            private string TraitementLong()
            {
                System.Threading.Thread.Sleep(2000);
                return "traitement long fini";
            }

            private string TraitementCourt()
            {
                System.Threading.Thread.Sleep(1000);
                return "traitement Court fini";
            }

            public void ParallelTaskRun()
            {

                Task.Factory.StartNew(() => { Console.WriteLine(TraitementLong()); });
                Task.Factory.StartNew(() => { Console.WriteLine(TraitementCourt()); });
            }

            public async void Run()
            {

                System.Threading.Thread.Sleep(5000);
                Console.WriteLine("...fin du chargement");
                string text1 = await Task.Run(() => { return TraitementLong(); });
                Console.WriteLine(text1);
                string text2 = await Task.Run(() => { return TraitementCourt(); });
                Console.WriteLine(text2);
            }

        }
        private static string ExempleContinueWith()
        {
            Console.WriteLine($"ExempleContinueWith ManagedThreadId =>{Thread.CurrentThread.ManagedThreadId}");

            const string sentence = "In computer science, future, promise, delay, and deferred refer " +
                                    "to constructs used for synchronizing program execution " +
                                    "in some concurrent programming languages.";

            if (string.IsNullOrEmpty(sentence)) return sentence;

            var taskScheduler = TaskScheduler.Default;


            var task = Task<string[]>.Factory.StartNew(() => Map(sentence))
                .ContinueWith<string[]>(t => Process(t.Result), taskScheduler)
                .ContinueWith<string>(t => Reduce(t.Result));

            Console.WriteLine("Result: {0}", task.Result);
            return task.Result.Trim();
        }


        private static void AttachedToParent()
        {
            var parent = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Parent task executing.");
                var child = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Attached child starting.");
                    Thread.SpinWait(5000000);
                    Console.WriteLine("Attached child completing.");
                });
            });
            parent.Wait();
            Console.WriteLine("Parent has completed.");

            // The example produces output like the following:
            // Outer task executing.
            // Nested task starting.
            // Outer has completed.
            // Nested task completing.
        }



        private static async Task TestId()
        {
            Console.WriteLine($"TestId 1=>{Thread.CurrentThread.ManagedThreadId}");

            await Task.Run(() =>
            {
                Console.WriteLine($"Task.Run 1=>{Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"TestId 2=>{Thread.CurrentThread.ManagedThreadId}");

        }

        private static string[] Map(string sentence)
        {
            Console.WriteLine($"Map ManagedThreadId =>{Thread.CurrentThread.ManagedThreadId}");
            return sentence.ToLower().Split();
        }

        private static string ReverseString(string s)
        {
            //Console.WriteLine($"ReverseString ManagedThreadId =>{Thread.CurrentThread.ManagedThreadId}");
            var ss = s.Skip(1);
            var sb = new StringBuilder();

            foreach (var VARIABLE in ss)
                sb.Append(VARIABLE);

            sb.Append(s[0]);
            sb.Append("ay");

            return sb.ToString();
        }

        public static string[] Process(string[] words)
        {
            Console.WriteLine($"Process ManagedThreadId =>{Thread.CurrentThread.ManagedThreadId}");
            for (var i = 0; i < words.Length; i++)
            {
                var index = i;
                Task.Factory.StartNew(
                    () => words[index] = ReverseString(words[index]),
                    TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            return words;
        }

        private static string Reduce(string[] words)
        {
            Console.WriteLine($"Reduce ManagedThreadId =>{Thread.CurrentThread.ManagedThreadId}");

            var sb = new StringBuilder();
            foreach (var word in words)
            {
                sb.Append(word);
                sb.Append(' ');
            }
            return sb.ToString();
        }



        public static string Process(string words)
        {
            return string.Empty;
        }

        public static List<string> sourceCollection = new List<string>();

        public static void ProcessCollection1()
        {
            // Version séquentielle            
            foreach (var item in sourceCollection)
                Process(item);

            // Équivalent parallèle
            Parallel.ForEach(sourceCollection, item => Process(item));
        }

        public static void ProcessCollection()
        {
            foreach (var item in sourceCollection)
                Task.Run(() => Process(item));

            Parallel.ForEach(sourceCollection, item => Process(item));
        }

        public static void ProcessCollection3()
        {
            Task.Factory.StartNew(() => Parallel.ForEach(sourceCollection, item => Process(item)));
        }

        public static void ProcessCollection4()
        {
            foreach (var item in sourceCollection)
                Parallel.Invoke(() => Process(item));

            Parallel.ForEach(sourceCollection, item => Process(item));

        }



        private static void MaxDegreeOfParallelism()
        {

            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Math.Max(Environment.ProcessorCount / 2, 1);


            Parallel.ForEach(sourceCollection, options, item => Process(item));
        }

        public static void SayHelloWorld1()
        {
            var task = new Task(() => Console.WriteLine("Hello world."));
            task.Start();
        }

        public static void SayHelloWorld()
        {
            Task.Run(() => Console.WriteLine("Hello world."));
        }


        public static async Task totoé()
        {
            //Task.Run(SomeAction);


            var tt = Task.Factory.StartNew(SomeAction,
                CancellationToken.None, TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
                TaskScheduler.Default);


            await tt;
        }

        private class CustomData
        {
            public long CreationTime;
            public int Name;
            public int ThreadNum;
        }

        private static void SomeAction1()
        {
            var taskArray = new Task[10];
            for (var i = 0; i < taskArray.Length; i++)
                taskArray[i] = Task.Factory.StartNew((object obj) =>
                    {
                        var data = new CustomData
                        {
                            Name = i,
                            CreationTime = DateTime.Now.Ticks,
                            ThreadNum = Thread.CurrentThread.ManagedThreadId
                        };
                        Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
                            data.Name, data.CreationTime, data.ThreadNum);
                    },
                    i);
            Task.WaitAll(taskArray);
        }
        // The example displays output like the following:
        // Task #10 created at 635116418427727841 on thread #4.
        // Task #10 created at 635116418427737842 on thread #4.
        // Task #10 created at 635116418427737842 on thread #4.
        // Task #10 created at 635116418427737842 on thread #4.
        // Task #10 created at 635116418427737842 on thread #4.
        // Task #10 created at 635116418427737842 on thread #4.
        // Task #10 created at 635116418427727841 on thread #3.
        // Task #10 created at 635116418427747843 on thread #3.
        // Task #10 created at 635116418427747843 on thread #3.
        // Task #10 created at 635116418427737842 on thread #4

        private static void SomeAction()
        {
            var taskArray = new Task[10];
            for (var i = 0; i < taskArray.Length; i++)
                taskArray[i] = Task.Factory.StartNew((object obj) =>
                    {
                        var data = obj as CustomData;
                        if (data == null)
                            return;

                        data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                        Console.WriteLine(
                            $"Task #{data.Name} created at {data.CreationTime} on thread #{data.ThreadNum}.");
                    },
                    new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });


            Task.WaitAll(taskArray);
        }

        // The example displays output like the following:
        // Task #0 created at 635116412924597583 on thread #3.
        // Task #1 created at 635116412924607584 on thread #4.
        // Task #3 created at 635116412924607584 on thread #4.
        // Task #4 created at 635116412924607584 on thread #4.
        // Task #2 created at 635116412924607584 on thread #3.
        // Task #6 created at 635116412924607584 on thread #3.
        // Task #5 created at 635116412924607584 on thread #4.
        // Task #8 created at 635116412924607584 on thread #4.
        // Task #7 created at 635116412924607584 on thread #3.
        // Task #9 created at 635116412924607584 on thread #4.


        public static void PLINQ()
        {

            var range = Enumerable.Range(1, 10000);

            var result = range.AsParallel().AsOrdered().Where(num => num % 2 == 0);
            Console.WriteLine($"{result.Count()} even numbers out of {range.Count()} total");
            // 5000 even numbers out of 10000 total  
        }


        public static void HandleExceptions1()
        {
            var task1 = Task.Run(() => throw new MyCustomException("This exception is expected!"));

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {

                    if (e is MyCustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else if (e is TaskCanceledException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

        }



        public static void HandleExceptions()
        {
            var task1 = Task.Run(() => throw new MyCustomException("This exception is expected!"));


            var w = task1.GetAwaiter();
            w.GetResult();

            while (!task1.IsCompleted) { }

            if (task1.Status == TaskStatus.Faulted)
            {
                foreach (var e in task1.Exception.InnerExceptions)
                {

                    if (e is MyCustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else if (e is TaskCanceledException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

        }

        public static void CancellationRequested()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var task = Task.Factory.StartNew(() =>
            {
                // Annulation demandée avant toutes opérations.
                // L’appel lève une exception
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    // On vérifie s’il y a eu une demande d’annulation
                    if (ct.IsCancellationRequested)
                    {
                        // Si oui on annule les actions en cours et on lève l’exception.
                        ct.ThrowIfCancellationRequested();
                    }

                }
            }, tokenSource2.Token);

            tokenSource2.Cancel();

            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                {
                    if (v is OperationCanceledException)
                        Console.WriteLine(e.Message + " " + v.Message);
                    else
                        throw;
                }
            }
            finally
            {
                tokenSource2.Dispose();
            }
        }


        public static async void NeverEverUseMe()
        {
            await Task.Run(() => new MyCustomException("I would never be intercepted!"));
        }


        //


        public async Task<string> ExampleMethodAsync()
        {
            var httpClient = new HttpClient();
            var responseBody = (await httpClient.GetStringAsync("http://www.ividatalink.com/"));

            return responseBody;
        }

        [Serializable]
        private class MyCustomException : Exception
        {
            public MyCustomException()
            {
            }

            public MyCustomException(string message) : base(message)
            {
            }

            public MyCustomException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected MyCustomException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
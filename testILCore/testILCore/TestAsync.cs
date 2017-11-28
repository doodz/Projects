using System;
using System.Threading.Tasks;

namespace testILCore
{
    public class TestAsync
    {

        
        public async Task TesTask()
        {
            await Task.Factory.StartNew(() => { Console.WriteLine("from task"); });
        }
    }
}
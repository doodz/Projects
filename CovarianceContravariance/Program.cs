using System;
using System.Collections.Generic;

namespace IvidataLink_Covariance_Contravariance
{
    class Program
    {
        static void Main(string[] args)
        {

            var test = new IvidataTest().Equals();
            test.Test();
            Console.ReadKey();

            string str = null;
            Console.WriteLine(str);


            new mycomp().Compare(new IvidataLinkDerived(), new IvidataLinkDerived());
        }
    }



    public class mycomp : IComparer<IvidataLinkDerived>
    {
        public int Compare(IvidataLinkDerived x, IvidataLinkDerived y)
        {
            throw new NotImplementedException();
        }
    }



}

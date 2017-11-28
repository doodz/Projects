using System;

public class toto
{

    public toto()
    {
        var mtuple = CreateTuple();
        Console.WriteLine($"my tuple item 1 {mtuple.Item1} and my tuple item 2 {mtuple.Item2}");
    }

    private Tuple<string, string> CreateTuple()
    {

        var t = new Tuple<string, string>("toto", "tata");
        return t;
    }

    //private Tuple<string, string> CreateTuple2()
    //{

    //    var t = new Tuple<string, string>(toto:"toto", "tata");
    //    return t;
    //}
}
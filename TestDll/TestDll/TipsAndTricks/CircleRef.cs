using System;

public class CircleRef
{
    private B b;
    public CircleRef()
    {
        b = new B();
        //CallGarbageCollector();
        //b = null;
        //CircleRef.CallGarbageCollector();
    }

    ~CircleRef()
    {
        Console.WriteLine("     CircleRef Collected");
    }

    public static void CallGarbageCollector()
    {
        Console.WriteLine("          Garbage collection running");

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Console.WriteLine("          Collected");
    }
}

public class A
{
    B b;
    public A(B b) { this.b = b; }

    ~A()
    {
        Console.WriteLine("         A Collected");
    }
}

public class B
{
    A a;
    public B() { this.a = new A(this); }

    ~B()
    {
        Console.WriteLine("         B Collected");
    }
}
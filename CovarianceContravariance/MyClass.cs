using IvidataLink_Covariance_Contravariance;
using System;

class MyClass
{
    public void Test2()
    {
        IvidataLinkCovariant<IvidataLinkBase> ividataLinkBase =
            new IvidataLinkCovariant<IvidataLinkBase>();
        IvidataLinkCovariant<IvidataLinkDerived> ividataLinkDerived =
            new IvidataLinkCovariant<IvidataLinkDerived>();


        ShowEmail(ividataLinkDerived);
        ShowEmail(ividataLinkBase);
    }


    private void ShowEmail(ICovariant<IvidataLinkBase> obj)
    {
        Console.WriteLine(obj.GetIvidataLinkObject().GetEmail());
    }
}
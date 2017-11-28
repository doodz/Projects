using System;

namespace IvidataLink_Covariance_Contravariance
{
    public class IvidataTest
    {
        public void Test()
        {
            IContravariant<IvidataLinkBase> ividataLinkBase =
                new IvidataLinkContravariant<IvidataLinkBase>();
            IContravariant<IvidataLinkDerived> ividataLinkDerived =
                new IvidataLinkContravariant<IvidataLinkDerived>();

            ividataLinkDerived = ividataLinkBase;

            DoSomething(ividataLinkBase);
            DoSomething(ividataLinkDerived);
        }

        public void DoSomething(IContravariant<IvidataLinkDerived> ividataLinkDerived)
        {
            ividataLinkDerived.SetIvidataLinkObject(new IvidataLinkDerived());
            ividataLinkDerived.Input = new IvidataLinkDerived();

          
        }
    }
}




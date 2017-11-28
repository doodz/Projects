namespace IvidataLink_Covariance_Contravariance
{
    public class IvidataLinkBase
    {
        public virtual string GetEmail()
        {
            return "IvidataLinkBase@ividata.com";
        }
    }

    public class IvidataLinkDerived : IvidataLinkBase
    {
        public override string GetEmail()
        {
            return "IvidataLinkDerived@ividata.com";
        }

        public void AddMoney(int money)
        {

        }
    }
}






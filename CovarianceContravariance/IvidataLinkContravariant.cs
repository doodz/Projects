namespace IvidataLink_Covariance_Contravariance
{
    public interface IContravariant<in TIn>
    {
        void SetIvidataLinkObject(TIn ividataObject);
        TIn Input { set; }
    }

    public class IvidataLinkContravariant<T> : IContravariant<T>
    {
        public void SetIvidataLinkObject(T ividataObject)
        {
            Input = ividataObject;
        }

        public T Input { get; set; }
    }
}





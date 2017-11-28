namespace IvidataLink_Covariance_Contravariance
{
    public interface IVariant<out TOut, in TIn>
    {
        TOut GetSetIvidataLinkObject(TIn ividataObject);
    }

    public class IvidataLinkVariant<T, TR> : IVariant<T, TR> where T : IvidataLinkBase
    {
        private readonly T _obj;
        public T GetSetIvidataLinkObject(TR ividataObject)
        {

            return _obj;
        }
    }
}
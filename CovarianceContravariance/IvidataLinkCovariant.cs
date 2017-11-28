namespace IvidataLink_Covariance_Contravariance
{
    public interface ICovariant<out TOut>
    {
        TOut GetIvidataLinkObject();
    }


    public class IvidataLinkCovariant<T> : ICovariant<T> where T : new()
    {
        private readonly T _obj;

        public IvidataLinkCovariant()
        {
            _obj = new T();
        }

        public T GetIvidataLinkObject()
        {
            return _obj;
        }
    }
}




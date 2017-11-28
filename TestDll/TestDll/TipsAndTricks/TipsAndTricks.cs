using System.Diagnostics;

namespace Ividatalink.TipsAndTricks
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class TipsAndTricks
    {
        private int _count { get; set; } = 8;

        public int Count
        {
            get
            {
                return _count;
            }
            set { _count = value; }
        }
        public bool Flag { get; set; }



        private string DebuggerDisplay => $"Count = { Count}, Flag = { Flag}";

        public override string ToString()
        {

#if DEBUG
            return $"My ToString => Count = {Count}, Flag = {Flag}";
#else
                return  base.ToString();
#endif
        }








    }
}
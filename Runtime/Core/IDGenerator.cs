namespace Congroo.Core
{
    public sealed class IDGenerator
    {
        private long mCurrent = 0;

        public long Next
        {
            get
            {
                long temp = mCurrent + 1;
                mCurrent = temp >= long.MaxValue ? -1 : temp;
                return mCurrent;
            }
        }
    }
}
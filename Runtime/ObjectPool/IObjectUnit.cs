namespace Congroo.Core
{
    public interface IObjectUnit
    {
        /// <summary>
        ///  «∑Ò∆Ù”√
        /// </summary>
        bool IsUsed { get; set; }

        void PoolGet();

        void PoolFree();

    }
}
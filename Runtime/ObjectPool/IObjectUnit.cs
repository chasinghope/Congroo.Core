namespace Congroo.Core
{
    public interface IObjectUnit
    {
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        bool IsUsed { get; set; }

        void PoolGet();

        void PoolFree();

    }
}
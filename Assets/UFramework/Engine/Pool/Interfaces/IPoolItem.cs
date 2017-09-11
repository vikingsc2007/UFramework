namespace UFramework.Engine.Pool.Interfaces
{
    public interface IPoolItem
    {
        void OnReborn();
        void OnCache();
        bool Cached{get;set;}
    }
}
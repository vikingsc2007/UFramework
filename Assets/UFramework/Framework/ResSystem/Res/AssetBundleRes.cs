using UFramework.Engine.Pool.Pools;
using UFramework.Framework.ResSystem.Interfaces;

namespace UFramework.Framework.ResSystem.Res
{
    public class AssetBundleRes:IRes
    {


        /// <summary>
        /// 引用计数为零时回收进入缓冲堆栈
        /// </summary>
        protected override void OnDispose()
        {
            ObjectPool<AssetBundleRes>.Inst.Drop(this);
        }
    }
}
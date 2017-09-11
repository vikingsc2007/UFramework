using UnityEngine;
using System.Collections;
using UFramework.Engine.Pool.Pools;
using UFramework.Framework.ResSystem.Res;
using UFramework.Framework.ResSystem.Interfaces;
using UFramework.Framework.ResSystem.Enums;
using UFramework.Engine.Debuger;
using UFramework.Framework.ResSystem.ResInfos;

public class ResFactory
{
    static ResFactory()
    {
        ObjectPool<InternalRes>.Inst.Initalize(ResPoolMaxCounts.Internal, 0);
        ObjectPool<AssetBundleRes>.Inst.Initalize(ResPoolMaxCounts.AssetBundle, 0);
        ObjectPool<AssetRes>.Inst.Initalize(ResPoolMaxCounts.Asset, 0);
    }


    public static IRes Create(string url)
    {
        return Create(url, AssetBundleInfo.Inst.GetUrlResType(url));
    }


    public static IRes Create(string url, ResType type)
    {
        switch (type)
        {
            case ResType.AssetBundle:
                return ObjectPool<AssetBundleRes>.Inst.Pick();
            case ResType.Asset:
                return ObjectPool<AssetRes>.Inst.Pick();
            //case eResType.kABScene:
            //    return SceneRes.Allocate(name);
            case ResType.Internal:
                return ObjectPool<InternalRes>.Inst.Pick();
            default:
                Log.e("Invalid assetType :" + type.ToString());
                return null;
        }
    }
}

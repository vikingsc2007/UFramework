﻿using System.Collections.Generic;
using UFramework.Engine.Pool.Pools;
using UFramework.Engine.Singleton;
using UFramework.Framework.ResSystem.Interfaces;
using UFramework.Framework.ResSystem.Res;

namespace UFramework.Framework.ResSystem
{
	
	public class ResSystem :SingletonMonoT<ResSystem>
	{

		public static int MaxLoaderCount = 20;
		public static int InitLoaderCount = 5;
		
		
		
		private Dictionary<string,IRes> _loadedResource;

		
		
		
		public override void OnInstanced()
		{
			ObjectPool<ResLoader>.Inst.Initalize(MaxLoaderCount,InitLoaderCount);
			_loadedResource = new Dictionary<string, IRes>();
        }




		public ResLoader GetLoader()
		{
            return ObjectPool<ResLoader>.Inst.Pick();
		}
		
		
		public bool ReleaseLoader(ResLoader loader)
		{
			return ObjectPool<ResLoader>.Inst.Drop(loader);
		}


	}
}


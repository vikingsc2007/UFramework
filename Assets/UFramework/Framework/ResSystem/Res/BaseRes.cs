using UnityEngine;
using System.Collections;
using UFramework.Engine.RefCounter;
using UFramework.Framework.ResSystem.Interfaces;



namespace UFramework.Framework.ResSystem.Res
{
    public class BaseRes : IRes
    {
        public string Name { get; private set; }

        public short ResState { get; private set; }

        public UnityEngine.Object Asset { get; set; }

        public float Progress { get; private set; }

    }

}

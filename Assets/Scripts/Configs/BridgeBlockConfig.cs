using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    [CreateAssetMenu(fileName = "BridgeBlock", menuName = "ScriptableObjects/BridgeBlock")]
    public class BridgeBlockConfig : ScriptableObject
    {
        [SerializeField] private float length;
        [SerializeField] private Material buildCorrectStateMaterial;
        [SerializeField] private Material buildWrongStateMaterial;
        [SerializeField] private Material compliteMaterial;
        [SerializeField] private BridgeBlock prefab;

        public float Length { get => length; }
        public Material BuildCorrectStateMaterial { get => buildCorrectStateMaterial;  }
        public Material BuildWrongStateMaterial { get => buildWrongStateMaterial;  }
        public Material CompliteMaterial { get => compliteMaterial;  }
        public BridgeBlock Prefab { get => prefab;  }
    }
}

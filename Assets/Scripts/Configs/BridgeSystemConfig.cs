using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    [CreateAssetMenu(fileName = "BridgeSystem", menuName = "ScriptableObjects/BridgeSystem")]
    public class BridgeSystemConfig : ScriptableObject
    {
        [SerializeField] private float maxBridgeLength;
        [SerializeField] private Vector3 offsetMoveStartPoint;
        [SerializeField] private LayerMask layersForRaycast;
        [SerializeField] private LayerMask layersObstacles;
        [SerializeField] private LayerMask layerBlockBuildState;
        [SerializeField] private LayerMask layerBlockCompliteState;
        [SerializeField] private Arrow arrow;
        [SerializeField] private BridgeBlockConfig startBlock;
        [SerializeField] private BridgeBlockConfig endBlock;
        [SerializeField] private BridgeBlockConfig middleBlock;
        [SerializeField] private BridgeBlockConfig fillerBlock;
        [SerializeField] private float compliteAnimationDuration;
        [SerializeField] private Ease compliteAnimationEase;
        public float MaxBridgeLength { get => maxBridgeLength; }
        public LayerMask LayersForRaycast { get => layersForRaycast; }
        public LayerMask LayerBlockBuildState { get => layerBlockBuildState; }
        public LayerMask LayerBlockCompliteState { get => layerBlockCompliteState; }
        public Vector3 OffsetMoveStartPoint { get => offsetMoveStartPoint;  }
        public Arrow Arrow { get => arrow; }
        public BridgeBlockConfig StartBlock { get => startBlock; }
        public BridgeBlockConfig EndBlock { get => endBlock;}
        public BridgeBlockConfig MiddleBlock { get => middleBlock; }
        public BridgeBlockConfig FillerBlock { get => fillerBlock; }
        public float CompliteAnimationDuration { get => compliteAnimationDuration;  }
        public Ease CompliteAnimationEase { get => compliteAnimationEase; }
        public LayerMask LayersObstacles { get => layersObstacles; }
    }
}

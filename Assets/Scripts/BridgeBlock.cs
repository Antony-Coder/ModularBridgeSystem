using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class BridgeBlock : MonoBehaviour
    {
        [SerializeField] private BridgeBlockConfig config;
        [SerializeField] private BridgeBlockTrigger trigger;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        public BridgeBlockTrigger Trigger { get => trigger;  }
        public Transform EndPoint { get => endPoint; }

#if UNITY_EDITOR

        [SerializeField] private float length;
        [SerializeField] private int directionRay = -1;
        private void OnDrawGizmos()
        {
            Debug.DrawRay(startPoint.position, startPoint.forward * directionRay * length,Color.green);
        }
#endif

        public bool IsObstacle()
        {
            if (!gameObject.activeSelf) return false;
            return Trigger.IsObstacle;
        }

        public virtual void Spawn(LayerMask obstacles)
        {
            gameObject.SetActive(false);
            SetMaterial(config.BuildCorrectStateMaterial);
            trigger.SetLayerObstacle(obstacles);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            Trigger.OnHide();
        }

        public virtual void IsCorrectState(bool state)
        {
            Material material = state ? config.BuildCorrectStateMaterial : config.BuildWrongStateMaterial;
            SetMaterial(material);
        }

        protected virtual void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public virtual void SetLayer(LayerMask layerMask)
        {
            trigger.gameObject.layer = layerMask.value;
        }

        public virtual void Complite()
        {
            if (gameObject.activeSelf)
            {
                SetMaterial(config.CompliteMaterial);
                Destroy(this);
                Destroy(trigger);
            }
            else
            {
                Destroy(gameObject);
            }
        }



    }
}

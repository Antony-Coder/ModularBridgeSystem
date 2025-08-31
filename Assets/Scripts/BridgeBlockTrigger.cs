using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class BridgeBlockTrigger : MonoBehaviour
    {
 
        private LayerMask layerObstacles;
        public bool IsObstacle{ get; private set; }

        public void SetLayerObstacle(LayerMask layerObstacles)
        {
            this.layerObstacles = layerObstacles;
        }

        public void OnHide()
        {
            IsObstacle = false;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & layerObstacles.value) != 0)
            {
                IsObstacle = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (((1 << other.gameObject.layer) & layerObstacles.value) != 0)
            {
                IsObstacle = false;
            }
        }
    }
}

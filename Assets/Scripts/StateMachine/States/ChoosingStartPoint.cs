using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class ChoosingStartPoint : StateBridgeSystem
    {
        private bool canChoose;
        public override void Enter()
        {
            bridgeSystem.Arrow.gameObject.SetActive(true);
        }

        public override void Update()
        {
            Move();

            if (Input.GetMouseButtonDown(0) && canChoose)
            {
                stateMachine.ChangeState(typeof(ChoosingEndPoint));
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                bridgeSystem.Arrow.gameObject.SetActive(false);
                stateMachine.ChangeState(typeof(ReadyState));
                return;
            }

        }

        private void Move()
        {
            Ray ray = stateMachine.Controller.Cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.magenta);

            if (Physics.Raycast(ray, out RaycastHit hitInfo,  Mathf.Infinity))
            {
                canChoose = (config.LayersForRaycast & (1 << hitInfo.collider.gameObject.layer)) != 0;

                bridgeSystem.Arrow.IsCorrect(canChoose);
                bridgeSystem.Arrow.transform.position = hitInfo.point;

            }
        }

        public override void Exit()
        {
            bridgeSystem.Arrow.gameObject.SetActive(false);
        }


    }
}

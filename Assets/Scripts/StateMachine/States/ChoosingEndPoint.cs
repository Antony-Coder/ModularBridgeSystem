using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class ChoosingEndPoint : StateBridgeSystem
    {
        private bool isCorrectState;
        public override void Enter()
        {
            bridge.Root.transform.position = bridgeSystem.Arrow.transform.position + config.OffsetMoveStartPoint;
            ShowMiniBridge();
        }

        public override void Update()
        {

            PreviewBridge();
            SetStateMaterial();

            if (Input.GetMouseButtonDown(0) && isCorrectState)
            {
                stateMachine.ChangeState(typeof(CompliteState));
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                HideBridge();
                stateMachine.ChangeState(typeof(CompliteState));
                return;
            }
        }

        private void SetStateMaterial()
        {
            bool IsObstacle = false;

            foreach(var block in bridge.GetAllBlocks())
            {
                if (block.IsObstacle())
                {
                  //  Debug.Log($"Material Fail {block.name}");
                    IsObstacle = true;
                    break;
                }
            }

            isCorrectState = !IsObstacle;

            foreach (var block in bridge.GetAllBlocks())
            {
                block.IsCorrectState(isCorrectState);
            }

        }

        private void HideBridge()
        {
            Bridge bridge = stateMachine.Controller.Bridge;

            foreach(var block in bridge.GetAllBlocks())
            {
                block.Hide();
            }
        }

        private void ShowMiniBridge()
        {
            bridge.StartBlock.transform.position = bridge.Root.position;
            bridge.MiddleBlockList[0].transform.position = bridge.StartBlock.EndPoint.position;
            bridge.EndBlock.transform.position = bridge.MiddleBlockList[0].EndPoint.position;

            bridge.StartBlock.Show();
            bridge.EndBlock.Show();
            bridge.MiddleBlockList[0].Show();
        }


        private void PreviewBridge()
        {

            Ray ray = bridgeSystem.Cam.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.magenta);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, config.LayersForRaycast))
            {
                bool canRotate = FindPointAtHeight(ray.origin, ray.direction, bridge.Root.position.y, out Vector3 endPointLook);

                if (canRotate)
                {
                    Vector3 directionLook = bridge.Root.position - endPointLook;
                    bridge.Root.rotation = Quaternion.LookRotation(directionLook, Vector3.up);
                }


                Vector3 linePointA = bridge.StartBlock.EndPoint.position;
                Vector3 linePointB = linePointA + bridge.StartBlock.EndPoint.forward * config.MaxBridgeLength;

                Vector3 endPoint = GetEndPoint(linePointA, linePointB, ray.origin, ray.direction);


                float bridgeLength = (endPoint - linePointA).magnitude;

                bridgeLength -= config.EndBlock.Length;

                ShowBridge(bridgeLength);
            }
        }

        private bool FindPointAtHeight(Vector3 rayOrigin, Vector3 rayDirection, float targetY, out Vector3 result)
        {
            if (Mathf.Approximately(rayDirection.y, 0f))
            {
                if (Mathf.Approximately(rayOrigin.y, targetY))
                {
                    result = rayOrigin;
                    return true;
                }

                result = Vector3.zero;
                return false;
            }

            float t = (targetY - rayOrigin.y) / rayDirection.y;

            if (t < 0f)
            {
                result = Vector3.zero;
                return false;
            }
  

            result = rayOrigin + t * rayDirection;
            return true;
        }


        private void ShowBridge(float bridgeLength)
        {
            if (bridgeLength < 0) return;

            float middleLength = config.MiddleBlock.Length;
            float fillerLength = config.FillerBlock.Length;

            int maxCountMiddle = Mathf.FloorToInt(bridgeLength / middleLength);

            if (maxCountMiddle == 0) return;

            int maxCountFiller = Mathf.FloorToInt((bridgeLength - maxCountMiddle * middleLength) / fillerLength);

            maxCountMiddle = Mathf.Min(maxCountMiddle, bridge.MiddleBlockList.Count);
            maxCountFiller = Mathf.Min(maxCountFiller, bridge.FillerBlockList.Count);

            int fillerCountHalf = maxCountFiller / 2;

            Transform pinPoint = bridge.StartBlock.EndPoint;


            //Debug.Log($"maxCountMiddle: {maxCountMiddle} maxCountFiller: {maxCountFiller} length: {bridgeLength}");

            for (int i = 0; i < fillerCountHalf; i++)
            {
                ShowBlock(bridge.FillerBlockList[i]);
            }

            for (int i = 0; i < maxCountMiddle; i++)
            {
                ShowBlock(bridge.MiddleBlockList[i]);
            }

            for (int i = fillerCountHalf; i < maxCountFiller; i++)
            {
                ShowBlock(bridge.FillerBlockList[i]);
            }

            BridgeBlock block = bridge.EndBlock;
            block.transform.position = pinPoint.position;
            block.Show();



            void ShowBlock(BridgeBlock block)
            {
                block.transform.position = pinPoint.position;
                block.transform.rotation = pinPoint.rotation;
                block.Show();
                pinPoint = block.EndPoint;
            }

            for (int i = maxCountMiddle; i < bridge.MiddleBlockList.Count; i++)
            {
                bridge.MiddleBlockList[i].Hide();
            }

            for (int i = maxCountFiller; i < bridge.FillerBlockList.Count; i++)
            {
                bridge.FillerBlockList[i].Hide();
            }

        }


        private Vector3 GetEndPoint(Vector3 linePointA, Vector3 linePointB, Vector3 rayOrigin, Vector3 rayDirection)
        {
            Vector3 lineDir = linePointB - linePointA;
            Vector3 diff = rayOrigin - linePointA;

            float lineDirSqrMag = lineDir.sqrMagnitude;
            if (lineDirSqrMag == 0f)
            {
                return linePointA;
            }

            float rayDirSqrMag = rayDirection.sqrMagnitude;
            if (rayDirSqrMag == 0f)
            {
                return linePointA + Vector3.Project(diff, lineDir);
            }

            float lineRayDot = Vector3.Dot(lineDir, rayDirection);
            float denominator = lineDirSqrMag * rayDirSqrMag - lineRayDot * lineRayDot;

            float diffLineDot = Vector3.Dot(diff, lineDir);
            float diffRayDot = Vector3.Dot(diff, rayDirection);

            if (!Mathf.Approximately(denominator, 0f))
            {
                float t = (diffLineDot * rayDirSqrMag - diffRayDot * lineRayDot) / denominator;
                float s = (diffLineDot * lineRayDot - diffRayDot * lineDirSqrMag) / denominator;

                if (s >= 0f)
                {
                    return linePointA + t * lineDir;
                }
                else
                {
                    return linePointA + Vector3.Project(diff, lineDir);
                }
            }
            else
            {
                return linePointA + Vector3.Project(diff, lineDir);
            }
        }

    }
}

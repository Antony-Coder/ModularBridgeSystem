using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class ReadyState : StateBridgeSystem
    {
        public override void Enter()
        {
            if (stateMachine.Controller.Bridge == null)
            {
                Spawn();
            }
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateMachine.ChangeState(typeof(ChoosingStartPoint));
                return;
            }
        }


        private void Spawn()
        {
            GameObject bridgeObject = new GameObject("Bridge");

            BridgeSystemConfig config = stateMachine.Controller.BridgeSystemConfig;


            int maxCountMiddle = Mathf.FloorToInt(config.MaxBridgeLength / config.MiddleBlock.Length);
            int maxCountFiller = Mathf.FloorToInt(config.MiddleBlock.Length / config.FillerBlock.Length);

            if (maxCountMiddle < 1)
            {
                maxCountMiddle = 1;
            }

         //   Debug.Log($"maxCountMiddle: {maxCountMiddle} maxCountFiller: {maxCountFiller} length: {config.MaxBridgeLength}");


            List<BridgeBlock> middleBlock = new List<BridgeBlock>();
            List<BridgeBlock> fillerBlock = new List<BridgeBlock>();

            BridgeBlock startBlock = GameObject.Instantiate(config.StartBlock.Prefab);
            BridgeBlock endBlock = GameObject.Instantiate(config.EndBlock.Prefab);

            for (int i = 0; i < maxCountMiddle; i++)
            {
                BridgeBlock newMiddle = GameObject.Instantiate(config.MiddleBlock.Prefab);
                middleBlock.Add(newMiddle);
            }

            for (int i = 0; i < maxCountFiller; i++)
            {
                BridgeBlock newFiller = GameObject.Instantiate(config.FillerBlock.Prefab);
                fillerBlock.Add(newFiller);
            }


            Bridge bridge = new Bridge(bridgeObject.transform, startBlock, endBlock, middleBlock, fillerBlock);


            foreach (var block in bridge.GetAllBlocks())
            {
                block.Spawn(config.LayersObstacles);
                block.transform.SetParent(bridgeObject.transform);
            }


            stateMachine.Controller.SetBridge(bridge);
        }
    }
}

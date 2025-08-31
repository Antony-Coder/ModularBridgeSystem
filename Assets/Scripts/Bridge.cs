using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class Bridge
    {
        public Transform Root { get; private set; }
        public BridgeBlock StartBlock { get; private set; }
        public BridgeBlock EndBlock { get; private set; }
        public List<BridgeBlock> MiddleBlockList { get; private set; }
        public List<BridgeBlock> FillerBlockList { get; private set; }



        public Bridge(Transform root, BridgeBlock startBlock, BridgeBlock endBlock, List<BridgeBlock> middleBlockList, List<BridgeBlock> fillerBlockList)
        {
            StartBlock = startBlock;
            EndBlock = endBlock;
            MiddleBlockList = middleBlockList;
            FillerBlockList = fillerBlockList;
            Root = root;
        }

        public IEnumerable<BridgeBlock> GetAllBlocks()
        {
            yield return StartBlock;

            if (MiddleBlockList != null)
            {
                foreach (var block in MiddleBlockList)
                {
                    yield return block;
                }
            }

            if (MiddleBlockList != FillerBlockList)
            {
                foreach (var block in FillerBlockList)
                {
                    yield return block;
                }
            }

            yield return EndBlock;
        }
    }
}

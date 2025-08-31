using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public abstract class StateBridgeSystem : State<BridgeSystem>
    {
        public Bridge bridge => stateMachine.Controller.Bridge;
        public BridgeSystem bridgeSystem => stateMachine.Controller;
        public BridgeSystemConfig config => stateMachine.Controller.BridgeSystemConfig;
    }
}

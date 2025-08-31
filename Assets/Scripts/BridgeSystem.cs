using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class BridgeSystem : MonoBehaviour
    {
        [SerializeField] private BridgeSystemConfig bridgeSystemConfig;
        [SerializeField] private Camera cam;

        private StateMachine<BridgeSystem> stateMachine;
        public BridgeSystemConfig BridgeSystemConfig { get => bridgeSystemConfig;  }
        public Bridge Bridge { get; private set; }
        public Arrow Arrow { get; private set; }
        public Camera Cam { get => cam;  }

        private void Start()
        {
            stateMachine = new StateMachine<BridgeSystem>(this);

            stateMachine.AddState(typeof(ReadyState), new ReadyState());
            stateMachine.AddState(typeof(ChoosingStartPoint), new ChoosingStartPoint());
            stateMachine.AddState(typeof(ChoosingEndPoint), new ChoosingEndPoint());
            stateMachine.AddState(typeof(CompliteState), new CompliteState());

            Arrow = GameObject.Instantiate(bridgeSystemConfig.Arrow);
            Arrow.gameObject.SetActive(false);

            stateMachine.ChangeState(typeof(ReadyState));
        }


        private void Update()
        {
            stateMachine.Update();
        }

        public void SetBridge(Bridge bridge)
        {
            Bridge = bridge;
        }

    }
}

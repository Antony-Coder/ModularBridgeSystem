

namespace ModularBridgeSystem
{
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine;
        public bool IsReady { get; protected set; } = true;
        public void SetStateMachine(StateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }

    }
}

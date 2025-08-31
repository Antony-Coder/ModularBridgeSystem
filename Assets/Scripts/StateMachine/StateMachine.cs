using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ModularBridgeSystem
{
    public class StateMachine<T>
    {
        private Dictionary<Type, State<T>> states = new Dictionary<Type, State<T>>();
        private T controller;
        private State<T> currentState;

        public event Action<State<T>> StateChanged;
        public State<T> CurrentState { get => currentState; }
        public T Controller { get => controller; }
        public bool IsReady
        {
            get
            {
                if (currentState == null)
                {
                    return true;
                }
                else
                {
                    return currentState.IsReady;
                }
            }
        }

        public StateMachine(T controller)
        {
            this.controller = controller;
        }


        public void AddState(Type type, State<T> state)
        {
            if (!states.ContainsKey(type))
            {
                states.Add(type, state);
            }            
        }


        public void Update()
        {
            currentState?.Update();
        }


        public bool IsCurrentState(Type type)
        {
            return CurrentState.GetType() == type;
        }

        public bool ChangeState(Type newState)
        {
            if(states.TryGetValue(newState, out State<T> state))
            {
                return ChangeState(state);
            }
            else
            {
                Debug.LogError($"Not Added State : {newState}");
                return false;
            }

        }


        public bool ChangeState(State<T> newState)
        {
            if (currentState != null && !currentState.IsReady)
            {
                Debug.Log($"ChangeState is canceled! : {newState}");
                return false;
            }

            newState?.SetStateMachine(this);
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
            StateChanged?.Invoke(currentState);
            return true;
        }






    }
}


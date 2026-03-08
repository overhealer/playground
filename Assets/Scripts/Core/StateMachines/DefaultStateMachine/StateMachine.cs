using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;
using System;
using System.Collections.Generic;

namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine
{
    public abstract class StateMachine :
        IStateMachine
    {
        protected Dictionary<Type, IExitableState> states;
        protected IExitableState activeState;

        public void Enter<TState>() where TState : class, IState
        {
            ChangeState<TState>().Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            ChangeState<TState>().Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            activeState?.Exit();

            TState state = GetState<TState>();
            activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return states[typeof(TState)] as TState;
        }
    }
}
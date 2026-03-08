using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;
using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.UpdatableInterfaces;

namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine
{
    public class UpdateStateMachine :
        StateMachine
    {
        public virtual void UpdateState()
        {
            if (activeState is IUpdateState updateState)
                updateState.Update();
        }

        public virtual void FixedUpdateState()
        {
            if (activeState is IFixedUpdateState fixedUpdateState)
                fixedUpdateState.FixedUpdate();
        }

        public virtual void LateUpdateState()
        {
            if (activeState is ILateUpdateState lateUpdateState)
                lateUpdateState.LateUpdate();
        }

        public IExitableState GetActiveState()
        {
            return activeState;
        }
    }
}
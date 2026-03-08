using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;

namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.UpdatableInterfaces
{
    public interface IFixedUpdateState :
        IExitableState
    {
        void FixedUpdate();
    }
}
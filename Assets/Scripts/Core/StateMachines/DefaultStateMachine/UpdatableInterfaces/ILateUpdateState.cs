using playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces;

namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.UpdatableInterfaces
{
    public interface ILateUpdateState :
        IExitableState
    {
        void LateUpdate();
    }
}
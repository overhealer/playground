namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces
{
    public interface IState :
        IExitableState
    {
        void Enter();
    }
}
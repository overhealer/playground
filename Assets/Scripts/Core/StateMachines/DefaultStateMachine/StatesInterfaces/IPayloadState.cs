namespace playground.Assets.Scripts.Core.StateMachines.DefaultStateMachine.StatesInterfaces
{
    public interface IPayloadState<TPayload> :
        IExitableState
    {
        void Enter(TPayload payload);
    }
}
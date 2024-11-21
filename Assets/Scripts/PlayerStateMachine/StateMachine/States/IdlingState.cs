public class IdlingState : MovementState
{
    public IdlingState(IStateSwitcher stateSwitcher, Player player, StateMachineData data) 
        : base(stateSwitcher, player, data)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Data.Speed = 0;

        View.StartIdling();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopIdling();
    }

    public override void Update()
    {
        base.Update();

        if(IsMovementInputZero())
            return;

        StateSwitcher.SwitchState<RuningState>();
    }
}

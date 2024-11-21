public class RuningState : MovementState
{
    private RuningStateConfig _config;

    public RuningState(IStateSwitcher stateSwitcher, Player player, StateMachineData data) 
        : base(stateSwitcher, player, data) => _config = player.Config.RuningStateConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _config.Speed;

        View.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsMovementInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }
}
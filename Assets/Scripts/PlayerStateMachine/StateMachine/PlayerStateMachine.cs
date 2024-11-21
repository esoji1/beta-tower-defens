using System.Collections.Generic;
using System.Linq;

public class PlayerStateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public PlayerStateMachine(Player player)
    {
        StateMachineData data = new StateMachineData();

        _states = new List<IState>()
        {
            new IdlingState(this, player, data),
            new RuningState(this, player, data)
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();
}

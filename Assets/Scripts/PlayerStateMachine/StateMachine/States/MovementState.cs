using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Windows;

public abstract class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;

    private readonly Player _player;

    public MovementState(IStateSwitcher stateSwitcher, Player player, StateMachineData data)
    {
        StateSwitcher = stateSwitcher;
        _player = player;
        Data = data;
    }

    protected PlayerInput Input => _player.Input;
    protected PlayerView View => _player.View;

    public virtual void Enter()
    {
        //Debug.Log(GetType());
    }

    public virtual void Exit()
    {
    }

    public void HandleInput()
    {
        Data.XInput = ReadHorizontalInput();
        Data.XVelocity = Data.XInput * Data.Speed;

        Data.YInput = ReadVerticalInput();
        Data.YVelocity = Data.YInput * Data.Speed;
    }

    public virtual void Update()
    {
        Vector3 velocity = GetConvertedVelocity();

        _player.transform.Translate(velocity * Time.deltaTime);
        FlipSprite(velocity);
        TurningStepsOffandOn();
    }

    protected bool IsMovementInputZero() => Data.XInput == 0 && Data.YInput == 0;

    private void FlipSprite(Vector3 direction)
    {
        if (direction.x > 0)
        {
            _player.ChangeThePlayersRotationX(false);
            _player.SetParticlesPosition(0f);
        }
        else if (direction.x < 0)
        {
            _player.ChangeThePlayersRotationX(true);
            _player.SetParticlesPosition(0.12f);
        }
    }

    private void TurningStepsOffandOn()
    {
        EmissionModule emission = _player.StepsParticles.emission;

        if (ReadInput() != Vector2.zero)
        {
            _player.StepsParticles.Pause();
            _player.StepsParticles.Play();

            emission.rateOverTime = 10;
        }
        else
        {
            emission.rateOverTime = 0;
        }
    }

    private Vector3 GetConvertedVelocity() => new Vector3(Data.XVelocity, Data.YVelocity, 0);

    private float ReadHorizontalInput() => Input.Movement.Move.ReadValue<Vector2>().x;

    private float ReadVerticalInput() => Input.Movement.Move.ReadValue<Vector2>().y;

    private Vector2 ReadInput() => Input.Movement.Move.ReadValue<Vector2>();
}
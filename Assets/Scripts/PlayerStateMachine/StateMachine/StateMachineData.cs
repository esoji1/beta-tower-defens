using System;

public class StateMachineData
{
    public float XVelocity;
    public float YVelocity;

    private float _speed;
    private float _xInput;
    private float _yInput;

    public float XInput
    {
        get => _xInput;
        set
        {
            if (_xInput < -1 || _xInput > 1)
                throw new ArgumentOutOfRangeException(nameof(_xInput));

            _xInput = value;
        }
    }

    public float YInput
    {
        get => _yInput;
        set
        {
            if (_yInput < -1 || _yInput > 1)
                throw new ArgumentOutOfRangeException(nameof(_yInput));

            _yInput = value;
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));

            _speed = value;
        }
    }
}
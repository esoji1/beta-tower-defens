using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class OrcVeiw : MonoBehaviour
{
    private const string IsRun = "IsRun";
    private const string IsIdle = "IsIdle";
    private const string IsAttack = "IsAttack";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    
    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void StartIdling() => _animator.SetBool(IsIdle, true);
    public void StopIdling() => _animator.SetBool(IsIdle, false);

    public void StartRuning() => _animator.SetBool(IsRun, true);
    public void StopRuning() => _animator.SetBool(IsRun, false);

    public void StartAttacking() => _animator.SetBool(IsAttack, true);
    public void StopAttacking() => _animator.SetBool(IsAttack, false);
}
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class WerewolfView : MonoBehaviour
{
    private const string IsRun = "IsRuning";
    private const string IsAttack = "IsAttack";

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void StartRuning() => _animator.SetBool(IsRun, true);
    public void StopRuning() => _animator.SetBool(IsRun, false);

    public void StartAttacking() => _animator.SetBool(IsAttack, true);
    public void StopAttacking() => _animator.SetBool(IsAttack, false);
}

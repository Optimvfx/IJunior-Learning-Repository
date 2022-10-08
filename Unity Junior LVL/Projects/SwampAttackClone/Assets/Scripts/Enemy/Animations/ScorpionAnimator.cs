using UnityEngine;

[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(CelebrationState))]
[RequireComponent(typeof(Animator))]
public class ScorpionAnimator : MonoBehaviour
{
    private readonly string _attackAnimationName = "Attack";
    private readonly string _idelAnimaitonName = "Idel";

    private AttackState _attackState;
    private CelebrationState _celebrationState;

    private Animator _animator;

    private void Awake()
    {
        _attackState = GetComponent<AttackState>();
        _celebrationState = GetComponent<CelebrationState>();

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _attackState.OnAttack += PlayAttackAnimation;
        _celebrationState.CelebrationActivate += PlayIdelAnimaiton;
    }

    private void OnDisable()
    {
        _attackState.OnAttack -= PlayAttackAnimation;
        _celebrationState.CelebrationActivate -= PlayIdelAnimaiton;
    }

    private void PlayIdelAnimaiton()
    {
        _animator.Play(_idelAnimaitonName);
    }

    private void PlayAttackAnimation()
    {
        _animator.Play(_attackAnimationName);
    }
}

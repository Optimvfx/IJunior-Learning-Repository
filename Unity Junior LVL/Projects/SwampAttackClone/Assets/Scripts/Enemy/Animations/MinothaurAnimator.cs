using UnityEngine;

[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(CelebrationState))]
[RequireComponent(typeof(Animator))]
public class MinothaurAnimator : MonoBehaviour
{
    private readonly string _attackAnimationName = "Attack";
    private readonly string _celebrationAnimaitonName = "Celebration";

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
        _celebrationState.CelebrationActivate += PlayCelebrationAnimaiton;
    }

    private void OnDisable()
    {
        _attackState.OnAttack -= PlayAttackAnimation;
        _celebrationState.CelebrationActivate -= PlayCelebrationAnimaiton;
    }

    private void PlayCelebrationAnimaiton()
    {
        _animator.Play(_celebrationAnimaitonName);
    }
    
    private void PlayAttackAnimation()
    {
        _animator.Play(_attackAnimationName);
    }
}

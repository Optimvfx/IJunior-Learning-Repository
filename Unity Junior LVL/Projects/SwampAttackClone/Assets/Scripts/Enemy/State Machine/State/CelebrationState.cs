using UnityEngine;
using UnityEngine.Events;

public class CelebrationState : State
{
    private Animator _animator;

    public event UnityAction CelebrationActivate;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CelebrationActivate?.Invoke();
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
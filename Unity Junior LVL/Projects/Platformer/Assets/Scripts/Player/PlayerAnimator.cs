using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly string _speedProperty = "Speed";

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private PlayerMovement _movement;

    private void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_movement.TargetVelocityX < 0)
            _spriteRenderer.flipX = false;
        else if(_movement.TargetVelocityX > 0)
            _spriteRenderer.flipX = true;

        _animator.SetFloat(_speedProperty, Mathf.Abs(_movement.TargetVelocityX));
    }
}

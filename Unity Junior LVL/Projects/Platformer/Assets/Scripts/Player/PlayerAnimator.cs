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
        if (Mathf.Approximately(_movement.TargetVelocityX, 0) == false)
            _spriteRenderer.flipX = _movement.TargetVelocityX > 0;

        _animator.SetFloat(_speedProperty, Mathf.Abs(_movement.TargetVelocityX));
    }
}

using UnityEngine;

[RequireComponent(typeof(PlayerShoter))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private readonly string _shootAnimationName = "TryShoot";

    private PlayerShoter _playerShoter;
    private Animator _animator;

    private void Awake()
    {
        _playerShoter = GetComponent<PlayerShoter>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerShoter.OnShot += PlayShotAnimaiton;
    }

    private void OnDisable()
    {
        _playerShoter.OnShot -= PlayShotAnimaiton;
    }

    private void PlayShotAnimaiton()
    {
        _animator.Play(_shootAnimationName);
    }
}

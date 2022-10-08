using UnityEngine;

[RequireComponent(typeof(AttackState))]
public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _attackSound;

    private AttackState _attackState;

    private void Awake()
    {
        _attackState = GetComponent<AttackState>();
    }

    private void OnEnable()
    {
        _attackState.OnAttack += PlayAttackSound;
    }

    private void OnDisable()
    {
        _attackState.OnAttack -= PlayAttackSound;
    }

    private void PlayAttackSound()
    {
        _attackSound.Play();
    }
}

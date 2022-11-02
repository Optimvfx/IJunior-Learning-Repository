using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerSaw _firstSaw;
    [SerializeField] private PlayerSaw _secondSaw;

    private PlayerMover _mover;

    public event UnityAction OnDie;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _mover.OnChangeState += TryDie;
    }

    private void OnDisable()
    {
        _mover.OnChangeState -= TryDie;
    }

    private void TryDie(PlayerMover.CurrentState state)
    {
        var sellectedSaw = _firstSaw;

        if (state == PlayerMover.CurrentState.RotateAroundSecond)
            sellectedSaw = _secondSaw;

        if (sellectedSaw.IsConected == false)
            Die();
    }

    private void Die()
    {
        OnDie?.Invoke();

        Destroy(gameObject);
    }
}

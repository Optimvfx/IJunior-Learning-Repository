using UnityEngine;
using UnityEngine.Events;
using System;

public class ReadOnlyEnemy : MonoBehaviour
{
    [Range(0, 1000)]
    [SerializeField] private int _health;
    [SerializeField] private uint _reward;

    [Header("Die")]
    [SerializeField] private UFloat _dieDelay;

    public int Reward => (int)_reward;

    public UnityEvent OnDie;

    public event UnityAction<ReadOnlyEnemy> Dying;

    private void OnDestroy()
    {
        Dying?.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        _health -= damage;

        if(_health <= 0)
        {
            Dying?.Invoke(this);

            OnDie?.Invoke();

            Destroy(gameObject, _dieDelay);
        }
    }
}

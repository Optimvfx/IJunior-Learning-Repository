using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(StandartEnemy))]
public class AttackState : State
{
    [SerializeField] private uint _damage;
    [SerializeField] private UFloat _delayInSeconds;

    private StandartEnemy _standartEnemy;

    private Coroutine _attackCorutine;

    public event UnityAction OnAttack;

    private void Awake()
    {
        _standartEnemy = GetComponent<StandartEnemy>();
    }

    public void OnEnable()
    {
        _attackCorutine = StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        if (_attackCorutine != null)
            StopCoroutine(_attackCorutine);
    }

    private IEnumerator Attack()
    {
        var delay = new WaitForSeconds(_delayInSeconds);

        while(_standartEnemy.Target != null)
        {
            Attack(_standartEnemy.Target);

            yield return delay;
        }
    }

    private void Attack(Player target)
    {
        OnAttack?.Invoke();

        target.ApplyDamage((int)_damage);
    }
}
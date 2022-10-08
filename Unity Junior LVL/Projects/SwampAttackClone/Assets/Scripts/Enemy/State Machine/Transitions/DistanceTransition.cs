using UnityEngine;

[RequireComponent(typeof(StandartEnemy))]
public class DistanceTransition : Transition
{
    [SerializeField] private UFloat _transitionRange;
    [SerializeField] private UFloat _rangetSpread;

    private StandartEnemy _standartEnemy;

    public override bool NeedTransit => _standartEnemy.Target == null || Vector2.Distance(transform.position, _standartEnemy.Target.transform.position) < _transitionRange;

    private void Awake()
    {
        _standartEnemy = GetComponent<StandartEnemy>();
        _transitionRange += Random.Range(-_rangetSpread, _rangetSpread);
    }
}
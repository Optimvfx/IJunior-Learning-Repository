using UnityEngine;

[RequireComponent(typeof(StandartEnemy))]
public class MoveState : State
{
    [SerializeField] private UFloat _speed;

    private StandartEnemy _standartEnemy;

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);
    }

    private void Awake()
    {
        _standartEnemy = GetComponent<StandartEnemy>();
    }

    private void Update()
    {
        transform.position += new Vector3((_standartEnemy.Target.transform.position - transform.position).normalized.x * _speed * Time.deltaTime, 0, 0);
    }
}
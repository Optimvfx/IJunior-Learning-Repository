using DG.Tweening;
using UnityEngine;

public class Folower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private UFloat _moveSpeedBySecond;
    [SerializeField] private UFloat _minMoveDistance;

    private void Update()
    {
        if (_target == null)
            return;

        if (Vector3.Distance(transform.position, _target.position) <= _minMoveDistance)
            return;

        var moveDirection = (_target.position - transform.position).normalized;

        transform.Translate(moveDirection * _moveSpeedBySecond * Time.deltaTime);
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}

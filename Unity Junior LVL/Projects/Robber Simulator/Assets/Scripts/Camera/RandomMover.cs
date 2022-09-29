using System.Collections;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _maximalMoveDistnace;
    [SerializeField] private float _minimalDistanceToTarget;

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);
        _maximalMoveDistnace = Mathf.Max(_maximalMoveDistnace, 0);
        _minimalDistanceToTarget = Mathf.Max(_minimalDistanceToTarget, 0);
    }

    private void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        var nextMoveTarget = (Vector2)transform.position;
        while(true)
        {
            if (Vector2.Distance(nextMoveTarget, transform.position) <= _minimalDistanceToTarget)
                nextMoveTarget = Random.insideUnitCircle * _maximalMoveDistnace;

            transform.position += (Vector3)(nextMoveTarget - (Vector2)transform.position).normalized * _speed * Time.deltaTime;

            yield return null;
        }
    }
}

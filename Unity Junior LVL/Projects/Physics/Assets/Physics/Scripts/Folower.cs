using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folower : MonoBehaviour
{
    [SerializeField] private Transform _folowing;

    [SerializeField] private float _speedBySecond;

    [SerializeField] protected bool _moveIn2D;

    private void OnValidate()
    {
        _speedBySecond = Mathf.Max(_speedBySecond, 0);
    }

    private void Update()
    {
        MoveToPoint(_folowing.position, _speedBySecond);
    }

    private void MoveToPoint(Vector3 point, float speed)
    {
        if (speed < 0)
            throw new System.ArgumentException();

        var normalizedDirection = (point - transform.position).normalized;

        if (_moveIn2D)
            normalizedDirection -= new Vector3(0, 0, normalizedDirection.z);

        transform.position += normalizedDirection * speed * Time.deltaTime;
    }
}

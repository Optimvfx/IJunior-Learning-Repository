using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Range(0, 1000)]
    [SerializeField] private float _shootDellayInSeconds;
    [SerializeField] private Bullet _bullet;

    private float _lastShootTime = 0;

    private void Update()
    {
        if(_lastShootTime <= 0)
        {
            Shoot();
            _lastShootTime = _shootDellayInSeconds;
        }

        _lastShootTime -= Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
    }
}

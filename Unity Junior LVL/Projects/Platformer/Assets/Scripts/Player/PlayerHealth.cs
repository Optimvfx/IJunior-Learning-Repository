using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _dieingTimeInSeconds;

    public event Action Dieing;

    private void OnValidate()
    {
        _dieingTimeInSeconds = Mathf.Max(0, _dieingTimeInSeconds);
    }

    public void TakeDamage()
    {
        Die();
    }

    private void Die()
    {
        Dieing?.Invoke();

        Destroy(gameObject, _dieingTimeInSeconds);
    }
}

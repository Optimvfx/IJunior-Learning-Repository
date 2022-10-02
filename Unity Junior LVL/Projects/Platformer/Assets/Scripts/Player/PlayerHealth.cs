using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action Dieing;

    [SerializeField] private float _dieingTimeInSeconds;

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

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _value;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;

    public float NormalizedValue => (_value - _minHealth) / _maxHealth;

    public event Action Changed;
    public event Action Died;

    private void OnValidate()
    {
        _minHealth = Mathf.Max(_minHealth, 0);
        _maxHealth = Mathf.Max(_maxHealth, _minHealth);

        _value = Mathf.Clamp(_value, _minHealth, _maxHealth);
    }

    public void Heal(float addingHealth)
    {
        if (addingHealth < 0)
            throw new ArgumentException();

        if (_value <= _minHealth || _value >= _maxHealth)
            return;

        _value += addingHealth;
        _value = Mathf.Min(_value, _maxHealth);

        Changed?.Invoke();;
    }
    
    public void TakeDamage(float damage)
    {
        if(damage < 0)
            throw new ArgumentException();

        if (_value <= _minHealth)
            return;

        _value -= damage;
        _value = Mathf.Max(_value, _minHealth);

        Changed?.Invoke();

        if (_value <= _minHealth)
            Died?.Invoke();
    }
}

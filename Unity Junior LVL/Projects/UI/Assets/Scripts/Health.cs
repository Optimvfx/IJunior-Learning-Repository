using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> HealthValueChanged;
    public event Action Died;

    [SerializeField] private float _health;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;

    public float MaxHealth => _maxHealth;
    public float MinHealth => _minHealth;

    private void OnValidate()
    {
        _minHealth = Mathf.Max(_minHealth, 0);
        _maxHealth = Mathf.Max(_maxHealth, _minHealth);

        _health = Mathf.Clamp(_health, _minHealth, _maxHealth);
    }

    public bool TryHeal(float addingHealth)
    {
        if (addingHealth < 0)
            throw new ArgumentException();

        if (_health <= _minHealth || _health >= _maxHealth)
            return false;

        _health += addingHealth;
        _health = Mathf.Min(_health, _maxHealth);

        HealthValueChanged?.Invoke(_health);

        return true;
    }
    
    public bool TryTakeDamage(float damage)
    {
        if(damage < 0)
            throw new ArgumentException();

        if (_health <= _minHealth)
            return false;

        _health -= damage;
        _health = Mathf.Max(_health, _minHealth);

        HealthValueChanged?.Invoke(_health);

        if (_health <= _minHealth)
            Died?.Invoke();

        return true;
    }
}

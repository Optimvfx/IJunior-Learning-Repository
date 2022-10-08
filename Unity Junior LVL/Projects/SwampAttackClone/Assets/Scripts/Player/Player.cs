using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Range(0, 1000)]
    [SerializeField] private int _maximalHealth;

    private int _currentHealth;

    public float NoramlizdeHealth => (float)_currentHealth / _maximalHealth;

    public event UnityAction<int, int> HealthChanged;

    private void Awake()
    {
        _currentHealth = _maximalHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _maximalHealth);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
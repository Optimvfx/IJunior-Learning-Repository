using UnityEngine.UI;
using UnityEngine;

public class HealthMenu : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Health _health;

    [Header("Add")]
    [SerializeField] private Button _addHealthButton;
    [SerializeField] private float _addingHealth;

    [Header("Remove")]
    [SerializeField] private Button _removeHealthButton;
    [SerializeField] private float _removingHealth;

    [Header("Error")]
    [SerializeField] private InfoLableShower _infoLableShower;
    [SerializeField] private string _errorMessage;
    [SerializeField] private Transform _messagePositon;

    private void OnValidate()
    {
        _addingHealth = Mathf.Max(_addingHealth, 0);
        _removingHealth = Mathf.Max(_removingHealth, 0);
    }

    private void OnEnable()
    {
        _addHealthButton.onClick.AddListener(AddHealth);
        _removeHealthButton.onClick.AddListener(RemoveHealth);
    }

    private void OnDisable()
    {
        _addHealthButton.onClick.RemoveListener(AddHealth);
        _removeHealthButton.onClick.RemoveListener(RemoveHealth);
    }

    private void AddHealth()
    {
        if(_health.TryHeal(_addingHealth) == false)
            _infoLableShower.Show(_errorMessage, _messagePositon.position);
    }

    private void RemoveHealth()
    {
        if (_health.TryTakeDamage(_removingHealth) == false)
            _infoLableShower.Show(_errorMessage, _messagePositon.position);
    }
}

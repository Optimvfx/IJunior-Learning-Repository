using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Extensions;
using System;

public class HealthView : MonoBehaviour
{
    private static readonly float _maximalDispresionHealth = 0.002f;

    [SerializeField] private Health _health;

    [Header("View")]
    [SerializeField] private Image _fillHealthBarImage;
    [SerializeField] private Slider _healthBar;

    [SerializeField] private float _changeHealthViewSpeedPerSecond;

    [Header("Die")]
    [SerializeField] private InfoLableShower _infoLableShower;
    [SerializeField] private string _dieMessage;
    [SerializeField] private Transform _dieMessagePosition;
    [SerializeField] private Color _lableDieColor;

    private Coroutine _changeHealthViewCoroutine;

    private void OnValidate()
    {
        _changeHealthViewSpeedPerSecond = Mathf.Max(_changeHealthViewSpeedPerSecond, 0);
    }

    private void OnEnable()
    {
        _health.HealthValueChanged += ChangeHealthView;
        _health.Died += ShowDieInfo;
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= ChangeHealthView;
        _health.Died -= ShowDieInfo;

        StopCoroutine(_changeHealthViewCoroutine);
    }

    private void ShowDieInfo()
    {
        _fillHealthBarImage.color = _lableDieColor;

        _infoLableShower.Show(_dieMessage, _dieMessagePosition.position);
    }

    private void ChangeHealthView(float healthValue)
    {
        var normalizedHealth = GetNormailizedHealth(healthValue);

        if (_changeHealthViewCoroutine != null)
            StopCoroutine(_changeHealthViewCoroutine);

        _changeHealthViewCoroutine = StartCoroutine(ChangeHealthViewToTarget(normalizedHealth));
    }

    private IEnumerator ChangeHealthViewToTarget(float noramlizedHealth)
    {
        if (noramlizedHealth < 0 || noramlizedHealth > 1)
            throw new ArgumentException();

        while(MathFExtensions.Equals(_healthBar.value, noramlizedHealth, _maximalDispresionHealth) == false)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, noramlizedHealth, _changeHealthViewSpeedPerSecond * Time.deltaTime);

            yield return null;
        }
    }

    private float GetNormailizedHealth(float healthValue)
    {
        return (healthValue - _health.MinHealth) / _health.MaxHealth;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

//Я знаю по код стилю запрещено называть скрипт контроллером, но здесь это подходит под логическое описание того, что делает класс.
[RequireComponent(typeof(ObservationCamera2d))]
public class ThreatController : MonoBehaviour
{
    public static readonly float MaximalThreatLevel = 100;
    public static readonly float MinimalThreatLevel = 0;

    public event Action<float> ThreatLevelChanged;

    [SerializeField] private float _calmThreatLevelModifierPerSecond;
    [SerializeField] private float _hackingThreatLevelModifierPerSecond;

    private float _threatLevel;
    private float _threatLevelModifierPerSecond = 0;

    private Coroutine _aplayThreatLevelModifierCoroutine;

    private ObservationCamera2d _observationCamera2D;

    private void OnValidate()
    {
        _calmThreatLevelModifierPerSecond = Mathf.Min(_calmThreatLevelModifierPerSecond, 0);
        _hackingThreatLevelModifierPerSecond = Mathf.Max(_hackingThreatLevelModifierPerSecond, 0);
    }

    private void Awake()
    {
        _observationCamera2D = GetComponent<ObservationCamera2d>();
    }

    private void OnEnable()
    {
        _observationCamera2D.SawRobber += ActivateHackingThreatLevel;
        _observationCamera2D.LostRobberOutOfSight += ActivateCalmTheatLevel;

        _aplayThreatLevelModifierCoroutine = StartCoroutine(AplayThreatLevelModifier());
    }

    private void OnDisable()
    {
        _observationCamera2D.SawRobber -= ActivateHackingThreatLevel;
        _observationCamera2D.LostRobberOutOfSight -= ActivateCalmTheatLevel;

        StopCoroutine(_aplayThreatLevelModifierCoroutine);   
    }

    private void ActivateHackingThreatLevel()
    {
      _threatLevelModifierPerSecond = _hackingThreatLevelModifierPerSecond;
    }

    private void ActivateCalmTheatLevel()
    {
        _threatLevelModifierPerSecond = _calmThreatLevelModifierPerSecond;
    }

    private IEnumerator AplayThreatLevelModifier()
    {
        float minimalTreatModifier = 0.02f;

        while(true)
        {
            if (MathFExtensions.Equals(_threatLevelModifierPerSecond, 0, minimalTreatModifier))
                yield return null;

            _threatLevel += _threatLevelModifierPerSecond * Time.deltaTime;
            _threatLevel = Mathf.Max(Mathf.Min(_threatLevel, MaximalThreatLevel), MinimalThreatLevel);

            ThreatLevelChanged?.Invoke(_threatLevel);

            yield return null;
        }
    }
}

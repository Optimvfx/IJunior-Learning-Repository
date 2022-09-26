using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(ObservationCamera2d))]
public class AlarmSystem : MonoBehaviour
{
    public static readonly float MaximalThreatLevel = 100;
    public static readonly float MinimalThreatLevel = 0;

    [SerializeField] private float _calmThreatLevelModifierPerSecond;
    [SerializeField] private float _hackingThreatLevelModifierPerSecond;

    private float _threatLevel;

    private Coroutine _hackingThreatLevelCorutine;
    private Coroutine _calmTheatLevelCorutine;

    private ObservationCamera2d _observationCamera2D;

    public float ThreatLevel => _threatLevel;

    public float NormolizedThreatLevel => (_threatLevel - MinimalThreatLevel) / (MaximalThreatLevel - MinimalThreatLevel);

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
    }

    private void OnDisable()
    {
        _observationCamera2D.SawRobber -= ActivateHackingThreatLevel;
        _observationCamera2D.LostRobberOutOfSight -= ActivateCalmTheatLevel;

        StopAllTheatLevelCoroutines();
    }

    private void ActivateCalmTheatLevel()
    {
        StopAllTheatLevelCoroutines();

      _calmTheatLevelCorutine = StartCoroutine(ActivateCalmTheatLevelCoroutine());
    }

    private void ActivateHackingThreatLevel()
    {
        StopAllTheatLevelCoroutines();

        _hackingThreatLevelCorutine = StartCoroutine(ActivateHackingThreatLevelCoroutine());
    }

    private IEnumerator ActivateHackingThreatLevelCoroutine()
    {
        while (true)
        { 
            _threatLevel = Mathf.MoveTowards(_threatLevel, MaximalThreatLevel, _hackingThreatLevelModifierPerSecond * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ActivateCalmTheatLevelCoroutine()
    {
        while (true)
        {
            _threatLevel = Mathf.MoveTowards(_threatLevel, MinimalThreatLevel, -_calmThreatLevelModifierPerSecond * Time.deltaTime);
            yield return null;
        }
    }

    private void StopAllTheatLevelCoroutines()
    {
        if (_calmTheatLevelCorutine != null)
            StopCoroutine(_calmTheatLevelCorutine);
        if (_hackingThreatLevelCorutine != null)
            StopCoroutine(_hackingThreatLevelCorutine);
    }
}

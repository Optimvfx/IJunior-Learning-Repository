using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObservationCamera2d))]
public class Alarmer : MonoBehaviour
{
    public static readonly float MaximalThreatLevel = 100;
    public static readonly float MinimalThreatLevel = 0;

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _calmThreatLevelModifierPerSecond;
    [SerializeField] private float _hackingThreatLevelModifierPerSecond;

    private float _threatLevel;

    private Coroutine _threatLevelModfierCorutine;

    private ObservationCamera2d _observationCamera2D;

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

        StopThreatLevelModifierCorutione();
    }

    private void Update()
    {
        if (_audioSource != null)
            ChangeAudioVolume();
    }

    private void ChangeAudioVolume()
    {
        _audioSource.volume = NormolizedThreatLevel;
    }

    private void ActivateCalmTheatLevel()
    {
        StopThreatLevelModifierCorutione();

        _threatLevelModfierCorutine = StartCoroutine(ActivateThreatLevelModifierCoroutine(MinimalThreatLevel, _calmThreatLevelModifierPerSecond));
    }

    private void ActivateHackingThreatLevel()
    {
        StopThreatLevelModifierCorutione();

        _threatLevelModfierCorutine = StartCoroutine(ActivateThreatLevelModifierCoroutine(MaximalThreatLevel, _hackingThreatLevelModifierPerSecond));
    }

    private IEnumerator ActivateThreatLevelModifierCoroutine(float target, float speedPerSecond)
    {
        while (true)
        { 
            _threatLevel = Mathf.MoveTowards(_threatLevel, target, Mathf.Abs(speedPerSecond) * Time.deltaTime);
            yield return null;
        }
    }

    private void StopThreatLevelModifierCorutione()
    {
        if (_threatLevelModfierCorutine != null)
            StopCoroutine(_threatLevelModfierCorutine);
    }
}

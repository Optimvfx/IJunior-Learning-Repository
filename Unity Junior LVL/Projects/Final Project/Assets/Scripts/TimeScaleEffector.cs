using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeScaleEffector : MonoBehaviour
{
    public const int StandartTimeScale = 1;
    public const int StopedTimeScale = 0;

    private readonly List<GameObject> _timeStopEffectsGivers = new List<GameObject>();

    public void AddTimeStopEffect(GameObject stopTimeEffect)
    {
        if (_timeStopEffectsGivers.Contains(stopTimeEffect))
            return;

            _timeStopEffectsGivers.Add(stopTimeEffect);

        ChangeTimeScale();
    }

    public void RemoveTimeStopEffect(GameObject stopTimeEffect)
    {
        if (_timeStopEffectsGivers.Contains(stopTimeEffect) == false)
            throw new ArgumentException();

        _timeStopEffectsGivers.Remove(stopTimeEffect);

        ChangeTimeScale();
    }

    public void ChangeTimeScale()
    {
        if (_timeStopEffectsGivers.Count <= 0)
            Time.timeScale = StandartTimeScale;
        else
            Time.timeScale = StopedTimeScale;
    }
}

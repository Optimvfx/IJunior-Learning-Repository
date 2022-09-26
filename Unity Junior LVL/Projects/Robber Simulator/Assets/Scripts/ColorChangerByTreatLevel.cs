using System.Collections.Generic;
using UnityEngine;

public class ColorChangerByTreatLevel : MonoBehaviour
{
    [SerializeField] private ThreatController _threatController;
    [SerializeField] private List<ColorLerpChanger> _colorLerpChangers;

    public void OnEnable()
    {
        _threatController.ThreatLevelChanged += ChangeColors;
    }

    public void OnDisable()
    {
        _threatController.ThreatLevelChanged += ChangeColors;
    }

    private void ChangeColors(float threatLevel)
    {
        var lerpValue = (threatLevel - ThreatController.MinimalThreatLevel) / (ThreatController.MaximalThreatLevel - ThreatController.MinimalThreatLevel);

        foreach(var colorLerpChanger in _colorLerpChangers)
        {
            colorLerpChanger.ChangeColor(lerpValue);
        }
    }
}

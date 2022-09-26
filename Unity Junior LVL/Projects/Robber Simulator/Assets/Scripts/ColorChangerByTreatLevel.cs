using System.Collections.Generic;
using UnityEngine;

public class ColorChangerByTreatLevel : MonoBehaviour
{
    [SerializeField] private Alarmer _threatController;
    [SerializeField] private List<ColorLerpChanger> _colorLerpChangers;

    private void Update()
    {
        ChangeColors();
    }

    private void ChangeColors()
    {
        foreach(var colorLerpChanger in _colorLerpChangers)
        {
            colorLerpChanger.ChangeColor(_threatController.NormolizedThreatLevel);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CollectedCylinderSlider : MonoBehaviour
{
    private readonly UFloat _sliderStandartMaxValue = 1;

    [SerializeField] private CylinderSpawnCounter _cylinderSpawnCounter;
    [Header("Color")]
    [SerializeField] private Image _fill;
    [SerializeField] private Color _sellectedColor;
    [SerializeField] private Color _unSellectedColor;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _sliderStandartMaxValue;
    }

    private void OnEnable()
    {
        _cylinderSpawnCounter.OnCylinderActivationCountChange += ShowActivatedProcent;
    }

    private void OnDisable()
    {
        _cylinderSpawnCounter.OnCylinderActivationCountChange -= ShowActivatedProcent;
    }

    private void ShowActivatedProcent(int activated, int allCount)
    {
        if (allCount < activated)
            throw new System.ArgumentException("Activated must by smaller then all!");

        var procent = activated / (float)allCount;

        _slider.value = procent;

        ShowActivatedProcentColor(procent);
    }

    private void ShowActivatedProcentColor(float procent)
    {
        _fill.color = Color.Lerp(_unSellectedColor, _sellectedColor, procent);
    }
}

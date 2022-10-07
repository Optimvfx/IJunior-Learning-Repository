using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBaseDOTween : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _loopTimeInSeconds;

    private Material _material;

    private void OnValidate()
    {
        _loopTimeInSeconds = Mathf.Max(_loopTimeInSeconds, 0);
    }

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;

        _material.DOColor(_targetColor, _loopTimeInSeconds).SetLoops(-1, LoopType.Yoyo);
        _material.DOFade(0, _loopTimeInSeconds).SetLoops(-1, LoopType.Yoyo);
    }
}

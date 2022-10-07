using DG.Tweening;
using UnityEngine;

public class FolowBaseDOTween : MonoBehaviour
{
    private readonly float _standartSpeed = 1;

    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeedBySecond;

    private Tweener _moveToTargetTween;

    private Vector3 _targetLastPosition;

    private void OnValidate()
    {
        _moveSpeedBySecond = Mathf.Max(_moveSpeedBySecond, 0);
    }

    private void Start()
    {
        StartMoveToTargetTween();
    }

    private void Update()
    {
        if (_target.position != _targetLastPosition)
            UpdateMoveToTargetTween();
    }

    private void StartMoveToTargetTween()
    {
        _moveToTargetTween = transform.DOMove(_target.position, _standartSpeed);
        _moveToTargetTween.SetAutoKill(false);
        _moveToTargetTween.DOTimeScale(GetMoveToTargetTimeScale(), 0);

        _targetLastPosition = _target.position;
    }

    private void UpdateMoveToTargetTween()
    {
        _moveToTargetTween.ChangeEndValue(_target.position, true).Restart();
        _moveToTargetTween.DOTimeScale(GetMoveToTargetTimeScale(), 0);

        _targetLastPosition = _target.position;
    }
    
    private float GetMoveToTargetTimeScale()
    {
        return _moveSpeedBySecond / Vector3.Distance(_target.position, transform.position);
    }
}

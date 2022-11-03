using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    private readonly Vector3 _rotationDirection = new Vector3(0, 1, 0);

    [SerializeField] private Player _self;
    [Header("Rotation")]
    [SerializeField] private UFloat _rotationSpeed;
    [Header("Pivot")]
    [SerializeField] private Transform _firstPivot;
    [SerializeField] private Transform _secondPivot;

    private Transform _currentPivotTransform;

    private CurrentState _currentState = CurrentState.RotateAroundFirst;

    public event UnityAction<CurrentState> OnChangeState;

    private void Awake()
    {
        ChangeState(_currentState);
    }

    private void Update()
    {
        Rotate();
    }

    public void FlipState()
    {
        var pivot = CurrentState.RotateAroundFirst;

        if (_currentState == CurrentState.RotateAroundFirst)
            pivot = CurrentState.RotateAroundSecond;

        ChangeState(pivot);

        OnChangeState?.Invoke(_currentState);
    }

    private void ChangeState(CurrentState state)
    {
        _currentState = state;

        var playerParent = _secondPivot;

        if (_currentState == CurrentState.RotateAroundFirst)
            playerParent = _firstPivot;

        _currentPivotTransform = playerParent;

        _self.transform.SetParent(playerParent);
    }

    private void Rotate()
    {
        _currentPivotTransform.transform.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime * (int)_currentState);
    }

    public enum CurrentState
    {
        RotateAroundFirst = -1,
        RotateAroundSecond = 1
    }
}

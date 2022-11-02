using UnityEngine;
using UnityEngine.Events;

public class MoveObserver : MonoBehaviour
{
    [SerializeField] private UFloat _minMoveOffset;

    public event UnityAction<Vector3> OnMove;
    public event UnityAction OnStop;

    private Vector3 _prewiusPosition;

    private void Awake()
    {
        _prewiusPosition = transform.position;
    }

    private void Update()
    {
        if(Mathf.Abs(_prewiusPosition.sqrMagnitude - transform.position.sqrMagnitude) < _minMoveOffset)
        {
            OnStop?.Invoke();
        }
        else
        {
            OnMove?.Invoke(_prewiusPosition - transform.position);
        }

        _prewiusPosition = transform.position;
    }
}

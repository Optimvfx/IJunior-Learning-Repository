using UnityEngine;

public class PositionBinder : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        if (_target.IsChildOf(transform) == false && transform.IsChildOf(_target) == false)
            transform.position = _target.transform.position;
    }
}

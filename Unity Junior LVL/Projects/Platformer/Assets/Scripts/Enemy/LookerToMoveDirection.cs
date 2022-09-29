using UnityEngine;

public class LookerToMoveDirection : MonoBehaviour
{
private static readonly float _fullSphareAngle = 360;

    private Vector2 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        LookToMoveDirection();
    }

    private void LookToMoveDirection()
    {
        var move = _lastPosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + _fullSphareAngle / 2, Vector3.forward);

        _lastPosition = transform.position;
    }
}

using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RandomMover : MonoBehaviour
{
    private readonly uint _sellectNextMoveTryMaximalCount = 100;

    private readonly uint _hitBufferLength = 16;

    private readonly Color _gizmosColor = Color.cyan;
    private readonly float _gizmosRadius = 1;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask _layerMask;

    [Header("General")]
    [SerializeField] private float _speed;

    [Header("Move Distanse")]
    [SerializeField] private float _maximalMoveDistnace;
    [SerializeField] private float _minimalMoveDistance;

    [Header("Contact")]
    [SerializeField] private float _minimalDistanceToTarget;

    private Vector2 _nextMoveTarget;

    private Rigidbody2D _rigidbody;

    private ContactFilter2D _contactFilter;

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);

        _maximalMoveDistnace = Mathf.Max(_maximalMoveDistnace, _minimalMoveDistance);
        _minimalMoveDistance = Mathf.Max(_minimalMoveDistance, 0);

        _minimalDistanceToTarget = Mathf.Max(_minimalDistanceToTarget, 0);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;

        Gizmos.DrawSphere(_nextMoveTarget, _gizmosRadius);
    }

    private IEnumerator MoveRandomly()
    {
        _nextMoveTarget = (Vector2)transform.position;

        while (true)
        {
            if (Vector2.Distance(_nextMoveTarget, transform.position) <= _minimalDistanceToTarget)
                _nextMoveTarget = (Vector2)transform.position + GetNextMoveVelocity();

            transform.position += (Vector3)(_nextMoveTarget - (Vector2)transform.position).normalized * _speed * Time.deltaTime;

            yield return null;
        }
    }

    private Vector2 GetNextMoveVelocity()
    {
        for (int i = 0; i < _sellectNextMoveTryMaximalCount; i++)
        {
            var randomVelocity = Random.insideUnitCircle * _maximalMoveDistnace;

            var hitBuffer = _rigidbody.GetProjectionContacts(randomVelocity, _hitBufferLength, _contactFilter);

            if (hitBuffer.Count() == 0)
                return randomVelocity;

            var minimalProjectionDistance = hitBuffer.Max(projection => projection.distance);

            if (minimalProjectionDistance > _minimalMoveDistance)
                return randomVelocity.normalized * minimalProjectionDistance;
        }

        return Vector2.zero;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private static readonly uint _hitBufferLength = 16;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask _layerMask;

    [Header("General")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _runModifier;

    [Header("Gravity")]
    [SerializeField] private float _minimalGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;

    [Header("Extra")]
    [SerializeField] private float _minimalMoveDistance = 0.001f;
    [SerializeField] private float _shellRadius = 0.01f;

    private Vector2 _velocity;
    private float _targetVelocityX;

    private ContactFilter2D _contactFilter;

    private bool _isGrounded;
    private Vector2 _groundNormal;

    private Rigidbody2D _rigidbody;

    public float TargetVelocityX => _targetVelocityX;

    private void OnValidate()
    {
        _speed = Mathf.Max(_speed, 0);
        _jumpForce = Mathf.Max(_jumpForce, 0);
        _runModifier = Mathf.Max(_runModifier, 2);

        _minimalGroundNormalY = Mathf.Max(_minimalGroundNormalY, 0);
        _gravityModifier = Mathf.Max(_gravityModifier, 0);

        _minimalMoveDistance = Mathf.Max(_minimalMoveDistance, 0);
        _shellRadius = Mathf.Max(_shellRadius, 0);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    public void SetMoveDirection(float direction, bool isRun)
    {
        _targetVelocityX = Mathf.Clamp(direction, -1, 1);

        if (isRun)
            _targetVelocityX *= _runModifier;
    }

    public bool TryJump()
    {
        if (_isGrounded == false)
            return false;

        _velocity.y = _jumpForce;

        return true;
    }

    private void DoMovement()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocityX * _speed;

        Vector2 deltaPosition = _velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        _isGrounded = false;

        DoMovement(move, false);

        move = Vector2.up * deltaPosition.y;

        DoMovement(move, true);
    }

    private void DoMovement(Vector2 move, bool doYMovement)
    {
        float distance = move.magnitude;

        if (distance > _minimalMoveDistance)
        {
            foreach (var hit in _rigidbody.GetProjectionContacts(move, _hitBufferLength, _contactFilter, _shellRadius))
            {
                Vector2 currentNormal = hit.normal;

                if (currentNormal.y > _minimalGroundNormalY)
                {
                    _isGrounded = true;

                    if (doYMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                ApplayProjection(currentNormal);

                float modifiedDistance = hit.distance - _shellRadius;
                distance = Mathf.Min(modifiedDistance, distance);
            }
        }

        _rigidbody.position += move.normalized * distance;
    }

    private void ApplayProjection(Vector2 normal)
    {
        normal = normal.normalized;

        float projection = Vector2.Dot(_velocity, normal);

        if (projection < 0)
        {
            _velocity = _velocity - projection * normal;
        }
    }
}


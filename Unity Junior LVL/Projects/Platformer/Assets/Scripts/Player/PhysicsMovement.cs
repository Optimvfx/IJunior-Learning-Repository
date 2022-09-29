using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _minimalGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;

    private Vector2 _velocity;
    private Vector2 _targetVelocity;

    private ContactFilter2D _contactFilter;

    private bool _isGrounded;
    private Vector2 _groundNormal;

    private Rigidbody2D _rigedbody;

    private const float _minimalMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    private const uint _hitBufferLength = 16;

    private void Start()
    {
        _rigedbody = GetComponent<Rigidbody2D>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
            _velocity.y = _jumpForce;
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x * _speed;

        _isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minimalMoveDistance)
        {
            foreach (var hit in GetProjectionContacts(move))
            {
                Vector2 currentNormal = hit.normal;

                if (currentNormal.y > _minimalGroundNormalY)
                {
                    _isGrounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = hit.distance - _shellRadius;
                distance = Mathf.Min(modifiedDistance, distance);
            }
        }

        _rigedbody.position = _rigedbody.position + move.normalized * distance;
    }

    private List<RaycastHit2D> GetProjectionContacts(Vector2 move)
    {
        float distance = move.magnitude;

        var hitBuffer = new RaycastHit2D[_hitBufferLength];

        int count = _rigedbody.Cast(move, _contactFilter, hitBuffer, distance + _shellRadius);

        return hitBuffer.Take(count).ToList();
    }
}

/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private _layerMask _layerMask;
    [SerializeField] private float _minimalGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    private Vector2 _targetVelocity;
    private ContactFilter2D _contactFilter;
    private bool _isGrounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigedbody;
    private const float _minimalMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;
    private const uint _hitBufferLength = 16;
    private void Start()
    {
        _rigedbody = GetComponent<Rigidbody2D>();
        _contactFilter = new ContactFilter2D();
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }
    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (Input.GetKey(KeyCode.Space) && _isGrounded)
            _velocity.y = 5;
    }
    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;
        _isGrounded = false;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        //DoMovement(move);
        move = Vector2.up * deltaPosition.y;
        //DoMovement(move);
    }
    private int GetProjectMoveCollisions(Vector2 move, out RaycastHit2D[] _hitBuffer)
    {
        float distance = move.magnitude;
        
        _hitBuffer = new RaycastHit2D[_hitBufferLength];
        return _rigedbody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);
    }
    private void DoVerticalMovement(float move, in RaycastHit2D[] _hitBuffer)
    {
        float distance = move;
        if (distance > _minimalMoveDistance)
        {
            for (int i = 0; i < _hitBuffer.Length; i++)
            {
                Vector2 currentNormal = _hitBuffer[i].normal;
                if (currentNormal.y > _minimalGroundNormalY)
                {
                    _isGrounded = true;
                    _groundNormal = currentNormal;
                }
            }
        }
        _rigedbody.position = _rigedbody.position + new Vector2(move, 0) * distance;
    }
    private void DoHorizontalMovment(float move, in RaycastHit2D[] _hitBuffer)
    {
        float distance = move;
        if (distance > _minimalMoveDistance)
        {
            for (int i = 0; i < _hitBuffer.Length; i++)
            {
                Vector2 currentNormal = _hitBuffer[i].normal;
                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }
                float modifiedDistance = _hitBuffer[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        _rigedbody.position = _rigedbody.position + new Vector2(move, 0) * distance;
    }
}*/
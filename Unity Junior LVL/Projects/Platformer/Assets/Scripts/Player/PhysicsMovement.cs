using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public float MinGroundNormalY = .65f;
    public float GravityModifier = 1f;
    public Vector2 Velocity;
    public LayerMask LayerMask;

    public float _speed;
    public float _jumpForce;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Velocity.y = _jumpForce;
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x * _speed;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                Debug.Log(currentNormal);

                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}

/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

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

    private int GetProjectMoveCollisions(Vector2 move, out RaycastHit2D[] hitBuffer)
    {
        float distance = move.magnitude;
        
        hitBuffer = new RaycastHit2D[_hitBufferLength];

        return _rigedbody.Cast(move, _contactFilter, hitBuffer, distance + _shellRadius);
    }

    private void DoVerticalMovement(float move, in RaycastHit2D[] hitBuffer)
    {
        float distance = move;

        if (distance > _minimalMoveDistance)
        {
            for (int i = 0; i < hitBuffer.Length; i++)
            {
                Vector2 currentNormal = hitBuffer[i].normal;

                if (currentNormal.y > _minimalGroundNormalY)
                {
                    _isGrounded = true;
                    _groundNormal = currentNormal;
                }
            }
        }

        _rigedbody.position = _rigedbody.position + new Vector2(move, 0) * distance;
    }

    private void DoHorizontalMovment(float move, in RaycastHit2D[] hitBuffer)
    {
        float distance = move;

        if (distance > _minimalMoveDistance)
        {
            for (int i = 0; i < hitBuffer.Length; i++)
            {
                Vector2 currentNormal = hitBuffer[i].normal;

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBuffer[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigedbody.position = _rigedbody.position + new Vector2(move, 0) * distance;
    }
}*/
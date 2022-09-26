using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;
using System.Linq;

public class ObservationCamera2d : GameObjectExtensions.MonoBehivour2D
{
    private static readonly Color _unDetectedColor = new Color(1, 0.1f, 0.1f);
    private static readonly Color _detectedColor = new Color(0.1f, 0.9f, 0.3f);

    public event Action SawRobber;
    public event Action LostRobberOutOfSight;

    [SerializeField] private RobberSpawner _robberSpawner;

    [SerializeField] private float _detectionDistance;

    [Range(0f, 120f)]
    [SerializeField] private float _detectionAngle;

    private bool _seeAnyRobber = false;

    private IEnumerable<Robber> _robbers => _robberSpawner.Robbers;

    private void OnValidate()
    {
        _detectionDistance = Mathf.Max(_detectionDistance, 0);
    }

    private void Update()
    {
        if (IsAnyRobberInDetectionZone(_robbers))
        {
            if (_seeAnyRobber)
                return;

            _seeAnyRobber = true;

            SawRobber?.Invoke();
        }
        else
        {
            if(_seeAnyRobber)
            {
                LostRobberOutOfSight?.Invoke();
            }

            _seeAnyRobber = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (_robberSpawner == null)
            return;

        foreach (var robber in _robbers)
        {
            DrawRobberDetectionGizmos(robber);
        }
    }

    private bool IsAnyRobberInDetectionZone(IEnumerable<Robber> robbers)
    {
        return robbers.Any(robber => IsRobberInDetectionZone(robber));
    }

    private bool IsRobberInDetectionZone(Robber robber)
    {
        if (robber == null)
            throw new NullReferenceException();

        if (Vector2.Distance(transform.position, robber.transform.position) > _detectionDistance)
            return false;

        if (VectorExtensions.GetAngleBetwinPoints(transform.position, robber.transform.position, Forward) > _detectionAngle)
            return false;

        var hit = Physics2D.Raycast(transform.position, VectorExtensions.GetNormal(transform.position, robber.transform.position));

        return hit  && hit.transform.gameObject == robber.gameObject;
    }

    private void DrawRobberDetectionGizmos(Robber robber)
    {
        var gizmosColor = IsRobberInDetectionZone(robber) ? _detectedColor : _unDetectedColor;

        GizmosExtensions.DrawLine(transform.position, (Vector2)transform.position + VectorExtensions.GetNormal(transform.position, robber.transform.position) * Vector2.Distance(transform.position, robber.transform.position), gizmosColor);
    }
}

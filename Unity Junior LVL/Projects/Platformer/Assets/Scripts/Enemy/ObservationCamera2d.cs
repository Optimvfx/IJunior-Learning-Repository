using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;
using System.Linq;

public class ObservationCamera2d : GameObjectExtensions.MonoBehivour2D
{
    private readonly Color _unDetectedColor = new Color(1, 0.1f, 0.1f);
    private readonly Color _detectedColor = new Color(0.1f, 0.9f, 0.3f);

    private readonly uint _hitBufferLength = 1;

    [Header("Layer Mask")]
    [SerializeField] private LayerMask _layerMask;

    [Header("General")]
    [SerializeField] private PlayerContacter _player;

    [SerializeField] private float _detectionDistance;

    [Range(0f, 120f)]
    [SerializeField] private float _detectionAngle;

    private bool _isPlayerDetected = false;

    private ContactFilter2D _contactFilter;

    public event Action SawPlayer;
    public event Action LostPlayerOutOfSight;

    private void OnValidate()
    {
        _detectionDistance = Mathf.Max(_detectionDistance, 0);
    }

    private void Awake()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        if(IsPlayerInDetectionZone(_player))
        {
            if(_isPlayerDetected == false)
            {
                SawPlayer?.Invoke();
                _isPlayerDetected = true;
            }
        }
        else
        {
            if(_isPlayerDetected)
            {
                LostPlayerOutOfSight?.Invoke();
                _isPlayerDetected = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        DrawPlayerDetectionGizmos(_player);
    }

    private bool IsPlayerInDetectionZone(PlayerContacter player)
    {
        if (player == null)
           return false;

        if (Vector2.Distance(transform.position, player.transform.position) > _detectionDistance)
            return false;

        if (VectorExtensions.GetAngleBetwinPoints(transform.position, player.transform.position, Forward) > _detectionAngle)
            return false;

        var hitResults = new RaycastHit2D[_hitBufferLength];

        var hitCount = Physics2D.Raycast(transform.position, VectorExtensions.GetNormal(transform.position, player.transform.position), _contactFilter, hitResults);

        return hitCount > 0 && hitResults[0].transform.gameObject == player.gameObject;
    }

    private void DrawPlayerDetectionGizmos(PlayerContacter player)
    {
        if (player == null)
            return;

        var gizmosColor = IsPlayerInDetectionZone(player) ? _detectedColor : _unDetectedColor;

        GizmosExtensions.DrawLine(transform.position, (Vector2)transform.position + VectorExtensions.GetNormal(transform.position, player.transform.position) * Vector2.Distance(transform.position, player.transform.position), gizmosColor);
    }
}

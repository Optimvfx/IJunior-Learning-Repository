using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Camera2D : MonoBehaviour
{
    [SerializeField] private RobberSpawner _robberSpawner;

    [SerializeField] private float _visitableAngle;
    [SerializeField] private float _visitableRange;

    private void OnValidate()
    {
        _visitableAngle = Mathf.Max(_visitableAngle, 0);
        _visitableRange = Mathf.Max(_visitableRange, 0);
    }

    private bool TryGetAnyVisibleRobber(out Robber robber)
    {
        robber = null;

        return false;
    }

    private bool IsRobberVisibe(Robber robber)
    {
        if (robber == null)
            throw new ArgumentException("Robber is null");



        throw new ArgumentException();

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CylinderMapPlacer : MonoBehaviour
{
    public IEnumerable<Cylinder> Place(ReadOnlyCylinderMap cylinderMap)
    {
        if (cylinderMap == null)
            throw new NullReferenceException();

        OnStartPlace(cylinderMap);

        var placedCylinders = new List<Cylinder>();

        for(int x = 0; x < cylinderMap.Widht; x++)
        {
            for(int y = 0; y < cylinderMap.Height; y++)
            {
                var newCylinder = PlaceAtPosition(new Vector2Int(x, y), cylinderMap[x, y]);

                placedCylinders.Add(newCylinder);
            }
        }

        return placedCylinders.Where(cylinder => cylinder != null);
    }

    protected abstract Cylinder PlaceAtPosition(Vector2Int positon, CylinderMap.MapObjectType mapObject);

    protected abstract void OnStartPlace(ReadOnlyCylinderMap cylinderMap);
}

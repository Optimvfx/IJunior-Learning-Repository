using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

public class CylinderFieldGenerator<Arguments> : CylinderFieldGenerator
    where Arguments : ICylinderMapGeneratorArguments
{
    protected void TryGenerate(CylinderMapPlacer cylinderMapPlacer, CylinderMapGenerator<Arguments> cylinderMapGenerator, Arguments arguments)
    {
        if (cylinderMapGenerator == null || cylinderMapPlacer == null)
            throw new ArgumentNullException();

        var map = cylinderMapGenerator.GetMapByArguments(arguments);

        foreach (var placed in cylinderMapPlacer.Place(map))
        {
            AddPlaced(placed);
        }
    }
}

public class CylinderFieldGenerator : MonoBehaviour
{
    private List<Cylinder> _placed = new List<Cylinder>();

    public IEnumerable<Cylinder> Placed => _placed;

    public event UnityAction<Cylinder> OnPlace;

    protected void AddPlaced(Cylinder placed)
    {
        OnPlace?.Invoke(placed);
        _placed.Add(placed);
    }
}

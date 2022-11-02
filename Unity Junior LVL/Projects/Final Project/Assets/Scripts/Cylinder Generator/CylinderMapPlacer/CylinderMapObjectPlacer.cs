using System.Collections.Generic;
using UnityEngine;

public class CylinderMapObjectPlacer : CylinderMapPlacer
{
    [SerializeField] private Transform _container;
    [Header("Prefab")]
    [SerializeField] private Cylinder _cylinderPrefab;
    [SerializeField] private Cylinder _emptyPrefab;

    private Dictionary<ReadOnlyCylinderMap.MapObjectType, Placer> _mapObjectToPlacerConvertor;

    private delegate Cylinder Placer(Vector2Int positon);

    private Vector2Int _mapSize = new Vector2Int(0, 0);

    protected override void OnStartPlace(ReadOnlyCylinderMap cylinderMap)
    {
        if (_mapObjectToPlacerConvertor == null)
            _mapObjectToPlacerConvertor = GenerateMapToObjectConvertor();

        _mapSize = new Vector2Int(cylinderMap.Widht, cylinderMap.Height);
    }

    protected override Cylinder PlaceAtPosition(Vector2Int positon, ReadOnlyCylinderMap.MapObjectType mapObject)
    {
        if (_mapObjectToPlacerConvertor.ContainsKey(mapObject) == false)
            return null;

        return _mapObjectToPlacerConvertor[mapObject](positon);
    }

    private Dictionary<ReadOnlyCylinderMap.MapObjectType, Placer> GenerateMapToObjectConvertor()
    {
        var mapObjectToPlacerConvertor = new Dictionary<ReadOnlyCylinderMap.MapObjectType, Placer>();

        mapObjectToPlacerConvertor[ReadOnlyCylinderMap.MapObjectType.Cylinder] = TryPlaceCylinderAtPosition;
        mapObjectToPlacerConvertor[ReadOnlyCylinderMap.MapObjectType.Non] = TryPlaceNonAtPosition;

        return mapObjectToPlacerConvertor;
    }

    private Cylinder TryPlaceNonAtPosition(Vector2Int positon)
    {
        if (_emptyPrefab != null)
            return Place(_emptyPrefab, positon);

        return null;
    }

    private Cylinder TryPlaceCylinderAtPosition(Vector2Int positon)
    {
        return Place(_cylinderPrefab, positon);
    }

    private Cylinder Place(Cylinder prefab, Vector2Int positon)
    {
        return Instantiate(prefab, GetPlacePosition(positon), prefab.transform.rotation, _container);
    }

    private Vector3 GetPlacePosition(Vector2Int positon)
    {
        const int center = 2;

        return transform.position + new Vector3(positon.x, 0, positon.y) - (new Vector3(_mapSize.x, 0, _mapSize.y) / center);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class TilemapEditor : MonoBehaviour
{
    private static readonly int _standartTileZPosition = 0;

    [SerializeField] private Tilemap _tilemap;

    private void OnValidate()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void SetTile(Vector2Int postion, Tile tile)
    {
        _tilemap.SetTile(new Vector3Int(postion.x, postion.y, _standartTileZPosition), tile);
    }

    public bool IsTileEmpty(Vector2Int postion)
    {
        return _tilemap.GetTile(new Vector3Int(postion.x, postion.y, _standartTileZPosition)) == null;
    }
}

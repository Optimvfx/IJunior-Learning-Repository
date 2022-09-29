using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(TilemapEditor))]
public class TileMapEditorFillCommand : MonoBehaviour
{
    [SerializeField] private TilemapEditor _tilemapEditor;

    [SerializeField] private Vector2Int _fillStartPositon;
    [SerializeField] private Vector2Int _fillEndPositon;

    [SerializeField] private List<Tile> _fillTiles;
 
    private void OnValidate()
    {
        _fillEndPositon = new Vector2Int(Mathf.Max(_fillStartPositon.x ,_fillEndPositon.x), Mathf.Max(_fillStartPositon.y, _fillEndPositon.y));

        _tilemapEditor = GetComponent<TilemapEditor>();
    }


    [ContextMenu("Fill")]
    private void Fill()
    {
        Fill(_fillStartPositon);
    }

    private void Fill(Vector2Int startPosition)
    {
        Stack<Vector2Int> tilesToFillPositon = new Stack<Vector2Int>();

        tilesToFillPositon.Push(startPosition);

        while(tilesToFillPositon.Count > 0)
        {
            var tileToFillPositon = tilesToFillPositon.Pop();

            Debug.Log(tileToFillPositon);

            if (_tilemapEditor.IsTileEmpty(tileToFillPositon) == false|| TilePositionOutOfBounds(tileToFillPositon))
                continue;

            _tilemapEditor.SetTile(tileToFillPositon, _fillTiles.GetRandomElement());

            foreach (var nextTilePostion in GetNextTilesPostions(tileToFillPositon))
            {
                tilesToFillPositon.Push(nextTilePostion);
            }
        }
    }

    private bool TilePositionOutOfBounds(Vector2Int tilePositon)
    {
        return tilePositon.x < _fillStartPositon.x || tilePositon.y < _fillStartPositon.y ||
               tilePositon.x > _fillEndPositon.x || tilePositon.y > _fillEndPositon.y; ;
    }

    private IEnumerable<Vector2Int> GetNextTilesPostions(Vector2Int position)
    {
        yield return position + Vector2Int.up;
        yield return position + Vector2Int.down;
        yield return position + Vector2Int.left;
        yield return position + Vector2Int.right;
    }
}

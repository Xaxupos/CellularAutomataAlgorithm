using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileState _tileState;
    [SerializeField] private TileDataSO _tileData;
    [SerializeField] private Vector2 _gridPosition;

    [SerializeField] private List<Tile> _neighborTiles = new List<Tile>();

    private Image _tileImage;

    private void Awake()
    {
        _tileImage = GetComponent<Image>();
    }

    private void Start()
    {
        SetIntialValues();
    }

    public Vector2 GetGridPosition()
    {
        return _gridPosition;
    }

    public void SetGridPosition(Vector2 pos)
    {
        _gridPosition = pos;
    }

    public void AddNeighborTile(Tile tile)
    {
        if(_neighborTiles.Contains(tile)) return;

        _neighborTiles.Add(tile);
    }

    private void SetIntialValues()
    {
        _tileImage.sprite = _tileData.BlackSprite;
        _tileState = TileState.Black;
    }
}

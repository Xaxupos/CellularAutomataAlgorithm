using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Tile : MonoBehaviour
{
    [SerializeField] private TileState _tileState;
    [SerializeField] private TileDataSO _tileData;
    [SerializeField] private List<Tile> _neighborTiles = new List<Tile>();

    private Vector2 _gridPosition;
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

    public List<Tile> GetNeighbors()
    {
        return _neighborTiles;
    }

    public void AddNeighborTile(Tile tile)
    {
        if(_neighborTiles.Contains(tile)) return;

        _neighborTiles.Add(tile);
    }

    public TileState GetTileState()
    {
        return _tileState;
    }

    public void ChangeTileState()
    {
        _tileState = _tileState == TileState.Dead ? _tileState = TileState.Alive : _tileState = TileState.Dead;

        if(_tileState == TileState.Alive)
        {
            _tileImage.sprite = _tileData.WhiteSprite;
        }
        else
        {
            _tileImage.sprite = _tileData.BlackSprite;
        }
    }

    private void SetIntialValues()
    {
        _tileImage.sprite = _tileData.BlackSprite;
        _tileState = TileState.Dead;
    }
}

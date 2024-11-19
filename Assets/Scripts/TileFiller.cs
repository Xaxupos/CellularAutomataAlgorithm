using UnityEngine;

public class TileFiller : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        FillBoardWithTiles();
    }

    private void FillBoardWithTiles()
    {
        float width = _rectTransform.sizeDelta.x;
        float height = _rectTransform.sizeDelta.y;

        GenerationManager.Instance.TileArray = new Tile[(int)width, (int)height];

        int columns = (int)width / 10;
        int rows = (int)height / 10;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                var tile = Instantiate(_tilePrefab, this.transform);
                GenerationManager.Instance.TileArray[x, y] = tile.GetComponent<Tile>();
                tile.GetComponent<Tile>().SetGridPosition(new Vector2(x, y));
            }
        }

        AssignNeighbors(columns, rows, GenerationManager.Instance.TileArray);
    }

    private void AssignNeighbors(int columns, int rows, Tile[,] tileArray)
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                var currentTile = tileArray[x, y];
                TryAddNeighbor(x, y + 1, x, y, tileArray, currentTile); // Right
                TryAddNeighbor(x, y - 1, x, y, tileArray, currentTile); // Left
                TryAddNeighbor(x - 1, y, x, y, tileArray, currentTile); // Up
                TryAddNeighbor(x + 1, y, x, y, tileArray, currentTile); // Down
    
                TryAddNeighbor(x - 1, y - 1, x, y, tileArray, currentTile); // Top-Left
                TryAddNeighbor(x + 1, y - 1, x, y, tileArray, currentTile); // Bottom-Left
                TryAddNeighbor(x - 1, y + 1, x, y, tileArray, currentTile); // Top-Right
                TryAddNeighbor(x + 1, y + 1, x, y, tileArray, currentTile); // Bottom-Right
            }
        }
    }

    private void TryAddNeighbor(int neighborX, int neighborY, int currentX, int currentY, Tile[,] tileArray, Tile currentTile)
    {
        if (neighborX >= 0 && neighborX < tileArray.GetLength(0) &&
            neighborY >= 0 && neighborY < tileArray.GetLength(1))
        {
            var neighborTile = tileArray[neighborX, neighborY];
            if (IsNeighbor(currentTile, neighborTile))
            {
                currentTile.AddNeighborTile(neighborTile);
            }
        }
    }

    private bool IsNeighbor(Tile currentTile, Tile neighborTile)
    {
        var currentPos = currentTile.GetGridPosition();
        var neighborPos = neighborTile.GetGridPosition();
    
        int deltaX = neighborPos.x - currentPos.x;
        int deltaY = neighborPos.y - currentPos.y;
    
        return (Math.Abs(deltaX) == 1 && deltaY == 0) || // Horizontal
               (Math.Abs(deltaY) == 1 && deltaX == 0) || // Vertical
               (Math.Abs(deltaX) == 1 && Math.Abs(deltaY) == 1); // Diagonal
    }
}

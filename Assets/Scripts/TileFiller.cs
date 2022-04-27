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

    private static void AssignNeighbors(int columns, int rows, Tile[,] tileArray)
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {   
                if(y < columns-1 && tileArray[x,y+1].GetGridPosition().x == tileArray[x, y].GetGridPosition().x)
                {
                    if (tileArray[x, y+1].GetGridPosition().y-1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x, y+1]);
                    }
                }
                if(y>0 && tileArray[x,y-1].GetGridPosition().x == tileArray[x, y].GetGridPosition().x)
                {
                    if (tileArray[x, y-1].GetGridPosition().y+1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x, y-1]);
                    }
                }
                if(x>0 && tileArray[x-1, y].GetGridPosition().y == tileArray[x, y].GetGridPosition().y)
                {
                    if (tileArray[x-1, y].GetGridPosition().x + 1 == tileArray[x, y].GetGridPosition().x)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x-1, y]);
                    }
                }
                if(x < rows-1 && tileArray[x+1, y].GetGridPosition().y == tileArray[x, y].GetGridPosition().y)
                {
                    if (tileArray[x+1, y].GetGridPosition().x - 1 == tileArray[x, y].GetGridPosition().x)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x+1, y]);
                    }
                }
                if(x>0 && y>0 && tileArray[x-1,y-1].GetGridPosition().x+1 == tileArray[x, y].GetGridPosition().x)
                {
                    if(tileArray[x - 1, y - 1].GetGridPosition().y + 1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x-1, y-1]);
                    }
                }
                if(y>0 && x<rows-1 && tileArray[x+1,y-1].GetGridPosition().x-1 == tileArray[x, y].GetGridPosition().x)
                {
                    if(tileArray[x + 1, y - 1].GetGridPosition().y + 1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x+1, y-1]);
                    }
                }
                if(x>0 && y<columns-1 && tileArray[x-1,y+1].GetGridPosition().x+1 == tileArray[x, y].GetGridPosition().x)
                {
                    if(tileArray[x - 1, y + 1].GetGridPosition().y - 1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x - 1, y + 1]);
                    }
                }
                if(y<columns-1 && x<rows-1 && tileArray[x+1,y+1].GetGridPosition().x-1 == tileArray[x, y].GetGridPosition().x)
                {
                    if(tileArray[x + 1, y + 1].GetGridPosition().y - 1 == tileArray[x, y].GetGridPosition().y)
                    {
                        tileArray[x, y].AddNeighborTile(tileArray[x + 1, y + 1]);
                    }
                }
            }
        }
    }
}

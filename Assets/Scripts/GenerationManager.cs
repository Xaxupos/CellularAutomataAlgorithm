using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public static GenerationManager Instance;
    public Tile[,] TileArray;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _stopButton;

    private List<Tile> _tilesToBeAlive = new List<Tile>();
    private List<Tile> _tilesToBeDead = new List<Tile>();

    private void Awake()
    {
        Instance = this;
    }

    public void StartGenerations()
    {
        _stopButton.SetActive(true);
        _startButton.SetActive(false);

        StartCoroutine(Generate());
    }

    public void StopAndClearBoard()
    {
        _stopButton.SetActive(false);
        _startButton.SetActive(true);

        StopAllCoroutines();

        for (int x = 0; x < 60; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                TileArray[x, y].SetTileState(TileState.Dead);
            }
        }
    }

    private IEnumerator Generate()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            _tilesToBeAlive.Clear();
            _tilesToBeDead.Clear();

            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    int deadNeighbors = 0;
                    int aliveNeighbors = 0;

                    foreach (var neighbor in TileArray[x, y].GetNeighbors())
                    {
                        if (neighbor.GetTileState() == TileState.Dead)
                        {
                            deadNeighbors++;
                        }
                        else
                        {
                            aliveNeighbors++;
                        }
                    }

                    if (TileArray[x, y].GetTileState() == TileState.Dead && aliveNeighbors == 3)
                    {
                        _tilesToBeAlive.Add(TileArray[x, y]);
                    }
                    if (TileArray[x, y].GetTileState() == TileState.Alive && (aliveNeighbors != 2 && aliveNeighbors != 3))
                    {
                        _tilesToBeDead.Add(TileArray[x, y]);
                    }
                }
            }

            foreach (var tile in _tilesToBeAlive)
            {
                tile.ChangeTileState();
            }
            foreach (var tile in _tilesToBeDead)
            {
                tile.ChangeTileState();
            }
        }  
    }
}

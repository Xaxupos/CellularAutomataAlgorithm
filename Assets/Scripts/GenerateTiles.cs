using UnityEngine;

public class GenerateTiles : MonoBehaviour
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

        int rows = (int)width / 10;
        int columns = (int)height / 10;

        for (int x = 0; x < rows * columns; x++)
        {
            var tile = Instantiate(_tilePrefab, this.transform);
        }
    }
}

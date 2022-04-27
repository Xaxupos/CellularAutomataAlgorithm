using UnityEngine;
using UnityEngine.EventSystems;

public class TileDetector : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var tile = GetComponent<Tile>();
        tile.ChangeTileState();
    }
}

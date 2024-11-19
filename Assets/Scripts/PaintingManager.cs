using UnityEngine;
using UnityEngine.EventSystems;

public class PaintingManager : MonoBehaviour
{
    public static PaintingManager Instance;
    public bool IsHolding = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        IsHolding = Input.GetMouseButton(0);
    }
}

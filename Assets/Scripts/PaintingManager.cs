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
        if(Input.GetMouseButtonDown(0))
        {
            IsHolding = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            IsHolding = false;
        }
    }

}

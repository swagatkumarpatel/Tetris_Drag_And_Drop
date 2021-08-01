using UnityEngine;

public class GragAndDrop : MonoBehaviour
{
    Vector3 MousePosition;
    Vector3 Offset;
    Vector3 StartingPosition;
    
    int Width = 7;
    int Height= 12;
    static Transform[,] grid = new Transform[7, 12];
    bool AddToArray;
    bool IsEmpty = true;

    void Start()
    {
        StartingPosition = transform.position;
    }
    private void OnMouseDown()
    {
        SpriteRenderer[] OrderLayer = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer OrderInLayerArray in OrderLayer)
        {
            OrderInLayerArray.sortingOrder = 1;
        }
        AddToArray = true;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        Offset = transform.position - MousePosition;
        if (CheckValidPosition())
        {
            foreach (Transform children in transform)
            {
                int Index_X = Mathf.FloorToInt(children.transform.position.x);
                int Index_Y = Mathf.FloorToInt(children.transform.position.y);
                grid[Index_X, Index_Y] = null;
            }
        }
    }
    private void OnMouseDrag()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePosition.z = 0;
        transform.position = MousePosition + Offset;
        if (CheckValidPosition())
        {
            transform.position = new Vector3(Mathf.RoundToInt(MousePosition.x + Offset.x), Mathf.RoundToInt(MousePosition.y + Offset.y), transform.position.z);
        }
    }
    private void OnMouseUp()
    {
        SpriteRenderer[] OrderLayer = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer OrderInLayerArray in OrderLayer)
        {
            OrderInLayerArray.sortingOrder = 0;
        }
        if (CheckValidPosition())
        {
            foreach (Transform children in transform)
            {
                int Index_X = Mathf.FloorToInt(children.transform.position.x);
                int Index_Y = Mathf.FloorToInt(children.transform.position.y);
                if (grid[Index_X, Index_Y] != null)
                {
                    AddToArray = false;
                    transform.position = StartingPosition;
                    Debug.Log("Index" + "[" + Index_X + "]" + "[" + Index_Y + "]"+" !=null ");
                    break;
                }
            }
            if (AddToArray)
            {
                foreach (Transform children in transform)
                {
                    int Index_X = Mathf.FloorToInt(children.transform.position.x);
                    int Index_Y = Mathf.FloorToInt(children.transform.position.y);
                    grid[Index_X, Index_Y] = children;
                    Debug.Log("Data Stored At Index" + "[" + Index_X + "]" + "[" + Index_Y + "]");

                }
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (grid[x, y] == null)
                        {
                            IsEmpty = false;
                            break;
                        }
                    }
                }
                if (IsEmpty)
                {
                    Debug.Log("Array is Full");
                }
            }
        }
        else
        {
            transform.position = StartingPosition;
        }
    }
    bool CheckValidPosition()
    {
        if (transform.GetChild(0).position.x > 0 && transform.GetChild(0).position.x < Width && transform.GetChild(0).position.y > 0 && transform.GetChild(0).position.y < Height &&
            transform.GetChild(1).position.x > 0 && transform.GetChild(1).position.x < Width && transform.GetChild(1).position.y > 0 && transform.GetChild(1).position.y < Height &&
            transform.GetChild(2).position.x > 0 && transform.GetChild(2).position.x < Width && transform.GetChild(2).position.y > 0 && transform.GetChild(2).position.y < Height &&
            transform.GetChild(3).position.x > 0 && transform.GetChild(3).position.x < Width && transform.GetChild(3).position.y > 0 && transform.GetChild(3).position.y < Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

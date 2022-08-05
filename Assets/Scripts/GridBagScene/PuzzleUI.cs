using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField] private GameObject gridItem;
    [SerializeField] private RectTransform gridParent;
    [SerializeField] private PuzzleOperate currentPuzzle;
    private GridItem[,] itemArray;
    private int height;
    private int width;
    private UnityAction<int, int> itemOnPointerEnter;
    private UnityAction<int, int> itemOnPointerExit;
    private UnityAction<int, int> itemOnDrop;
    private UnityAction operateItemBeginDrag;
    private UnityAction<PointerEventData> operateItemOnDrag;
    private UnityAction operateItemEndDrag;

    public void Init()
    {
        currentPuzzle.AddListener(OperateItemBeginDrag, OperateItemOnDrag, OperateItemEndDrag);
    }

    public void AddItemListener(UnityAction<int, int> _itemOnPointerEnter, UnityAction<int, int> _itemOnPointerExit, UnityAction<int, int> _itemOnDrop)
    {
        itemOnPointerEnter = _itemOnPointerEnter;
        itemOnPointerExit = _itemOnPointerExit;
        itemOnDrop = _itemOnDrop;
    }

    public void AddOperateItemBeginDrag(UnityAction _operateItemBeginDrag,UnityAction<PointerEventData> _operateItemOnDrag, UnityAction _operateItemEndDrag)
    {
        operateItemBeginDrag = _operateItemBeginDrag;
        operateItemOnDrag = _operateItemOnDrag;
        operateItemEndDrag = _operateItemEndDrag;
    }

    public void Create(int _height, int _width)
    {
        width = _width;
        height = _height;
        itemArray = new GridItem[width, height];
        gridParent.sizeDelta = new Vector2((50 * width) + (10 * (width - 1)), (50 * height) + (10 * (height - 1)));

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GridItem item = Instantiate(gridItem, gridParent).GetComponent<GridItem>();
                itemArray[x, y] = item;
                item.SetPosition(x, y);
                item.AddListener(ItemOnPointerEnter, ItemOnPointerExit, ItemOnDrop);
            }
        }
    }

    public void SetGridColor(Vector2 center, Vector2[] points, Color color)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 point = points[i];
            int x = (int)(center.x + point.x);
            int y = (int)(center.y + point.y);
            if (x < 0 || x >= width || y < 0 || y >= height)
                continue;
            itemArray[x, y].SetColor(color);
        }
    }

    public void SetOperateItemStyle(Vector2[] points)
    {
        currentPuzzle.SetStyle(points);
    }

    public void SetOperateItemRaycastTarget(bool state)
    {
        currentPuzzle.SetRaycastTarget(state);
    }

    private void ItemOnPointerEnter(int x, int y)
    {
        itemOnPointerEnter?.Invoke(x, y);
    }

    private void ItemOnPointerExit(int x, int y)
    {
        itemOnPointerExit?.Invoke(x, y);
    }

    private void ItemOnDrop(int x, int y)
    {
        itemOnDrop?.Invoke(x, y);
    }

    private void OperateItemBeginDrag()
    {
        operateItemBeginDrag?.Invoke();
    }

    private void OperateItemOnDrag(PointerEventData eventData)
    {
        operateItemOnDrag?.Invoke(eventData);
    }

    private void OperateItemEndDrag()
    {
        operateItemEndDrag?.Invoke();
    }
}

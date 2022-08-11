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
    private UnityAction operateItemBeginDrag;
    private UnityAction<PointerEventData> operateItemOnDrag;
    private UnityAction operateItemEndDrag;

    public void Init()
    {
        currentPuzzle.AddListener(OperateItemBeginDrag, OperateItemOnDrag, OperateItemEndDrag);
    }

    public void AddOperateItemListener(UnityAction _operateItemBeginDrag,UnityAction<PointerEventData> _operateItemOnDrag, UnityAction _operateItemEndDrag)
    {
        operateItemBeginDrag = _operateItemBeginDrag;
        operateItemOnDrag = _operateItemOnDrag;
        operateItemEndDrag = _operateItemEndDrag;
    }

    public void SetBoardPosition(Vector2 value)
    {
        gridParent.position = value;
    }

    public void SetBoardSize(int width,int height)
    {
        gridParent.sizeDelta = new Vector2(width, height);
    }

    public void Create(int row, int column)
    {
        itemArray = new GridItem[row, column];

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                GridItem item = Instantiate(gridItem, gridParent).GetComponent<GridItem>();
                itemArray[x, y] = item;
                item.SetPosition(x, y);
            }
        }
    }

    public void SetGridColor(GridPoint point, Color color)
    {
            itemArray[point.x, point.y].SetColor(color);

        //for (int i = 0; i < points.Length; i++)
        //{
        //    Vector2 point = points[i];
        //    int x = (int)(center.x + point.x);
        //    int y = (int)(center.y + point.y);
        //    if (x < 0 || x >= width || y < 0 || y >= height)
        //        continue;
        //    itemArray[x, y].SetColor(color);
        //}
    }

    public void SetOperateItemStyle(GridPoint[] points)
    {
        currentPuzzle.SetStyle(points);
    }

    public void SetOperateItemColor(in Color color)
    {
        currentPuzzle.SetColor(color);
    }

    public void SetOperateItemPosition(in Vector2 value)
    {
        currentPuzzle.SetPosition(value);
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

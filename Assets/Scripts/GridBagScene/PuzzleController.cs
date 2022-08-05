using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleController : MonoBehaviour
{
    private class GridData
    {
        public bool isUse = false;
    }

    private class PuzzlePiece
    {
        public PuzzlePiece(Vector2[] _points)
        {
            points = new Vector2[_points.Length];
            for (int i = 0; i < _points.Length; i++)
                points[i] = _points[i];
        }

        public Vector2[] points;
    }

    [SerializeField] private PuzzleUI puzzleUI;
    private int height;
    private int width;
    private GridData[,] gridDataArray;
    private List<PuzzlePiece> pieceList;
    private PuzzlePiece currentPiece;
    private bool isOperating;
    private bool currentState;
    private Color failedColor = Color.red;
    private Color successColor = Color.blue;
    private Color inUseColor = new Color(0.58f, 0.96f, 1, 1);

    private void Start()
    {
        Init();
        puzzleUI.Init();
        RandomPiece();
    }

    private void Init()
    {
        height = 5;
        width = 8;
        gridDataArray = new GridData[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
                gridDataArray[i, j] = new GridData();
        }

        CreatePuzzlePieces();

        puzzleUI.AddItemListener(OnItemPointerEnter, OnItemPointerExit, OnItemDrop);
        puzzleUI.AddOperateItemBeginDrag(OnOperateItemBeginDrag,OnOperateItemOnDrag, OnOperateItemEndDrag);
        puzzleUI.Create(height, width);

        isOperating = false;
        currentState = false;
    }

    private void CreatePuzzlePieces()
    {
        pieceList = new List<PuzzlePiece>();

        Vector2[] points = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(0, 3) };
        PuzzlePiece piece = new PuzzlePiece(points);
        pieceList.Add(piece);

        points = new Vector2[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
        piece = new PuzzlePiece(points);
        pieceList.Add(piece);

        points = new Vector2[] { new Vector2(0, 1), new Vector2(0, 2) };
        piece = new PuzzlePiece(points);
        pieceList.Add(piece);
    }

    private void RandomPiece()
    {
        currentPiece = pieceList[Random.Range(0, pieceList.Count)];
        puzzleUI.SetOperateItemStyle(currentPiece.points);
    }

    private void OnItemPointerEnter(int _x, int _y)
    {
        //if (!isOperating)
        //    return;

        //Vector2[] points = currentPiece.points;
        //bool isUse = false;
        //currentState = true;
        //for (int i = 0; i < points.Length; i++)
        //{
        //    Vector2 point = points[i];
        //    int x = (int)(_x + point.x);
        //    int y = (int)(_y + point.y);
        //    if (x < 0 || x >= width || y < 0 || y >= height || gridDataArray[x, y].isUse)
        //    {
        //        isUse = true;
        //        currentState = false;
        //        break;
        //    }
        //}

        //Color color = isUse ? failedColor : successColor;
        //puzzleUI.SetGridColor(new Vector2(_x, _y), points, color);
    }

    private void OnItemPointerExit(int _x, int _y)
    {
        //if (!isOperating)
        //    return;

        //puzzleUI.SetGridColor(new Vector2(_x, _y), currentPiece.points, Color.white);
    }

    private void OnItemDrop(int _x, int _y)
    {
        //if (!isOperating)
        //    return;

        //if (currentState)
        //{
        //    puzzleUI.SetGridColor(new Vector2(_x, _y), currentPiece.points, Color.blue);
        //    currentState = false;
        //}
    }

    private void OnOperateItemOnDrag(PointerEventData eventData)
    {
        Debug.Log("eventData:"+ eventData.position);
    }

    private void OnOperateItemBeginDrag()
    {
        //Debug.Log("OnOperateItemBeginDrag");
        isOperating = true;
        puzzleUI.SetOperateItemRaycastTarget(false);
    }

    private void OnOperateItemEndDrag()
    {
        //Debug.Log("OnOperateItemEndDrag");
        isOperating = false;
        puzzleUI.SetOperateItemRaycastTarget(true);
    }
}

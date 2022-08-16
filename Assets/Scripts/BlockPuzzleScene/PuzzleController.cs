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

    private class PuzzleShape
    {
        public PuzzleShape(GridPoint[] _points)
        {
            right = 0;
            up = 0;

            points = new GridPoint[_points.Length];
            for (int i = 0; i < _points.Length; i++)
            {
                GridPoint point = _points[i];
                points[i] = point;
                if (point.x > right)
                    right = point.x;
                if (point.y > up)
                    up = point.y;
            }
        }

        public GridPoint[] points;
        public int right;
        public int up;
    }

    private class PuzzleBoardData
    {
        public Vector2 boardPosition;
        public int cellSize;
        public int spacing;
        public int row;
        public int column;
        public int width;
        public int height;
        public float right;
        public float up;

        public PuzzleBoardData(Vector2 _boardPosition, int _cellSize, int _spacing, int _row, int _column)
        {
            boardPosition = _boardPosition;
            cellSize = _cellSize;
            spacing = _spacing;
            row = _row;
            column = _column;
            width = cellSize * row + spacing * (row - 1);
            height = cellSize * column + spacing * (column - 1);
            right = boardPosition.x + width;
            up = boardPosition.y + height;
            Debug.Log($"right:{right} up:{up}");
        }
    }

    private class OperateData
    {
        public Vector2 origPosition;
        public Vector2 operateOffset;
        public float right = 0;
        public float up = 0;
        public bool operateState = false;
        public List<GridPoint> points = new List<GridPoint>();

        public void AddPoint(int x, int y)
        {
            points.Add(new GridPoint(x, y));
        }

        public void ClearPoints()
        {
            points.Clear();
        }
    }

    [SerializeField] private PuzzleUI puzzleUI;
    private PuzzleBoardData boardData;
    private GridData[,] gridDataArray;
    private List<PuzzleShape> pieceList;
    private PuzzleShape currentPiece;
    private OperateData operateData;
    private readonly Color normalColor = Color.white;
    private readonly Color dragColor = new Color(1, 1, 1, 0.5f);
    private readonly Color failedColor = new Color(1, 0, 0, 0.5f);
    private readonly Color successColor = Color.blue;
    private readonly Color inUseColor = new Color(0, 0.42f, 1, 1);

    private void Start()
    {
        Init();
        puzzleUI.Init();
        RandomPiece();
    }

    private void Init()
    {
        boardData = new PuzzleBoardData(_boardPosition: new Vector2(750, 400), _cellSize: 50, _spacing: 10, _row: 3, _column: 4);
        operateData = new OperateData();
        operateData.origPosition = new Vector2(244, 540);
        operateData.operateOffset = new Vector2(boardData.cellSize * 0.5f, boardData.cellSize * 0.5f);

        puzzleUI.SetBoardPosition(boardData.boardPosition);
        puzzleUI.SetBoardSize(boardData.width, boardData.height);

        gridDataArray = new GridData[boardData.row, boardData.column];
        for (int i = 0; i < boardData.row; i++)
        {
            for (int j = 0; j < boardData.column; j++)
                gridDataArray[i, j] = new GridData();
        }

        CreatePuzzlePieces();

        puzzleUI.AddOperateItemListener(OnOperateItemBeginDrag, OnOperateItemOnDrag, OnOperateItemEndDrag);
        puzzleUI.Create(boardData.row, boardData.column);

        operateData.operateState = false;
    }

    /// <summary>
    /// 創建拼圖片
    /// </summary>
    private void CreatePuzzlePieces()
    {
        pieceList = new List<PuzzleShape>();

        GridPoint[] points = new GridPoint[] { new GridPoint(0, 0), new GridPoint(0, 1), new GridPoint(0, 2), new GridPoint(0, 3) };
        PuzzleShape piece = new PuzzleShape(points);
        pieceList.Add(piece);

        points = new GridPoint[] { new GridPoint(0, 0), new GridPoint(0, 1), new GridPoint(1, 0), new GridPoint(1, 1) };
        piece = new PuzzleShape(points);
        pieceList.Add(piece);

        points = new GridPoint[] { new GridPoint(0, 0), new GridPoint(0, 1) };
        piece = new PuzzleShape(points);
        pieceList.Add(piece);
    }

    private void RandomPiece()
    {
        currentPiece = pieceList[Random.Range(0, pieceList.Count)];
        puzzleUI.SetOperateItemStyle(currentPiece.points);
    }

    private void OnOperateItemOnDrag(PointerEventData eventData)
    {
        if (operateData.operateState)
        {
            for (int i = 0; i < operateData.points.Count; i++)
                puzzleUI.SetGridColor(operateData.points[i], normalColor);
        }

        float mouseX = eventData.position.x;
        float mouseY = eventData.position.y;
        puzzleUI.SetOperateItemPosition(new Vector2(mouseX + operateData.operateOffset.x, mouseY + operateData.operateOffset.y));

        if (mouseX + boardData.cellSize < boardData.boardPosition.x ||
            mouseY + boardData.cellSize < boardData.boardPosition.y ||
            mouseX + operateData.right >= boardData.right ||
            mouseY + operateData.up >= boardData.up)
        {
            puzzleUI.SetOperateItemColor(dragColor);
            operateData.operateState = false;
            return;
        }

        //底下在判斷滑鼠在網格區內的哪個位置
        mouseX -= boardData.boardPosition.x;
        mouseY -= boardData.boardPosition.y;
        Debug.Log($"mouse:{mouseX}, {mouseY}");

        int row = (int)(mouseX / (boardData.cellSize + boardData.spacing));
        int column = (int)(mouseY / (boardData.cellSize + boardData.spacing));
        if (row < 0)
            row = 0;
        if (column < 0)
            column = 0;

        Debug.Log($"滑鼠目前的網格位置:[{row}, {column}]");

        operateData.operateState = true;
        operateData.ClearPoints();
        for (int i = 0; i < currentPiece.points.Length; i++)
        {
            GridPoint point = currentPiece.points[i];
            int x = row + point.x;
            int y = column + point.y;
            if (gridDataArray[x, y].isUse)
                operateData.operateState = false;

            operateData.AddPoint(x, y);
        }

        if (operateData.operateState)
        {
            puzzleUI.SetOperateItemColor(dragColor);
            for (int i = 0; i < operateData.points.Count; i++)
                puzzleUI.SetGridColor(operateData.points[i], successColor);
        }
        else
        {
            puzzleUI.SetOperateItemColor(failedColor);
        }
    }

    private void OnOperateItemBeginDrag()
    {
        operateData.right = (boardData.cellSize * currentPiece.right) + (boardData.spacing * (currentPiece.right - 1));
        operateData.up = (boardData.cellSize * currentPiece.up) + (boardData.spacing * (currentPiece.up - 1));
    }

    private void OnOperateItemEndDrag()
    {
        if (operateData.operateState)
        {
            for (int i = 0; i < operateData.points.Count; i++)
            {
                GridPoint point = operateData.points[i];
                puzzleUI.SetGridColor(point, inUseColor);
                gridDataArray[point.x, point.y].isUse = true;
            }

            RandomPiece();

            if (IsFinished())
            {
                Debug.Log("恭喜你完成了");
            }
        }

        puzzleUI.SetOperateItemPosition(operateData.origPosition);
        puzzleUI.SetOperateItemColor(normalColor);

        operateData.ClearPoints();
    }

    private bool IsFinished()
    {
        for (int i = 0; i < boardData.row; i++)
        {
            for (int j = 0; j < boardData.column; j++)
                if (!gridDataArray[i, j].isUse)
                    return false;
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RandomPiece();
    }
}

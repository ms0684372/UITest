using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GridItem : MonoBehaviour
{
    [SerializeField] Image image;
    private int x;
    private int y;
    private UnityAction<int, int> onPointerEnter;
    private UnityAction<int, int> onPointerExit;
    private UnityAction<int, int> onDrop;

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
        name = x + "_" + y;
    }
}

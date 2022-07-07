using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public ItemData(string _name, Sprite _sprite, int _count)
    {
        name = _name;
        sprite = _sprite;
        count = _count;
    }

    private string name;
    private Sprite sprite;
    private int count;

    public string GetName()
    {
        return name;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public int GetCount()
    {
        return count;
    }
}
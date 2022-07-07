using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    private Inventory inventory;

    [SerializeField] private BagController bagController;
    [SerializeField] private Sprite[] sprites;

    void Start()
    {
        inventory = Inventory.GetInstance();
        inventory.Init();
        for (int i = 0; i < 3; i++)
            inventory.AddItem(new ItemData("item" + 1, sprites[i], 1));

        bagController.Init();
    }
}

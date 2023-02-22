using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuManager : MonoBehaviour
{
    public bool buildMode = false;
    public GameObject[] buildNet = new GameObject[0];
    public Canvas downMenu;
    public Canvas inventory;
    public Blueprint arrays;
    public Building building;
    private InventoryUI invCode;
    private ScreenRes sr;
    private InventoryUI IU;
    private SortLvlTier slt;
    public void startBuild(bool x) //t - enable inventory / f - disable inventory
    {
        buildMode = !buildMode;
        downMenu.enabled = !x;//вимкнути нижнє меню
        inventory.enabled = x;//ввімкнути відображення предметів
        for (int i = 0; i < buildNet.Length; i++)
        {
            buildNet[i].SetActive(x);
            arrays.cells[i].GetComponent<BoxCollider2D>().enabled = x;
        }
        for (int i = 0; i < arrays.invSlots.Length; i++)
        {
            if (arrays.empty[i])
            {
                arrays.invSlots[i].SetActive(x);
                arrays.img[i].SetActive(x);
            }
        }
    }

    public GameObject inventory2;
    public bool enabledInv = false;
    public RectTransform panelRT;

    public void OpenInventory()
    {
        enabledInv = !enabledInv;
        downMenu.enabled = !downMenu.enabled;
        inventory2.SetActive(!inventory2.activeSelf);
        invCode.Sorting(3);
        sr.SetActiveColiders();
        IU.currentSortType = 3;
        slt.SortLvl();
        slt.SortTier();
}

    private void Update()
    {
        if (!buildMode) for (int i = 0; i < arrays.invSlots.Length; i++) { arrays.invSlots[i].SetActive(false); }
    }
    private void Start()
    {
        sr = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
        invCode = GameObject.Find("Main Camera").GetComponent<InventoryUI>();
        IU = GameObject.Find("Main Camera").GetComponent<InventoryUI>();
        slt = GameObject.Find("Main Camera").GetComponent<SortLvlTier>();
        Array.Resize(ref buildNet, arrays.cells.Length);
        for (int i = 0; i < buildNet.Length; i++)
        {
            buildNet[i] = arrays.cells[i].transform.GetChild(0).gameObject;
        }
    } 
}

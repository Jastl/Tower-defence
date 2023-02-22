using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUI : MonoBehaviour
{
    public ScreenRes sr;
    public Blueprint blueprint;
    public Camera cam;

    public GameObject invPanel;
    public RectTransform invPanelRT;
    public int currentSortType = 0;
    public int currentSortData = 0;

    private Transform slot; 
    private Vector3 posClick;
    private bool pressed = false;
    public float delta;
    private void Start()
    {
        cam = Camera.main;
        invPanelRT = invPanel.gameObject.GetComponent<RectTransform>();
        sr = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
    }

    public void OnClick(GameObject pressedSlot) //finding slot
    {
        for (int i = 0; i < blueprint.inv2Slots.Length; i++)
        {
            if (blueprint.inv2Slots[i] == pressedSlot)
            {
                posClick = Input.mousePosition;
                slot = pressedSlot.transform;
                pressed = true;

                delta = invPanel.transform.position.y - cam.ScreenToWorldPoint(posClick).y; //difference of panel's and slot's positions
                break;
            }
        }
    }
    public void Sorting(int typeOfItem) //sorting items in inventory
    {
        for (int i = 0; i < blueprint.invId.Length; i++)
        {
            blueprint.inv2Slots[i].SetActive(false);
            if (typeOfItem == 0 && blueprint.items[blueprint.invId[i]].type == 0) blueprint.inv2Slots[i].SetActive(true);
            if (typeOfItem == 1 && blueprint.items[blueprint.invId[i]].type == 1) blueprint.inv2Slots[i].SetActive(true);
            if (typeOfItem == 2 && blueprint.items[blueprint.invId[i]].type == 2) blueprint.inv2Slots[i].SetActive(true);
            if (typeOfItem == 3) blueprint.inv2Slots[i].SetActive(true);
            if (blueprint.empty[i] == false) blueprint.inv2Slots[i].SetActive(false);
            SetHeightIventory();
            currentSortType = typeOfItem;
        }
    }
    public void Sorting(int typeOfItem/*тип айтемів*/, string type/*"lvl" "tier"*/, int side/*нпарям сортування 0-1..0   1-0..1*/)
    {
        Sorting(typeOfItem);
        GameObject[] slots = new GameObject[0];
        Array.Resize(ref slots, blueprint.inv2Slots.Length);
        for (int i = 0; i < slots.Length; i++) slots[i] = blueprint.inv2Slots[i];
        int[] lvl = new int[0];
        Array.Resize(ref lvl, blueprint.lvl.Length);
        if (type == "lvl") for (int i = 0; i < lvl.Length; i++) lvl[i] = blueprint.lvl[i];
        else for (int i = 0; i < lvl.Length; i++) lvl[i] = blueprint.tier[i];
        for (int i = 0; i < lvl.Length; i++)//bubble sort
        {
            for (int j = 0; j < lvl.Length - 1; j++)
            {
                if (lvl[j] > lvl[j + 1])
                {
                    int z = lvl[j];
                    GameObject x = slots[j];
                    lvl[j] = lvl[j + 1];
                    slots[j] = slots[j + 1];
                    lvl[j + 1] = z;
                    slots[j + 1] = x;
                }
            }
        }
        if (side == 1) //напрям сортування
        {
            Array.Reverse(slots);
            Array.Reverse(lvl);
        }
        for (int i = 0; i < lvl.Length; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }

    }
    public void SetHeightIventory()
    {
        int countOfSlots = 0;
        for (int i = 0; i < blueprint.inv2Slots.Length; i++) if (blueprint.inv2Slots[i].activeSelf == true) countOfSlots++;
        invPanelRT.sizeDelta = new Vector2(invPanelRT.sizeDelta.x, (float)sr.SizeW(Math.Ceiling((double)(countOfSlots / 5f)) * sr.widthSlot2 + countOfSlots / 5 * 10 + 30));//size of inventory //не заокруглює
        if (invPanelRT.sizeDelta.y < sr.SizeH(1770)) invPanelRT.sizeDelta = new Vector2(invPanelRT.sizeDelta.x, sr.SizeH(1770));
        invPanelRT.localPosition = new Vector2(invPanelRT.localPosition.x, (float)(invPanelRT.sizeDelta.y / -2 + sr.heightScreen / 2 - sr.SizeH(150)));
    }
    public bool move = false; //player begin move
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && pressed)
        {
            pressed = false;
            move = false;
        }
        if (pressed)
        {
            float mousePosY = Input.mousePosition.y;
            if (mousePosY > posClick.y + sr.SizeW(20) || mousePosY < posClick.y - sr.SizeW(20)) move = true;
            if (move)
            {
                //move
                invPanel.transform.position = new Vector2(0, cam.ScreenToWorldPoint(Input.mousePosition).y + delta);
                invPanelRT.localPosition = new Vector3(invPanelRT.localPosition.x, invPanelRT.localPosition.y, 0);
                //bound
                float posY = cam.WorldToScreenPoint(invPanelRT.position).y;
                float height = invPanelRT.sizeDelta.y;

                if (posY > height / 2) invPanelRT.localPosition = new Vector2(invPanelRT.localPosition.x, (height - sr.heightScreen) / 2);
                if (posY < height / -2 + sr.heightScreen - sr.SizeH(150)) invPanelRT.localPosition = new Vector2(invPanelRT.localPosition.x, height / -2 + sr.heightScreen / 2 - sr.SizeH(150));
                invPanelRT.localPosition = new Vector3(invPanelRT.localPosition.x, invPanelRT.localPosition.y, 0);
            }
        }
    }
}


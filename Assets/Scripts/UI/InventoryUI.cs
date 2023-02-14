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

    public void OnClick(GameObject pressedSlot)
    {
        for (int i = 0; i < blueprint.inv2Slots.Length; i++)
        {
            if (blueprint.inv2Slots[i] == pressedSlot)
            {
                posClick = Input.mousePosition;
                slot = pressedSlot.transform;
                pressed = true;

                delta = invPanel.transform.position.y - cam.ScreenToWorldPoint(posClick).y;
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
            }
    }
    public void SetHeightIventory()
    {
        int countOfSlots = 0;
        for (int i = 0; i < blueprint.inv2Slots.Length; i++) if (blueprint.inv2Slots[i].activeSelf == true) countOfSlots++;
        invPanelRT.sizeDelta = new Vector2((float)sr.SizeW(5 * sr.widthSlot2 + 80), (float)sr.SizeW(Math.Ceiling((double)(countOfSlots / 5f)) * sr.widthSlot2 + countOfSlots / 5 * 10 + 30));//size of inventory //не заокруглює
        if (invPanelRT.sizeDelta.y < 1480) invPanelRT.sizeDelta = new Vector2(invPanelRT.sizeDelta.x, 1480);
        invPanelRT.position = new Vector2(invPanelRT.position.x, cam.ScreenToWorldPoint(new Vector2((float)(invPanelRT.sizeDelta.y / -2 + sr.heightScreen - 862.5), 0)).x);
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

                if (posY > height / 2) invPanelRT.position = new Vector2(invPanelRT.position.x, cam.ScreenToWorldPoint(new Vector2 (height / 2 - 420, 0)).x); //костиль
                if (posY < height / -2 + sr.heightScreen - 440) invPanelRT.position = new Vector2(invPanelRT.position.x, cam.ScreenToWorldPoint(new Vector2 (height / -2 + 1060, 0)).x); //костиль
                invPanelRT.localPosition = new Vector3(invPanelRT.localPosition.x, invPanelRT.localPosition.y, 0);
            }
        }
    }
}

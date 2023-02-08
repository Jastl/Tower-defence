using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (mousePosY > posClick.y + 20f / 1080 * sr.widthScreen || mousePosY < posClick.y - 20f / 1080 * sr.widthScreen) move = true;
            if (move)
            {
                //move
                invPanel.transform.position = new Vector2(0, cam.ScreenToWorldPoint(Input.mousePosition).y + delta);
                invPanelRT.localPosition = new Vector3(invPanelRT.localPosition.x, invPanelRT.localPosition.y, 0);
                //bound
                float posY = cam.ScreenToWorldPoint(invPanelRT.position).y;
                if (posY > invPanelRT.sizeDelta.y / 1920 * sr.heightScreen / 2) invPanelRT.position = new Vector2(invPanelRT.position.x, cam.ScreenToWorldPoint(new Vector2 (invPanelRT.sizeDelta.y / 1920 * sr.heightScreen / 2, 0)).x);
                if (posY > invPanelRT.sizeDelta.y / 1920 * sr.heightScreen / -2 + sr.heightScreen) invPanelRT.position = new Vector2(invPanelRT.position.x, cam.ScreenToWorldPoint(new Vector2 (invPanelRT.sizeDelta.y / 1920 * sr.heightScreen / -2 + sr.heightScreen, 0)).x);
                invPanelRT.localPosition = new Vector3(invPanelRT.localPosition.x, invPanelRT.localPosition.y, 0);
            }
        }
    }
}

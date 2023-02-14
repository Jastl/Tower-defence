using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoveSlotOrInv : MonoBehaviour
{
    public Blueprint blueprint;
    private int nps; //nummber of pressed slot
    private GameObject gops; //game object of pressed slot
    private bool pressed = false;
    private Vector3 mousePos; //position of mouse
    public bool verticalyMovement = false;//рух по вертикалі?
    public float timer = 0f;//таймер
    public GameObject panel;//панель з слотами
    private RectTransform panelRT;
    private float delta;//різниця х координат курсора і панелі
    private GameObject spiritOfBuild = null;
    public Building building;
    public ScreenRes sr;
    private bool onCell = false;
    private int countCell;
    public GameObject invMenu;
    private Camera cam;
    private void Start()
    {
        panelRT = panel.GetComponent<RectTransform>();
        cam = Camera.main;
    }
    private bool build = false;
    private void Update()
    {
        mousePos = Input.mousePosition;//позиція курсору(screenPoint)
        if (Input.GetMouseButtonUp(0) && pressed)//відміна
        {
            if (onCell) building.Buildingg(nps, countCell);
            pressed = false;
            verticalyMovement = false;
            timer = 0f;
            build = false;
            Destroy(spiritOfBuild);
            spiritOfBuild = null;
            onCell = false;
            building.MoveInv(true, true);
        }
        if (pressed) //moving slot's on verticaly
        {
            StartCoroutine(timeOfPress());//запуск таймера
            if (cam.WorldToScreenPoint(gops.GetComponent<RectTransform>().transform.position).y +
            gops.GetComponent<RectTransform>().sizeDelta.y / 2 < mousePos.y || gops.GetComponent<RectTransform>().transform.position.y -
              gops.GetComponent<RectTransform>().sizeDelta.y / 2 > mousePos.y) build = true;//якщо обєкт вийшов за верхню/нижню межу слота
            if (build)
            {
                building.MoveInv(false, false);
                if (spiritOfBuild != null)
                {
                    spiritOfBuild.transform.position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, cam.ScreenToWorldPoint(mousePos).y, 0);
                }
                else spiritOfBuild = Instantiate(blueprint.items[blueprint.invId[nps]].image);
                verticalyMovement = true;
                float hwoc = (float)sr.widthCell / 2;
                Vector3 posOfCell; //position cell in screen point
                for (int i = 0; i < blueprint.cells.Length; i++)
                {
                    posOfCell = cam.WorldToScreenPoint(blueprint.cells[i].transform.position);
                    if (mousePos.x < posOfCell.x + hwoc && mousePos.x > posOfCell.x - hwoc &&
                        mousePos.y < posOfCell.y + hwoc && mousePos.y > posOfCell.y - hwoc)
                    {
                        spiritOfBuild.transform.position = new Vector3(blueprint.cells[i].transform.position.x, blueprint.cells[i].transform.position.y, 0);
                        countCell = i;
                        onCell = true;
                        break;
                    }
                }
            }
            if (!verticalyMovement && timer == 250) delta = panel.transform.position.x - cam.ScreenToWorldPoint(mousePos).x;
            if (!verticalyMovement && timer > 250)//рух по горизонталі
            {
                panel.transform.position = new Vector3(cam.ScreenToWorldPoint(mousePos).x + delta, panel.transform.position.y,
                    panel.transform.position.z);
                delta = panel.transform.position.x - cam.ScreenToWorldPoint(mousePos).x;
            }
        }
        float posX = cam.WorldToScreenPoint(panelRT.position).x;
        if (posX > panelRT.sizeDelta.x / 2)
            panelRT.position = new Vector2(cam.ScreenToWorldPoint(new Vector2(panelRT.sizeDelta.x / 2, 0)).x,
                panelRT.position.y);
        if (posX < panelRT.sizeDelta.x / -2 + sr.widthScreen)
            panelRT.position = new Vector2(cam.ScreenToWorldPoint(new Vector2(panelRT.sizeDelta.x / -2 + (float)sr.widthScreen, 0)).x,
                panelRT.position.y);
        panelRT.localPosition = new Vector3(panelRT.localPosition.x, panelRT.localPosition.y, 0);

        //change dSize of panel
        float wc = panel.GetComponent<GridLayoutGroup>().cellSize.x; //width of cell
        float pc = panel.GetComponent<GridLayoutGroup>().spacing.x; //padding of cells
        ushort count = 0;
        foreach (bool a in blueprint.empty)
        {
            if (a) count++;
        } 
        if (count % 2 == 0) panelRT.sizeDelta = new Vector2(wc * (count / 2) + pc * (count / 2) + pc, panelRT.sizeDelta.y); //change width of inv panel
        else panelRT.sizeDelta = new Vector2(wc * (count / 2) + pc * (count / 2) + 2 * pc + wc, panelRT.sizeDelta.y);
        if (panelRT.sizeDelta.x < sr.widthScreen) panelRT.sizeDelta = new Vector2((float)sr.widthScreen, panelRT.sizeDelta.y); //minimal width of inv panel
    }
    public void OnClick(GameObject go)//*натиск*
    {
        for (int i = 0; i < blueprint.invSlots.Length; i++)//знаходження натиснутого обєкту
        {
            if (blueprint.invSlots[i] == go)
            {
                nps = i;
                gops = blueprint.invSlots[i];
                pressed = true;
                break;
            }
        }
    }
    IEnumerator timeOfPress()//таймер
    {
        yield return new WaitForSeconds(0.01f);
        timer = timer + 10f;
    }
}

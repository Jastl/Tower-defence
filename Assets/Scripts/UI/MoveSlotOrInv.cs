using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveSlotOrInv : MonoBehaviour
{
    public Blueprint blueprint;
    public Building building;
    public ScreenRes sr;
    private Camera cam;

    //initialized in engine
    public GameObject panel;//панель з слотами
    private GridLayoutGroup panelGLG;
    private RectTransform panelRT;
    public GameObject invMenu;

    private GameObject gops; //game object of pressed slot
    private GameObject spiritOfBuild = null;
    private Vector3 mousePos; //position of mouse
    private int nps; //nummber of pressed slot
    private int countCell;
    private float timer = 0f;//таймер
    private float delta;//різниця х координат курсора і панелі
    private bool pressed = false;
    private bool verticalyMovement = false;//рух по вертикалі?    
    private bool build = false;
    private bool onCell = false;

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
            if (cam.WorldToScreenPoint(gops.GetComponent<RectTransform>().transform.position).y +
            gops.GetComponent<RectTransform>().sizeDelta.y / 2 < mousePos.y || gops.GetComponent<RectTransform>().transform.position.y -
              gops.GetComponent<RectTransform>().sizeDelta.y / 2 > mousePos.y) build = true;//якщо обєкт вийшов за верхню/нижню межу слота
            if (build)
            {
                building.MoveInv(false, false);
                if (spiritOfBuild != null)
                {
                    spiritOfBuild.transform.position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, cam.ScreenToWorldPoint(mousePos).y, 0);
                    spiritOfBuild.transform.SetParent(invMenu.transform);
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
        if (posX > panelRT.sizeDelta.x / 2) panelRT.position = new Vector2(cam.ScreenToWorldPoint(new Vector2(panelRT.sizeDelta.x / 2, 0)).x, panelRT.position.y);
        if (posX < panelRT.sizeDelta.x / -2 + sr.widthScreen) panelRT.position = new Vector2(cam.ScreenToWorldPoint(new Vector2(panelRT.sizeDelta.x / -2 + (float)sr.widthScreen, 0)).x, panelRT.position.y);
        panelRT.localPosition = new Vector3(panelRT.localPosition.x, panelRT.localPosition.y, 0);


        //change dSize of panel
        float wc = panelGLG.cellSize.x; //width of cell
        float pc = panelGLG.GetComponent<GridLayoutGroup>().spacing.x; //padding of cells
        ushort count = 0;
        foreach (bool a in blueprint.empty)
        {
            if (a) count++;
        } 
        if (count % 2 == 0) panelRT.sizeDelta = new Vector2(wc * (count / 2) + pc * (count / 2) + pc, panelRT.sizeDelta.y); //change width of inv panel
        else panelRT.sizeDelta = new Vector2(wc * (count / 2) + pc * (count / 2) + 2 * pc + wc, panelRT.sizeDelta.y);
        if (panelRT.sizeDelta.x < sr.widthScreen) panelRT.sizeDelta = new Vector2((float)sr.widthScreen, panelRT.sizeDelta.y); //minimal width of inv panel
    }


    IEnumerator timeOfPress()//таймер
    {
        yield return new WaitForSeconds(0.01f);
        timer = timer + 10f;
    }
    private void FixedUpdate()
    {
        StartCoroutine(timeOfPress());//запуск таймера
    }
    private void Start()
    {
        panelGLG = panel.GetComponent<GridLayoutGroup>();
        panelRT = panel.GetComponent<RectTransform>();
        cam = Camera.main;
    }
}

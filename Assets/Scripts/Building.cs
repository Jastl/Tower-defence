using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Blueprint blueprint;
    public ScreenRes sr;

    public GameObject buildingMassive;
    public GameObject panel; //inventory panel
    public GameObject invMenu;
    public GameObject moverMenu; //button which turn on/off p_inventory
    public bool deleteMod = false;
    private bool upInv = true;
    public void Buildingg(int num, int numC)
    {
        CellData cd0 = blueprint.cells[numC].GetComponent<CellData>();
        if (!cd0.busy) //if the cell isn't busy
        {
            cd0.numberOfSlot = num;
            cd0.id = blueprint.invId[num];
            cd0.lvl = blueprint.lvl[num];
            cd0.busy = true;
            cd0.hp = blueprint.items[blueprint.invId[num]].healthPoints;
            blueprint.empty[num] = false;
        }
    }
    private void Update()
    {
        for (int i = 0; i < blueprint.cells.Length; i++)
        {
            CellData cd = blueprint.cells[i].GetComponent<CellData>();
            if (cd.busy && cd.build == null)
            {
                cd.build = Instantiate(blueprint.items[cd.id].prefab);
                cd.build.transform.position = blueprint.cells[i].transform.position;
                cd.build.transform.position += new Vector3(0, 0, 1); 
                cd.build.name = i.ToString();
                cd.build.transform.SetParent(buildingMassive.transform);
            }
        }
        invMenu.GetComponent<RectTransform>().localPosition = new Vector3(invMenu.GetComponent<RectTransform>().localPosition.x,
            invMenu.GetComponent<RectTransform>().localPosition.y, 0);
    }
    public void ChangeDelMod()
    {
        if (deleteMod)
        {
            deleteMod = false;
            MoveInv(true, true);
        }
        else
        {
            deleteMod = true;
            MoveInv(false, false);
        }
    }
    public void MoveInv2()
    {
        if (upInv)
        {
            MoveInv(false, true);
            moverMenu.transform.rotation = new Quaternion(0, 0, 180, 0);
        }
        else
        {
            MoveInv(true, true);
            moverMenu.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public void DeleteBuild(GameObject go)
    {
        int num = 0;
            for (int i = 0; i < blueprint.cells.Length; i++)
            {
                if (blueprint.cells[i] == go)
                {
                    num = i;
                    break;
                }
            }
            CellData cd = blueprint.cells[num].GetComponent<CellData>();
            cd.id = 0;
            cd.busy = false;
            Destroy(cd.build);
            cd.build = null;
            cd.lvl = 0;
            blueprint.empty[cd.numberOfSlot] = true;
            cd.numberOfSlot = 0;
    }
    public void MoveInv(bool x, bool y)
    {
        upInv = x;
        if (x)
        {
            invMenu.transform.position = Camera.main.ScreenToWorldPoint(new Vector2((float)sr.SizeW(540), (float)sr.SizeH(550.8)));
        }
        else
        {
            invMenu.transform.position = Camera.main.ScreenToWorldPoint(new Vector2((float)sr.SizeW(540), (float)sr.SizeH(105.8)));
        }
        panel.SetActive(x);
        moverMenu.SetActive(y);
    }
}

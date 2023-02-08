using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public int idCell;
    private Blueprint blueprint;

    public float damage;

    public float damageB = 1;
    public float speedOfBullet = 1;
    public float speedOfReload = 1;
    public float distance = 1;

    public float health = 1;
    public float healthRecovery = 0;


    public float damageH;
    public float speedOfBulletH;
    public float speedOfReloadH;
    public float distanceH;
    public float healthH;
    public float healthRecoveryH;
    private void Start()
    {
        blueprint = GameObject.Find("Main Camera").GetComponent<Blueprint>();
    }

    private void Update()
    {
        Buffer[] cells = new Buffer[4];
        if (idCell % 7 != 0) 
        { 
            if (blueprint.cells[idCell - 1].GetComponent<CellData>().build != null) cells[0] = blueprint.cells[idCell - 1].GetComponent<CellData>().build.GetComponent<Buffer>();
            else cells[0] = this.gameObject.GetComponent<Buffer>();
        }
        else cells[0] = this.gameObject.GetComponent<Buffer>();
        if (idCell % 7 != 6) 
        { 
            if (blueprint.cells[idCell + 1].GetComponent<CellData>().build != null) cells[1] = blueprint.cells[idCell + 1].GetComponent<CellData>().build.GetComponent<Buffer>();
            else cells[1] = this.gameObject.GetComponent<Buffer>();
        }
        else cells[1] = this.gameObject.GetComponent<Buffer>();
        if (idCell > 6) 
        { 
            if (blueprint.cells[idCell - 7].GetComponent<CellData>().build != null) cells[2] = blueprint.cells[idCell - 7].GetComponent<CellData>().build.GetComponent<Buffer>();
            else cells[2] = this.gameObject.GetComponent<Buffer>();
        }
        else cells[2] = this.gameObject.GetComponent<Buffer>();
        if (idCell < 63) 
        { 
            if (blueprint.cells[idCell + 7].GetComponent<CellData>().build != null) cells[3] = blueprint.cells[idCell + 7].GetComponent<CellData>().build.GetComponent<Buffer>();
            else cells[3] = this.gameObject.GetComponent<Buffer>();
        }
        else cells[3] = this.gameObject.GetComponent<Buffer>();

        damageB = 1 + cells[0].damageH + cells[1].damageH + cells[2].damageH + cells[3].damageH;
        speedOfBullet = 1 + cells[0].speedOfBulletH + cells[1].speedOfBulletH + cells[2].speedOfBulletH + cells[3].speedOfBulletH;
        speedOfReload = 1 - cells[0].speedOfReloadH - cells[1].speedOfReloadH - cells[2].speedOfReloadH - cells[3].speedOfReloadH;
        distance = 1 + cells[0].distanceH + cells[1].distanceH + cells[2].distanceH + cells[3].distanceH;
        health = 1 + cells[0].healthH + cells[1].healthH + cells[2].healthH + cells[3].healthH;
        healthRecovery = cells[0].healthRecoveryH + cells[1].healthRecoveryH + cells[2].healthRecoveryH + cells[3].healthRecoveryH;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellData : MonoBehaviour
{
    public bool busy = false;
    public int id = 0; //id of obj in the cell
    public int idOfCell;
    public int lvl = 0; //lvl of obj in the cell
    public int numberOfSlot = 0; //number of obj in inv
    public GameObject build = null;
    private Building building;
    public float damage = 10;
    public float hp;
    public float maxHP;

    private void Start()
    {
        building = GameObject.Find("Main Camera").GetComponent<Building>();
    }
    private void Update()
    {
        damage = 10;

        if (build != null) 
        {
            Buffer a = build.GetComponent<Buffer>();
            a.damage = damage;
            a.idCell = idOfCell;
        }
    }
    public void TakeDamage(float dmg)
    {
        hp = hp - dmg;
        if (hp <= 0) building.DeleteBuild(this.gameObject);
    }
    private void OnMouseDown()
    {
        if (building.deleteMod) building.DeleteBuild(this.gameObject);
    }
}

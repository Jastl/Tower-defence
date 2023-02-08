using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    private Buffer buffer;
    private ushort idCell;
    private Blueprint blueprint;
    private Transform tran;
    public int timer = 0;
    private Quaternion corner;

    //settings
    private float distanse;
    private float damage;
    public float speedOfReload;
    private float speedOfBullet;
    //improvments
    private float distanceB;
    private float damageB;
    public float speedOfReloadB;
    private float speedOfBulletB;
    private void Start()
    {
        blueprint = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        buffer = this.gameObject.GetComponent<Buffer>();
        tran = transform;
    }
    private void Update()
    {
        int idOfBuild = blueprint.cells[idCell].GetComponent<CellData>().id;

        distanse = blueprint.items[idOfBuild].distance;
        speedOfReload = blueprint.items[idOfBuild].timeReloadShot;
        speedOfBullet = blueprint.items[idOfBuild].speedOfBullet;
        damage = buffer.damage;

        distanceB = buffer.distance;
        speedOfReloadB = buffer.speedOfReload;
        speedOfBulletB = buffer.speedOfBullet;
        damageB = buffer.damageB;

        idCell = (ushort)buffer.idCell;

        foreach(GameObject a in GameObject.FindGameObjectsWithTag("Enemy"))
        {

        }


    }
}

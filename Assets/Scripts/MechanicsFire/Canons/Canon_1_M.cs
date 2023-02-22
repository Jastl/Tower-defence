using System.Collections;
using UnityEngine;

public class Canon_1_M : MonoBehaviour
{
    private Buffer buffer;
    private Blueprint blueprint;
    private Transform tran;

    private Quaternion corner; //значення повороту зброї в сторону ворога
    private int timer = 0; // <0 - stop; 0 - shot
    private ushort idCell; //номер клітинки, в якій знаходиться будівля
    //settings
    private float distance;
    private float damage;
    public float speedOfReload;
    private float speedOfBullet;
    //improvments
    private float distanceB;
    private float damageB;
    public float speedOfReloadB;
    private float speedOfBulletB;

    private void Update()
    {
        int idOfBuild = blueprint.cells[idCell].GetComponent<CellData>().id;

        distance = blueprint.items[idOfBuild].distance;
        speedOfReload = blueprint.items[idOfBuild].timeReloadShot;
        speedOfBullet = blueprint.items[idOfBuild].speedOfBullet;
        damage = buffer.damage;

        distanceB = buffer.distance;
        speedOfReloadB = buffer.speedOfReload;
        speedOfBulletB = buffer.speedOfBullet;
        damageB = buffer.damageB;

        idCell = (ushort)buffer.idCell;

        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Enemy")) //перебір ворогів
        {
            if (distance * distanceB >= Vector2.Distance(this.tran.position, a.transform.position)) //якщо дистанція між ворогом і зброєю задовільна
            {
                corner = Quaternion.Euler(tran.rotation.eulerAngles.x, tran.rotation.eulerAngles.y, 
                    Mathf.Atan2(a.transform.position.y - tran.position.y, a.transform.position.x - tran.position.x) * Mathf.Rad2Deg - 90); //знаходження кута
                tran.rotation = corner;

                if (timer == 0) //вогонь
                {
                    timer = (int)(speedOfReload * speedOfReloadB);

                    GameObject bullet = Instantiate(blueprint.items[blueprint.cells[idCell].GetComponent<CellData>().id].imageBullet, tran);
                    Canon_1_BM bulletSetings = bullet.GetComponent<Canon_1_BM>();

                    bulletSetings.corner = corner;
                    Vector2 vector = a.transform.position - tran.position;
                    bulletSetings.speed = vector * speedOfBullet * speedOfBulletB / Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
                    bullet.transform.position = tran.position;
                    bulletSetings.damage = damage * damageB;
                    bullet.transform.SetParent(null);
                    bulletSetings.parentWeapon = tran.position;
                    bulletSetings.distance = distanceB * distance;
                }
            }
        }
    }
    IEnumerator timerr()
    {
        yield return new WaitForSeconds(0.001f);
        if (timer > 0) timer--;
    }
    private void FixedUpdate()
    {
        StartCoroutine(timerr());
    }
    private void Start()
    {
        blueprint = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        buffer = this.gameObject.GetComponent<Buffer>();
        tran = transform;
    }
}

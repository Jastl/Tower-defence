using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public float hp; //health points
    public float maxHp; //maximum health points
    public float startXScale; //
    public float damage; 
    public int id;

    private GameObject lineOfHealth; //a line that shows the number of health points
    public int[] road = new int[0]; //the way of this enemy
    public Vector2 goal = new Vector2(0, 0); //the goal of this enemy
    public bool dontMove = false; //false - enemy can move / true - enemy can't move

    private Enemys enemys;
    private SearchRoad sroad;
    private Blueprint blueprint;
    private ScreenRes sr;
    private float widthEnemy; //the scale of the enemy's diagonal
    private Camera cam;
    private Transform tran;
    private LayerMask layerMask;
    private void Start()
    {
        enemys = GameObject.Find("Main Camera").GetComponent<Enemys>();
        sroad = GameObject.Find("Main Camera").GetComponent<SearchRoad>();
        blueprint = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        sr = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
        cam = Camera.main;
        lineOfHealth = this.gameObject.transform.GetChild(0).gameObject;
        tran = transform;
        layerMask = LayerMask.GetMask("Cells");
        Vector2 size = GetComponent<Collider2D>().bounds.size;
        widthEnemy = Mathf.Sqrt(size.x * size.x + size.y + size.y);
        //widthEnemy = Mathf.Sqrt(tran.localScale.x * tran.localScale.x + tran.localScale.y * tran.localScale.y) * 4;

        if (enemys.enemysData[id].searchType >= 0) for (int i = 0; i < 7; i++) //if the enemy has priority
        {
                for (int j = 0; j < blueprint.cells.Length; j++)
                {
                    Vector2 cellPos = blueprint.cells[j].transform.position;
                    float halfWidth = cam.WorldToScreenPoint(new Vector3((float)sr.widthCell, 0, 0)).x;

                    if (cellPos.x - halfWidth / 2 < tran.position.x && tran.position.x < cellPos.x + halfWidth / 2 &&
                        cellPos.y - halfWidth / 2 < tran.position.y && tran.position.y < cellPos.y + halfWidth / 2)
                    {
                        road = sroad.Search23lvl(j, (short)enemys.enemysData[id].preorityBuild, (short)enemys.enemysData[id].preorityType, (byte)enemys.enemysData[id].searchType);
                        break;
                    }
                } //searching the way
            }
    }
    private void Update()
    {
        if (lineOfHealth != null) 
            lineOfHealth.transform.localScale = new Vector3(hp / maxHp * startXScale, lineOfHealth.transform.localScale.y, lineOfHealth.transform.localScale.z);
        Move();
        if (!dontMove)
        {
            if (road.Length != 0) tran.position = Vector2.MoveTowards(tran.position, goal, enemys.enemysData[id].speed / 20);
            else tran.position -= new Vector3(0, enemys.enemysData[id].speed / 20, 0); //if the enemy hasn't target
        }
        if (tran.position.y < -22) Destroy(this.gameObject); //if the enemy is outside the border
    }

    public void damaged(float damage, int idOfWeapon)
    {
        foreach (int id in enemys.enemysData[id].imunity)
        {
            if (idOfWeapon == id) return;
        }
        hp = hp - damage;
        if (hp <= 0) Destroy(this.gameObject);
    }
    private void Move()
    {
        if (road.Length != 0 && !dontMove)
        {
            for (int i = road.Length - 1; i > -1; i--)
            {
                GameObject cell = blueprint.cells[road[i]];
                Vector2 heading = cell.transform.position - tran.position;
                Vector2 direction = heading / heading.magnitude;
                Vector2 dir90 = new Vector2(0, 0);

                if (direction.x > 0 && direction.y > 0) dir90 = new Vector2(-direction.y, direction.x);
                else if (direction.x < 0 && direction.y < 0) dir90 = new Vector2(-direction.y, direction.x);
                else if (direction.x < 0 && direction.y > 0) dir90 = new Vector2(direction.y, -direction.x);
                else if (direction.x > 0 && direction.y < 0) dir90 = new Vector2(direction.y, -direction.x);
                else if (direction.x == 0 && direction.y < 0) dir90 = new Vector2(1, 0);
                else if (direction.x == 0 && direction.y > 0) dir90 = new Vector2(-1, 0);
                else if (direction.x < 0 && direction.y == 0) dir90 = new Vector2(0, -1);
                else if (direction.x > 0 && direction.y == 0) dir90 = new Vector2(0, 1);

                RaycastHit2D ray1 = Physics2D.Raycast(tran.position + new Vector3(widthEnemy / 2 * dir90.x, widthEnemy / 2 * dir90.y, 0), direction, heading.magnitude, layerMask);
                RaycastHit2D ray2 = Physics2D.Raycast(tran.position + new Vector3(widthEnemy / -2 * dir90.x, widthEnemy / -2 * dir90.y, 0), direction, heading.magnitude, layerMask);
                RaycastHit2D ray3 = Physics2D.Raycast(direction * heading.magnitude + (Vector2)tran.position + 
                    new Vector2(widthEnemy / -2 * dir90.x, widthEnemy / -2 * dir90.y), dir90, widthEnemy, layerMask);

                Debug.DrawRay(tran.position + new Vector3(widthEnemy / 2 * dir90.x, widthEnemy / 2 * dir90.y, 0), direction * heading.magnitude, Color.yellow);
                Debug.DrawRay(tran.position + new Vector3(widthEnemy / -2 * dir90.x, widthEnemy / -2 * dir90.y, 0), direction * heading.magnitude, Color.yellow);
                Debug.DrawRay(direction * heading.magnitude + (Vector2)tran.position + new Vector2(widthEnemy / -2 * dir90.x, widthEnemy / -2 * dir90.y), dir90 * widthEnemy, Color.red);
                if (ray1.collider == null && ray2.collider == null)
                {
                    goal = cell.transform.position;
                    break;
                }
                if (enemys.enemysData[id].searchType == 1 || i == road.Length - 1) 
                    if (ray1.collider != null && ray2.collider != null && ray1.collider.gameObject == cell.GetComponent<CellData>().build && ray2.collider.gameObject == cell.GetComponent<CellData>().build)
                {
                    goal = cell.transform.position;
                    break;
                }
            }
            if (!blueprint.cells[road[road.Length - 1]].GetComponent<CellData>().busy)
            {
                for (int j = 0; j < blueprint.cells.Length; j++)
                {
                    Vector2 cellPos = blueprint.cells[j].transform.position;
                    float halfWidth = cam.WorldToScreenPoint(new Vector3((float)sr.widthCell, 0, 0)).x;

                    if (cellPos.x - halfWidth / 2 < tran.position.x && tran.position.x < cellPos.x + halfWidth / 2 &&
                        cellPos.y - halfWidth / 2 < tran.position.y && tran.position.y < cellPos.y + halfWidth / 2)
                    {
                        road = sroad.Search23lvl(j, (short)enemys.enemysData[id].preorityBuild, (short)enemys.enemysData[id].preorityType, (byte)enemys.enemysData[id].searchType);
                        break;
                    }
                }
            }
        }
    }
}

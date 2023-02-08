using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject target;

    private EnemyData eData;
    private Enemys enemys;
    private Blueprint bl;
    private int id;
    private Transform tran;
    private LayerMask layerMask;

    private int[] road = new int[0];
    public int timer = 0;
    private void Start()
    {
        eData = this.GetComponent<EnemyData>();
        enemys = GameObject.Find("Main Camera").GetComponent<Enemys>();
        bl = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        id = eData.id;
        tran = transform;
        layerMask = LayerMask.GetMask("Cells");
    }
    private void Update()
    {
        StartCoroutine(timers());

        road = eData.road;
        for (int i = 0; i < road.Length; i++)
        {
            CellData Celld = bl.cells[road[i]].GetComponent<CellData>();
            if (Celld.build != null && 
                Vector2.Distance(tran.position, Celld.build.transform.position) < enemys.enemysData[id].distance)
            {
                Vector2 heading = Celld.build.transform.position - tran.position;
                Vector2 dir = heading / heading.magnitude;
                RaycastHit2D ray = Physics2D.Raycast(tran.position, dir, enemys.enemysData[id].distance, layerMask);

                Debug.DrawRay(tran.position, dir * heading.magnitude, Color.yellow);

                if (ray.collider != null && ray.collider.gameObject == Celld.build)
                {
                    eData.dontMove = true;
                    target = Celld.build;

                    if (timer == 0) timer++;
                    if (timer >= enemys.enemysData[id].speedAttak)
                    {
                        timer = 0;
                        Celld.TakeDamage(enemys.enemysData[id].damage);
                    }
                }
            }
        }
        if(target == null) eData.dontMove = false;
    }

    IEnumerator timers()
    {
        yield return new WaitForSeconds(0.001f);
        if (timer > 0) timer++;
    }
}

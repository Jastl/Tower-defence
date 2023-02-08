using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    public GameObject[] enemys = new GameObject[0];
    public DataOfEnemy[] enemysData = new DataOfEnemy[0];
    private void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy"); //
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == null)
            {

            }
        }
    }
}
[System.Serializable]
public class DataOfEnemy{
    public string name;
    public int id = 0;

    public float hp = 0;
    public float damage = 0;
    public float distance = 0;
    public float speedAttak = 0;
    public float speed = 0;

    public int preorityType = -1;
    public int preorityBuild = -1;
    public sbyte searchType = -1;

    public int[] imunity;

    public GameObject prefabShell;
    public float speedShell = 0;

    public GameObject prefab = null;
}

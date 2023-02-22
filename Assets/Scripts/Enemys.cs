using UnityEngine;
using System;

public class Enemys : MonoBehaviour
{
    public GameObject[] enemys = new GameObject[0];
    public DataOfEnemy[] enemysData = new DataOfEnemy[0];


    public void AddEnemy(GameObject enemy)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == null)
            {
                enemys[i] = enemy;
                break;
            }
            if (i == enemys.Length - 1)
            {
                Array.Resize(ref enemys, enemys.Length + 1);
                enemys[enemys.Length - 1] = enemy;
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

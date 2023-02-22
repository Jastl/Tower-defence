using System.Collections;
using UnityEngine;
using System;

public class WavesData1 : MonoBehaviour
{
    private Enemys enemys;
    private ScreenRes screen;
    private Blueprint blueprint;
    private System.Random random = new System.Random();

    public BW[] bw = new BW[0];

    public bool startWave = false;
    public int numBW;

    private int[] countE = new int[0];
    private int[] timerE = new int[0];
    private bool[] finishE = new bool[0];

    private int countD = 0;
    private int timerD = 0;
    private bool finishD = false;
    private int[] DNow = new int[0];

    private int countL = 0;
    private int timerL = 0;
    private bool finishL = false;

    public int countM = 0;
    private int timerM = 0;
    private bool finishM = false;
    private void Start()
    {
        enemys = GameObject.Find("Main Camera").GetComponent<Enemys>();
        screen = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
        blueprint = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        Array.Resize(ref DNow, 1);
        ChangeSizeWE();
        DNow[0] = 0;
    }
    public void Update()
    {
        //test 
        if (Input.GetKeyDown(KeyCode.S))
        {
            startWave = true;
            numBW = 0;
        }
        //

        StartCoroutine(timers());
        if (startWave) //if wave started
        {
            //ew
            ChangeSizeWE();
            for (int i = 0; i < DNow.Length; i++)
            {
                if (countE[i] < bw[numBW].mw[countM].lw[countL].dw[DNow[i]].countEnemys)
                {
                    if (timerE[i] == 0) timerE[i]++;
                    if (timerE[i] >= bw[numBW].mw[countM].lw[countL].dw[countD].DTime) finishE[i] = true;
                    if (finishE[i])
                    {
                        countE[i]++;
                        GameObject enemy = Instantiate(enemys.enemysData[bw[numBW].mw[countM].lw[countL].dw[countD].idEnemys].prefab);
                        enemys.AddEnemy(enemy);
                        enemy.transform.position = new Vector2(UnityEngine.Random.Range(blueprint.cells[0].transform.position.x, blueprint.cells[6].transform.position.x), blueprint.cells[0].transform.position.y);
                        timerE[i] = 0;
                        finishE[i] = false;
                    }
                }
            }
            //dw
            if (countD < bw[numBW].mw[countM].lw[countL].dw.Length)
            {
                if (timerD == 0) timerD++;
                if (timerD >= bw[numBW].mw[countM].lw[countL].dw[countD].time && countD + 1 != bw[numBW].mw[countM].lw[countL].dw.Length)
                {
                    Array.Resize(ref DNow, DNow.Length + 1);
                    ChangeSizeWE();
                    DNow[DNow.Length - 1] = countD + 1;
                    countD++;
                    timerD = 0;
                }
            }
            //lw
            if (countL < bw[numBW].mw[countM].lw.Length)
            {
                if (timerL == 0) timerL++;
                if (timerL >= bw[numBW].mw[countM].lw[countL].time) finishL = true;
                if (finishL)
                {
                    if (countL + 1 != bw[numBW].mw[countM].lw.Length)
                    {
                        timerL = 0;
                        finishL = false;
                        Array.Resize(ref DNow, 1);
                        ChangeSizeWE();
                        timerD = 0;
                        countD = 0;
                        finishD = false;
                        timerE[0] = 0;
                        countE[0] = 0;
                        finishE[0] = false;
                        countL++; 
                    }
                    else if (enemys.enemys.Length == 0)
                    {
                        finishM = true;
                    }
                }
            }
            if (finishM)
            {
                countM++;
                finishM = false;
                countE = new int[0];
                timerE = new int[0];
                finishE = new bool[0];
                countD = 0;
                timerD = 0;
                finishD = false;
                DNow = new int[0];
                countL = 0;
                timerL = 0;
                finishL = false;
            }
        }
        else
        {
            countE = new int[0];
            timerE = new int[0];
            finishE = new bool[0];
            countD = 0;
            timerD = 0;
            finishD = false;
            DNow = new int[0];
            countL = 0;
            timerL = 0;
            finishL = false;
            countM = 0;
            timerM = 0;
            finishM = false;
        }
    }
    private void ChangeSizeWE()
    {
        Array.Resize(ref finishE, DNow.Length);
        Array.Resize(ref countE, DNow.Length);
        Array.Resize(ref timerE, DNow.Length);
    }
    IEnumerator timers()
    {
        yield return new WaitForSeconds(0.001f);
        for (int i = 0; i < DNow.Length; i++) 
            if (timerE[i] > 0) timerE[i]++;
        if (timerD > 0) timerD++;
        if (timerL > 0) timerL++;
    }
}
[System.Serializable]
public class BW
{
    public MW[] mw = new MW[1];
}
[System.Serializable]
public class MW
{
    public LW[] lw = new LW[1];
    public bool savePoint = false;
}
[System.Serializable]
public class LW
{
    public DW[] dw = new DW[1];
    public int time = 0;     
}
[System.Serializable]
public class DW
{
    public int countEnemys = 0;
    public int time = 0;
    public int DTime = 0;
    public int idEnemys = 0;
}

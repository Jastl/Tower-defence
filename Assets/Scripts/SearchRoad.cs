using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SearchRoad : MonoBehaviour
{
    private Blueprint bl;
    public int[] road = new int[0];//тест
    public Cell23[] cells = new Cell23[70];

    public GameObject point;
    private void Start()
    {
        bl = GameObject.Find("Main Camera").GetComponent<Blueprint>();
        Array.Resize(ref cells, bl.cells.Length);
        for (int i = 0; i < cells.Length; i++) cells[i] = new Cell23();
    }
    private bool showRoad = false;
    private GameObject[] points = new GameObject[0];
    private void Update() //повністю ткстовий (Q, W, show road)
    {
        if (Input.GetKey(KeyCode.Q)) road = Search23lvl(3, 3, 10, 0); //тест
        if (Input.GetKey(KeyCode.W)) road = Search23lvl(3, 3, 10, 1); //тест


        if (Input.GetKeyDown(KeyCode.Y)) showRoad = !showRoad;
        if (showRoad)
        {
            Array.Resize(ref points, road.Length);
            for (int i = 0; i < road.Length; i++)
            {
                if (points[i] == null) points[i] = Instantiate(point);
                points[i].transform.position = bl.cells[road[i]].transform.position;
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++) Destroy(points[i]);
            Array.Resize(ref points, 0);
        }
    }

    //searchOfDeykstera
    public int[] Search23lvl(int start, short targetID, short targetType, byte type) //start position, id of target, type of target, type of search(0 - empty, 1 - hp, 2 - time *coming soon*)
    {
        SaveDataCell23(start, 0, 1, 0); //set data first cell
        for (int i = 1; i < cells.Length; i++) //set all data cell
        {
            for (int j = 0; j < cells.Length; j++)
            {
                if (cells[j].number == i)
                {
                    if (j % 7 != 6) checkCell(j + 1, -1, type); //перевірка клітинок           
                    if (j % 7 != 0) checkCell(j - 1, 1, type);            
                    if (j - 7 >= 0) checkCell(j - 7, 7, type);            
                    if (j + 7 <= 69) checkCell(j + 7, -7, type);
                }
            }
        }


        int numOfGoal = -1;
        float minPath = 0;
        for (int i = 0; i < cells.Length; i++)
        {
            if (bl.cells[i].GetComponent<CellData>().id == targetID || bl.items[bl.cells[i].GetComponent<CellData>().id].type == targetType) //вибір цілі
                if (minPath == 0)
                {
                    minPath = cells[i].minPath;
                    numOfGoal = i;
                }
                else if (cells[i].minPath < minPath)
                {
                    minPath = cells[i].minPath;
                    numOfGoal = i;
                }
        }
        if (numOfGoal > -1 && type == 0 && minPath - checkHP(numOfGoal) == 0) return (createRoad(numOfGoal)); //якщо шлях пустий і тип пошуку 0
        if (numOfGoal > -1 && type == 1) return (createRoad(numOfGoal));
        return (new int[0]);
    }
    private int[] createRoad(int goal) //створення дороги на основі даних в клітинках 
    {
        int[] path = new int[0];
        Array.Resize(ref path, cells[goal].number);
        path[0] = goal;
        for (int i = 1; i < path.Length; i++)
        {
            path[i] = path[i - 1] + cells[path[i - 1]].vector;
        }
        Array.Reverse(path);
        return (path);
    }
    private void checkCell(int num, int vector, byte type) //зміна даних клітинки
    {
        if (cells[num + vector].number == 1) SaveDataCell23(num, vector, cells[num + vector].number + 1, checkHP(num)); //якщо попередня клітинка є першою, то мінімальний шлях наступної дорівнює шляху від 1 до 2
        else if (cells[num + vector].minPath + checkHP(num) < cells[num].minPath) //якщо мінімальний шлях через цю клітинку менший ніж будь який відомий
            SaveDataCell23(num, vector, cells[num + vector].number + 1, cells[num + vector].minPath + checkHP(num)); //зберегти дані
    }
    private float checkHP(int num) //перевірка клітинки на кількість здоров'я
    {
        if (bl.cells[num].GetComponent<CellData>().id == 0) return (0);
        if (bl.cells[num].GetComponent<CellData>().id > 0) return (bl.cells[num].GetComponent<CellData>().hp);
        return (0);
    }
    private void SaveDataCell23(int num, int vector, int number, float minPath) //номер клітики, напрямок до попередньої клітинки, номер циклу перевірки клітинки, найкоротший шлях до клітинки
    {
        cells[num].vector = vector;
        cells[num].number = number;
        cells[num].minPath = minPath;
    }
}
[System.Serializable]
public class Cell23
{
    public int vector = 0;
    public int number = 0;
    public float minPath = 999999;
}
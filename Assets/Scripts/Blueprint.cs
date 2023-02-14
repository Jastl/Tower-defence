using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blueprint : MonoBehaviour
{
    public Items[] items = new Items[0];//все постройки
    public GameObject[] cells = new GameObject[0];//все клетки
    public GameObject net;//панель с клетками

    //primitive inventory
    public GameObject[] invSlots = new GameObject[0];//все слоты инвентаря
    public GameObject[] img = new GameObject[0];//картинки построек в инвентаре
    public int[] invId = new int[0];//айди построек в инвентаре
    public bool[] empty = new bool[0];//использован ли объект
    public int[] tier = new int[0];//тир
    public int[] lvl = new int[0];//уровень

    //advanced inventory
    public GameObject[] inv2Slots = new GameObject[0];//всі слоти продвинутого інвентарю
    public GameObject[] img2 = new GameObject[0];

    public GameObject invPanel;//панель инвентаря
    public GameObject inv2Panel;
    public GameObject prefabSlot;//пример слота

    public MenuManager MenuManager;
    private Camera cam;

    public void Start()
    {
        cam = Camera.main;
        MenuManager = GameObject.Find("Main Camera").GetComponent<MenuManager>();
        Array.Resize(ref cells, net.transform.childCount);//инициализация количества клеток
        for (int i = 0; i < cells.Length; i++)//инициализация клеток
        {
            cells[i] = GameObject.Find("Cell (" + i + ')');//поиск клетки
            cells[i].GetComponent<CellData>().idOfCell = i;
        }
    }
    public void test()//выдать постройку с айди
    {
        saveItem(1, 1);
        saveItem(3, 2);
        saveItem(4, 20);
        saveItem(5, 4);
    }
    private bool loadingSlot;
    public void saveItem(int savedId, int count)//сохранения объекта в инвентаре
    {
        for (int i = 0; i < count; i++)
        {
            loadingSlot = true;
            Array.Resize(ref invSlots, invSlots.Length + 1);//изменения розмера инвентаря
            Array.Resize(ref inv2Slots, invSlots.Length);
            invSlots[invSlots.Length - 1] = Instantiate(prefabSlot);//создания слота
            inv2Slots[inv2Slots.Length - 1] = Instantiate(prefabSlot);
            Array.Resize(ref invId, invSlots.Length);
            Array.Resize(ref empty, invSlots.Length);
            Array.Resize(ref img, invSlots.Length);
            Array.Resize(ref img2, invSlots.Length);
            Array.Resize(ref lvl, invSlots.Length);
            Array.Resize(ref tier, invSlots.Length);
            invSlots[invSlots.Length - 1].transform.SetParent(invPanel.transform);//присвоения слота к панели инвентаря
            inv2Slots[inv2Slots.Length - 1].transform.SetParent(inv2Panel.transform);//присвоения слота к панели prod инвентаря
            invId[invId.Length - 1] = savedId;//сохранения айди
            empty[empty.Length - 1] = true;
            lvl[lvl.Length - 1] = 1;//присвоения уровня
            tier[tier.Length - 1] = items[savedId].tier;//присвоения тира
            loadingSlot = false;
        }
    }
    public void Update()
    {
        if (!loadingSlot)
        {
            for (int i = 0; i < invSlots.Length; i++)//обновления слотов
            {
                if (empty[i] == true)//если постройка не используется
                {
                    if (img[i] == null)//подключения картинки
                    {
                        img[i] = Instantiate(items[invId[i]].image);
                        img[i].transform.SetParent(invPanel.transform);
                    }
                    img[i].transform.position = invSlots[i].transform.position;//позиционирования картинки по слоту
                    invSlots[i].SetActive(true);//показ ячейки
                    invSlots[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);//выравневания ячейки
                    IndexOfSlots index = invSlots[i].GetComponent<IndexOfSlots>();
                    index.level = lvl[i].ToString();
                    index.tier = tier[i].ToString();
                }
                else
                {
                    invSlots[i].SetActive(false);//скрыть ячейку
                    img[i].SetActive(false);
                }
            }
            for (int i = 0; i < inv2Slots.Length; i++)
            {
                if (MenuManager.buildMode || MenuManager.enabledInv) if (empty[i] == true)
                {
                    if (img2[i] == null)
                    {
                        img2[i] = Instantiate(items[invId[i]].image);
                        img2[i].transform.SetParent(inv2Slots[i].transform);
                        img2[i].transform.localScale = new Vector3(1, 1, 0);
                    }
                    img2[i].transform.position = inv2Slots[i].transform.position;
                    inv2Slots[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                    IndexOfSlots index = inv2Slots[i].GetComponent<IndexOfSlots>();
                    index.level = lvl[i].ToString();
                    index.tier = tier[i].ToString();
                }
                else
                {
                    inv2Slots[i].SetActive(false);
                    img2[i].SetActive(false);
                }
            }
        }
    }
}
[System.Serializable]
public class Items
{
    public string name;//имя
    public int id;//айди
    public int type;//тип(акт. вооружения, пас. защита, другое) 0 - активний захист, 1 - пасивний захист, 2 - допоміжні будівлі, 3 - інше
    public GameObject image;//картинка
    public int maxLevel;//максимальный лвл
    public int tier;
    public float healthPoints;
    public float distance;
    public float timeReloadShot;
    public float speedOfBullet;
    public GameObject imageBullet;
    public GameObject prefab;//префаб постройки с механикой стрельбы и функций
}


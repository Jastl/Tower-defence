using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private SaveData saveData;
    private WavesData1 wavesData1;

    private int a;
    private void Start()
    {
        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();
        wavesData1 = GameObject.Find("Main Camera").GetComponent<WavesData1>();
        if (true) Debug.Log("JJJJJJJJJ");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) saveData.ToSaveData();
        if (Input.GetKeyDown(KeyCode.Minus)) saveData.ToLoadData();
        if (Input.GetKeyDown(KeyCode.KeypadMultiply)) saveData.ToResetData();

        if (Input.GetKeyDown(KeyCode.S)) 
        { 
            wavesData1.startWave = true;
            wavesData1.numBW = 0;
        }
        if (Input.GetKeyDown(KeyCode.A)) wavesData1.startWave = false;
    }
    //gljdukgsni
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private SaveData saveData;
    private WavesData1 wavesData1;
    private AdaptiveInterface ai;
    private ScreenRes sr;
    private void Start()
    {
        saveData = GameObject.Find("Main Camera").GetComponent<SaveData>();
        wavesData1 = GameObject.Find("Main Camera").GetComponent<WavesData1>();
        ai = GameObject.Find("Main Camera").GetComponent<AdaptiveInterface>();
        sr = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
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

        //adaptive interface
        if (Input.GetKeyDown(KeyCode.E)) ai.useAI();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sr.widthScreen = 1080;
            sr.heightScreen = 1920;
            ai.useAI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sr.widthScreen = 1080;
            sr.heightScreen = 2400;
            ai.useAI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sr.widthScreen = 480;
            sr.heightScreen = 800;
            ai.useAI();
        }
    }
}

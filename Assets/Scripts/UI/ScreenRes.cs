using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    public float widthScreen = 1080;
    public float heightScreen = 1920;

    public float widthCell = 150;
    public float widthSlot1 = 200;
    public float widthSlot2 = 187;
    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        widthScreen = resolutions[resolutions.Length - 1].height;
        heightScreen = resolutions[resolutions.Length - 1].width;
        widthCell = 150f / 1080 * widthScreen;
        widthSlot1 = 200f / 1080 * widthScreen;
        widthSlot2 = 187f / 1080 * widthScreen;
    }
    private void Update()
    {
        widthCell = 150f / 1080 * widthScreen;
        widthSlot1 = 200f / 1080 * widthScreen;
        widthSlot2 = 187f / 1080 * widthScreen;
    }
}

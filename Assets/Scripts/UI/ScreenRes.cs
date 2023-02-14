using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    public double widthScreen = 1080;
    public double heightScreen = 1920;

    public double widthCell = 150;
    public double widthSlot1 = 200;
    public double widthSlot2 = 187;

    public GameObject Colliders;

    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        widthScreen = resolutions[resolutions.Length - 1].height;
        heightScreen = resolutions[resolutions.Length - 1].width;
        widthCell = SizeW(150);
        widthSlot1 = SizeW(200);
        widthSlot2 = SizeW(187);
    }
    public double SizeH(double size) 
    {
        return (size / 1920 * heightScreen);
    }
    public double SizeW(double size)
    {
        return (size / 1080 * widthScreen);
    }
    public void SetActiveColiders(bool active)
    {
        Colliders.SetActive(active);
    }
    public void SetActiveColiders()
    {
        Colliders.SetActive(!Colliders.activeSelf);
    }
}

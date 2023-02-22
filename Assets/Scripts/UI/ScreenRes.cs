using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    public int widthScreen = 1080;
    public int heightScreen = 1920;

    public double widthCell = 150;

    public double widthSlot1;
    public GameObject panelPinv;

    public double widthSlot2;
    public GameObject panelInv;

    public GameObject Colliders;

    
    public float SizeH(double size) 
    {
        return ((float)size / 1920 * heightScreen);
    }
    public float SizeW(double size)
    {
        return ((float)size / 1080 * widthScreen);
    }
    public void SetActiveColiders(bool active)
    {
        Colliders.SetActive(active);
    }
    public void SetActiveColiders()
    {
        Colliders.SetActive(!Colliders.activeSelf);
    }


    private void Start()
    {
        //Resolution[] resolutions = Screen.resolutions;
        //widthScreen = resolutions[resolutions.Length - 1].height;
        //heightScreen = resolutions[resolutions.Length - 1].width;
        widthCell = SizeW(150);
        widthSlot1 = SizeW(200);
        widthSlot2 = SizeW(200);
    }
}

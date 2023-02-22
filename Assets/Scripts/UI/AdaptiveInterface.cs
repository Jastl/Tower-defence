using UnityEngine;

public class AdaptiveInterface : MonoBehaviour
{
    private ScreenRes sr;
    private Camera cam;

    public GameObject[] widthScale = new GameObject[0];

    public void useAI()
    {
        foreach (GameObject go in widthScale) widthScaleF(go);
    }
    private void widthScaleF(GameObject uiO)
    {
        float deltaSizeX = (float)sr.widthScreen / (float)1080;
        float deltaSizeY = (float)sr.heightScreen / (float)1920;
        RectTransform uiRT = uiO.GetComponent<RectTransform>();
        uiRT.localScale = new Vector2(uiRT.localScale.x * deltaSizeX, uiRT.localScale.y * deltaSizeX);
        uiRT.localPosition = new Vector2(uiRT.localPosition.x * deltaSizeX, uiRT.localPosition.y * deltaSizeY);
    }
    private void Start()
    {
        sr = GameObject.Find("Main Camera").GetComponent<ScreenRes>();
        cam = Camera.main;
    }
}

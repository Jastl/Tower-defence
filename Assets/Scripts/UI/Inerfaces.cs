using UnityEngine;

public class Inerfaces : MonoBehaviour
{
    public GameObject canvas;
    public Transform[] interfaces;
    public GameObject panel;
    public GameObject warringLoadNewMap;
    public GameObject pickMap;

    private void Update()
    {
        panel.SetActive(false);
        foreach (Transform a in interfaces)
        {
            if (a.gameObject.activeSelf == true) panel.SetActive(true);
        }
    }

    public void WLoadNewMap()
    {
        warringLoadNewMap.SetActive(true);
    }
    public void WELoadNewMap()
    {
        warringLoadNewMap.SetActive(false);
    }

    public void PickMapMenu()
    {
        warringLoadNewMap.SetActive(false);
        pickMap.SetActive(true);
    }
    public void Story()
    {
        pickMap.SetActive(false);
    }
    public void Survival()
    {
        pickMap.SetActive(false);
    }
    public void Chelenge()
    {
        pickMap.SetActive(false);
    }
}

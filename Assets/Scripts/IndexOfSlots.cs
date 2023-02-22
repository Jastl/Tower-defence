using UnityEngine;
using UnityEngine.UI;

public class IndexOfSlots : MonoBehaviour
{
    private GameObject obj;
    public string tier;
    public string level;

    private void Start()
    {
        obj = this.gameObject;
        obj.transform.Find("Tier").GetComponent<Text>().text = tier;
        obj.transform.Find("Level").GetComponent<Text>().text = level;
    }
}

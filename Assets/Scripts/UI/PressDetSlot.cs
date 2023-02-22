using UnityEngine;

public class PressDetSlot : MonoBehaviour
{
    private MoveSlotOrInv msoi;
    private InventoryUI invUI;

    private GameObject parent;


    private void OnMouseDown()
    {
        if (this.gameObject.transform.parent.gameObject != parent) msoi.OnClick(this.gameObject);
        else invUI.OnClick(this.gameObject);
    }
    private void Start()
    {
        msoi = GameObject.Find("Main Camera").GetComponent<MoveSlotOrInv>();
        parent = GameObject.Find("lnventory Panel");
        invUI = GameObject.Find("Main Camera").GetComponent<InventoryUI>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDetSlot : MonoBehaviour
{
    private MoveSlotOrInv msoi;
    private GameObject parent;
    private InventoryUI invUI;
    private void Start()
    {
        msoi = GameObject.Find("Main Camera").GetComponent<MoveSlotOrInv>();
        parent = GameObject.Find("lnventory Panel");
        invUI = GameObject.Find("Main Camera").GetComponent<InventoryUI>();
    }
    private void OnMouseDown()
    {
        if (this.gameObject.transform.parent.gameObject != parent) msoi.OnClick(this.gameObject);
        else invUI.OnClick(this.gameObject);
    }
}

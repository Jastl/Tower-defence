using UnityEngine;

public class SortLvlTier : MonoBehaviour
{
    private InventoryUI IU;

    public GameObject arrowLvlUp;
    public GameObject arrowLvlDown;
    public GameObject arrowTierUp;
    public GameObject arrowTierDown;

    private int statusSortLvl = 1;
    private int statusSortTier = 0;


    public void SortLvlButton() //button of sort_lvl has been pressed
    {
        if (statusSortLvl < 2) statusSortLvl++;
        else statusSortLvl = 1;
        SortLvl();
    }
    public void SortLvl()
    {
        if (statusSortLvl != 0)
        {
            arrowTierUp.SetActive(false);
            arrowTierDown.SetActive(false);
        }
        if (statusSortLvl == 1)
        {
            arrowLvlDown.SetActive(true);
            arrowLvlUp.SetActive(false);
            IU.Sorting(IU.currentSortType, "lvl", 1);
            statusSortTier = 0;
        }
        if (statusSortLvl == 2)
        {
            arrowLvlUp.SetActive(true);
            arrowLvlDown.SetActive(false);
            IU.Sorting(IU.currentSortType, "lvl", 0);
            statusSortTier = 0;
        }
    }


    public void SortTierButton() //button of sort_tier has been pressed
    {
        if (statusSortTier < 2) statusSortTier++;
        else statusSortTier = 1;
        SortTier();
    }
    public void SortTier()
    {
        if (statusSortTier != 0)
        {
            arrowLvlUp.SetActive(false);
            arrowLvlDown.SetActive(false);
        }
        if (statusSortTier == 1)
        {
            arrowTierDown.SetActive(true);
            arrowTierUp.SetActive(false);
            IU.Sorting(IU.currentSortType, "tier", 0);
            statusSortLvl = 0;
        }
        if (statusSortTier == 2)
        {
            arrowTierUp.SetActive(true);
            arrowTierDown.SetActive(false);
            IU.Sorting(IU.currentSortType, "tier", 1);
            statusSortLvl = 0;
        }
    }


    private void Start()
    {
        IU = GameObject.Find("Main Camera").GetComponent<InventoryUI>();
    }
}

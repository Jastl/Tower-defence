using UnityEngine;

public class CellData : MonoBehaviour
{
    private Building building;

    public GameObject build = null;
    public int id = 0; //id of obj in the cell
    public int idOfCell;
    public int lvl = 0; //lvl of obj in the cell
    public int numberOfSlot = 0; //number of obj in inv
    public float damage = 10;
    public float hp;
    public float maxHP;
    public bool busy = false;
    

    private void Update()
    {
        damage = 10;

        if (build != null) 
        {
            Buffer a = build.GetComponent<Buffer>();
            a.damage = damage;
            a.idCell = idOfCell;
        }
    }
    public void TakeDamage(float dmg)
    {
        hp = hp - dmg;
        if (hp <= 0) building.DeleteBuild(this.gameObject);
    }


    private void OnMouseDown()
    {
        if (building.deleteMod) building.DeleteBuild(this.gameObject);
    }
    private void Start()
    {
        building = GameObject.Find("Main Camera").GetComponent<Building>();
    }
}

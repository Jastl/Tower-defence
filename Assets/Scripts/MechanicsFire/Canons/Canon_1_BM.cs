using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon_1_BM : MonoBehaviour
{
    public Quaternion corner;
    public Vector2 parentWeapon;
    public Vector2 speed;
    public float distance;
    public float damage;

    private Transform tran;
    private void Start()
    {
        tran = transform;
    }
    private void Update()
    {
        transform.position += new Vector3(speed.x, speed.y, 0);
        if (parentWeapon != null && distance <= Vector2.Distance(tran.position, parentWeapon)) Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyData>().damaged(damage, 1);
            Destroy(this.gameObject);
        }
    }
}

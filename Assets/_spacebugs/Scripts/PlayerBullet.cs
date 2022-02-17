using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemyBullet = collision.gameObject.GetComponent<Bullet>();
        if (enemyBullet != null)
        {
            Destroy(gameObject);
        }
    }
}

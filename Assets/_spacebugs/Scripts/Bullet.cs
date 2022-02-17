using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerBullet = collision.gameObject.GetComponent<PlayerBullet>();
        // TODO maybe some enemy bullets are indestructible etc
        if (playerBullet != null)
        {
            Destroy(gameObject);
        }
    }
}

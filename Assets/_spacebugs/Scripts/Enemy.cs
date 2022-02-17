using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletIntervalMin = 0.4f;
    public float bulletIntervalMax = 3f;

    private float timeUntilNextBullet = 0f;

    public static event UnityAction<Enemy> OnEnemyDestroyed = delegate { };

    private void Awake()
    {
        timeUntilNextBullet = Random.Range(bulletIntervalMin, bulletIntervalMax);
    }

    private void Update()
    {
        timeUntilNextBullet -= Time.deltaTime;
        if (timeUntilNextBullet <= 0f)
        {
            timeUntilNextBullet = Random.Range(bulletIntervalMin, bulletIntervalMax);

            // TODO check if someone is below me
            var coll = Physics2D.OverlapBox(transform.position.ToVector2() + 2f * Vector2.down, Vector2.one * 0.5f, 0f);
            if (coll == null)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log($"{name} could not shoot because {coll.name} is in the way");
            }
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed(this);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<PlayerBullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move speed in units/sec")]
    public float moveSpeed;
    public InputActionReference moveAction;
    public InputActionReference shootAction;
    
    public GameObject playerBulletPrefab;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootAction.action.started += OnShootAction;
    }

    private void Update()
    {
        var delta = moveAction.action.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;
        delta.y = 0f;
        transform.position += new Vector3(delta.x, delta.y, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            //Destroy(gameObject);
            Debug.Log("Ouch!");
            Destroy(collision.gameObject);
        }
    }

    private void OnShootAction(InputAction.CallbackContext context)
    {
        var bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
    }
}

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

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        shootAction.action.started += OnShootAction;
    }

    private void Update()
    {
        var delta = moveAction.action.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;
        delta.y = 0f;
        if (delta != Vector2.zero)
        {
            Debug.Log(delta);
        }
        transform.position += new Vector3(delta.x, delta.y, 0f);
    }

    private void OnShootAction(InputAction.CallbackContext context)
    {
        var bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        Debug.Log("Shooty shoot");
        // todo configure speed blah blah
        bullet.GetComponent<Rigidbody>().velocity = Vector3.up * 5f;
    }
}

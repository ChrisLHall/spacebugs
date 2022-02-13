using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("World positions of the boundaries of enemy movement")]
    public Transform moveBoundsMinPos;
    public Transform moveBoundsMaxPos;

    // the local positions of the farthest enemy to the left/right/top/bottom
    // used to decide when to move the other direction
    private Vector3 minRelativeEnemyPos;
    private Vector3 maxRelativeEnemyPos;

    private Enemy[] enemies = new Enemy[0];

    // increase as enemies are destroyed?
    public float MoveSpeed
    {
        get
        {
            return maxMoveSpeed / Mathf.Max(1f, enemies.Length);
        }
    }
    public float maxMoveSpeed = 10f;
    private bool movingRight = true;
    public float moveDownDistance = 0.8f;
    // move down a certain distance before moving horizontal again
    private float moveDownDistanceRemaining = 0f;



    private void Awake()
    {
        ListEnemies();
        RecalculateRelativeEnemyPos();
        Enemy.OnEnemyDestroyed += Enemy_OnEnemyDestroyed; ;
    }

    private void Update()
    {
        if (enemies.Length > 0)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {
        // move left or right until you hit an edge
        // Then move down a little and switch
        // Don't move down past the minimum
        if (moveDownDistanceRemaining > 0f && transform.position.y + minRelativeEnemyPos.y > moveBoundsMinPos.position.y)
        {
            float deltaY = MoveSpeed * Time.deltaTime;
            moveDownDistanceRemaining -= deltaY;
            transform.position += Vector3.down * deltaY;
        }
        else if ((movingRight && transform.position.x + maxRelativeEnemyPos.x > moveBoundsMaxPos.position.x) || (!movingRight && transform.position.x + minRelativeEnemyPos.x < moveBoundsMinPos.position.x))
        {
            // switch directions
            movingRight = !movingRight;
            moveDownDistanceRemaining = moveDownDistance;
        }
        else
        {
            // normal side to side movement
            float deltaX = MoveSpeed * Time.deltaTime;
            transform.position += Vector3.right * deltaX * (movingRight ? 1f : -1f);
        }

        // TODO what happens when they reach the bottom
    }

    private void Enemy_OnEnemyDestroyed(Enemy enemy)
    {
        ListEnemies();
        RecalculateRelativeEnemyPos();
    }

    private void ListEnemies()
    {
        enemies = GetComponentsInChildren<Enemy>();
    }

    private void RecalculateRelativeEnemyPos()
    {
        if (enemies.Length == 0) { return; }
        minRelativeEnemyPos = maxRelativeEnemyPos = enemies[0].transform.localPosition;
        foreach (var enemy in enemies)
        {
            var pos = enemy.transform.localPosition;
            minRelativeEnemyPos.x = Mathf.Min(minRelativeEnemyPos.x, pos.x);
            minRelativeEnemyPos.y = Mathf.Min(minRelativeEnemyPos.y, pos.y);
            maxRelativeEnemyPos.x = Mathf.Max(maxRelativeEnemyPos.x, pos.x);
            maxRelativeEnemyPos.y = Mathf.Max(maxRelativeEnemyPos.y, pos.y);
        }
    }

    private void OnDrawGizmos()
    {
        //Vector3 topLeft = new Vector3(moveBoundsMin.x, moveBoundsMax.y, 0f);
        //Vector3 bottomRight = new Vector3(moveBoundsMax.x, moveBoundsMin.y, 0f);
        if (moveBoundsMinPos != null && moveBoundsMaxPos != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube((moveBoundsMinPos.position + moveBoundsMaxPos.position) / 2f, moveBoundsMaxPos.position - moveBoundsMinPos.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemies.Length > 0)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position + minRelativeEnemyPos, transform.position + maxRelativeEnemyPos);
        }
    }
}

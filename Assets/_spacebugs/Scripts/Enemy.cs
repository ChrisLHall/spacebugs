using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public static event UnityAction<Enemy> OnEnemyDestroyed = delegate { };

    private void OnDestroy()
    {
        OnEnemyDestroyed(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    public static EventManager current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }


    public event Action onEnemyDamage;
    public void EnemyDamage()
    {
        if (onEnemyDamage != null)
        {
            onEnemyDamage();
        }
    }

    public event Action<Vector3, int, int> onPlayerDamage;
    public void PlayerDamage(Vector3 attackDirection, int healthDamageAmount, int poiseDamageAmount)
    {
        if (onPlayerDamage != null)
        {
            onPlayerDamage(attackDirection, healthDamageAmount, poiseDamageAmount);
        }
    }

    public event Action onEnemyDestroy;
    public void DestroyEnemy()
    {
        if (onEnemyDestroy != null)
        {
            onEnemyDestroy();
        }
    }
   
}

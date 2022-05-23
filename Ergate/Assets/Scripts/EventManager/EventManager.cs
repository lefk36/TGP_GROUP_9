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

    public event Action<int> onEnemyDamage;
    public void EnemyDamage(int id)
    {
        if (onEnemyDamage != null)
        {
            onEnemyDamage(id);
        }
    }

    public event Action onPlayerDamage;
    public void PlayerDamage()
    {
        if (onPlayerDamage != null)
        {
            onPlayerDamage();
        }
    }
}

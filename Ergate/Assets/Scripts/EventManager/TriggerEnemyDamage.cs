using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemyDamage : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            EventManager.current.EnemyDamage(id);
        }
    }
}

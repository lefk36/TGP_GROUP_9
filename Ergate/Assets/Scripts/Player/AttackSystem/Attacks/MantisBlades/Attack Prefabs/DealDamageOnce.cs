using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnce : MonoBehaviour
{
    private List<BaseEnemy> damagedEnemies;
    private void Start()
    {
        damagedEnemies = new List<BaseEnemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<BaseEnemy>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnce : MonoBehaviour
{
    public float damage;
    private List<GameObject> damagedEnemies;
    private void Start()
    {
        damagedEnemies = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject enemy = other.transform.parent.gameObject;
            damagedEnemies.Add(enemy);
            enemy.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
        if (other.tag == "barrel" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject barrel = other.transform.parent.gameObject;
            barrelStatsScript barrelScript = barrel.GetComponent<barrelStatsScript>();
            damagedEnemies.Add(barrel);
            barrelScript.takeDamage(damage);
        }
    }
}

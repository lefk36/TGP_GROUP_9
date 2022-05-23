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
        if (other.tag == "Enemy" && !damagedEnemies.Contains(other.transform.parent.parent.gameObject))
        {
            GameObject enemy = other.transform.parent.parent.gameObject;
            damagedEnemies.Add(enemy);
            enemy.GetComponent<BaseEnemy>().TakeDamage(damage);
        }
    }
}

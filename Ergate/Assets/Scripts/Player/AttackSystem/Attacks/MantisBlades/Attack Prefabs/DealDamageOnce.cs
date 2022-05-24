using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DealDamageOnce : MonoBehaviour
{
    public float damage;
    public Vector3 force;
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
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            damagedEnemies.Add(enemy);
            BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
            enemyScript.rb.velocity = new Vector3(0, 0, 0);
            enemyScript.rb.AddForce(force, ForceMode.Impulse);
            enemyScript.TakeDamage(damage);
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

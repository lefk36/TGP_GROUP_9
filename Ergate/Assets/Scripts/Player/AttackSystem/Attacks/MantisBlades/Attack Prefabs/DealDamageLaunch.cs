using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DealDamageLaunch : MonoBehaviour
{
    public float damage;
    public float knockPower;
    private List<GameObject> damagedEnemies;
    public bool overrideDirection = false;
    public Vector3 newDirection;
    private void Start()
    {
        damagedEnemies = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject enemy = other.transform.parent.gameObject;
            BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
            damagedEnemies.Add(enemy);
            Vector3 knockbackForceDirection;
            if (!overrideDirection)
            {
                Vector3 positionNoY = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
                knockbackForceDirection = enemy.transform.position - enemyScript.m_Target.transform.position;
            }
            else
            {
                knockbackForceDirection = transform.rotation * newDirection;
            }
            knockbackForceDirection.Normalize();
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemyScript.rb.velocity = new Vector3(0, 0, 0);
            enemyScript.rb.AddForce(knockbackForceDirection * knockPower, ForceMode.Impulse);
            enemyScript.TakeDamage(damage);
        }
    }
}

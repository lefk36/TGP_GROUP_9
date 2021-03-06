using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DealDamageKnockback : MonoBehaviour
{
    public float damage;
    public float knockPower;
    private List<GameObject> damagedEnemies;
    public bool overrideDirection = false;
    public bool rotate = false;
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
            if (!enemyScript.isDead)
            {
                damagedEnemies.Add(enemy);
                Vector3 knockbackForceDirection;
                if (!overrideDirection)
                {
                    Vector3 positionNoY = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
                    knockbackForceDirection = enemy.transform.position - positionNoY;
                }
                else
                {
                    if (!rotate)
                    {
                        knockbackForceDirection = newDirection;
                    }
                    else
                    {
                        knockbackForceDirection = transform.rotation * newDirection;
                    }
                }
                knockbackForceDirection.Normalize();
                enemyScript.m_Agent.enabled = false;

                enemyScript.rb.velocity = new Vector3(0, 0, 0);
                enemyScript.rb.AddForce(knockbackForceDirection * knockPower, ForceMode.Impulse);
                enemyScript.TakeDamage(damage, false);
            }
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

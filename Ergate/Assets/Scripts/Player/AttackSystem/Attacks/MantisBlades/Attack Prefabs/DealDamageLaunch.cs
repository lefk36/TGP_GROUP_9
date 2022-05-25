using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DealDamageLaunch : MonoBehaviour
{
    public float damage;
    public float range;
    public float speed;
    public float stoppingPower;
    private List<GameObject> damagedEnemies;
    public Vector3 direction;
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

            direction.Normalize();
            enemyScript.StartCoroutine(Launch(enemy, enemyScript));
        }
        if (other.tag == "barrel" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject barrel = other.transform.parent.gameObject;
            barrelStatsScript barrelScript = barrel.GetComponent<barrelStatsScript>();
            damagedEnemies.Add(barrel);
            barrelScript.takeDamage(damage);
        }
    }
    IEnumerator Launch(GameObject enemy, BaseEnemy enemyScript)
    {
        enemyScript.rb.velocity = new Vector3(0, 0, 0);
        float distanceTravelled = 0;
        enemyScript.TakeDamage(damage, true);
        do
        {
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemyScript.rb.velocity = direction * speed;
            Vector3 lastPos = enemy.transform.position;
            yield return null;
            Vector3 currentPosition = enemy.transform.position;
            float nextStep = (lastPos - currentPosition).magnitude;
            distanceTravelled += nextStep;
        } while (distanceTravelled < range);
        enemyScript.rb.velocity = enemyScript.rb.velocity / stoppingPower;
    }
}

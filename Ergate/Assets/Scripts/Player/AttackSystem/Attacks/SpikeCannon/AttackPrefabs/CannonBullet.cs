using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CannonBullet : MonoBehaviour
{
    public float damage;
    public float speed;
    Rigidbody rb;
    private List<GameObject> damagedEnemies;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        damagedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject enemy = other.transform.parent.gameObject;
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            BaseEnemy enemyScript = enemy.GetComponent<BaseEnemy>();
            if (!enemyScript.isDead)
            {
                damagedEnemies.Add(enemy);
                enemyScript.rb.velocity = new Vector3(0, 0, 0);
                enemyScript.TakeDamage(damage, true);
                Destroy(this.gameObject);
            }
        }
        else if (other.tag == "barrel" && !damagedEnemies.Contains(other.transform.parent.gameObject))
        {
            GameObject barrel = other.transform.parent.gameObject;
            barrelStatsScript barrelScript = barrel.GetComponent<barrelStatsScript>();
            damagedEnemies.Add(barrel);
            barrelScript.takeDamage(damage);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Player")
        {
            //do nothing
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

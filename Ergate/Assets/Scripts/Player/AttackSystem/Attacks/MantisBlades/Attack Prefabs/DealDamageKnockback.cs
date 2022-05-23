using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageKnockback : MonoBehaviour
{
    public float damage;
    public float knockPower;
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
            Vector3 positionNoY = new Vector3(transform.position.x, enemy.transform.position.y, transform.position.z);
            Vector3 knockbackForceDirection = enemy.transform.position - positionNoY;
            Debug.Log(knockbackForceDirection);
            enemy.GetComponent<Rigidbody>().AddForce(knockbackForceDirection * knockPower);
        }
    }
}

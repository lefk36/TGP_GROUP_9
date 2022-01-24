using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    private Transform Target;
    [Header("attributes")]
    public float range = 100f;
    public float rateOfFire;
    public float rateOfFireReset = 3f;
    public Transform firepoint;
    public GameObject player;
    public GameObject bulletPrefab;
    void Start()
    {
        rateOfFire = rateOfFireReset;
        InvokeRepeating("FindTarget", 0f, 1f);
    }
    public void FindTarget()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (player != null && distanceToPlayer <= range)
            Target = player.transform;
        else
            Target = null;
    }
    void Update()
    {
        if (Target == null)
            return;
        rateOfFire -= Time.deltaTime;
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (rateOfFire <= 0)
        {
            rateOfFire = 3f;
            Shoot();
            Debug.Log("bang bang");
            return;
        }
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.AimForTarget(Target);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

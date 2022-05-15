using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody m_BulletRB;
    public float m_BulletSpeed;
    private PlayerPoiseAndHealth m_PlayerStats;
    public int m_BulletDamage;
    

    private void Awake()
    {
        m_BulletRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_PlayerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPoiseAndHealth>();
        StartCoroutine(BulletDestroy());
    }

    private void FixedUpdate()
    {
        m_BulletRB.AddForce(transform.Find("Pistol Bullet").forward * m_BulletSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_PlayerStats.TakeDamage(Vector3.zero, m_BulletDamage, 0);
            Destroy(gameObject);
        }
        
    }

    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

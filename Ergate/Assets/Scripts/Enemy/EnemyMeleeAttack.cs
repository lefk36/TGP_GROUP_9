using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public int healthDamageAmount = 20;
    public int poiseDamageAmount = 20;
    public bool m_canAttack;
    public float m_attackCooldown = 1.5f;
    public GameObject parentObject;
    private void Start()
    {
        m_canAttack = true;
        parentObject = transform.parent.gameObject;
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && m_canAttack == true)
        {
            PlayerPoiseAndHealth healthComponent = collider.GetComponent<PlayerPoiseAndHealth>();
            Debug.Log("damage almost taken?");
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(Vector3.zero, healthDamageAmount, poiseDamageAmount);
                Debug.Log("damage taken from Melee");
                m_canAttack = false;
                Invoke(nameof(ResetAttack), m_attackCooldown);
                //Do animation on the zombie using parentObject variable
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (m_canAttack == true)
    //    {
    //        if (other.GetComponent<Collider>().CompareTag("Player"))
    //        {
    //            Debug.Log("melee hit the player");
    //            other.GetComponent<PlayerPoiseAndHealth>().TakeDamage(Vector3.zero, healthDamageAmount, poiseDamageAmount);
    //            m_canAttack = false;
    //            Invoke(nameof(ResetAttack), m_attackCooldown);
    //        }
    //    }
    //}
    void ResetAttack()
    {
        m_canAttack = true;
    }
}

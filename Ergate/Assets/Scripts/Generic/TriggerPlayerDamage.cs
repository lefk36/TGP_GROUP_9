using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class TriggerPlayerDamage : MonoBehaviour
//{
//    Vector3 attackDirection;
//    int healthDamageAmount;
//    int poiseDamageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player took damage");
            EventManager.current.PlayerDamage(attackDirection, healthDamageAmount, poiseDamageAmount);
        }
    }
}

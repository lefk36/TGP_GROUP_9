using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDamageScripts : MonoBehaviour
{
    public int spikeDamage;
    private bool damageNeed = true;
    private bool inSpike = false;
    private GameObject player;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.gameObject.tag == "Player")
        {
            player = collision.transform.gameObject;
            inSpike = true;
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        inSpike = false;
    }

    private void Update()
    {
        if(inSpike)
        {
            if(damageNeed)
            {
                StartCoroutine(takeDamage());
            }
           
        }
    }

    private IEnumerator takeDamage()
    {
        if(damageNeed)
        {
            player.GetComponent<PlayerPoiseAndHealth>().TakeDamage(Vector3.zero, spikeDamage, 0);
            damageNeed = false;
        }
        
        yield return new WaitForSeconds(1f);
        damageNeed = true;
    }



}

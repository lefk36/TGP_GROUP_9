using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelStatsScript : MonoBehaviour
{
    public float m_health;

    

    public void takeDamage(float damage)
    {
        m_health -= damage;

        if(m_health <= 0)
        {

            StartCoroutine(damaged());
        }
    }


    private IEnumerator damaged()
    {
        yield return new WaitForSeconds(1f);
        transform.GetComponent<explosionScript>().explode();
    }
    
}

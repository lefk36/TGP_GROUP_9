using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float m_health = 100f;
    public float m_maxHealth = 100f;
    public bool m_alive = true;

    public void takeDamage(float damage)
    {
        m_health -= damage;
        if(m_health <= 0f)
        {
            m_alive = false;
            gameObject.GetComponent<playerSpawn>().onDeath();
        }
    }
   
}

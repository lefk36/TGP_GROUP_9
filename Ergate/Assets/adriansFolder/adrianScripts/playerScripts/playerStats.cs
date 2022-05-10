using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float m_CurrentHealth;
    public float m_MaxHealth = 100f;
    public bool m_alive = true;
    private Animator m_Animator;
    private GameObject m_Character;
    private GameObject m_Model;

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_Character = transform.Find("Character").gameObject;
        if (m_Character != null)
        {
            m_Model = m_Character.transform.Find("Model").gameObject;
            if (m_Model != null)
            {
                m_Animator = m_Model.GetComponent<Animator>();
            }
        }
    }
    public void takeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        if (m_CurrentHealth <= 0f)
        {
            m_alive = false;
            gameObject.GetComponent<playerSpawn>().onDeath();
           
        }
        
    }
   
}

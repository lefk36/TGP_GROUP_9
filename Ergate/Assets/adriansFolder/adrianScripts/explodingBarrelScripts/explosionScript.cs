using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class explosionScript : MonoBehaviour
{
    public float m_radius;
    public float m_explosionForce;
    public float m_damage = 100f;
    public ParticleSystem m_explosionParticles;
  
    private Collider[] colliders;
    private Collider[] destroyedObjects;
    public void explode()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        
        destroyedObjects = Physics.OverlapSphere(transform.position, m_radius);

        for(int x = 0; x < destroyedObjects.Length; x++)
        {
            if(destroyedObjects[x].GetComponent<destructibleObject>() != null)
            {
                destroyedObjects[x].GetComponent<destructibleObject>().destroyObject();
            }
        }

        
        
        colliders = Physics.OverlapSphere(transform.position, m_radius);
        for(int i = 0; i < colliders.Length; i++)
        {

            if(colliders[i].attachedRigidbody != null)
            {
                if(colliders[i].GetComponent<NavMeshAgent>() != null)
                {
                    
                    if(colliders[i].GetComponent<BaseEnemy>() != null)
                    {

                        colliders[i].GetComponent<BaseEnemy>().TakeDamage(m_damage, true);
                        colliders[i].GetComponent<NavMeshAgent>().enabled = false;
                        Debug.Log("the enemy has taken damage");
                    }
                    
                }
                if(colliders[i].GetComponent<barrelStatsScript>() != null)
                {
                    colliders[i].GetComponent<barrelStatsScript>().takeDamage(m_damage);
                }

                if(colliders[i].GetComponent<objectStats>() != null)
                {
                    colliders[i].GetComponent<objectStats>().takeDamage(m_damage);
                }

                
                colliders[i].attachedRigidbody.AddExplosionForce(m_explosionForce, transform.position, m_radius);
                ParticleSystem temp = Instantiate(m_explosionParticles, position, rotation);
                
                
                
            }
            

        }

        FindObjectOfType<audioController>().play("explosion");

       




    }
}

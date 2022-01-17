using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    public float m_radius;
    public float m_explosionForce;
    public float m_damage = 100f;
    public ParticleSystem m_explosionParticles;
    public GameObject m_barrel_partOne;
    public GameObject m_barrel_partTwo;
    private Collider[] colliders;
    public void explode()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        GameObject b1 = Instantiate(m_barrel_partOne, new Vector3(position.x,position.y + 0.5f, position.z), rotation);
        GameObject b2 = Instantiate(m_barrel_partTwo, new Vector3(position.x, position.y - 0.5f, position.z), rotation);
        Destroy(transform.gameObject);

        colliders = Physics.OverlapSphere(transform.position, m_radius);
        for(int i = 0; i < colliders.Length; i++)
        {

            if(colliders[i].attachedRigidbody != null)
            {

                if(colliders[i].GetComponent<barrelStatsScript>() != null)
                {
                    colliders[i].GetComponent<barrelStatsScript>().takeDamage(m_damage);
                }
                colliders[i].attachedRigidbody.AddExplosionForce(m_explosionForce, transform.position, m_radius);
                ParticleSystem temp = Instantiate(m_explosionParticles, position, rotation);
                
                
                
            }
            

        }

       




    }
}

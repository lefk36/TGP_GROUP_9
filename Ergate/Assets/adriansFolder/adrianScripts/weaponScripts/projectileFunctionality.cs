using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileFunctionality : MonoBehaviour
{
    private Rigidbody m_RB;
    public float m_speed;
    public float m_damage;
    // Start is called before the first frame update
    void Start()
    {
        m_RB = transform.gameObject.GetComponent<Rigidbody>();

        m_RB.AddForce(transform.forward * m_speed, ForceMode.Impulse);

        StartCoroutine(destroyPro());
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "barrel")
        {
            collision.gameObject.GetComponent<barrelStatsScript>().takeDamage(m_damage);
            Destroy(transform.gameObject);
        }
    }

    private IEnumerator destroyPro()
    {
        yield return new WaitForSeconds(5f);
        Destroy(transform.gameObject);

    }


}

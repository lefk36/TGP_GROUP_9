using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private Transform target;
    public Rigidbody rb;
    public int damageToHealth = 30;
    public int damageToPoise = 30;
    #region hidden stuff
    private int tempHealthDamage = 5;
    private int tempPoiseDamage = 5;
    private float selfDestruct = 5f;
    #endregion
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        selfDestruct -= Time.deltaTime;
        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        if (selfDestruct <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        //if (other.collider.tag == "Player")
        //{
        //    Debug.Log("hit the player");
        //    other.gameObject.GetComponent<PlayerPoiseAndHealth>().TakeDamage(Vector3.zero, damageToHealth, damageToPoise);
        //    Destroy(gameObject);
        //}
        if (other.collider.tag != "Player")
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            Debug.Log("hit the player");
            other.GetComponent<PlayerPoiseAndHealth>().TakeDamage(Vector3.zero, damageToHealth, damageToPoise);
            Destroy(gameObject);
        }
    }
}

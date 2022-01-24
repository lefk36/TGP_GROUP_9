using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    
    public int damageToHealth;
    public int damageToPoise;
    public float speed = 20f;
    #region hideden stuff
    private Rigidbody rb;
    private int tempHealthDamage = 5;
    private int tempPoiseDamage = 5;
    private float selfDestruct = 5f;
    #endregion

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    damageToPoise = tempPoiseDamage;
    //    damageToHealth = tempHealthDamage;
    //}
    public void AimForTarget(Transform _target)
    {
        rb = GetComponent<Rigidbody>();
        target = _target;
        
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
        //rb.velocity =  new Vector3(gameObject.transform.position + target.position * Time.deltaTime * speed, 0f);
        rb.AddForce(transform.position - target.position * speed, ForceMode.Impulse);
        damageToPoise = tempPoiseDamage;
        damageToHealth = tempHealthDamage;
    }
    void Update()
    {
        selfDestruct -= Time.deltaTime;
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if (selfDestruct <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void OnCollision(Collision collision)
    {
        if (collision.gameObject.transform.position == target.transform.position)
            HitTarget();
        else
            Destroy(gameObject);
    }
    void HitTarget()
    {
        target.GetComponent<PlayerPoiseAndHealth>().TakeDamage(new Vector3(0f,0f,0f),damageToHealth, damageToPoise);
    }
}

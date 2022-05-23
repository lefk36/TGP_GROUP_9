using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnce : MonoBehaviour
{
    private List<GameObject> damagedEnemies;
    private void Start()
    {
        damagedEnemies = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Enemy" && damagedEnemies.Contains(other.transform.))
        //{
        //    GameObject enemy = other.transform.parent.gameObject;
        //}
    }
}

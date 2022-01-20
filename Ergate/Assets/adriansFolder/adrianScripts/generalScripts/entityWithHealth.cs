using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityWithHealth : MonoBehaviour
{
    public float health;


    public void takeDamage(float damage)
    {
        health -= damage;
    }
}

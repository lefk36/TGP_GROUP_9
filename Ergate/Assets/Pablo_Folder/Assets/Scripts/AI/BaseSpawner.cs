using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    //Enemies to be spawned
    public int m_EnemiesToBeSpawned;
    //Interval between enemies spawning
    public float m_SpawnRate;
    //Variable of the prefab
    public GameObject m_Prefab;
}


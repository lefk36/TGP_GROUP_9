using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleObject : MonoBehaviour
{
    public GameObject m_destroyedPrefab;

    

    public void destroyObject()
    {
        GameObject destroyedObject = Instantiate(m_destroyedPrefab, transform.position, transform.rotation);
        Destroy(transform.gameObject);
    }
}

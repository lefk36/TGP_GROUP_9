using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(desteoy());
    }

    private IEnumerator desteoy()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(transform.gameObject);
    }

    
}

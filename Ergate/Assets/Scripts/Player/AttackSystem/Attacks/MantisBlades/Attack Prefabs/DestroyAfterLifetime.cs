using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour
{
    public float secondsOfLife;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Life());
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(secondsOfLife);
        Destroy(this.gameObject);
    }
}

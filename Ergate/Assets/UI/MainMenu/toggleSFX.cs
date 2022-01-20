using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleSFX : MonoBehaviour
{
    [SerializeField]
    private GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnSmokeOff()
    {
        smoke.SetActive(false);
    }

    public void turnSmokeON()
    {
        smoke.SetActive(true);
    }
}

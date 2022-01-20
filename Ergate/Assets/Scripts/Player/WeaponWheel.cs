using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform cursorHolderUI;
    public GameObject weaponWheelUI;
    private PlayerController playerControllerScript;
    
    void Start()
    {
        playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>();
        weaponWheelUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform cursorHolderUI;
    public GameObject weaponWheelUI;
    private PlayerController playerControllerScript;
    private bool wheelActive = false;
    
    void Start()
    {
        playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("WeaponWheel")){
            if(!wheelActive)
            {
                weaponWheelUI.SetActive(true);
                wheelActive = true;
            }
        }
        if (Input.GetButtonUp("WeaponWheel"))
        {
            weaponWheelUI.SetActive(false);
            wheelActive = false;
        }
    }
}

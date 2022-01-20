using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    //Inspector values
    public LayerMask uiMask;
    public float buttonHoldingTimeRequired;

    //UI Objects
    public RectTransform cursorHolderUI;
    private RectTransform canvas;
    public GameObject weaponWheelUI;

    //game scripts
    private PlayerController playerControllerScript;
    private Camera_Movement cameraMovementScript;

    //private values
    private bool mouseCursorState = false;
    private bool weaponWheelState = false;
    private float timeButtonIsHeld = 0;

    void Start()
    {
        playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>();
        cameraMovementScript = transform.parent.Find("Camera Centre").GetComponent<Camera_Movement>();
        canvas = weaponWheelUI.transform.parent as RectTransform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("WeaponWheel"))
        {
            timeButtonIsHeld += Time.deltaTime;
            if (timeButtonIsHeld > 0.1f) //the button needs to be held for 0.1 seconds for the weapon wheel to trigger
            {
                if(weaponWheelState == false && playerControllerScript.readyForAction == true)
                {
                    WheelActive(true);
                }
                if (weaponWheelState == true)
                {
                    RotateCursor();
                    //
                }

            }
        }
        if (Input.GetButtonUp("WeaponWheel"))
        {
            mouseCursorState = false;
            cursorHolderUI.rotation = Quaternion.Euler(0, 0, 0);
            timeButtonIsHeld = 0;
            WheelActive(false);
        }
    }
    void RotateCursor()
    {
        Vector2 controllerInput = new Vector2(Input.GetAxisRaw("ControllerHorizontal"), -Input.GetAxisRaw("ControllerVertical"));
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        if (mouseInput.magnitude > buttonHoldingTimeRequired) //if mouse cursor is in use
        {
            if (mouseCursorState == false)
            {
                mouseCursorState = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out mousePos); //converts mouse pos to UI coordinates
            Vector2 directionToMouse = mousePos - cursorHolderUI.anchoredPosition;
            float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.y) * Mathf.Rad2Deg;
            cursorHolderUI.rotation = Quaternion.Euler(0, 0, -angle);//rotates the arrow cursor towards mouse cursor
        }
        if (controllerInput.magnitude > 0.1f) //if the controller is in use
        {
            if (mouseCursorState == true)
            {
                mouseCursorState = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            float angle = Mathf.Atan2(controllerInput.x, controllerInput.y) * Mathf.Rad2Deg;
            cursorHolderUI.rotation = Quaternion.Euler(0, 0, -angle); //rotates the arrow cursor with the direction of 
        }
    }
    void WheelActive(bool state)
    {
        Time.timeScale = Convert.ToInt32(!state);
        weaponWheelUI.SetActive(state);
        cameraMovementScript.enabled = !state;
        playerControllerScript.readyForAction = !state; //disable player movement, disable camera movement, turn the weapon wheel UI on and pause the game
        playerControllerScript.lockMovement = state;
        playerControllerScript.lockAttackDirection = state;
        playerControllerScript.lockFalling = state;
        weaponWheelState = state;
    }

}

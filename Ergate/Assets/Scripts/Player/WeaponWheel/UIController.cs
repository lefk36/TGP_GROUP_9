using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Inspector values
    public float buttonHoldingTimeRequired;
   

    //UI Objects
    public EventSystem wheelEventSystem;
    public RectTransform cursorHolderUI;
    private RectTransform canvas;
    public GameObject weaponWheelUI;
    public GameObject pauseMenu;
    [HideInInspector] public string buttonString;
    [HideInInspector] public bool pauseMenuState = false;


    //game scripts
    private PlayerController playerControllerScript;
    private Camera_Movement cameraMovementScript;
    private EnemiesCameraLock cameraLockScript;
    private AttackInput inputScript;

    //private values
    private bool mouseCursorState = false;
    private bool weaponWheelState = false;
    private float timeButtonIsHeld = 0;


    //public non-inspector values
    [HideInInspector] public Type activeWeaponType;
    [HideInInspector] public int activeWeaponIndex = 0;
    [HideInInspector] public List<Weapon> weaponScripts;

    void Start()
    {
        weaponScripts.Add(transform.GetComponent<MantisBlades>());
        weaponScripts.Add(transform.GetComponent<TentacleLasher>());
        weaponScripts.Add(transform.GetComponent<SpikeCannon>());
        playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>();
        cameraMovementScript = transform.parent.Find("Camera Centre").GetComponent<Camera_Movement>();
        cameraLockScript = cameraMovementScript.transform.Find("Main Camera").GetComponent<EnemiesCameraLock>();
        inputScript = GetComponent<AttackInput>();
        canvas = weaponWheelUI.transform.parent as RectTransform;

    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenuState == false)
        {
            if(weaponWheelState == false && inputScript.enabled == false)
            {
                inputScript.enabled = true;
            }
            if (Input.GetButton("WeaponWheel"))
            {
                timeButtonIsHeld += Time.deltaTime;
                if (timeButtonIsHeld > 0.1f) //the button needs to be held for 0.1 seconds for the weapon wheel to trigger
                {
                    if (weaponWheelState == false && playerControllerScript.readyForAction == true)
                    {
                        WheelActive(true);
                    }
                    if (weaponWheelState == true)
                    {
                        RotateCursor();
                    }

                }
            }
            if (Input.GetButtonUp("WeaponWheel"))
            {
                GameObject buttonObj = wheelEventSystem.currentSelectedGameObject;
                if (buttonObj != null)
                {
                    Button button;
                    button = buttonObj.GetComponent<Button>();
                    button.onClick.Invoke();
                    SwitchWeapons(buttonString);
                }
                mouseCursorState = false;
                cursorHolderUI.rotation = Quaternion.Euler(0, 0, 0);
                timeButtonIsHeld = 0;
                WheelActive(false);
            }
            if (weaponWheelState == false)
            {
                if (Input.GetButtonDown("Pause"))
                {
                    inputScript.CancelAttacks();
                    inputScript.enabled = false;
                    pauseMenuState = true;
                    pauseMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                }
            }
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
            angle = ClampTo360(angle);
            cursorHolderUI.localRotation = Quaternion.Euler(0, 0, angle);//rotates the arrow cursor towards mouse cursor
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
            angle = ClampTo360(angle);
            cursorHolderUI.localRotation = Quaternion.Euler(0, 0, angle); //rotates the arrow cursor with the direction of 
        }
    }
    void WheelActive(bool state)
    {
        Time.timeScale = Convert.ToInt32(!state);
        weaponWheelUI.SetActive(state);
        inputScript.CancelAttacks();
        inputScript.enabled = !state;
        if (!cameraLockScript.m_LockOn)
        {
            cameraLockScript.enabled = !state;
            cameraMovementScript.enabled = !state;
        }
        else
        {
            cameraLockScript.enabled = !state;
        }
        playerControllerScript.readyForAction = !state; //disable player movement, disable camera movement, turn the weapon wheel UI on and pause the game
        playerControllerScript.lockMovement = state;
        playerControllerScript.lockAttackDirection = state;
        playerControllerScript.lockFalling = state;
        weaponWheelState = state;
    }
    float ClampTo360(float angle)
    {
        float result = -angle;
        if (result < 0)
        {
            result += 360f;
        }
        return result;
    }
    public float ReturnCleanAngle()
    {
        float angle;
        angle = cursorHolderUI.localRotation.eulerAngles.z;
        angle = ClampTo360(angle);
        return angle;
    }
    void SwitchWeapons(string weaponName)
    {
        if(weaponName == "Blades")
        {
            for(int i =0; i<weaponScripts.Count; i++)
            {
                if(weaponScripts[i].GetType() == typeof(MantisBlades))
                {
                    activeWeaponIndex = i;
                    activeWeaponType = typeof(MantisBlades);
                }
            }
        }
        else if(weaponName == "Lasher")
        {
            for (int i = 0; i < weaponScripts.Count; i++)
            {
                if (weaponScripts[i].GetType() == typeof(TentacleLasher))
                {
                    activeWeaponIndex = i;
                    activeWeaponType = typeof(TentacleLasher);
                }
            }
        }
        else if(weaponName == "Spike")
        {
            for (int i = 0; i < weaponScripts.Count; i++)
            {
                if (weaponScripts[i].GetType() == typeof(SpikeCannon))
                {
                    activeWeaponIndex = i;
                    activeWeaponType = typeof(SpikeCannon);
                }
            }
        }
    }
}

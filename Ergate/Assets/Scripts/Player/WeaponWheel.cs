using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform cursorHolderUI;
    private RectTransform canvas;
    public GameObject weaponWheelUI;
    private PlayerController playerControllerScript;
    private Camera_Movement cameraMovementScript;
    private bool mouseCursorState = false;
    
    void Start()
    {
        playerControllerScript = transform.parent.gameObject.GetComponent<PlayerController>();
        cameraMovementScript = transform.parent.Find("Camera Centre").GetComponent<Camera_Movement>();
        canvas = weaponWheelUI.transform.parent as RectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("WeaponWheel"))
        {
            Time.timeScale = 0.0f;
            weaponWheelUI.SetActive(true);
            cameraMovementScript.enabled = false;

        }
        if (Input.GetButton("WeaponWheel"))
        {
            if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0) //if mouse cursor is in use
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
                cursorHolderUI.rotation = Quaternion.Euler(0, 0, -angle);
            }
            if (Mathf.Abs(Input.GetAxisRaw("ControllerHorizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("ControllerVertical")) > 0) //if the controller is in use
            {
                if (mouseCursorState == true)
                {
                    mouseCursorState = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                Vector2 inputDirection = new Vector2(Input.GetAxisRaw("ControllerHorizontal"), Input.GetAxisRaw("ControllerVertical"));
                float angle = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
                cursorHolderUI.rotation = Quaternion.Euler(0, 0, -angle);
            }
        }
        if (Input.GetButtonUp("WeaponWheel"))
        {
            mouseCursorState = false;
            Time.timeScale = 1.0f;
            weaponWheelUI.SetActive(false);
            cameraMovementScript.enabled = true;
        }
    }
}

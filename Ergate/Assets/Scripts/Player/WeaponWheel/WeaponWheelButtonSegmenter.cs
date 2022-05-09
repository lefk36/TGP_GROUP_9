using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponWheelButtonSegmenter : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RectTransform> buttons;
    public float segmentAngle;
    public RectTransform cursor;
    public UIController wheelControllerScript;
    public EventSystem wheelEventSystem;

    void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].rotation = Quaternion.Euler(0, 0, -segmentAngle * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float cursorAngle = wheelControllerScript.ReturnCleanAngle();
        for(int i = 0; i < buttons.Count; i++)
        {
            float minAngle =(segmentAngle*i)-(segmentAngle/2);
            float maxAngle = (segmentAngle * i) + (segmentAngle / 2);
            if (cursorAngle > minAngle && cursorAngle < maxAngle)
            {
                wheelEventSystem.SetSelectedGameObject(buttons[i].transform.GetChild(0).gameObject);
            }
        }
    }
    public void SendStringToWheelController(string sentString)
    {
        wheelControllerScript.buttonString = sentString;
    }
}

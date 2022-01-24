using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelButtonReturnFunction : MonoBehaviour
{
    WeaponWheelButtonSegmenter uiSegmenterScript;
    private void Start()
    {   
        uiSegmenterScript = transform.parent.parent.GetComponent<WeaponWheelButtonSegmenter>();
    }
    public void ReturnName()
    {
        uiSegmenterScript.SendButtonNameToWheel(gameObject.name);
    }
}

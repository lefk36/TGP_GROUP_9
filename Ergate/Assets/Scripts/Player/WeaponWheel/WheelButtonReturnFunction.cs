using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelButtonReturnFunction : MonoBehaviour
{
    public WeaponWheelButtonSegmenter uiSegmenterScript;
    public void ReturnName()
    {
        uiSegmenterScript.SendButtonNameToWheel(gameObject.name);
    }
}

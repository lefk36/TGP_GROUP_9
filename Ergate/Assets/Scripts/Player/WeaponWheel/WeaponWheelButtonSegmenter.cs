using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheelButtonSegmenter : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RectTransform> buttons;
    public float segmentAngle;
    void Start()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].rotation = Quaternion.Euler(0, 0, -segmentAngle * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

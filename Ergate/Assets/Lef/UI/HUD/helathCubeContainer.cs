using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helathCubeContainer : MonoBehaviour
{

    public helathCubeContainer next;

    [SerializeField]
    float fill = 100;
    [SerializeField] private Image fillImage;

    private float percentage;

    private float currentAmount;

   public void SetCube(float count)
    {
        fill = count;
        fillImage.fillAmount = (fill*100);
        count--;
        if(next != null)
        {
            next.SetCube(count);
        }
    }
}

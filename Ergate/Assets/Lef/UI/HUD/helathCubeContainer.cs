using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helathCubeContainer : MonoBehaviour
{

    public helathCubeContainer next;

    [Range(0, 1)] float fill;
    [SerializeField] private Image fillImage;

   public void SetCube(float count)
    {
        fill = count;
        fillImage.fillAmount = fill;
        count--;
        if(next != null)
        {
            next.SetCube(count);
        }
    }
}

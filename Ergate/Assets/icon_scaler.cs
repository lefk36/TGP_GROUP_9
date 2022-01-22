using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icon_scaler : MonoBehaviour
{
    private Image icon;
    public RectTransform rectTrans;
    public float scaleConstant = 1f;

    public void AdjustImage()
    {
        icon = GetComponent<Image>(); 
        float topDistance = rectTrans.rect.height - icon.sprite.texture.height;
        float horDistance = rectTrans.rect.width - icon.sprite.texture.width;
        Vector3 position = transform.localPosition;
        position.y = topDistance * scaleConstant;
        position.x = horDistance * scaleConstant;
        transform.localPosition = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSFX : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smoke;

    public Toggle.ToggleEvent onValueChangedOverride;

    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.onValueChanged = onValueChangedOverride;

        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });

    }

    void Update()
    {
        
    }

   

    public void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            smoke.Pause();
        }
        else
        {
            smoke.Play();
        }
    }
}

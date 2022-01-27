using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class PlayerHealthSimple : MonoBehaviour
{
    public PlayerPoiseAndHealth _playerHealth;
    public Image fillImage;
    private Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        float fillvalue = _playerHealth.m_currentPlayerHealth;
        slider.value = fillvalue;
    }
}

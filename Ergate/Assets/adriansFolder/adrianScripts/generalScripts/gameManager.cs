using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public audioController m_audioController;
    public GameObject m_audioManager;

    private void Awake()
    {
        //m_audioManager = FindObjectOfType<audioController>().gameObject;
        //m_audioController = m_audioManager.GetComponent<audioController>();

       // m_audioController.playMusic("musicOne");
    }

    private void Start()
    {
        inGameMusic();
    }

    public void inGameMusic()
    {
        m_audioController.play("musicOne");
    }
}

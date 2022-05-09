using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public audioController m_audioController;
    public GameObject m_audioManager;

    private void Awake()
    {
        SceneManager.LoadScene("playerScene", LoadSceneMode.Additive);
        m_audioManager = FindObjectOfType<audioController>().gameObject;
        m_audioController = m_audioManager.GetComponent<audioController>();
        inGameMusic();
    }

    public void inGameMusic()
    {
        m_audioController.play("musicOne");
    }
}

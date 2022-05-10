using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public audioController m_audioController;
    public GameObject m_audioManager;

    private Scene playerScene;

    private void Awake()
    {
        SceneManager.LoadScene("playerScene", LoadSceneMode.Additive);
        playerScene = SceneManager.GetSceneByName("playerScene");
        StartCoroutine(InitiateAfterSceneLoad());
    }

    public void inGameMusic()
    {
        m_audioController.play("musicOne");
    }
    IEnumerator InitiateAfterSceneLoad()
    {
        while (!playerScene.isLoaded)
        {
            yield return null;
        }
        if (m_audioManager == null)
        {
            m_audioManager = FindObjectOfType<audioController>().gameObject;
        }
        m_audioController = m_audioManager.GetComponent<audioController>();
        inGameMusic();
    }
    
}

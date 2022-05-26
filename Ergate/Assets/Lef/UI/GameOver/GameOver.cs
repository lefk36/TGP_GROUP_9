using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject playButtons;

    // Start is called before the first frame update
    void Start()
    {
        playButtons.SetActive(false);
        Invoke("hordeMode", 2.0f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

   

    private void ButtonsActive()
    {
        playButtons.SetActive(true);
       
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

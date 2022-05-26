using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public Slider masterSlider;
    public Slider soundEffectSlider;
    public Slider musicSlider;
    public GameObject settingsMenu;
    public GameObject mainMenu;

    private float masterVolume;
    private float musicVolume;
    private float soundEffectVolume;

    public AudioMixer audioMixer;

    public UIController uiControllerScript;


    private void Start()
    {

        audioMixer.GetFloat("masterVolume", out masterVolume);
        audioMixer.GetFloat("musicVolume", out musicVolume);
        audioMixer.GetFloat("soundEffectVolume", out soundEffectVolume);


        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        soundEffectSlider.value = soundEffectVolume;
    }
    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void setSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat("soundEffectVolume", volume);
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void unPause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        uiControllerScript.pauseMenuState = false; ;
    }

    public void startGame()
    {
        SceneManager.LoadScene("hordeMode", LoadSceneMode.Single);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void backToMain()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}

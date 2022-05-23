using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image fillImage;
    [SerializeField] private Text timeText;

    public int timeDuration { get; private set; }

    private int remainingTime;

    private void Awake()
    {
        ResetTimer();
    }

    private void Update()
    {
        if(remainingTime <= 0)
        {
            Debug.Log("Player Lost");
        }
    }

    private void ResetTimer()
    {
        timeText.text = "00:00";
        fillImage.fillAmount = 0f;

        timeDuration = remainingTime = 0;
    }

    public Timer SetTime(int seconds) 
    {
        timeDuration = remainingTime = seconds;
        return this;   
    }

    public void BeginTimer()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }


    private IEnumerator UpdateTimer()
    {
        while(remainingTime > 0)
        {
            UpdateUI(remainingTime);
            remainingTime--;
            yield return new WaitForSeconds(1f);
        }
        EndTimer();
    }

    private void UpdateUI(int seconds)
    {
        timeText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
        fillImage.fillAmount = Mathf.InverseLerp(0, timeDuration, seconds);
    }

    public void EndTimer()
    {
        ResetTimer();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}

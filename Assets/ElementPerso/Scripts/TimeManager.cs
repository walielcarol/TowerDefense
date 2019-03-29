using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public Text timeText;

    public string currentTime;

    public float time;


    public void Start()
    {
        Time.timeScale = 0f;
    }

    public void Update()
    {
        time = Time.time;

        currentTime = string.Format("{0:F1}", time);
        string s = string.Format("Survive: {0:F1}", time);
        timeText.text = s;
    }

    public void TimeStart()
    {
        Time.timeScale = 1f;
    }

    public void TimePause()
    {
        Time.timeScale = 0f;
    }

    public string CurrentTime()
    {
        return currentTime;
    }

}

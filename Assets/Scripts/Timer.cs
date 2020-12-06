using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
    public float startTime = 60;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        float seconds = startTime - Time.timeSinceLevelLoad;

        if (seconds <= 0)
        {
            SceneManager.LoadScene(1);
        }

        TimeSpan timespan = TimeSpan.FromSeconds(seconds);
        text.text = timespan.ToString("mm\\:ss");
    }
}

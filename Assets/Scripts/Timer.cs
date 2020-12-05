using System;
using UnityEngine;
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
        TimeSpan timespan = TimeSpan.FromSeconds(seconds);
        text.text = timespan.ToString("mm\\:ss");
    }
}

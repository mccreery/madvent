using System;
using UnityEngine;

public class AnimateCookies : MonoBehaviour
{
    [Serializable]
    public struct Animation
    {
        public Transform target;
        public Transform[] waypoints;
    }

    public Animation[] animations;
    public float time = 1f;

    private float startTime;
    public int waypoint;

    public void PlayWaypoint(int waypoint)
    {
        this.waypoint = waypoint;
        startTime = Time.time;
    }

    private void Start()
    {
        PlayWaypoint(1);
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 500, 100, 20), "Animate in"))
        {
            PlayWaypoint(1);
        }

        if (GUI.Button(new Rect(0, 530, 100, 20), "Animate out"))
        {
            PlayWaypoint(2);
        }
    }

#endif

    private void Update()
    {
        float t = Mathf.InverseLerp(startTime, startTime + time, Time.time);
        t = Mathf.Clamp01(t);
        t = CircleCurve(t);

        foreach (Animation animation in animations)
        {
            Transform start = animation.waypoints[waypoint - 1];
            Transform end = animation.waypoints[waypoint];

            animation.target.position = Vector3.Lerp(start.position, end.position, t);
            animation.target.rotation = Quaternion.Lerp(start.rotation, end.rotation, t);
        }
    }

    // t 0..1
    private float CircleCurve(float t)
    {
        t = 1 - t;
        return Mathf.Sqrt(1 - t*t);
    }
}

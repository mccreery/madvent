using UnityEngine;

public class CookieCheck : MonoBehaviour
{
    public CookieDraw draw;
    public Texture2D target;

    public float drawableRegion = 0.25f;
    public float minScore = 0.6f;

    public float CalculateScore()
    {
        Texture2D drawing = draw.texture;

        Color[] drawingPixels = drawing.GetPixels();
        Color[] targetPixels = target.GetPixels();

        float totalDifference = 0;

        float totalTarget = 0;

        for (int i = 0; i < drawingPixels.Length; i++)
        {
            totalDifference += Mathf.Pow(Mathf.Abs(drawingPixels[i].a - targetPixels[i].a), 2);
            totalTarget += targetPixels[i].a * targetPixels[i].a;
        }

        GUI.Label(new Rect(0, 200, 100, 100), (totalTarget / drawingPixels.Length).ToString());

        float drawableRegion = totalTarget / drawingPixels.Length;

        float unscaled = 1 - totalDifference / (drawableRegion * drawingPixels.Length);
        return unscaled;
        //return Mathf.Clamp01(Mathf.InverseLerp(minScore, 1, unscaled));
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 100, 100), "Current score: " + CalculateScore());
    }

#endif
}

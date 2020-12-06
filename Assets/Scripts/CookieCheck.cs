using UnityEngine;

public class CookieCheck : MonoBehaviour
{
    public CookieDraw draw;
    public Texture2D target;

    // Proportion of full error pixels corresponding to 0% score
    [Range(0, 1)]
    public float maxError = 0.25f;

    public float CalculateScore()
    {
        Texture2D drawing = draw.texture;

        Color[] drawingPixels = drawing.GetPixels();
        Color[] targetPixels = target.GetPixels();

        float totalErrorSq = 0;

        for (int i = 0; i < drawingPixels.Length; i++)
        {
            float maxComponentError = 0;

            for (int j = 0; j < 4; j++)
            {
                float error = Mathf.Abs(drawingPixels[i][j] - targetPixels[i][j]);
                maxComponentError = Mathf.Max(maxComponentError, error);
            }

            totalErrorSq += maxComponentError * maxComponentError;
        }

        float score = 1 - totalErrorSq / (maxError * drawingPixels.Length);
        return Mathf.Clamp01(score);
    }

#if UNITY_EDITOR

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 100, 100), "Current score: " + CalculateScore());
    }

#endif
}
